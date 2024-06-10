using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Helpes.Identity.Image;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class TestimonalService : ITestimonalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Testimonal> _repo;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private readonly string Section = "Testimonal section";
        public TestimonalService(IGenericRepository<Testimonal> repo, IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Testimonal>();
            _imageHelper = imageHelper;
            _toasty = toasty;
        }

        public async Task<List<TestimonalListVM>> GetAllAsync()
        {
            var testimonalListVM = await _repo.GetAll()
                .ProjectTo<TestimonalListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return testimonalListVM;
        }

        public async Task AddTestimonalAsync(TestimonalAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.testimonal, null);
            if (imageResult.Error != null)
            {
                _toasty.AddErrorToastMessage(imageResult.Error, new ToastrOptions { Title = NotificationMessagesWebapplication.FailedTitle });
                return;
            }
            request.FileName = imageResult.FileName!;
            request.FileType = imageResult.FileType!;

            var testimonal = _mapper.Map<Testimonal>(request);
            await _repo.AddEntityAsync(testimonal);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebapplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task DeleteTestimonalAsync(int id)
        {
            var testimonal = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(testimonal);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(testimonal.FileName);
            _toasty.AddWarningToastMessage(NotificationMessagesWebapplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task UpdateTestimonalAsync(TestimonalUpdateVM request)
        {
            var oldTestimonal = await _repo.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();

            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.testimonal, null);
                if (imageResult.Error != null)
                {
                    _toasty.AddErrorToastMessage(imageResult.Error, new ToastrOptions { Title = NotificationMessagesWebapplication.FailedTitle }); 
                    return;
                }
                request.FileName = imageResult.FileName!;
                request.FileType = imageResult.FileType!;
            }

            var testimonalUpdate = _mapper.Map<Testimonal>(request);
            _repo.UpdateEntity(testimonalUpdate);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
                if (request.Photo != null)
                {
                    _imageHelper.DeleteImage(request.FileName);
                }
				throw new ClientSideExceptions(ExceptionMessage.ConcurencyException);
			}
			if (request.Photo != null)
                _imageHelper.DeleteImage(oldTestimonal.FileName);
            _toasty.AddInfoToastMessage(NotificationMessagesWebapplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task<TestimonalUpdateVM> GetTestimonalById(int Id)
        {
            var testimonal = await _repo.Where(x => x.Id == Id)
                .ProjectTo<TestimonalUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return testimonal;
        }
    }
}

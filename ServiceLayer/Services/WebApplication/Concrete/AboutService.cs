using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Enumerators;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Helpes.Identity.Image;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
	public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<About> _repo;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private readonly string Section = "About section";
        public AboutService(IGenericRepository<About> repo, IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<About>();
            _imageHelper = imageHelper;
            _toasty = toasty;
        }

        public async Task<List<AboutListVM>> GetAllAsync()
        {
            var aboutListVM = await _repo.GetAll()
                .ProjectTo<AboutListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return aboutListVM;
        }

        public async Task AddAboutAsync(AboutAddVM request)
        {
            var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.about, null);
            if (imageResult.Error != null)
            {
                _toasty.AddErrorToastMessage(imageResult.Error, new ToastrOptions { Title = NotificationMessagesWebapplication.FailedTitle });
                return;
            }
            request.FileName = imageResult.FileName!;
            request.FileType = imageResult.FileType!;

            var about = _mapper.Map<About>(request);
            await _repo.AddEntityAsync(about);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebapplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(about);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(about.FileName);
            _toasty.AddWarningToastMessage(NotificationMessagesWebapplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task UpdateAboutAsync(AboutUpdateVM request)
        {
            var oldAbout = await _repo.Where(x => x.Id == request.Id).AsNoTracking().FirstAsync();

            if (request.Photo != null)
            {
                var imageResult = await _imageHelper.ImageUpload(request.Photo, ImageType.about, null);
                if (imageResult.Error != null)
                {
                    _toasty.AddErrorToastMessage(
                        imageResult.Error,
                        new ToastrOptions { Title = NotificationMessagesWebapplication.FailedTitle });
                    return;
                }
                request.FileName = imageResult.FileName!;
                request.FileType = imageResult.FileType!;
            }

            var aboutUpdate = _mapper.Map<About>(request);
            _repo.UpdateEntity(aboutUpdate);
            await _unitOfWork.CommitAsync();
            if (request.Photo != null)
                _imageHelper.DeleteImage(oldAbout.FileName);
            _toasty.AddInfoToastMessage(NotificationMessagesWebapplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task<AboutUpdateVM> GetAboutById(int Id)
        {
            var about = await _repo.Where(x => x.Id == Id)
                .ProjectTo<AboutUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            //var aboutExist = await _repo.GetEntityByIdAsync(Id); 
            //var about = _mapper.Map<AboutUpdateVM>(aboutExist);

            return about;
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.SocialMediaVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
	public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<SocialMedia> _repo;
        private readonly IToastNotification _toasty;
        private readonly string Section = "Social Media section";
        public SocialMediaService(IGenericRepository<SocialMedia> repo, IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<SocialMedia>();
            _toasty = toasty;
        }

        public async Task<List<SocialMediaListVM>> GetAllAsync()
        {
            var socialMediaListVM = await _repo.GetAll()
                .ProjectTo<SocialMediaListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return socialMediaListVM;
        }

        public async Task AddSocialMediaAsync(SocialMediaAddVM request)
        {
            var socialMedia = _mapper.Map<SocialMedia>(request);
            await _repo.AddEntityAsync(socialMedia);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebapplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task DeleteSocialMediaAsync(int id)
        {
            var socialMedia = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(socialMedia);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebapplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task UpdateSocialMediaAsync(SocialMediaUpdateVM request)
        {
            var socialMediaUpdate = _mapper.Map<SocialMedia>(request);
            _repo.UpdateEntity(socialMediaUpdate);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				throw new ClientSideExceptions(ExceptionMessage.ConcurencyException);
			}
			_toasty.AddInfoToastMessage(NotificationMessagesWebapplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task<SocialMediaUpdateVM> GetSocialMediaById(int Id)
        {
            var socialMedia = await _repo.Where(x => x.Id == Id)
                .ProjectTo<SocialMediaUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return socialMedia;
        }

		//UI Service Methods
		public async Task<List<SocialMediaUI>> GetAllListForUI()
		{
			var uiList = await _repo.GetAll().ProjectTo<SocialMediaUI>(_mapper.ConfigurationProvider).ToListAsync();
			return uiList;
		}
	}
}

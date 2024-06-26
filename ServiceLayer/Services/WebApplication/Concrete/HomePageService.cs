﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.HomePageVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
	public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<HomePage> _repo;
        private readonly IToastNotification _toasty;
        private readonly string Section = "HomePage section";

        public HomePageService(IGenericRepository<HomePage> repo, IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<HomePage>();
            _toasty = toasty;
        }

        public async Task<List<HomePageListVM>> GetAllAsync()
        {
            var homePageListVM = await _repo.GetAll()
                .ProjectTo<HomePageListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return homePageListVM;
        }

        public async Task AddHomePageAsync(HomePageAddVM request)
        {
            var homePage = _mapper.Map<HomePage>(request);
            await _repo.AddEntityAsync(homePage);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebapplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task DeleteHomePageAsync(int id)
        {
            var homePage = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(homePage);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebapplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task UpdateHomePageAsync(HomePageUpdateVM request)
        {
            var homePageUpdate = _mapper.Map<HomePage>(request);
            _repo.UpdateEntity(homePageUpdate);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				throw new ClientSideExceptions(ExceptionMessage.ConcurencyException);
			}
			_toasty.AddInfoToastMessage(NotificationMessagesWebapplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task<HomePageUpdateVM> GetHomePageById(int Id)
        {
            var homePage = await _repo.Where(x => x.Id == Id)
                .ProjectTo<HomePageUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return homePage;
        }

		//UI Service Methods

		public async Task<List<HomePageUI>> GetAllListForUI()
		{
			var uiList = await _repo.GetAll().ProjectTo<HomePageUI>(_mapper.ConfigurationProvider).ToListAsync();
			return uiList;
		}
	}
}

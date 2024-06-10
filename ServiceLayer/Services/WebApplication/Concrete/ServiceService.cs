using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ServiceVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Service> _repo;
        private readonly IToastNotification _toasty;
        private readonly string Section = "Service section";
        public ServiceService(IGenericRepository<Service> repo, IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Service>();
            _toasty = toasty;
        }

        public async Task<List<ServiceListVM>> GetAllAsync()
        {
            var serviceListVM = await _repo.GetAll()
                .ProjectTo<ServiceListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return serviceListVM;
        }

        public async Task AddServiceAsync(ServiceAddVM request)
        {
            var service = _mapper.Map<Service>(request);
            await _repo.AddEntityAsync(service);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebapplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(service);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebapplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task UpdateServiceAsync(ServiceUpdateVM request)
        {
            var serviceUpdate = _mapper.Map<Service>(request);
            _repo.UpdateEntity(serviceUpdate);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				throw new ClientSideExceptions(ExceptionMessage.ConcurencyException);
			}
			_toasty.AddInfoToastMessage(NotificationMessagesWebapplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebapplication.Success });
        }

        public async Task<ServiceUpdateVM> GetServiceById(int Id)
        {
            var service = await _repo.Where(x => x.Id == Id)
                .ProjectTo<ServiceUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return service;
        }
    }
}

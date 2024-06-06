using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ServiceVM;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
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

        public ServiceService(IGenericRepository<Service> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Service>();
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
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(service);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateServiceAsync(ServiceUpdateVM request)
        {
            var serviceUpdate = _mapper.Map<Service>(request);
            _repo.UpdateEntity(serviceUpdate);
            await _unitOfWork.CommitAsync();
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

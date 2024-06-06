using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.HomePageVM;
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
    public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<HomePage> _repo;

        public HomePageService(IGenericRepository<HomePage> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<HomePage>();
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
        }

        public async Task DeleteHomePageAsync(int id)
        {
            var homePage = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(homePage);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateHomePageAsync(HomePageUpdateVM request)
        {
            var homePageUpdate = _mapper.Map<HomePage>(request);
            _repo.UpdateEntity(homePageUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task<HomePageUpdateVM> GetHomePageById(int Id)
        {
            var homePage = await _repo.Where(x => x.Id == Id)
                .ProjectTo<HomePageUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return homePage;
        }
    }
}

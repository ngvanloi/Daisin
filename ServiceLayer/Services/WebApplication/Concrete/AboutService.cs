using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<About> _repo;

        public AboutService(IGenericRepository<About> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<About>();
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
            var about = _mapper.Map<About>(request);
            await _repo.AddEntityAsync(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAboutAsync(AboutUpdateVM request)
        {
            var aboutUpdate = _mapper.Map<About>(request);
            _repo.UpdateEntity(aboutUpdate);
            await _unitOfWork.CommitAsync();
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

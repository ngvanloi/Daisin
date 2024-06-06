using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TestimonalVM;
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
    public class TestimonalService : ITestimonalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Testimonal> _repo;

        public TestimonalService(IGenericRepository<Testimonal> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Testimonal>();
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
            var testimonal = _mapper.Map<Testimonal>(request);
            await _repo.AddEntityAsync(testimonal);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTestimonalAsync(int id)
        {
            var testimonal = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(testimonal);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTestimonalAsync(TestimonalUpdateVM request)
        {
            var testimonalUpdate = _mapper.Map<Testimonal>(request);
            _repo.UpdateEntity(testimonalUpdate);
            await _unitOfWork.CommitAsync();
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

using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.CategoryVM;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _repo;

        public CategoryService(IGenericRepository<Category> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = _unitOfWork.GetGenericRepository<Category>();
        }

        public async Task<List<CategoryListVM>> GetAllAsync()
        {
            var categoryListVM = await _repo.GetAll()
                .ProjectTo<CategoryListVM>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return categoryListVM;
        }

        public async Task AddCategoryAsync(CategoryAddVM request)
        {
            var category = _mapper.Map<Category>(request);
            await _repo.AddEntityAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repo.GetEntityByIdAsync(id);
            _repo.DeleteEntity(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateCategoryAsync(CategoryUpdateVM request)
        {
            var categoryUpdate = _mapper.Map<Category>(request);
            _repo.UpdateEntity(categoryUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CategoryUpdateVM> GetCategoryById(int Id)
        {
            var category = await _repo.Where(x => x.Id == Id)
                .ProjectTo<CategoryUpdateVM>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return category;
        }
    }
}

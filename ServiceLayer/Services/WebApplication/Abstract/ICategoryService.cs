using EntityLayer.WebApplication.ViewModels.CategoryVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface ICategoryService
    {
        Task<List<CategoryListVM>> GetAllAsync();
        Task AddCategoryAsync(CategoryAddVM request);
        Task DeleteCategoryAsync(int id);
        Task UpdateCategoryAsync(CategoryUpdateVM request);
        Task<CategoryUpdateVM> GetCategoryById(int Id);
		Task<List<CategoryUI>> GetAllListForUI();
	}
}

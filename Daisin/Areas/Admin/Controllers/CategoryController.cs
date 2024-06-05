using EntityLayer.WebApplication.ViewModels.CategoryVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Category")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		[HttpGet("GetCategoryList")]
		public async Task<IActionResult> GetCategoryList()
		{
			var categoryList = await _categoryService.GetAllAsync();
			return View(categoryList);
		}

		[HttpGet("AddCategory")]
		public IActionResult AddCategory()
		{
			return View();
		}
		[HttpPost("AddCategory")]
		public async Task<IActionResult> AddCategory(CategoryAddVM request)
		{
			await _categoryService.AddCategoryAsync(request);
			return RedirectToAction("GetCategoryList", "Category", new { Areas = ("Admin") });
		}

		[HttpGet("UpdateCategory")]
		public async Task<IActionResult> UpdateCategory(int id)
		{
			var category = await _categoryService.GetCategoryById(id);
			return View(category);
		}
		[HttpPost("UpdateCategory")]
		public async Task<IActionResult> UpdateCategory(CategoryUpdateVM request)
		{
			await _categoryService.UpdateCategoryAsync(request);
			return RedirectToAction("GetCategoryList", "Category", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteCategory(int Id)
		{
			await _categoryService.DeleteCategoryAsync(Id);
			return RedirectToAction("GetCategoryList", "Category", new { Areas = ("Admin") });
		}
	}
}

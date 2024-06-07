using EntityLayer.WebApplication.ViewModels.CategoryVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Category")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IValidator<CategoryAddVM> _addValidator;
		private readonly IValidator<CategoryUpdateVM> _updateValidator;

		public CategoryController(ICategoryService categoryService, IValidator<CategoryAddVM> addValidator, IValidator<CategoryUpdateVM> updateValidator)
		{
			_categoryService = categoryService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
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
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _categoryService.AddCategoryAsync(request);
				return RedirectToAction("GetCategoryList", "Category", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
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

			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _categoryService.UpdateCategoryAsync(request);
				return RedirectToAction("GetCategoryList", "Category", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteCategory(int Id)
		{
			await _categoryService.DeleteCategoryAsync(Id);
			return RedirectToAction("GetCategoryList", "Category", new { Areas = ("Admin") });
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly ICategoryService _categoryService;

		public CategoryViewComponent(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _categoryService.GetAllListForUI();
			return View(uiList);
		}
	}
}

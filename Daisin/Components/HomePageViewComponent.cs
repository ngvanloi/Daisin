using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class HomePageViewComponent : ViewComponent
	{
		private readonly IHomePageService _homePageService;

		public HomePageViewComponent(IHomePageService homePageService)
		{
			_homePageService = homePageService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _homePageService.GetAllListForUI();
			return View(uiList);
		}
	}
}

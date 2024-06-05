using EntityLayer.WebApplication.ViewModels.HomePageVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/HomePage")]
	public class HomePageController : Controller
	{
		private readonly IHomePageService _homePageService;

		public HomePageController(IHomePageService homePageService)
		{
			_homePageService = homePageService;
		}
		[HttpGet("GetHomePageList")]
		public async Task<IActionResult> GetHomePageList()
		{
			var homePageList = await _homePageService.GetAllAsync();
			return View(homePageList);
		}

		[HttpGet("AddHomePage")]
		public IActionResult AddHomePage()
		{
			return View();
		}
		[HttpPost("AddHomePage")]
		public async Task<IActionResult> AddHomePage(HomePageAddVM request)
		{
			await _homePageService.AddHomePageAsync(request);
			return RedirectToAction("GetHomePageList", "HomePage", new { Areas = ("Admin") });
		}

		[HttpGet("UpdateHomePage")]
		public async Task<IActionResult> UpdateHomePage(int id)
		{
			var homePage = await _homePageService.GetHomePageById(id);
			return View(homePage);
		}
		[HttpPost("UpdateHomePage")]
		public async Task<IActionResult> UpdateHomePage(HomePageUpdateVM request)
		{
			await _homePageService.UpdateHomePageAsync(request);
			return RedirectToAction("GetHomePageList", "HomePage", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteHomePage(int Id)
		{
			await _homePageService.DeleteHomePageAsync(Id);
			return RedirectToAction("GetHomePageList", "HomePage", new { Areas = ("Admin") });
		}
	}
}

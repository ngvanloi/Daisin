using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
using EntityLayer.WebApplication.ViewModels.HomePageVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/HomePage")]
	public class HomePageController : Controller
	{
		private readonly IHomePageService _homePageService;
		private readonly IValidator<HomePageAddVM> _addValidator;
		private readonly IValidator<HomePageUpdateVM> _updateValidator;

		public HomePageController(IHomePageService homePageService, IValidator<HomePageAddVM> addValidator, IValidator<HomePageUpdateVM> updateValidator)
		{
			_homePageService = homePageService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
		}

		[HttpGet("GetHomePageList")]
		public async Task<IActionResult> GetHomePageList()
		{
			var homePageList = await _homePageService.GetAllAsync();
			return View(homePageList);
		}

		[ServiceFilter(typeof(GenericAddPreventationFilter<HomePage>))]
		[HttpGet("AddHomePage")]
		public IActionResult AddHomePage()
		{
			return View();
		}

		[HttpPost("AddHomePage")]
		public async Task<IActionResult> AddHomePage(HomePageAddVM request)
		{
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _homePageService.AddHomePageAsync(request);
				return RedirectToAction("GetHomePageList", "HomePage", new { Area = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[ServiceFilter(typeof(GenericNotFoundFilter<HomePage>))]
		[HttpGet("UpdateHomePage")]
		public async Task<IActionResult> UpdateHomePage(int id)
		{
			var homePage = await _homePageService.GetHomePageById(id);
			return View(homePage);
		}
		[HttpPost("UpdateHomePage")]
		public async Task<IActionResult> UpdateHomePage(HomePageUpdateVM request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _homePageService.UpdateHomePageAsync(request);
				return RedirectToAction("GetHomePageList", "HomePage", new { Area = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteHomePage(int Id)
		{
			await _homePageService.DeleteHomePageAsync(Id);
			return RedirectToAction("GetHomePageList", "HomePage", new { Area = ("Admin") });
		}
	}
}

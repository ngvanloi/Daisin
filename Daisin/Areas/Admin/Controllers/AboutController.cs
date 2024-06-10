using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/About")]
	public class AboutController : Controller
	{
		private readonly IAboutService _aboutService;
		private readonly IValidator<AboutAddVM> _addValidator;
		private readonly IValidator<AboutUpdateVM> _updateValidator;
		private readonly IToastNotification _toasty;
		public AboutController(IAboutService aboutService, IValidator<AboutUpdateVM> updateValidator, IValidator<AboutAddVM> addValidator, IToastNotification toasty)
		{
			_aboutService = aboutService;
			_updateValidator = updateValidator;
			_addValidator = addValidator;
			_toasty = toasty;
		}

		[HttpGet("GetAboutList")]
		public async Task<IActionResult> GetAboutList()
		{
			var aboutList = await _aboutService.GetAllAsync();
			return View(aboutList);
		}

		[ServiceFilter(typeof(GenericAddPreventationFilter<About>))]
		[HttpGet("AddAbout")]
		public IActionResult AddAbout()
		{
			return View();
		}

		[HttpPost("AddAbout")]
		public async Task<IActionResult> AddAbout(AboutAddVM request)
		{
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _aboutService.AddAboutAsync(request);
				return RedirectToAction("GetAboutList", "About", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[ServiceFilter(typeof(GenericNotFoundFilter<About>))]
		[HttpGet("UpdateAbout")]
		public async Task<IActionResult> UpdateAbout(int id)
		{
			var about = await _aboutService.GetAboutById(id);
			return View(about);
		}
		[HttpPost("UpdateAbout")]
		public async Task<IActionResult> UpdateAbout(AboutUpdateVM request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _aboutService.UpdateAboutAsync(request);
				return RedirectToAction("GetAboutList", "About", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteAbout(int Id)
		{
			await _aboutService.DeleteAboutAsync(Id);
			return RedirectToAction("GetAboutList", "About", new { Areas = ("Admin") });
		}
	}
}

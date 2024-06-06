using EntityLayer.WebApplication.ViewModels.AboutVM;
using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Testimonal")]
	public class TestimonalController : Controller
	{
		private readonly ITestimonalService _testimonalService;
		private readonly IValidator<TestimonalAddVM> _addValidator;
		private readonly IValidator<TestimonalUpdateVM> _updateValidator;

		public TestimonalController(ITestimonalService testimonalService, IValidator<TestimonalAddVM> addValidator, IValidator<TestimonalUpdateVM> updateValidator)
		{
			_testimonalService = testimonalService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
		}
		[HttpGet("GetTestimonalList")]
		public async Task<IActionResult> GetTestimonalList()
		{
			var testimonalList = await _testimonalService.GetAllAsync();
			return View(testimonalList);
		}

		[HttpGet("AddTestimonal")]
		public IActionResult AddTestimonal()
		{
			return View();
		}
		[HttpPost("AddTestimonal")]
		public async Task<IActionResult> AddTestimonal(TestimonalAddVM request)
		{
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _testimonalService.AddTestimonalAsync(request);
				return RedirectToAction("GetTestimonalList", "Testimonal", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("UpdateTestimonal")]
		public async Task<IActionResult> UpdateTestimonal(int id)
		{
			var testimonal = await _testimonalService.GetTestimonalById(id);
			return View(testimonal);
		}
		[HttpPost("UpdateTestimonal")]
		public async Task<IActionResult> UpdateTestimonal(TestimonalUpdateVM request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _testimonalService.UpdateTestimonalAsync(request);
				return RedirectToAction("GetTestimonalList", "Testimonal", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteTestimonal(int Id)
		{
			await _testimonalService.DeleteTestimonalAsync(Id);
			return RedirectToAction("GetTestimonalList", "Testimonal", new { Areas = ("Admin") });
		}
	}
}

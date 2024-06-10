using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.PortfolioVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize(Roles = "SuperAdmin")]
	[Area("Admin")]
	[Route("Admin/Portfolio")]
	public class PortfolioController : Controller
	{
		private readonly IPortfolioService _portfolioService;
		private readonly IValidator<PortfolioAddVM> _addValidator;
		private readonly IValidator<PortfolioUpdateVM> _updateValidator;

		public PortfolioController(IPortfolioService portfolioService, IValidator<PortfolioAddVM> addValidator, IValidator<PortfolioUpdateVM> updateValidator)
		{
			_portfolioService = portfolioService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
		}
		[HttpGet("GetPortfolioList")]
		public async Task<IActionResult> GetPortfolioList()
		{
			var portfolioList = await _portfolioService.GetAllAsync();
			return View(portfolioList);
		}

		[HttpGet("AddPortfolio")]
		public IActionResult AddPortfolio()
		{
			return View();
		}
		[HttpPost("AddPortfolio")]
		public async Task<IActionResult> AddPortfolio(PortfolioAddVM request)
		{
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _portfolioService.AddPortfolioAsync(request);
				return RedirectToAction("GetPortfolioList", "Portfolio", new { Area = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[ServiceFilter(typeof(GenericNotFoundFilter<Portfolio>))]
		[HttpGet("UpdatePortfolio")]
		public async Task<IActionResult> UpdatePortfolio(int id)
		{

			var portfolio = await _portfolioService.GetPortfolioById(id);
			return View(portfolio);
		}
		[HttpPost("UpdatePortfolio")]
		public async Task<IActionResult> UpdatePortfolio(PortfolioUpdateVM request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _portfolioService.UpdatePortfolioAsync(request);
				return RedirectToAction("GetPortfolioList", "Portfolio", new { Area = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeletePortfolio(int Id)
		{
			await _portfolioService.DeletePortfolioAsync(Id);
			return RedirectToAction("GetPortfolioList", "Portfolio", new { Area = ("Admin") });
		}
	}
}

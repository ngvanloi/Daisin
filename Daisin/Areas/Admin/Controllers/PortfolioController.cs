using EntityLayer.WebApplication.ViewModels.PortfolioVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("Admin/Portfolio")]
	public class PortfolioController : Controller
	{
		private readonly IPortfolioService _portfolioService;

		public PortfolioController(IPortfolioService portfolioService)
		{
			_portfolioService = portfolioService;
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
			await _portfolioService.AddPortfolioAsync(request);
			return RedirectToAction("GetPortfolioList", "Portfolio", new { Areas = ("Admin") });
		}

		[HttpGet("UpdatePortfolio")]
		public async Task<IActionResult> UpdatePortfolio(int id)
		{
			var portfolio = await _portfolioService.GetPortfolioById(id);
			return View(portfolio);
		}
		[HttpPost("UpdatePortfolio")]
		public async Task<IActionResult> UpdatePortfolio(PortfolioUpdateVM request)
		{
			await _portfolioService.UpdatePortfolioAsync(request);
			return RedirectToAction("GetPortfolioList", "Portfolio", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeletePortfolio(int Id)
		{
			await _portfolioService.DeletePortfolioAsync(Id);
			return RedirectToAction("GetPortfolioList", "Portfolio", new { Areas = ("Admin") });
		}
	}
}

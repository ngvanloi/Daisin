using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize(Policy = "AdminObserver")]
	[Area("Admin")]
	[Route("Admin/Dashboard")]
	public class DashboardController : Controller
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ViewBag.Services = await _dashboardService.GetAllServicesCountAsync();
			ViewBag.Portfolios = await _dashboardService.GetAllPortfoliosAsync();
			ViewBag.Teams = await _dashboardService.GetAllTeamsCountAsync();
			ViewBag.Categories = await _dashboardService.GetAllCategoriesCountAsync();
			ViewBag.Users = _dashboardService.GetAllUsersCount();
			ViewBag.Testimonals = await _dashboardService.GetAllTestimonalsCountAsync();

			return View();
		}
	}
}

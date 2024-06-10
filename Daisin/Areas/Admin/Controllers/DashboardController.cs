using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize(Roles = "SuperAdmin")]
	[Area("Admin")]
	[Route("Admin/Dashboard")]
	public class DashboardController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}

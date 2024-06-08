using Microsoft.AspNetCore.Mvc;

namespace Daisin.Areas.User.Controllers
{
	[Area("User")]
	[Route("User/Dashboard")]
	public class DashboardController : Controller
	{
		[HttpGet("Index")]
		public IActionResult Index()
		{
			return View();
		}
	}
}

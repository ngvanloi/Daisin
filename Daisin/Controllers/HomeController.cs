using Microsoft.AspNetCore.Mvc;

namespace Daisin.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

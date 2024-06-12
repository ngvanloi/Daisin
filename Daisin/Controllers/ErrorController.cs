using CoreLayer.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceLayer.Exceptions.WebApplication;

namespace Daisin.Controllers
{
	public class ErrorController : Controller
	{
		private readonly ILogger<ErrorController> _logger;

		public ErrorController(ILogger<ErrorController> logger)
		{
			_logger = logger;
		}

		[Route("Error/GeneralExceptions")]
		public IActionResult GeneralExceptions()
		{
			var exceptions = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;
			if (exceptions is ClientSideExceptions)
			{
				return View(new ErrorVM(exceptions.Message, 401));
			}

			//if (exceptions is DbUpdateConcurrencyException)
			//{
			//	return View(new ErrorVM("Your data has been changed. Please try again later.", 401));
			//}

			if (exceptions.InnerException is SqlException sqlException && sqlException.Number == 547)
			{
				return View(new ErrorVM("You have to delete all relevant data before to move on.", 401));
			}

			_logger.LogError("The Error Message From System : -----" + exceptions.Message + "-----");
			Console.WriteLine(exceptions.Message);
			return View(new ErrorVM("Server error, Please speak your admin", 401));
		}

		public IActionResult PageNotFound()
		{
			return View();
		}
	}
}

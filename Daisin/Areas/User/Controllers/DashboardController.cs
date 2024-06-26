﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Daisin.Areas.User.Controllers
{
	[Authorize(Roles = "Member,SuperAdmin")]
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

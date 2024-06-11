using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Services.Identity.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize(Policy = "AdminObserver")]
	[Area("Admin")]
	[Route("Admin/Admin")]
	public class AdminController : Controller
	{
		private readonly IAuthenticationAdminService _authAdminService;
		private readonly IToastNotification _toasty;

		public AdminController(IToastNotification toasty, IAuthenticationAdminService authAdminService)
		{
			_toasty = toasty;
			_authAdminService = authAdminService;
		}

		[HttpGet("GetUserList")]
		public async Task<IActionResult> GetUserList()
		{
			var userListVM = await _authAdminService.GetUserListAsync();
			return View(userListVM);
		}

		public async Task<IActionResult> ExtendClaim(string username)
		{
			var renewClaim = await _authAdminService.ExtendClaimAsync(username);

			if (!renewClaim.Succeeded)
			{
				_toasty.AddErrorToastMessage(NotificationMessagesIdentity.ExtendClaimFailed, new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
				return RedirectToAction("GetUserList", "Admin", new { Area = "Admin" });
			}
			_toasty.AddSuccessToastMessage(NotificationMessagesIdentity.ExtendClaimSuccess, new ToastrOptions { Title = NotificationMessagesIdentity.SuccessTitle });
			return RedirectToAction("GetUserList", "Admin", new { Area = "Admin" });
		}
	}
}

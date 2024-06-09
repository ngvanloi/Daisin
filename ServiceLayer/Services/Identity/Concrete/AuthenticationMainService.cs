using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpes.Identity.EmailHelper;
using ServiceLayer.Services.Identity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Identity.Concrete
{
	public class AuthenticationMainService : IAuthenticationMainService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IEmailSendMethod _email;

		public AuthenticationMainService(IEmailSendMethod email, UserManager<AppUser> userManager)
		{
			_email = email;
			_userManager = userManager;
		}

		public async Task CreateResetCredentitalsAndSend(AppUser user, HttpContext context, IUrlHelper Url)
		{
			string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var passwordResetLink = Url.Action("ResetPassword", "Authentication", new
			{
				UserId = user.Id,
				Token = resetToken,
			}, context.Request.Scheme);

			await _email.SendResetPasswordLinkWithToken(passwordResetLink!, user.Email!);
		}
	}
}

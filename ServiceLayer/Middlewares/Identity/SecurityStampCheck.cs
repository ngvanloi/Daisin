using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Messages.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Middlewares.Identity
{
	public class SecurityStampCheck
	{
		private readonly RequestDelegate _next;

		public SecurityStampCheck(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, UserManager<AppUser> userManager)
		{
			if (context.User.Identity == null)
			{
				await _next(context);
				return;
			}

			if (context.User.Identity.IsAuthenticated)
			{
				var ssCookie = context.User.Claims.FirstOrDefault(x => x.Type.Contains("SecurityStamp"))!.Value;
				var user = await userManager.GetUserAsync(context.User);
				if (ssCookie != user!.SecurityStamp)
				{
					context.Response.Cookies.Delete("DaisinCompany");
					context.Response.Redirect($"/Authentication/LogIn?errorMessage={IdentityMessage.SecurityStampError}");
				}
			}
			await _next(context);
			return;
		}


	}
}

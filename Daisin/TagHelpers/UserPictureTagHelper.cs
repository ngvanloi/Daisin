using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.TagHelpers
{
	public class UserPictureTagHelper : TagHelper
	{
		public string fileName { get; set; } = null!;

		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public UserPictureTagHelper(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public override async Task<Task> ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "img";

			var signedInUsername = _signInManager.Context.User.Claims.First(x => x.Type.Contains("identifier")).Value;
			var user = await _userManager.FindByIdAsync(signedInUsername);

			if(!string.IsNullOrEmpty(user!.FileName))
			{
				output.Attributes.SetAttribute("src", $"/images/{fileName}");
				return base.ProcessAsync(context, output);
			}

			output.Attributes.SetAttribute("src", $"/images/user/default.jpg");
			return base.ProcessAsync(context, output);
		}
	}
}

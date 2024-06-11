using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Daisin.Areas.User.Components
{
    [Authorize]
	[Area("User")]
	public class LayoutViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public LayoutViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            if(Id == null)
            {
                Id = UserClaimsPrincipal.Claims.FirstOrDefault(x => x.Type.Contains("identifier"))!.Value;
            }
            var user = await _userManager.FindByIdAsync(Id);

            if (user == null || user.FileName == null)
            {
                return View(new UserPictureVM { FileName = "Default" });
            }
			return View(new UserPictureVM { FileName = user.FileName });

		}
    }
}

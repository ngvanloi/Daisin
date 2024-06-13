using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class SocialMediaViewComponent : ViewComponent
	{
		private readonly ISocialMediaService _socialMediaService;

		public SocialMediaViewComponent(ISocialMediaService socialMediaService)
		{
			_socialMediaService = socialMediaService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _socialMediaService.GetAllListForUI();
			return View(uiList);
		}
	}
}

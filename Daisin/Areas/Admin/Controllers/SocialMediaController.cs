using EntityLayer.WebApplication.ViewModels.SocialMediaVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("Admin/SocialMedia")]
	public class SocialMediaController : Controller
	{
		private readonly ISocialMediaService _socialMediaService;

		public SocialMediaController(ISocialMediaService socialMediaService)
		{
			_socialMediaService = socialMediaService;
		}
		[HttpGet("GetSocialMediaList")]
		public async Task<IActionResult> GetSocialMediaList()
		{
			var socialMediaList = await _socialMediaService.GetAllAsync();
			return View(socialMediaList);
		}

		[HttpGet("AddSocialMedia")]
		public IActionResult AddSocialMedia()
		{
			return View();
		}
		[HttpPost("AddSocialMedia")]
		public async Task<IActionResult> AddSocialMedia(SocialMediaAddVM request)
		{
			await _socialMediaService.AddSocialMediaAsync(request);
			return RedirectToAction("GetSocialMediaList", "SocialMedia", new { Areas = ("Admin") });
		}

		[HttpGet("UpdateSocialMedia")]
		public async Task<IActionResult> UpdateSocialMedia(int id)
		{
			var socialMedia = await _socialMediaService.GetSocialMediaById(id);
			return View(socialMedia);
		}
		[HttpPost("UpdateSocialMedia")]
		public async Task<IActionResult> UpdateSocialMedia(SocialMediaUpdateVM request)
		{
			await _socialMediaService.UpdateSocialMediaAsync(request);
			return RedirectToAction("GetSocialMediaList", "SocialMedia", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteSocialMedia(int Id)
		{
			await _socialMediaService.DeleteSocialMediaAsync(Id);
			return RedirectToAction("GetSocialMediaList", "SocialMedia", new { Areas = ("Admin") });
		}
	}
}

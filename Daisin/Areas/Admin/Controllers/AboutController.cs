using EntityLayer.WebApplication.ViewModels.AboutVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;
using System.Runtime.CompilerServices;

namespace Daisin.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("Admin/About")]
	public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
		[HttpGet("GetAboutList")]
		public async Task<IActionResult> GetAboutList()
        {
            var aboutList = await _aboutService.GetAllAsync();
            return View(aboutList);
        }

        [HttpGet("AddAbout")]
        public IActionResult AddAbout()
        {
            return View();
        }
        [HttpPost("AddAbout")]
        public async Task<IActionResult> AddAbout(AboutAddVM request)
        {
            await _aboutService.AddAboutAsync(request);
            return RedirectToAction("GetAboutList", "About", new {Areas = ("Admin")});
        }

        [HttpGet("UpdateAbout")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var about = await _aboutService.GetAboutById(id);
            return View(about);
        }
        [HttpPost("UpdateAbout")]
        public async Task<IActionResult> UpdateAbout(AboutUpdateVM request)
        {
            await _aboutService.UpdateAboutAsync(request);
            return RedirectToAction("GetAboutList", "About", new { Areas = ("Admin") });
        }

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteAbout(int Id)
        {
            await _aboutService.DeleteAboutAsync(Id);
            return RedirectToAction("GetAboutList", "About", new { Areas = ("Admin") });
        }
    }
}

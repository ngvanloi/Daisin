using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Testimonal")]
    public class TestimonalController : Controller
    {
        private readonly ITestimonalService _testimonalService;

        public TestimonalController(ITestimonalService testimonalService)
        {
            _testimonalService = testimonalService;
        }
        [HttpGet("GetTestimonalList")]
        public async Task<IActionResult> GetTestimonalList()
        {
            var testimonalList = await _testimonalService.GetAllAsync();
            return View(testimonalList);
        }

        [HttpGet("AddTestimonal")]
        public IActionResult AddTestimonal()
        {
            return View();
        }
        [HttpPost("AddTestimonal")]
        public async Task<IActionResult> AddTestimonal(TestimonalAddVM request)
        {
            await _testimonalService.AddTestimonalAsync(request);
            return RedirectToAction("GetTestimonalList", "Testimonal", new { Areas = ("Admin") });
        }

        [HttpGet("UpdateTestimonal")]
        public async Task<IActionResult> UpdateTestimonal(int id)
        {
            var testimonal = await _testimonalService.GetTestimonalById(id);
            return View(testimonal);
        }
        [HttpPost("UpdateTestimonal")]
        public async Task<IActionResult> UpdateTestimonal(TestimonalUpdateVM request)
        {
            await _testimonalService.UpdateTestimonalAsync(request);
            return RedirectToAction("GetTestimonalList", "Testimonal", new { Areas = ("Admin") });
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteTestimonal(int Id)
        {
            await _testimonalService.DeleteTestimonalAsync(Id);
            return RedirectToAction("GetTestimonalList", "Testimonal", new { Areas = ("Admin") });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class TestimonalViewComponent : ViewComponent
	{
		private readonly ITestimonalService _testimonalService;

		public TestimonalViewComponent(ITestimonalService testimonalService)
		{
			_testimonalService = testimonalService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _testimonalService.GetAllListForUI();
			return View(uiList);
		}
	}
}

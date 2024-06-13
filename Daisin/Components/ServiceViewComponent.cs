using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class ServiceViewComponent : ViewComponent
	{
		private readonly IServiceService _serviceService;

		public ServiceViewComponent(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _serviceService.GetAllListForUI();
			return View(uiList);
		}
	}
}

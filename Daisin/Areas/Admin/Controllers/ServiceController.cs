using EntityLayer.WebApplication.ViewModels.ServiceVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Service")]
	public class ServiceController : Controller
	{
		private readonly IServiceService _serviceService;

		public ServiceController(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}
		[HttpGet("GetServiceList")]
		public async Task<IActionResult> GetServiceList()
		{
			var serviceList = await _serviceService.GetAllAsync();
			return View(serviceList);
		}

		[HttpGet("AddService")]
		public IActionResult AddService()
		{
			return View();
		}
		[HttpPost("AddService")]
		public async Task<IActionResult> AddService(ServiceAddVM request)
		{
			await _serviceService.AddServiceAsync(request);
			return RedirectToAction("GetServiceList", "Service", new { Areas = ("Admin") });
		}

		[HttpGet("UpdateService")]
		public async Task<IActionResult> UpdateService(int id)
		{
			var service = await _serviceService.GetServiceById(id);
			return View(service);
		}
		[HttpPost("UpdateService")]
		public async Task<IActionResult> UpdateService(ServiceUpdateVM request)
		{
			await _serviceService.UpdateServiceAsync(request);
			return RedirectToAction("GetServiceList", "Service", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteService(int Id)
		{
			await _serviceService.DeleteServiceAsync(Id);
			return RedirectToAction("GetServiceList", "Service", new { Areas = ("Admin") });
		}
	}
}

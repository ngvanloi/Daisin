using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
using EntityLayer.WebApplication.ViewModels.ServiceVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Service")]
	public class ServiceController : Controller
	{
		private readonly IServiceService _serviceService;
		private readonly IValidator<ServiceAddVM> _addValidator;
		private readonly IValidator<ServiceUpdateVM> _updateValidator;
		public ServiceController(IServiceService serviceService, IValidator<ServiceAddVM> addValidator, IValidator<ServiceUpdateVM> updateValidator)
		{
			_serviceService = serviceService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
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
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _serviceService.AddServiceAsync(request);
				return RedirectToAction("GetServiceList", "Service", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[ServiceFilter(typeof(GenericNotFoundFilter<Service>))]
		[HttpGet("UpdateService")]
		public async Task<IActionResult> UpdateService(int id)
		{
			var service = await _serviceService.GetServiceById(id);
			return View(service);
		}
		[HttpPost("UpdateService")]
		public async Task<IActionResult> UpdateService(ServiceUpdateVM request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _serviceService.UpdateServiceAsync(request);
				return RedirectToAction("GetServiceList", "Service", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteService(int Id)
		{
			await _serviceService.DeleteServiceAsync(Id);
			return RedirectToAction("GetServiceList", "Service", new { Areas = ("Admin") });
		}
	}
}

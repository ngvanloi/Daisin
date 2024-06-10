using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ContactVM;
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
	[Route("Admin/Contact")]
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;
		private readonly IValidator<ContactAddVM> _addValidator;
		private readonly IValidator<ContactUpdateVM> _updateValidator;

		public ContactController(IContactService contactService, IValidator<ContactAddVM> addValidator, IValidator<ContactUpdateVM> updateValidator)
		{
			_contactService = contactService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
		}
		[HttpGet("GetContactList")]
		public async Task<IActionResult> GetContactList()
		{
			var contactList = await _contactService.GetAllAsync();
			return View(contactList);
		}

		[ServiceFilter(typeof(GenericAddPreventationFilter<Contact>))]
		[HttpGet("AddContact")]
		public IActionResult AddContact()
		{
			return View();
		}
		[HttpPost("AddContact")]
		public async Task<IActionResult> AddContact(ContactAddVM request)
		{
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _contactService.AddContactAsync(request);
				return RedirectToAction("GetContactList", "Contact", new { Area = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[ServiceFilter(typeof(GenericNotFoundFilter<Contact>))]
		[HttpGet("UpdateContact")]
		public async Task<IActionResult> UpdateContact(int id)
		{
			var contact = await _contactService.GetContactById(id);
			return View(contact);
		}
		[HttpPost("UpdateContact")]
		public async Task<IActionResult> UpdateContact(ContactUpdateVM request)
		{
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _contactService.UpdateContactAsync(request);
				return RedirectToAction("GetContactList", "Contact", new { Area = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteContact(int Id)
		{
			await _contactService.DeleteContactAsync(Id);
			return RedirectToAction("GetContactList", "Contact", new { Area = ("Admin") });
		}
	}
}

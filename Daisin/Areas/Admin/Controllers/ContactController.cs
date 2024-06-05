using EntityLayer.WebApplication.ViewModels.ContactVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Contact")]
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;

		public ContactController(IContactService contactService)
		{
			_contactService = contactService;
		}
		[HttpGet("GetContactList")]
		public async Task<IActionResult> GetContactList()
		{
			var contactList = await _contactService.GetAllAsync();
			return View(contactList);
		}

		[HttpGet("AddContact")]
		public IActionResult AddContact()
		{
			return View();
		}
		[HttpPost("AddContact")]
		public async Task<IActionResult> AddContact(ContactAddVM request)
		{
			await _contactService.AddContactAsync(request);
			return RedirectToAction("GetContactList", "Contact", new { Areas = ("Admin") });
		}

		[HttpGet("UpdateContact")]
		public async Task<IActionResult> UpdateContact(int id)
		{
			var contact = await _contactService.GetContactById(id);
			return View(contact);
		}
		[HttpPost("UpdateContact")]
		public async Task<IActionResult> UpdateContact(ContactUpdateVM request)
		{
			await _contactService.UpdateContactAsync(request);
			return RedirectToAction("GetContactList", "Contact", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteContact(int Id)
		{
			await _contactService.DeleteContactAsync(Id);
			return RedirectToAction("GetContactList", "Contact", new { Areas = ("Admin") });
		}
	}
}

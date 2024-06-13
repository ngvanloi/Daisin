using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class ContactViewComponent : ViewComponent
	{
		private readonly IContactService _contactService;

		public ContactViewComponent(IContactService contactService)
		{
			_contactService = contactService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _contactService.GetAllListForUI();
			return View(uiList);
		}
	}
}

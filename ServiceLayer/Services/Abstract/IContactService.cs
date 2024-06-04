using EntityLayer.WebApplication.ViewModels.ContactVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstract
{
	public interface IContactService
	{
		Task<List<ContactListVM>> GetAllAsync();
		Task AddContactAsync(ContactAddVM request);
		Task DeleteContactAsync(int id);
		Task UpdateContactAsync(ContactUpdateVM request);
		Task<ContactUpdateVM> GetContactById(int Id);
	}
}

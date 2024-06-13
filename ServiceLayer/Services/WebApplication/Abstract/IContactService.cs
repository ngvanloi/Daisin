using EntityLayer.WebApplication.ViewModels.ContactVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface IContactService
    {
        Task<List<ContactListVM>> GetAllAsync();
        Task AddContactAsync(ContactAddVM request);
        Task DeleteContactAsync(int id);
        Task UpdateContactAsync(ContactUpdateVM request);
        Task<ContactUpdateVM> GetContactById(int Id);
		Task<List<ContactUI>> GetAllListForUI();
	}
}

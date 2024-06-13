using EntityLayer.WebApplication.ViewModels.ServiceVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface IServiceService
    {
        Task<List<ServiceListVM>> GetAllAsync();
        Task AddServiceAsync(ServiceAddVM request);
        Task DeleteServiceAsync(int id);
        Task UpdateServiceAsync(ServiceUpdateVM request);
        Task<ServiceUpdateVM> GetServiceById(int Id);
		Task<List<ServiceUI>> GetAllListForUI();
	}
}

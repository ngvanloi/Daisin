using EntityLayer.WebApplication.ViewModels.ServiceVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstract
{
	public interface IServiceService
	{
		Task<List<ServiceListVM>> GetAllAsync();
		Task AddServiceAsync(ServiceAddVM request);
		Task DeleteServiceAsync(int id);
		Task UpdateServiceAsync(ServiceUpdateVM request);
		Task<ServiceUpdateVM> GetServiceById(int Id);
	}
}

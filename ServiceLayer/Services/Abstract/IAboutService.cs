using EntityLayer.WebApplication.ViewModels.AboutVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstract
{
	public interface IAboutService
	{
		Task<List<AboutListVM>> GetAllAsync();
		Task AddAboutAsync(AboutAddVM request);
		Task DeleteAboutAsync(int id);
		Task UpdateAboutAsync(AboutUpdateVM request);
		Task<AboutUpdateVM> GetAboutById(int Id);
	}
}

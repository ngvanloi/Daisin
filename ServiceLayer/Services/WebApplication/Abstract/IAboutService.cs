using EntityLayer.WebApplication.ViewModels.AboutVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface IAboutService
    {
        Task<List<AboutListVM>> GetAllAsync();
        Task AddAboutAsync(AboutAddVM request);
        Task DeleteAboutAsync(int id);
        Task UpdateAboutAsync(AboutUpdateVM request);
        Task<AboutUpdateVM> GetAboutById(int Id);
        Task<List<AboutUI>> GetAllListForUI();
	}
}

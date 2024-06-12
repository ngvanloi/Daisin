using EntityLayer.WebApplication.ViewModels.HomePageVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface IHomePageService
    {
        Task<List<HomePageListVM>> GetAllAsync();
        Task AddHomePageAsync(HomePageAddVM request);
        Task DeleteHomePageAsync(int id);
        Task UpdateHomePageAsync(HomePageUpdateVM request);
        Task<HomePageUpdateVM> GetHomePageById(int Id);
        Task<List<HomePageUI>> GetAllListForUI();

    }
}

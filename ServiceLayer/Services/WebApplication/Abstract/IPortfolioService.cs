using EntityLayer.WebApplication.ViewModels.PortfolioVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface IPortfolioService
    {
        Task<List<PortfolioListVM>> GetAllAsync();
        Task AddPortfolioAsync(PortfolioAddVM request);
        Task DeletePortfolioAsync(int id);
        Task UpdatePortfolioAsync(PortfolioUpdateVM request);
        Task<PortfolioUpdateVM> GetPortfolioById(int Id);
		Task<List<PortfolioUI>> GetAllListForUI();
	}
}

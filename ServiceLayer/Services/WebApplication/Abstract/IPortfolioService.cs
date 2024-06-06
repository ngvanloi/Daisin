using EntityLayer.WebApplication.ViewModels.PortfolioVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IPortfolioService
    {
        Task<List<PortfolioListVM>> GetAllAsync();
        Task AddPortfolioAsync(PortfolioAddVM request);
        Task DeletePortfolioAsync(int id);
        Task UpdatePortfolioAsync(PortfolioUpdateVM request);
        Task<PortfolioUpdateVM> GetPortfolioById(int Id);
    }
}

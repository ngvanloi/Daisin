using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class PortfolioViewComponent : ViewComponent
	{
		private readonly IPortfolioService _portfolioService;

		public PortfolioViewComponent(IPortfolioService portfolioService)
		{
			_portfolioService = portfolioService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _portfolioService.GetAllListForUI();
			return View(uiList);
		}
	}
}

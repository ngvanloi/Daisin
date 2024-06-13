using EntityLayer.WebApplication.ViewModels.PortfolioVM;

namespace EntityLayer.WebApplication.ViewModels.CategoryVM
{
	public class CategoryUI
	{
		public string Name { get; set; } = null!;
		public List<PortfolioUI> Portfolios { get; set; } = null!;
	}
}

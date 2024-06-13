using EntityLayer.WebApplication.ViewModels.CategoryVM;

namespace EntityLayer.WebApplication.ViewModels.PortfolioVM
{
	public class PortfolioUI
	{
		public string Title { get; set; } = null!;
		public string FileName { get; set; } = null!;
		public CategoryUI Category { get; set; } = null!;
	}
}

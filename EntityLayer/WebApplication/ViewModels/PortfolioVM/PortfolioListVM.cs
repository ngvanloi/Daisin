using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.CategoryVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.PortfolioVM
{
	public class PortfolioListVM
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; } = DateTime.Now.ToString("d");
		public string? UpdatedDate { get; set; }

		public string Title { get; set; } = null!;
		public string FileName { get; set; } = null!;
		public string FileType { get; set; } = null!;

		public int CategoryId { get; set; }
		public CategoryListVM Category { get; set; } = null!;
	}
}

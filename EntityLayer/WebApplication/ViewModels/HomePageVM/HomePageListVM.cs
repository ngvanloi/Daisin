using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.HomePageVM
{
	public class HomePageListVM
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; } = null!;
		public string? UpdatedDate { get; set; }

		public string Header { get; set; } = null!;
		public string VideoLink { get; set; } = null!;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.HomePageVM
{
    public class HomePageUpdateVM
	{
		public int Id { get; set; }
		public string? UpdatedDate { get; set; }
		public virtual byte[] RowVersion { get; set; } = null!;

		public string Header { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string VideoLink { get; set; } = null!;
	}
}

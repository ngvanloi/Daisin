using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.ServiceVM
{
	public class ServiceListVM
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; } = DateTime.Now.ToString("d");
		public string? UpdatedDate { get; set; }

		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Icon { get; set; } = null!;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.ServiceVM
{
    public class ServiceUpdateVM
	{
		public int Id { get; set; }
		public string? UpdatedDate { get; set; }
		public virtual byte[] RowVersion { get; set; } = null!;

		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Icon { get; set; } = null!;
	}
}

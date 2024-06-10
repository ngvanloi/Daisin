using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.SocialMediaVM
{
	public class SocialMediaListVM
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; } = null!;
		public string? UpdatedDate { get; set; }

		public string? Twitter { get; set; }
		public string? Linkedin { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }

		public AboutListVM About { get; set; } = null!;
	}
}

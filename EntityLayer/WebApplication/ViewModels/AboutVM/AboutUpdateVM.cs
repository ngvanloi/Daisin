using EntityLayer.WebApplication.ViewModels.SocialMediaVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.ViewModels.AboutVM
{
    public class AboutUpdateVM
    {
		public int Id { get; set; }
		public string? UpdatedDate { get; set; }
		public virtual byte[] RowVersion { get; set; } = null!;

		public string Header { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int Clients { get; set; }
		public int Project { get; set; }
		public int HoursOfSupport { get; set; }
		public int HardWorkers { get; set; }
		public string FileName { get; set; } = null!;
		public string FileType { get; set; } = null!;

		public int SocialMediaId { get; set; }
		public SocialMediaUpdateVM SocialMedia { get; set; } = null!;
	}
}

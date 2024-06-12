using EntityLayer.WebApplication.ViewModels.SocialMediaVM;

namespace EntityLayer.WebApplication.ViewModels.AboutVM
{
	public class AboutUI
	{
		public string Header { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int Clients { get; set; }
		public int Project { get; set; }
		public int HoursOfSupport { get; set; }
		public int HardWorkers { get; set; }
		public string FileName { get; set; } = null!;

		public SocialMediaUI SocialMedia { get; set; } = null!;
	}
}

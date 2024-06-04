using EntityLayer.WebApplication.ViewModels.SocialMediaVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstract
{
	public interface ISocialMediaService
	{
		Task<List<SocialMediaListVM>> GetAllAsync();
		Task AddSocialMediaAsync(SocialMediaAddVM request);
		Task DeleteSocialMediaAsync(int id);
		Task UpdateSocialMediaAsync(SocialMediaUpdateVM request);
		Task<SocialMediaUpdateVM> GetSocialMediaById(int Id);
	}
}

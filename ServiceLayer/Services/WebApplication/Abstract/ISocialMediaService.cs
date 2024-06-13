using EntityLayer.WebApplication.ViewModels.SocialMediaVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface ISocialMediaService
    {
        Task<List<SocialMediaListVM>> GetAllAsync();
        Task AddSocialMediaAsync(SocialMediaAddVM request);
        Task DeleteSocialMediaAsync(int id);
        Task UpdateSocialMediaAsync(SocialMediaUpdateVM request);
        Task<SocialMediaUpdateVM> GetSocialMediaById(int Id);
		Task<List<SocialMediaUI>> GetAllListForUI();
	}
}

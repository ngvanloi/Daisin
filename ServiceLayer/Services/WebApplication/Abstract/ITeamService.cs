using EntityLayer.WebApplication.ViewModels.TeamVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface ITeamService
    {
        Task<List<TeamListVM>> GetAllAsync();
        Task AddTeamAsync(TeamAddVM request);
        Task DeleteTeamAsync(int id);
        Task UpdateTeamAsync(TeamUpdateVM request);
        Task<TeamUpdateVM> GetTeamById(int Id);
		Task<List<TeamUI>> GetAllListForUI();
	}
}

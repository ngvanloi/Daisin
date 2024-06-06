using EntityLayer.WebApplication.ViewModels.TeamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface ITeamService
    {
        Task<List<TeamListVM>> GetAllAsync();
        Task AddTeamAsync(TeamAddVM request);
        Task DeleteTeamAsync(int id);
        Task UpdateTeamAsync(TeamUpdateVM request);
        Task<TeamUpdateVM> GetTeamById(int Id);
    }
}

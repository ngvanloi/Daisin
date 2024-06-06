using EntityLayer.WebApplication.ViewModels.TeamVM;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Route("Admin/Team")]
	public class TeamController : Controller
	{
		private readonly ITeamService _teamService;

		public TeamController(ITeamService teamService)
		{
			_teamService = teamService;
		}
		[HttpGet("GetTeamList")]
		public async Task<IActionResult> GetTeamList()
		{
			var teamList = await _teamService.GetAllAsync();
			return View(teamList);
		}

		[HttpGet("AddTeam")]
		public IActionResult AddTeam()
		{
			return View();
		}
		[HttpPost("AddTeam")]
		public async Task<IActionResult> AddTeam(TeamAddVM request)
		{
			await _teamService.AddTeamAsync(request);
			return RedirectToAction("GetTeamList", "Team", new { Areas = ("Admin") });
		}

		[HttpGet("UpdateTeam")]
		public async Task<IActionResult> UpdateTeam(int id)
		{
			var team = await _teamService.GetTeamById(id);
			return View(team);
		}
		[HttpPost("UpdateTeam")]
		public async Task<IActionResult> UpdateTeam(TeamUpdateVM request)
		{
			await _teamService.UpdateTeamAsync(request);
			return RedirectToAction("GetTeamList", "Team", new { Areas = ("Admin") });
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteTeam(int Id)
		{
			await _teamService.DeleteTeamAsync(Id);
			return RedirectToAction("GetTeamList", "Team", new { Areas = ("Admin") });
		}

	}
}

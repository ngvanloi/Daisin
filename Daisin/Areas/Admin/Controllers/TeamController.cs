using EntityLayer.WebApplication.ViewModels.AboutVM;
using EntityLayer.WebApplication.ViewModels.TeamVM;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Team")]
	public class TeamController : Controller
	{
		private readonly ITeamService _teamService;
		private readonly IValidator<TeamAddVM> _addValidator;
		private readonly IValidator<TeamUpdateVM> _updateValidator;

		public TeamController(ITeamService teamService, IValidator<TeamAddVM> addValidator, IValidator<TeamUpdateVM> updateValidator)
		{
			_teamService = teamService;
			_addValidator = addValidator;
			_updateValidator = updateValidator;
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
			var validation = await _addValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _teamService.AddTeamAsync(request);
				return RedirectToAction("GetTeamList", "Team", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
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
			var validation = await _updateValidator.ValidateAsync(request);
			if (validation.IsValid)
			{
				await _teamService.UpdateTeamAsync(request);
				return RedirectToAction("GetTeamList", "Team", new { Areas = ("Admin") });
			}
			validation.AddToModelState(this.ModelState);
			return View();
		}

		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeleteTeam(int Id)
		{
			await _teamService.DeleteTeamAsync(Id);
			return RedirectToAction("GetTeamList", "Team", new { Areas = ("Admin") });
		}

	}
}

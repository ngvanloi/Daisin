using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Daisin.Components
{
	public class TeamViewComponent : ViewComponent
	{
		private readonly ITeamService _teamService;

		public TeamViewComponent(ITeamService teamService)
		{
			_teamService = teamService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var uiList = await _teamService.GetAllListForUI();
			return View(uiList);
		}
	}
}

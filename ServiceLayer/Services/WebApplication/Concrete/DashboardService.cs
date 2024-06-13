using EntityLayer.Identity.Entities;
using EntityLayer.WebApplication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
	public class DashboardService : IDashboardService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<AppUser> _userManager;

		public DashboardService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		public async Task<int> GetAllCategoriesCountAsync()
		{
			var count = await _unitOfWork.GetGenericRepository<Category>().GetAllCount();
			return count;
		}

		public async Task<int> GetAllPortfoliosAsync()
		{
			var count = await _unitOfWork.GetGenericRepository<Portfolio>().GetAllCount();
			return count;
		}

		public async Task<int> GetAllServicesCountAsync()
		{
			var count = await _unitOfWork.GetGenericRepository<Service>().GetAllCount();
			return count;
		}

		public async Task<int> GetAllTeamsCountAsync()
		{
			var count = await _unitOfWork.GetGenericRepository<Team>().GetAllCount();
			return count;
		}

		public async Task<int> GetAllTestimonalsCountAsync()
		{
			var count = await _unitOfWork.GetGenericRepository<Testimonal>().GetAllCount();
			return count;
		}

		public int GetAllUsersCount()
		{
			var count = _userManager.Users.Count();
			return count;
		}
	}
}

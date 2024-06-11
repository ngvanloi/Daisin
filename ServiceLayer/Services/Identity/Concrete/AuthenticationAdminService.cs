using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Identity.Abstract;
using System.Security.Claims;

namespace ServiceLayer.Services.Identity.Concrete
{
	public class AuthenticationAdminService : IAuthenticationAdminService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public AuthenticationAdminService(UserManager<AppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<IdentityResult> ExtendClaimAsync(string username)
		{
			var user = await _userManager.FindByNameAsync(username);
			var claim = await _userManager.GetClaimsAsync(user!);
			var existingClaim = claim.FirstOrDefault(x => x.Type.Contains("Observer"));

			var newExtendClaim = new Claim("AdminObserverExpireDate", DateTime.Now.AddDays(5).ToString());

			return await _userManager.ReplaceClaimAsync(user!, existingClaim!, newExtendClaim);
		}

		public async Task<List<UserVM>> GetUserListAsync()
		{
			var userList = await _userManager.Users.ToListAsync();
			var userListVM = _mapper.Map<List<UserVM>>(userList);
			for (int i = 0; i < userList.Count; i++)
			{
				var userRoles = await _userManager.GetRolesAsync(userList[i]);
				userListVM[i].UserRoles = userRoles;

				var userClaims = await _userManager.GetClaimsAsync(userList[i]);
				userListVM[i].UserClaims = userClaims;
			}
			return userListVM;
		}
	}
}

﻿using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Identity.Abstract
{
	public interface IAuthenticationUserService
	{
		Task<UserEditVM> FindUserAsync(HttpContext httpcontext);
		Task<IdentityResult> UserEditAsync(UserEditVM request, AppUser user);
	}
}

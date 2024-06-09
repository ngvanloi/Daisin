using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Identity.Abstract
{
	public interface IAuthenticationMainService
	{
		Task CreateResetCredentitalsAndSend(AppUser user, HttpContext context, IUrlHelper Url);
	}
}

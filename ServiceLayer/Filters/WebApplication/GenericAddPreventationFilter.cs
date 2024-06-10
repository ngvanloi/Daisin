using CoreLayer.BaseEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.WebApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Filters.WebApplication
{
	public class GenericAddPreventationFilter<T> : IAsyncActionFilter where T : class, IBaseEntity, new()
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IToastNotification _toasty;

		public GenericAddPreventationFilter(IUnitOfWork unitOfWork, IToastNotification toasty)
		{
			_unitOfWork = unitOfWork;
			_toasty = toasty;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var entityList = await _unitOfWork.GetGenericRepository<T>().GetAll().ToListAsync();
			var methodName = typeof(T).Name;
			if (entityList.Any())
			{
				_toasty.AddErrorToastMessage(
					$"You already have an {methodName} Section. Please delete it first and try again later!!",
					new ToastrOptions { Title = "I am Sorry!!" });
				context.Result = new RedirectToActionResult($"Get{methodName}List", methodName, new { Area = ("Admin") });
				return;
			}

			await next.Invoke();
			return;
		}
	}
}

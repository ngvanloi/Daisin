﻿using CoreLayer.BaseEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Filters.WebApplication
{
	public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : class, IBaseEntity, new()
	{
		private readonly IUnitOfWork _unitOfWork;

		public GenericNotFoundFilter(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var value = context.ActionArguments.FirstOrDefault().Value;
			if (value == null)
			{
				throw new ClientSideExceptions("The input is invalid. Please try to use valid id");
			}

			var id = (int)value!;
			var entityCheck = await _unitOfWork.GetGenericRepository<T>().GetEntityByIdAsync(id);
            if (entityCheck == null)
            {
				throw new ClientSideExceptions("The id does not exist. Please try to use exist id");
			}
			await next.Invoke();
			return;
		}
	}
}

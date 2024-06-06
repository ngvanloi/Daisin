using EntityLayer.WebApplication.ViewModels.TeamVM;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.TeamValidation
{
	public class TeamUpdateValidation : AbstractValidator<TeamUpdateVM>
	{
		public TeamUpdateValidation()
		{
			RuleFor(x => x.FullName)
				.NotEmpty()
				.NotNull()
				.MaximumLength(100);
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(100);
			RuleFor(x => x.FileName)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.FileType)
				.NotEmpty()
				.NotNull();
		}
	}
}

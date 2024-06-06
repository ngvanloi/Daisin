using EntityLayer.WebApplication.ViewModels.CategoryVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.CategoryValidation
{
	public class CategoryAddValidation : AbstractValidator<CategoryAddVM>
	{
		public CategoryAddValidation()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Name"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Name"))
				.MaximumLength(50).WithMessage(ValidationMessage.MaximumCharacterAllance("Name", 50));
		}
	}
}

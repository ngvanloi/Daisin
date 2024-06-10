using EntityLayer.WebApplication.ViewModels.TeamVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.TeamValidation
{
	public class TeamAddValidation : AbstractValidator<TeamAddVM>
	{
		public TeamAddValidation()
		{
			RuleFor(x => x.FullName)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("FullName"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("FullName"))
				.MaximumLength(100).WithMessage(ValidationMessage.MaximumCharacterAllance("FullName", 100));
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Title"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Title"))
				.MaximumLength(100).WithMessage(ValidationMessage.MaximumCharacterAllance("Title", 100));
			RuleFor(x => x.Photo)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Photo"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Photo"));
		}
	}
}

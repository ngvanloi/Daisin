using EntityLayer.Identity.ViewModels;
using FluentValidation;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.Identity
{
	public class LogInValidation : AbstractValidator<LogInVM>
	{
		public LogInValidation()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Email"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Email"))
				.EmailAddress().WithMessage(IdentityMessage.CheckEmailAddress());
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Password"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Password"));
		}
	}
}

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
	public class ResetPasswordValidation : AbstractValidator<ResetPasswordVM>
	{
		public ResetPasswordValidation()
		{
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Password"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Password"));
			RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("ConfirmPassword"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("ConfirmPassword"))
				.Equal(x => x.Password).WithMessage(IdentityMessage.ComparePassword());
		}
	}
}

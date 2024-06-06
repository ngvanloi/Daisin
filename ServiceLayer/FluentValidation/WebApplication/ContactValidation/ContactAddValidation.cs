using EntityLayer.WebApplication.ViewModels.ContactVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.ContactValidation
{
	public class ContactAddValidation : AbstractValidator<ContactAddVM>
	{
		public ContactAddValidation()
		{
			RuleFor(x => x.Location)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Location"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Location"))
				.MaximumLength(200).WithMessage(ValidationMessage.MaximumCharacterAllance("Location", 200));
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Email"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Email"))
				.MaximumLength(100).WithMessage(ValidationMessage.MaximumCharacterAllance("Email", 100));
			RuleFor(x => x.Call)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Call"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Call"))
				.MaximumLength(17).WithMessage(ValidationMessage.MaximumCharacterAllance("Call", 17));
			RuleFor(x => x.Map)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Map"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Map"));
		}
	}
}

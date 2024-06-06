using EntityLayer.WebApplication.ViewModels.ServiceVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.ServiceValidation
{
	public class ServiceAddValidation : AbstractValidator<ServiceAddVM>
	{
		public ServiceAddValidation()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Name"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Name"))
				.MaximumLength(200).WithMessage(ValidationMessage.MaximumCharacterAllance("Name", 200));
			RuleFor(x => x.Description)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Description"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Description"))
				.MaximumLength(2000).WithMessage(ValidationMessage.MaximumCharacterAllance("Description", 2000));
			RuleFor(x => x.Icon)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Icon"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Icon"))
				.MaximumLength(100).WithMessage(ValidationMessage.MaximumCharacterAllance("Icon", 100));
		}
	}
}

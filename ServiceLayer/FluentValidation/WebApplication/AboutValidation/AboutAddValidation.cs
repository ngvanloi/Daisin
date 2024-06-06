using EntityLayer.WebApplication.ViewModels.AboutVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.AboutValidation
{
	public class AboutAddValidation : AbstractValidator<AboutAddVM>
	{
		public AboutAddValidation()
		{
			RuleFor(x => x.Header)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Header"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Header"))
				.MaximumLength(200).WithMessage(ValidationMessage.MaximumCharacterAllance("Header",200));

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Description"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Description"))
				.MaximumLength(2000).WithMessage(ValidationMessage.MaximumCharacterAllance("Description", 2000));

			RuleFor(x => x.Clients)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Clients"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Clients"))
				.GreaterThan(0).WithMessage(ValidationMessage.GreaterThanMessage("Clients", 0))
				.LessThan(1000).WithMessage(ValidationMessage.LessThanMessage("Clients", 1000));

			RuleFor(x => x.Project)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Project"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Project"))
				.GreaterThan(0).WithMessage(ValidationMessage.GreaterThanMessage("Project", 0))
				.LessThan(10000).WithMessage(ValidationMessage.LessThanMessage("Project", 10000));

			RuleFor(x => x.HoursOfSupport)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("HoursOfSupport"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("HoursOfSupport"))
				.GreaterThan(0).WithMessage(ValidationMessage.GreaterThanMessage("HoursOfSupport", 0))
				.LessThan(100000).WithMessage(ValidationMessage.LessThanMessage("HoursOfSupport", 100000));

			RuleFor(x => x.HardWorkers)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("HardWorkers"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("HardWorkers"))
				.GreaterThan(0).WithMessage(ValidationMessage.GreaterThanMessage("HardWorkers", 0))
				.LessThan(99).WithMessage(ValidationMessage.LessThanMessage("HardWorkers", 99));

			RuleFor(x => x.Photo)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Photo"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Photo"));
		}
	}
}

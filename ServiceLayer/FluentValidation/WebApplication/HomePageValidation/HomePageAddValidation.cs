using EntityLayer.WebApplication.ViewModels.HomePageVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.HomePageValidation
{
	public class HomePageAddValidation : AbstractValidator<HomePageAddVM>
	{
		public HomePageAddValidation()
		{
			RuleFor(x => x.Header)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Header"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Header"))
				.MaximumLength(200).WithMessage(ValidationMessage.MaximumCharacterAllance("Header", 200));
			RuleFor(x => x.Description)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Description"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Description"))
				.MaximumLength(2000).WithMessage(ValidationMessage.MaximumCharacterAllance("Description", 2000));
			RuleFor(x => x.VideoLink)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("VideoLink"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("VideoLink"));
		}
	}
}

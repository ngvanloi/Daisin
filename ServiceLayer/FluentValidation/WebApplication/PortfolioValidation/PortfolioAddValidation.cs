using EntityLayer.WebApplication.ViewModels.PortfolioVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.PortfolioValidation
{
	public class PortfolioAddValidation : AbstractValidator<PortfolioAddVM>
	{
		public PortfolioAddValidation()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Title"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Title"))
				.MaximumLength(200).WithMessage(ValidationMessage.MaximumCharacterAllance("Title", 200));
			//RuleFor(x => x.FileName)
			//	.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("FileName"))
			//	.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("FileName"));
			//RuleFor(x => x.FileType)
			//	.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("FileType"))
			//	.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("FileType"));
			RuleFor(x => x.Photo)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Photo"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Photo"));
		}
	}
}

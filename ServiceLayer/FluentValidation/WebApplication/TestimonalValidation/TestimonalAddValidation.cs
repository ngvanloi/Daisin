using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.FluentValidation.WebApplication.TestimonalValidation
{
	public class TestimonalAddValidation : AbstractValidator<TestimonalAddVM>
	{
		public TestimonalAddValidation()
		{
			RuleFor(x => x.FullName)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("FullName"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("FullName"))
				.MaximumLength(100).WithMessage(ValidationMessage.MaximumCharacterAllance("FullName", 100));
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Title"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Title"))
				.MaximumLength(100).WithMessage(ValidationMessage.MaximumCharacterAllance("Title", 100));
			RuleFor(x => x.Comment)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Comment"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Comment"))
				.MaximumLength(2000).WithMessage(ValidationMessage.MaximumCharacterAllance("Comment", 2000));
			RuleFor(x => x.FileName)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("FileName"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("FileName"));
			RuleFor(x => x.FileType)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("FileType"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("FileType"));
			RuleFor(x => x.Photo)
				.NotEmpty().WithMessage(ValidationMessage.NullEmptyMessage("Photo"))
				.NotNull().WithMessage(ValidationMessage.NullEmptyMessage("Photo"));
		}
	}
}

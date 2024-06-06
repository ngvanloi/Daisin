using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using FluentValidation;
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
				.NotEmpty()
				.NotNull()
				.MaximumLength(100);
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(100);
			RuleFor(x => x.Comment)
				.NotEmpty()
				.NotNull()
				.MaximumLength(2000);
			RuleFor(x => x.FileName)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.FileType)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Photo)
				.NotEmpty()
				.NotNull();
		}
	}
}

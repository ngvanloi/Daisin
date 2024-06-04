using EntityLayer.WebApplication.ViewModels.TestimonalVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstract
{
	public interface ITestimonalService
	{
		Task<List<TestimonalListVM>> GetAllAsync();
		Task AddTestimonalAsync(TestimonalAddVM request);
		Task DeleteTestimonalAsync(int id);
		Task UpdateTestimonalAsync(TestimonalUpdateVM request);
		Task<TestimonalUpdateVM> GetTestimonalById(int Id);
	}
}

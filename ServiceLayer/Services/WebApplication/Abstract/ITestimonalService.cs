using EntityLayer.WebApplication.ViewModels.TestimonalVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface ITestimonalService
    {
        Task<List<TestimonalListVM>> GetAllAsync();
        Task AddTestimonalAsync(TestimonalAddVM request);
        Task DeleteTestimonalAsync(int id);
        Task UpdateTestimonalAsync(TestimonalUpdateVM request);
        Task<TestimonalUpdateVM> GetTestimonalById(int Id);
		Task<List<TestimonalUI>> GetAllListForUI();
	}
}

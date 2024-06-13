using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.TestimonalVM;

namespace ServiceLayer.Automapper.WebApplication
{
	public class TestimonalMapper : Profile
    {
        public TestimonalMapper()
        {
            CreateMap<Testimonal, TestimonalListVM>().ReverseMap();
            CreateMap<Testimonal, TestimonalAddVM>().ReverseMap();
            CreateMap<Testimonal, TestimonalUpdateVM>().ReverseMap();
            CreateMap<Testimonal, TestimonalUI>().ReverseMap();
        }
    }
}

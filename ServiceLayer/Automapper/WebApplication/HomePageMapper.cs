using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.HomePageVM;

namespace ServiceLayer.Automapper.WebApplication
{
    public class HomePageMapper : Profile
    {
        public HomePageMapper()
        {
            CreateMap<HomePage, HomePageListVM>().ReverseMap();
            CreateMap<HomePage, HomePageAddVM>().ReverseMap();
            CreateMap<HomePage, HomePageUpdateVM>().ReverseMap();
            CreateMap<HomePage, HomePageUI>().ReverseMap();
        }
    }
}

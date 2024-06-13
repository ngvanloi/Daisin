using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.PortfolioVM;

namespace ServiceLayer.Automapper.WebApplication
{
	public class PortfolioMapper : Profile
    {
        public PortfolioMapper()
        {
            CreateMap<Portfolio, PortfolioListVM>().ReverseMap();
            CreateMap<Portfolio, PortfolioAddVM>().ReverseMap();
            CreateMap<Portfolio, PortfolioUpdateVM>().ReverseMap();
            CreateMap<Portfolio, PortfolioUI>().ReverseMap();
        }
    }
}

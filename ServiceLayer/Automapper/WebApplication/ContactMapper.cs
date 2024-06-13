using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ContactVM;

namespace ServiceLayer.Automapper.WebApplication
{
	public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<Contact, ContactListVM>().ReverseMap();
            CreateMap<Contact, ContactAddVM>().ReverseMap();
            CreateMap<Contact, ContactUpdateVM>().ReverseMap();
            CreateMap<Contact, ContactUI>().ReverseMap();
        }
    }
}
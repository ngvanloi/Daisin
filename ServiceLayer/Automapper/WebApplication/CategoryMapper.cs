﻿using AutoMapper;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.CategoryVM;

namespace ServiceLayer.Automapper.WebApplication
{
	public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryListVM>().ReverseMap();
            CreateMap<Category, CategoryAddVM>().ReverseMap();
            CreateMap<Category, CategoryUpdateVM>().ReverseMap();
            CreateMap<Category, CategoryUI>().ReverseMap();
        }
    }
}

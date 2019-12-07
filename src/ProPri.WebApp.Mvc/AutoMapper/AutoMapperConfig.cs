using AutoMapper;
using ProPri.Users.Application.Queries.Dtos;
using ProPri.WebApp.Mvc.Views.Users.ViewModels;

namespace ProPri.WebApp.Mvc.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserIndexDto, UserIndexViewModel>();
        }
    }
}
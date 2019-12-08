using AutoMapper;
using ProPri.Users.Domain;
using ProPri.Users.Domain.Dtos;
using ProPri.WebApp.Mvc.Views.Users.ViewModels;
using System.Linq;

namespace ProPri.WebApp.Mvc.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserIndexDto, UserIndexViewModel>();
            CreateMap<User, UserFormDto>()
                .ForMember(ufd => ufd.RoleId, opt => 
                    opt.MapFrom(u => u.UserRoles.Single().RoleId))
                .ForMember(ufd => ufd.FirstName, opt =>
                    opt.MapFrom(u => u.Name.FirstName))
                .ForMember(ufd => ufd.Surname, opt =>
                    opt.MapFrom(u => u.Name.Surname));
            CreateMap<UserFormDto, UserFormViewModel>();

            CreateMap<Role, RoleIdNameDto>();
            CreateMap<RoleIdNameDto, RoleIndexViewModel>();
        }
    }
}
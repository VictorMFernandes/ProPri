using AutoMapper;
using ProPri.Users.Application.Commands;
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
            CreateMap<User, UserFormDto>()
                .ForMember(ufd => ufd.RoleId, opt => 
                    opt.MapFrom(u => u.UserRoles.Single().RoleId));
            CreateMap<UserFormDto, UserFormViewModel>();

            CreateMap<Role, RoleIdNameDto>();
            CreateMap<RoleIdNameDto, RoleIndexViewModel>();

            CreateMap<UserFormViewModel, EditUserCommand>();
            CreateMap<UserFormViewModel, CreateUserCommand>();
        }
    }
}
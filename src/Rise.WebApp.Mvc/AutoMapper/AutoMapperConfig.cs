using AutoMapper;
using Rise.Users.Application.Commands;
using Rise.Users.Domain;
using Rise.Users.Domain.Dtos;
using Rise.WebApp.Mvc.Views.Users.ViewModels;
using System.Linq;

namespace Rise.WebApp.Mvc.AutoMapper
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
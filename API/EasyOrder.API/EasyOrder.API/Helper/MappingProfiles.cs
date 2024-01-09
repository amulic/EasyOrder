using EasyOrder.API.Models.Domain;
using AutoMapper;
using EasyOrder.API.Models.DTO;

namespace EasyOrder.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Administrator, AdministratorDto>();
            CreateMap<AdministratorDto, Administrator>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}

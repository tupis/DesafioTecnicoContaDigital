
using AutoMapper;

using Application.Dto;
using Domain.Entities;
using Application.DTO.UserDto;

namespace Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            //Account
            CreateMap<Account, CreateAccountDto>().ReverseMap();
            CreateMap<Account, CreateUserAccountResponse>().ReverseMap();

            //User
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, CreateUserDtoResponse>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                .ReverseMap();
        }
    }
}

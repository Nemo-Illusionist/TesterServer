using System;
using AutoMapper;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Dto.Users;

namespace Tester.Infrastructure.Profiles
{
    public class TesterDtoMappingProfile : Profile
    {
        public TesterDtoMappingProfile()
        {
            CreateMap<Role, BaseDto<Guid>>();
            CreateMap<User, UserDto>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.UserData.Name))
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.UserData.LastName));
        }
    }
}
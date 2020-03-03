using System;
using AutoMapper;
using Tester.Db.Model.Client;
using Tester.Dto.Users;

namespace Tester.Dto.Profiles
{
    public class TesterMappingProfile : Profile
    {
        public TesterMappingProfile()
        {
            CreateMap<Role, BaseDto<Guid>>();
            CreateMap<User, UserModel>();
        }
    }
}
using System;
using AutoMapper;
using Tester.Db.Model.Client;

namespace Tester.Dto.Profiles
{
    public class TesterMappingProfile : Profile
    {
        public TesterMappingProfile()
        {
            CreateMap<Role, BaseDto<Guid>>();
        }
    }
}
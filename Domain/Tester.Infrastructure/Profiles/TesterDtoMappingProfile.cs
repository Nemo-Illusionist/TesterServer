using System;
using AutoMapper;
using Tester.Db.Model.Client;
using Tester.Dto;

namespace Tester.Infrastructure.Profiles
{
    public class TesterDtoMappingProfile : Profile
    {
        public TesterDtoMappingProfile()
        {
            CreateMap<Role, BaseDto<Guid>>();
        }
    }
}
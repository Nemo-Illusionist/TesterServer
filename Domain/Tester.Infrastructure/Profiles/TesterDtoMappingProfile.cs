using System;
using AutoMapper;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Dto.Question;
using Tester.Dto.Topic;
using Tester.Dto.Users;

namespace Tester.Infrastructure.Profiles
{
    public class TesterDtoMappingProfile : Profile
    {
        public TesterDtoMappingProfile()
        {
            CreateMap<Role, BaseDto<Guid>>();

            CreateMap<User, BaseDto<Guid>>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.UserData.Name));
            CreateMap<User, UserDto>()
                .IncludeBase<User, BaseDto<Guid>>()
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.UserData.LastName));

            CreateMap<UserRequest, User>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Salt, x => x.Ignore())
                .ForMember(x => x.Password, x => x.Ignore())
                .ForMember(x => x.SecurityTimestamp, x => x.Ignore())
                .ForMember(x => x.CreatedUtc, x => x.Ignore())
                .ForMember(x => x.UpdatedUtc, x => x.Ignore())
                .ForMember(x => x.DeletedUtc, x => x.Ignore())
                .ForMember(x => x.UserData, x => x.Ignore())
                .ForMember(x => x.Topics, x => x.Ignore())
                .ForMember(x => x.Questions, x => x.Ignore())
                .ForMember(x => x.Tests, x => x.Ignore())
                .ForMember(x => x.UserRoles, x => x.Ignore())
                .ForMember(x => x.Observers, x => x.Ignore())
                .ForMember(x => x.UserTests, x => x.Ignore());
            CreateMap<UserRequest, UserData>()
                .ForMember(x => x.UserId, x => x.Ignore())
                .ForMember(x => x.Gender, x => x.Ignore())
                .ForMember(x => x.UpdatedUtc, x => x.Ignore())
                .ForMember(x => x.User, x => x.Ignore());

            CreateMap<Question, QuestionDto>();
            CreateMap<Question, QuestionFullDto>()
                .IncludeBase<Question, QuestionDto>();
            CreateMap<QuestionRequest, Question>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.AuthorId, x => x.MapFrom((r, q) => q?.AuthorId ?? r.AuthorId))
                .ForMember(x => x.CreatedUtc, x => x.Ignore())
                .ForMember(x => x.DeletedUtc, x => x.Ignore())
                .ForMember(x => x.Topic, x => x.Ignore())
                .ForMember(x => x.Author, x => x.Ignore())
                .ForMember(x => x.UserAnswer, x => x.Ignore());
            CreateMap<Topic, TopicDto>();
        }
    }
}
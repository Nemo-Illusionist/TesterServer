using System;
using AutoMapper;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;
using Tester.Db.Model.Statistics;
using Tester.Dto;
using Tester.Dto.Question;
using Tester.Dto.Statistic;
using Tester.Dto.Test;
using Tester.Dto.TestTopic;
using Tester.Dto.Topic;
using Tester.Dto.User;

namespace Tester.Infrastructure.Profiles
{
    public class TesterDtoMappingProfile : Profile
    {
        public TesterDtoMappingProfile()
        {
            CreateMap<Role, BaseDto<Guid>>();

            User();
            Question();
            Topic();
            Test();
            TestTopic();
            Statistic();
        }

        private void Statistic()
        {
            CreateMap<UserTest, BaseDto<Guid>>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Test.Name));
            CreateMap<UserAnswer, UserAnswerDto>()
                .ForMember(x => x.Topic, x => x.MapFrom(y => y.Question.Topic))
                .ForMember(x => x.Author, x => x.MapFrom(y => y.Question.Author))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Question.Name))
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Question.Description))
                .ForMember(x => x.Hint, x => x.MapFrom(y => y.Question.Hint))
                .ForMember(x => x.Type, x => x.MapFrom(y => y.Question.Type));
            CreateMap<UserTest, UserTestDto>()
                .ForMember(x => x.TestAuthor, x => x.MapFrom(y => y.Test.Author))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Test.Name))
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Test.Description))
                .ForMember(x => x.NumberOfQuestions, x => x.MapFrom(y => y.Test.NumberOfQuestions));
        }

        private void User()
        {
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
        }

        private void Topic()
        {
            CreateMap<Topic, BaseDto<Guid>>();
            CreateMap<Topic, TopicDto>()
                .IncludeBase<Topic, BaseDto<Guid>>();
            CreateMap<Topic, TopicFullDto>()
                .IncludeBase<Topic, TopicDto>();
            CreateMap<TopicRequest, Topic>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.CreatedUtc, x => x.Ignore())
                .ForMember(x => x.UpdatedUtc, x => x.Ignore())
                .ForMember(x => x.DeletedUtc, x => x.Ignore())
                .ForMember(x => x.Author, x => x.Ignore())
                .ForMember(x => x.AuthorId, x => x.MapFrom((r, q) => r.AuthorId ?? q.AuthorId))
                .ForMember(x => x.Parent, x => x.Ignore())
                .ForMember(x => x.Children, x => x.Ignore())
                .ForMember(x => x.Questions, x => x.Ignore())
                .ForMember(x => x.TestTopics, x => x.Ignore());
        }

        private void Test()
        {
            CreateMap<Test, BaseDto<Guid>>();
            CreateMap<Test, TestDto>()
                .IncludeBase<Test, BaseDto<Guid>>();
            CreateMap<Test, TestFullDto>()
                .IncludeBase<Test, TestDto>();
            CreateMap<TestRequest, Test>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.CreatedUtc, x => x.Ignore())
                .ForMember(x => x.UpdatedUtc, x => x.Ignore())
                .ForMember(x => x.DeletedUtc, x => x.Ignore())
                .ForMember(x => x.Author, x => x.Ignore())
                .ForMember(x => x.UserTests, x => x.Ignore())
                .ForMember(x => x.AuthorId, x => x.MapFrom((r, q) => r.AuthorId ?? q.AuthorId))
                .ForMember(x => x.TestTopics, x => x.Ignore());
        }

        private void Question()
        {
            CreateMap<Question, BaseDto<Guid>>();
            CreateMap<Question, QuestionDto>()
                .IncludeBase<Question, BaseDto<Guid>>();
            CreateMap<Question, QuestionFullDto>()
                .IncludeBase<Question, QuestionDto>();
            CreateMap<QuestionRequest, Question>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.AuthorId, x => x.MapFrom((r, q) => r.AuthorId ?? q.AuthorId))
                .ForMember(x => x.CreatedUtc, x => x.Ignore())
                .ForMember(x => x.DeletedUtc, x => x.Ignore())
                .ForMember(x => x.Topic, x => x.Ignore())
                .ForMember(x => x.Author, x => x.Ignore())
                .ForMember(x => x.UserAnswer, x => x.Ignore());
        }

        private void TestTopic()
        {
            CreateMap<TestTopic, TestTopicDto>();
            CreateMap<TestTopicRequest, TestTopic>()
                .ForMember(x => x.CreatedUtc, x => x.Ignore())
                .ForMember(x => x.DeletedUtc, x => x.Ignore())
                .ForMember(x => x.Test, x => x.Ignore())
                .ForMember(x => x.Topic, x => x.Ignore());
        }
    }
}
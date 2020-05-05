using System;
using System.Collections.Generic;
using AutoMapper;
using Tester.Db.Model.App;
using Tester.Dto;
using Tester.Dto.Question;
using Tester.Web.Admin.Services;

namespace Tester.Web.Admin.Profiles
{
    public class TesterQuestionMappingProfile : Profile
    {
        public TesterQuestionMappingProfile()
        {
            CreateMap<QuestionModel, Question>();
            CreateMap<IEnumerable<QuestionModel>, Question>();
            CreateMap<Topic, TopicModel>();
            CreateMap<CreateQuestionModel, Question>();
            // CreateMap<CreateQuestionModel, Question>()
            //     .ForMember(x => x.Topic,
            //         s => s.MapFrom(o => o.Topic));
            // CreateMap<CreateQuestionModel, Question>()
            //     .ForMember(x => x.Author,
            //         s => s.MapFrom(o => o.Author));
            CreateMap<UpdateQuestionModel, Question>();
            CreateMap<Question, QuestionModel>();
            CreateMap<Question, IEnumerable<QuestionModel>>();
            CreateMap<Question, QuestionModel>()
                .ForMember(s => s.Topic,
                    o => o.MapFrom(x => x.Topic));
        }
    }
}
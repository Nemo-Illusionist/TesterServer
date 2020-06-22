using System;
using System.Text.Json;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using JetBrains.Annotations;
using Tester.Core.Common;
using Tester.Dto.Question;

namespace Tester.Web.Admin.Validation.Question
{
    public class QuestionValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionValidator()
        {
            RuleFor(request => request.Answer).NotEmpty().NotNull();
            RuleFor(request => request.Type).IsInEnum().NotNull();
            RuleFor(request => request.Name).NotNull();
            RuleFor(request => request.AuthorId).NotNull();
            RuleFor(request => request.TopicId).NotNull();
            RuleFor(request => request.Answer).Must(BeOpen)
                .When(request => (request.Type == QuestionType.Open));
            RuleFor(request => request.Answer).Must(BeSingleMultipleSection)
                .When(request => (request.Type == QuestionType.SingleSelection || request.Type == QuestionType.MultipleSelection ));
        }

        public bool BeOpen(string answer)
        {
            try
            {
                var openAnswer = JsonSerializer.Deserialize<string[]>(answer);
                return openAnswer.Length != 0;
            }
            catch(Exception ex)
            {
                //Console.WriteLine("{0} Exception caught.", ex);
                return false;
            }
        }

        public bool BeSingleMultipleSection(string answer)
        {
            try
            {
                var sectionAnswer = JsonSerializer.Deserialize<string[][]>(answer);
                return sectionAnswer.Length != 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
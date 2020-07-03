using System;
using System.Linq;
using System.Text.Json;
using FluentValidation;
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
            RuleFor(request => request.Answer).Must(BeSingleSection)
                .When(request => (request.Type == QuestionType.SingleSelection ));
            RuleFor(request => request.Answer).Must(BeMultipleSection)
                .When(request => (request.Type == QuestionType.MultipleSelection ));
        }

        public bool BeOpen(string answer)
        {
            try
            {
                var openAnswer = JsonSerializer.Deserialize<OpenQuestion>(answer);
                return openAnswer.Answers.Length != 0;
            }
            catch(Exception ex)
            {
                //Console.WriteLine("{0} Exception caught.", ex);
                return false;
            }
        }

        public bool BeSingleSection(string answer)
        {
            try
            {
                var sectionAnswer = JsonSerializer.Deserialize<SingleSectionQuestion>(answer);
                var valid = (sectionAnswer.Values.Contains(sectionAnswer.Answer))
                                && sectionAnswer.Values != null;
                return valid;
            }
            catch
            {
                return false;
            }
        }
        public bool BeMultipleSection(string answer)
        {
            try
            {
                var multipleAnswer = JsonSerializer.Deserialize<MultipleSectionQuestion>(answer);
                foreach (var ans in multipleAnswer.Answers)
                {
                    if (!multipleAnswer.Values.Contains(ans))
                        return false;
                }
                return multipleAnswer.Values != null;
            }
            catch
            {
                return false;
            }

        }
    }
}
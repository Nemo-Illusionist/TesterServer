using FluentValidation;
using Tester.Dto.Question;

namespace Tester.Web.Admin.Validation.Question
{
    public class QuestionValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionValidator()
        {
        }
    }
}
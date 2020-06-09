using System;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    public class QuestionDto : BaseDto<Guid>
    {
        public QuestionType Type { get; set; }
    }
}
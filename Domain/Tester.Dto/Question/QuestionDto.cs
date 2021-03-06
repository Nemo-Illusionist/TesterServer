using System;
using JetBrains.Annotations;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    [PublicAPI]
    public class QuestionDto : BaseDto<Guid>
    {
        public QuestionType Type { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? DeletedUtc { get; set; }
    }
}
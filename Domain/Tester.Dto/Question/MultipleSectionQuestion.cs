using System;
using JetBrains.Annotations;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class MultipleSectionQuestion
    {
        public string[] answers { get; set; }
        public string[] values { get; set; }
    }
}
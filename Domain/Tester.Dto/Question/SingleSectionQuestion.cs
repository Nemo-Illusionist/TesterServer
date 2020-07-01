using System;
using JetBrains.Annotations;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class SingleSectionQuestion
    {
        public string answer { get; set; }
        public string[] values { get; set; }
    }
}
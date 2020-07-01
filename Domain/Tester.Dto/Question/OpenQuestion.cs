using System;
using JetBrains.Annotations;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class OpenQuestion
    {
        public string[] answers { get; set; }
    }
}
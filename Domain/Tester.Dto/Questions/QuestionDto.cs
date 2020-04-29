using System;
using System.Collections.Generic;
using System.Text;
using Tester.Core.Common;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.Dto.Questions
{
    public class QuestionDto: BaseDto<Guid>
    {
        public string Description { get; set; }

        public string Hint { get; set; }

        public QuestionType Type { get; set; }
        
        public string Answer { get; set; }


        public Topic Topic { get; set; }

        public User Author { get; set; }

    }
}

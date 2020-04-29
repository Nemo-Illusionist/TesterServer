using System;
using System.Collections.Generic;
using System.Text;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.Dto.Topics
{
    public class TopicRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public User Author { get; set; }
        public Topic Parent { get; set; }
    }
}

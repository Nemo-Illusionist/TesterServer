using System;
using System.Collections.Generic;
using System.Text;
using Tester.Db.Model.Client;

namespace Tester.Dto.Tests
{
    public class TestRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public User Author { get; set; }
    }
}

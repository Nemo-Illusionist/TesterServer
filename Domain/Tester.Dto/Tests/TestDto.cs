using System;
using System.Collections.Generic;
using System.Text;
using Tester.Db.Model.Client;

namespace Tester.Dto.Tests
{
    public class TestDto: BaseDto<Guid>
    {
        public User Author { get; set; }
        public string Description { get; set; }
    }
}

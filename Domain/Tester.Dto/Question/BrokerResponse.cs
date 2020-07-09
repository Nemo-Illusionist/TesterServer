using System;

namespace Tester.Dto.Question
{
    public class BrokerResponse
    {
        public Guid Key { get; set; }
        public IssuedQuestion Question { get; set; }
        public int Count { get; set; }
    }
}
using System;

namespace Tester.Dto
{
    public class BaseDto<TKey> where TKey : IComparable
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}
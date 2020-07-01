using System.Collections.Generic;
using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Radilovsoft.Rest.Infrastructure.Dto;

namespace Tester.Web.Broker.Models
{
    public class FilterRequest
    {
        public IPageFilter PageFilter { get; set; }
        public Filter Filter { get; set; }
        public IEnumerable<IOrder> Orders { get; set; }
    }
}
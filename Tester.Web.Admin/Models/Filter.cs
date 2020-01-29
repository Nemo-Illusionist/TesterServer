using REST.Infrastructure.Contract.Dto;
using REST.Infrastructure.Dto;

namespace Tester.Web.Admin.Models
{
    public class FilterRequest
    {
       public IPageFilter PageFilter {get;set;}
       public Filter Filter {get;set;}
       public IOrder[] Orders {get;set;}
    }
}
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tenta.Models.Customer
{
    public class CustomersVM
    {
        public IEnumerable<CustomerDTO>? Customers { get; set; }
        
        public List<SelectListItem>? Countries { get; set; }
        public string? q { get; set; }

        public CustomerDTO? CustomerCreateDTO { get; set; }
    }
}

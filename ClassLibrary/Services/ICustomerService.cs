using ClassLibrary.Data;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassLibrary.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAllCustomers(string q);
        List<SelectListItem> FillCountryDropDown();
        void CreateCustomer(CustomerDTO customerDTO);
    }

    
}


using ClassLibrary.Services;
using ClassLibrary.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenta.Controllers;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Tenta.Models.Customer;

namespace MVCControllerEnd.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        private Mock<ICustomerService> _customerServiceMock;
        private ApplicationDbContext _dbContext;
        private CustomerController _sut;

        [TestInitialize]
        public void Setup()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _dbContext = new ApplicationDbContext();
            _sut = new CustomerController(_customerServiceMock.Object, _dbContext);
        }

        // Customers -  Customers - Customers - Customers - Customers -
        // Customers -  Customers - Customers - Customers - Customers -
        // Customers -  Customers - Customers - Customers - Customers -
        // Customers -  Customers - Customers - Customers - Customers -
        [TestMethod]
        public void Customers_Does_Not_Return_Null()
        {
            // Arrange
            var q = "searchString";

            // Act
            var result = _sut.Customers(q) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Customers_Returns_ModelType_CustomersVM()
        {
            // Arrange
            var q = "searchString";

            // Act
            var result = _sut.Customers(q) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(CustomersVM));
        }

        [TestMethod]
        public void Customers_Returns_List_Of_CustomerDTO()
        {
            // Arrange
            var q = "searchString";
            var customers = new List<CustomerDTO> 
            { 
                new CustomerDTO { Name = "Test Customer" } 
            };
            _customerServiceMock.Setup(x => x.GetAllCustomers(q)).Returns(customers);

            // Act
            var result = _sut.Customers(q) as ViewResult;

            // Assert
            var model = result.ViewData.Model as CustomersVM;
            Assert.AreEqual(model.Customers, customers);
        }

        [TestMethod]
        public void Customers_Returns_List_Of_Countries()
        {
            // Arrange
            var q = "searchString";
            var countries = new List<SelectListItem> 
            { 
                new SelectListItem 
                { 
                    Text = "Text Country", 
                    Value = "Value Country"  
                } 
            };
            _customerServiceMock.Setup(x => x.FillCountryDropDown()).Returns(countries);

            // Act
            var result = _sut.Customers(q) as ViewResult;

            // Assert
            var model = result.ViewData.Model as CustomersVM;
            Assert.AreEqual(model.Countries, countries);
        }

        // CustomersVM POST - CustomersVM POST - CustomersVM POST - CustomersVM POST -  
        // CustomersVM POST - CustomersVM POST - CustomersVM POST - CustomersVM POST -  
        // CustomersVM POST - CustomersVM POST - CustomersVM POST - CustomersVM POST -  
        // CustomersVM POST - CustomersVM POST - CustomersVM POST - CustomersVM POST -  

        [TestMethod]
        public void Customers_Post_ValidData_RedirectsToCustomersAction()
        {
            // Arrange
            var customersVM = new CustomersVM
            {
                CustomerCreateDTO = new CustomerDTO
                {
                    Name = "CustomerDTO Test",
                    CountryLabel = "Sweden",
                    Age = 30,
                    Birthday= DateTime.Now,
                }
            };

            // Act
            var result = _sut.Customers(customersVM) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Customers", result.ActionName);
            Assert.AreEqual("Customer", result.ControllerName);
        }
    }
}

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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Castle.Core.Resource;

namespace MVCControllerEnd.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        private Mock<ICustomerService> _customerServiceMock;
        private ICustomerService _customerService;
        private ApplicationDbContext _dbContext;
        private CustomerController _sut;

        [TestInitialize]
        public void Setup()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            
            _dbContext = new ApplicationDbContext();
            _customerService = new CustomerService(_dbContext);
            _sut = new CustomerController(_customerServiceMock.Object, _dbContext);

            // Set up TempData - Stack Overflow
            var tempDataProvider = new Mock<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider.Object);
            var tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            _sut.TempData = tempData;

            // Set up TempData - Chat-gpt
            //var mockTempData = new Mock<ITempDataDictionary>();
            //_sut.TempData = mockTempData.Object;
        }

        // READ -  READ - READ - READ - READ - READ - READ - READ - READ - READ -
        // READ -  READ - READ - READ - READ - READ - READ - READ - READ - READ -
        // READ -  READ - READ - READ - READ - READ - READ - READ - READ - READ -
        // READ -  READ - READ - READ - READ - READ - READ - READ - READ - READ -
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

        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE -
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE -
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE -
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE -

        [TestMethod]
        public void Customers_Post_Does_Not_Return_Null()
        {
            // Arrange
            var customersVM = new CustomersVM();

            // Act
            var result = _sut.Customers(customersVM) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Customers_Post_Returns_Action_Customers()
        {
            // Arrange
            var customersVM = new CustomersVM();

            // Act
            var result = _sut.Customers(customersVM) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Customers", result.ActionName);
        }

        [TestMethod]
        public void Customers_Post_Returns_Controller_Customer()
        {
            // Arrange
            var customersVM = new CustomersVM();

            // Act
            var result = _sut.Customers(customersVM) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Customer", result.ControllerName);
        }

        // READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS -
        // READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS -
        // READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS -
        // READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS - READ ALL RICHARDS -

        [TestMethod]
        public void Customer_get_All_Richards_Returns_Correct_List()
        {
            // Arrange
            var allCustomers = new List<CustomerDTO>()
            {
                new CustomerDTO{Name = "Linda"},
                new CustomerDTO{Name = "Alicia"},
                new CustomerDTO{Name = "Richard"}, // should be returned
                new CustomerDTO{Name = "Lucas"},
                new CustomerDTO{Name = "Richard Chalk"}, // should be returned
            };

            var expected = new List<CustomerDTO>()
            {
                new CustomerDTO{Name = "Richard"}, // should be returned
                new CustomerDTO{Name = "Richard Chalk"}, // should be returned
            };

            // Act
            var result =_customerService.GetAllRichards(allCustomers).ToList();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}

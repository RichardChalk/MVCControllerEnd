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
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Data.Sqlite;

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
            // Fixing dbContext
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseSqlite(connection)
              .Options;

            var _dbContext = new ApplicationDbContext(contextOptions);
            _dbContext.Database.EnsureCreated();
            _dbContext.Countries.Add(new Country { Id = 1, CountryLabel = "Sweden" });
            _dbContext.Customers.Add(new Customer { Id = 1, Name = "Richard", CountryId = 1 });
            _dbContext.Customers.Add(new Customer { Id = 2, Name = "Linda", CountryId = 1 });
            _dbContext.Customers.Add(new Customer { Id = 3, Name = "Alicia", CountryId = 1 });
            _dbContext.SaveChanges();

            // Set up Dependencies
            _customerServiceMock = new Mock<ICustomerService>();
            _sut = new CustomerController(_customerServiceMock.Object, _dbContext);

            // Set up TempData - Chat-gpt
            var mockTempData = new Mock<ITempDataDictionary>();
            _sut.TempData = mockTempData.Object;

            // Set up TempData - Stack Overflow
            //var tempDataProvider = new Mock<ITempDataProvider>();
            //var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider.Object);
            //var tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            //_sut.TempData = tempData;
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

        [TestMethod]
        public void Edit_Get_Returns_Not_Null()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.Edit(customerId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_Get_Returns_ModelType_CustomerVM()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.Edit(customerId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(CustomerVM));
        }

        [TestMethod]
        public void Edit_Get_Returns_List_Of_Countries()
        {
            // Arrange
            var customerId = 1;
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
            var result = _sut.Edit(customerId) as ViewResult;

            // Assert
            var model = result.ViewData.Model as CustomerVM;
            Assert.AreEqual(model.Countries, countries);
        }

        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST -
        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST -
        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST -
        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST -

        [TestMethod]
        public void Edit_Post_Does_Not_Return_Null()
        {
            // Arrange
            var customerVM = new CustomerVM()
            {
                Id = 1,
                Name = "Richard",
                CountryLabel = "Sweden",
                Countries = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Text Country",
                    Value = "Value Country"
                }
            },
                Age = 52,
                Birthday = DateTime.Now,
            };

            var customerId = 1;

            // Act
            var result = _sut.Edit(customerId, customerVM) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_Post_Returns_Action_Customers()
        {
            // Arrange
            var customerVM = new CustomerVM()
            {
                Id = 1,
                Name = "Richard",
                CountryLabel = "Sweden",
                Countries = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Text Country",
                    Value = "Value Country"
                }
            },
                Age = 52,
                Birthday = DateTime.Now,
            };

            var customerId = 1;

            // Act
            var result = _sut.Edit(customerId, customerVM) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Customers", result.ActionName);
        }

        [TestMethod]
        public void Edit_Post_Returns_Controller_Customer()
        {
            // Arrange
            var customerVM = new CustomerVM()
            {
                Id = 1,
                Name = "Richard",
                CountryLabel = "Sweden",
                Countries = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Text Country",
                    Value = "Value Country"
                }
            },
                Age = 52,
                Birthday = DateTime.Now,
            };

            var customerId = 1;

            // Act
            var result = _sut.Edit(customerId, customerVM) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Customer", result.ControllerName);
        }

        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -

        [TestMethod]
        public void Details_Does_Not_Return_Null()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.Details(customerId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details_Returns_ModelType_CustomerVM()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.Details(customerId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(CustomerVM));
        }

        // DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET -
        // DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET -
        // DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET -
        // DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET - DELETE GET -

        [TestMethod]
        public void Delete_Get_Returns_Not_Null()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.Delete(customerId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Get_Returns_ModelType_CustomerVM()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.Delete(customerId) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(CustomerVM));
        }

        // DELETE POST - DELETE POST - DELETE POST - DELETE POST - DELETE POST -
        // DELETE POST - DELETE POST - DELETE POST - DELETE POST - DELETE POST -
        // DELETE POST - DELETE POST - DELETE POST - DELETE POST - DELETE POST -
        // DELETE POST - DELETE POST - DELETE POST - DELETE POST - DELETE POST -

        [TestMethod]
        public void Delete_Post_Does_Not_Return_Null()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.DeleteCustomer(customerId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Post_Returns_Action_Customers()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.DeleteCustomer(customerId) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Customers", result.ActionName);
        }

        [TestMethod]
        public void Delete_Post_Returns_Controller_Customer()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = _sut.DeleteCustomer(customerId) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Customer", result.ControllerName);
        }
    }
}

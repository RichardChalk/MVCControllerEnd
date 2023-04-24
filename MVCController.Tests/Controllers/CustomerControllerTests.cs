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
        private Mock<ICustomerService> _customerService;
        private ApplicationDbContext _dbContext;
        private CustomerController _sut;

        [TestInitialize]
        public void Setup()
        {
            _customerService = new Mock<ICustomerService>();
            _dbContext = new ApplicationDbContext();
            _sut = new CustomerController(_customerService.Object, _dbContext);
        }

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
    }
}

using ClassLibrary.Data;
using ClassLibrary.DTOs;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tenta.Models.Customer;

namespace Tenta.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationDbContext _context;

        public CustomerController(ICustomerService customerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        public IActionResult Customers(string q)
        
        {
            var customersVM = new CustomersVM();
            customersVM.Customers = _customerService.GetAllCustomers(q);
            customersVM.Countries = _customerService.FillCountryDropDown();

            return View(customersVM);
        }

        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Customers(CustomersVM customersVM)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customersVM.CustomerCreateDTO);

                // Used for Toastr notifications
                TempData["success"] = "Customer created successfully";

                return RedirectToAction("Customers", "Customer");
            }

            customersVM.Customers = _customerService.GetAllCustomers(customersVM.q);
            customersVM.Countries = _customerService.FillCountryDropDown();

            return View(customersVM);
        }

        // EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - 
        // EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - 
        // EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - 
        // EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - EDIT GET - 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customerVM = new CustomerVM();
            customerVM.Countries = _customerService.FillCountryDropDown();
            var customerDB = _context.Customers
                .Include(c => c.Country)
                .First(c => c.Id == id);

            customerVM.Name = customerDB.Name;
            customerVM.CountryLabel = customerDB.Country.CountryLabel;
            customerVM.Age = customerDB.Age;
            customerVM.Birthday = customerDB.Birthday;

            return View(customerVM);
        }

        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - 
        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - 
        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - 
        // EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - EDIT POST - 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                //Hämta objekt från db
                var customerDB = _context.Customers.First(c => c.Id == id);
                customerDB.Name = customerVM.Name;
                customerDB.Country = _context.Countries
                    .Where(c => c.CountryLabel == customerVM.CountryLabel)
                    .First();
                customerDB.Age = customerVM.Age;
                customerDB.Birthday = customerVM.Birthday;

                _context.SaveChanges();

                // Used for Toastr notifications
                TempData["success"] = "Customer edited successfully";

                //Redirect
                return RedirectToAction("Customers", "Customer");
            }

            return View(customerVM);
        }

        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        // DETAILS -  DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS - DETAILS -
        [HttpGet]
        public IActionResult Details(int id)
        {
            var customerVM = new CustomerVM();
            var customerDB = _context.Customers
                .Include(c => c.Country)
                .First(c => c.Id == id);

            customerVM.Name = customerDB.Name;
            customerVM.CountryLabel = customerDB.Country.CountryLabel;
            customerVM.Age = customerDB.Age;
            customerVM.Birthday = customerDB.Birthday;

            return View(customerVM);
        }

        // DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE
        // DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE
        // DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE
        // DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE DELETE
        // GET
        public IActionResult Delete(int id)
        {
            var customerDB = _context.Customers
                .Include(_c => _c.Country)
                .FirstOrDefault(u => u.Id == id);
            var customerVM = new CustomerVM();

            if (customerDB == null)
            {
                return NotFound();
            }

            customerVM.Id = id;
            customerVM.Name = customerDB.Name;
            customerVM.CountryLabel= customerDB.Country.CountryLabel;
            customerVM.Age= customerDB.Age;
            customerVM.Birthday= customerDB.Birthday;

            return View(customerVM);

            if (customerDB == null)
            {
                return NotFound();
            }
        }


        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE -
        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE -
        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE -
        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE -
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomer(int id)
        {
            var customerDB = _context.Customers.Find(id);
            if (customerDB == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerDB);
            _context.SaveChanges();

            // Used for Toastr notifications
            TempData["success"] = "Customer deleted successfully";

            return RedirectToAction("Customers", "Customer");
        }
    }
}

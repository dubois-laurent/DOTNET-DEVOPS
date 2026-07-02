using aspnet.Data;
using aspnet.Models;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICarCustomerRepository _repository;

        public CustomerController(ILogger<CustomerController> logger, ICarCustomerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var customers = _repository.Customers;
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarCustomerCreateVM customer)
        {
            if (!ModelState.IsValid)
                return View(customer);
            _repository.Add(new CarCustomer() { Name = customer.Name, Email = customer.Email, PhoneNumber = customer.PhoneNumber });
            _logger.Log(LogLevel.Debug, customer.Name + " created");
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var customer = _repository.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(new CarCustomerUpdateVM { Id = customer.Id, Name = customer.Name, Email = customer.Email, PhoneNumber = customer.PhoneNumber });
        }

        [HttpPost]
        public IActionResult Update(CarCustomerUpdateVM customer)
        {
            if (!ModelState.IsValid)
                return View(customer);
            _repository.Update(new CarCustomer() { Id = customer.Id, Name = customer.Name, Email = customer.Email, PhoneNumber = customer.PhoneNumber });
            _logger.Log(LogLevel.Debug, customer.Name + " updated");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var customer = _repository.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(new CarCustomerDeleteVM { Id = customer.Id });
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _repository.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            _repository.Delete(customer);
            _logger.Log(LogLevel.Debug, customer.Name + " deleted");
            return RedirectToAction("Index");
        }
    }
}

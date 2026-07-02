using aspnet.Interfaces;
using aspnet.Models;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarRepository _repository;

        public CarController(ILogger<CarController> logger, ICarRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var cars = _repository.Cars;
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarCreateVM car)
        {
            if (!ModelState.IsValid)
                return View(car);
            _repository.Add(new Car() { CarModelId = car.CarModelId, PlateNumber = car.PlateNumber });
            _logger.Log(LogLevel.Debug, car.PlateNumber + " created");
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var car = _repository.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(new CarUpdateVM { Id = car.Id, CarModelId = car.CarModelId, PlateNumber = car.PlateNumber });
        }

        [HttpPost]
        public IActionResult Update(CarUpdateVM car)
        {
            if (!ModelState.IsValid)
                return View(car);
            _repository.Update(new Car() { Id = car.Id, CarModelId = car.CarModelId, PlateNumber = car.PlateNumber });
            _logger.Log(LogLevel.Debug, car.PlateNumber + " updated");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var car = _repository.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(new CarDeleteVM { Id = car.Id });
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _repository.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            _repository.Delete(car);
            _logger.Log(LogLevel.Debug, car.PlateNumber + " deleted");
            return RedirectToAction("Index");
        }
    }
}

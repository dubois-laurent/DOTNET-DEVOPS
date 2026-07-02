using aspnet.Data;
using aspnet.Models;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    public class ModelController : Controller
    {
        private readonly ILogger<ModelController> _logger;
        private readonly ICarModelRepository _repository;

        public ModelController(ILogger<ModelController> logger, ICarModelRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var models = _repository.Models;
            return View(models);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarModelCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _repository.Add(new CarModel() { Name = model.Name, DailyPrice = model.DailyPrice, NbSeats = model.NbSeats, FuelType = model.FuelType, CarBrandId = model.CarBrandId });
            _logger.Log(LogLevel.Debug, model.Name + " created");
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var model = _repository.Models.FirstOrDefault(m => m.Id == id);
            if (model == null) return NotFound();
            return View(new CarModelUpdateVM { Id = model.Id, Name = model.Name, DailyPrice = model.DailyPrice, NbSeats = model.NbSeats, FuelType = model.FuelType, CarBrandId = model.CarBrandId });
        }

        [HttpPost]
        public IActionResult Update(CarModelUpdateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _repository.Update(new CarModel() { Id = model.Id, Name = model.Name, DailyPrice = model.DailyPrice, NbSeats = model.NbSeats, FuelType = model.FuelType, CarBrandId = model.CarBrandId });
            _logger.Log(LogLevel.Debug, model.Name + " updated");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var model = _repository.Models.FirstOrDefault(m => m.Id == id);
            if (model == null) return NotFound();
            return View(new CarModelDeleteVM { Id = model.Id });
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var model = _repository.Models.FirstOrDefault(m => m.Id == id);
            if (model == null) return NotFound();
            _repository.Delete(model);
            _logger.Log(LogLevel.Debug, model.Name + " deleted");
            return RedirectToAction("Index");
        }
    }
}

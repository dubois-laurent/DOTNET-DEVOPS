using aspnet.Data;
using aspnet.Models;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    public class BrandController : Controller
    {
        private readonly ILogger<BrandController> _logger;
        private readonly ICarBrandRepository _repository;

        public BrandController(ILogger<BrandController> logger, ICarBrandRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var brands = _repository.Brands;
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarBrandCreateVM brand)
        {
            if (!ModelState.IsValid)
                return View(brand);
            _repository.Add(new CarBrand() { Name = brand.Name });
            _logger.Log(LogLevel.Debug, brand.Name + " created");
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var brand = _repository.Brands.FirstOrDefault(b => b.Id == id);
            if (brand == null) return NotFound();
            return View(new CarBrandUpdateVM { Id = brand.Id, Name = brand.Name });
        }

        [HttpPost]
        public IActionResult Update(CarBrandUpdateVM brand)
        {
            if (!ModelState.IsValid)
                return View(brand);
            _repository.Update(new CarBrand() { Id = brand.Id, Name = brand.Name });
            _logger.Log(LogLevel.Debug, brand.Name + " updated");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var brand = _repository.Brands.FirstOrDefault(b => b.Id == id);
            if (brand == null) return NotFound();
            return View(new CarBrandDeleteVM { Id = brand.Id });
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var brand = _repository.Brands.FirstOrDefault(b => b.Id == id);
            if (brand == null) return NotFound();
            _repository.Delete(brand);
            _logger.Log(LogLevel.Debug, brand.Name + " deleted");
            return RedirectToAction("Index");
        }
    }
}

using aspnet.Data;
using aspnet.Models;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly ICarReservationRepository _repository;

        public ReservationController(ILogger<ReservationController> logger, ICarReservationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var reservations = _repository.Reservations;
            return View(reservations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarReservationCreateVM reservation)
        {
            if (!ModelState.IsValid)
                return View(reservation);
            _repository.Add(new CarReservation() { CarId = reservation.CarId, CarCustomerId = reservation.CustomerId, StartDate = reservation.StartDate, EndDate = reservation.EndDate });
            _logger.Log(LogLevel.Debug, reservation.CarId + " created");
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var reservation = _repository.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();
            return View(new CarReservationUpdateVM { Id = reservation.Id, CarId = reservation.CarId, CustomerId = reservation.CarCustomerId, StartDate = reservation.StartDate, EndDate = reservation.EndDate });
        }

        [HttpPost]
        public IActionResult Update(CarReservationUpdateVM reservation)
        {
            if (!ModelState.IsValid)
                return View(reservation);
            _repository.Update(new CarReservation() { Id = reservation.Id, CarId = reservation.CarId, CarCustomerId = reservation.CustomerId, StartDate = reservation.StartDate, EndDate = reservation.EndDate });
            _logger.Log(LogLevel.Debug, reservation.CarId + " updated");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var reservation = _repository.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();
            return View(new CarReservationDeleteVM { Id = reservation.Id });
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var reservation = _repository.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();
            _repository.Delete(reservation);
            _logger.Log(LogLevel.Debug, reservation.CarId + " deleted");
            return RedirectToAction("Index");
        }
    }
}

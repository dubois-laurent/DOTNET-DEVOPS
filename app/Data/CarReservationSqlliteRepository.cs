using aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnet.Data
{
    public class CarReservationSqlliteRepository : ICarReservationRepository
    {
        private readonly AppDbContext _context;

        public CarReservationSqlliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarReservation> Reservations => _context.CarReservations
            .Include(r => r.Car)
                .ThenInclude(c => c.CarModel)
                    .ThenInclude(m => m.CarBrand)
            .Include(r => r.CarCustomer);

        public bool Add(CarReservation reservation)
        {
            _context.CarReservations.Add(reservation);
            _context.SaveChanges();
            return true;
        }

        public bool Update(CarReservation reservation)
        {
            var existing = _context.CarReservations.FirstOrDefault(r => r.Id == reservation.Id);
            if (existing == null) return false;
            existing.StartDate = reservation.StartDate;
            existing.EndDate = reservation.EndDate;
            existing.CarId = reservation.CarId;
            existing.CarCustomerId = reservation.CarCustomerId;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(CarReservation reservation)
        {
            var existing = _context.CarReservations.FirstOrDefault(r => r.Id == reservation.Id);
            if (existing == null) return false;
            _context.CarReservations.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public List<string> GetAllReservations()
        {
            return _context.CarReservations.Select(r => r.Id.ToString()).ToList();
        }
    }
}

using aspnet.Interfaces;
using aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnet.Data
{
    public class CarSqlliteRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarSqlliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> Cars => _context.Cars
            .Include(c => c.CarModel)
                .ThenInclude(m => m.CarBrand);

        public bool Add(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Car car)
        {
            var existing = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
            if (existing == null) return false;
            existing.PlateNumber = car.PlateNumber;
            existing.CarModelId = car.CarModelId;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Car car)
        {
            var existing = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
            if (existing == null) return false;
            _context.Cars.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }
    }
}

using aspnet.Models;

namespace aspnet.Data
{
    public class CarModelSqlliteRepository : ICarModelRepository
    {
        private readonly AppDbContext _context;

        public CarModelSqlliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarModel> Models => _context.CarModels;

        public bool Add(CarModel model)
        {
            _context.CarModels.Add(model);
            _context.SaveChanges();
            return true;
        }

        public bool Update(CarModel model)
        {
            var existing = _context.CarModels.FirstOrDefault(m => m.Id == model.Id);
            if (existing == null) return false;
            existing.Name = model.Name;
            existing.DailyPrice = model.DailyPrice;
            existing.NbSeats = model.NbSeats;
            existing.FuelType = model.FuelType;
            existing.CarBrandId = model.CarBrandId;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(CarModel model)
        {
            var existing = _context.CarModels.FirstOrDefault(m => m.Id == model.Id);
            if (existing == null) return false;
            _context.CarModels.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public List<string> GetAllModels()
        {
            return _context.CarModels.Select(m => m.Name).ToList();
        }
    }
}

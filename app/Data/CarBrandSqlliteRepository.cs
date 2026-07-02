using aspnet.Models;

namespace aspnet.Data
{
    public class CarBrandSqlliteRepository : ICarBrandRepository
    {
        private readonly AppDbContext _context;

        public CarBrandSqlliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarBrand> Brands => _context.CarBrands;

        public bool Add(CarBrand brand)
        {
            _context.CarBrands.Add(brand);
            _context.SaveChanges();
            return true;
        }

        public bool Update(CarBrand brand)
        {
            var existing = _context.CarBrands.FirstOrDefault(b => b.Id == brand.Id);
            if (existing == null) return false;
            existing.Name = brand.Name;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(CarBrand brand)
        {
            var existing = _context.CarBrands.FirstOrDefault(b => b.Id == brand.Id);
            if (existing == null) return false;
            _context.CarBrands.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public List<string> GetAllBrands()
        {
            return _context.CarBrands.Select(b => b.Name).ToList();
        }
    }
}

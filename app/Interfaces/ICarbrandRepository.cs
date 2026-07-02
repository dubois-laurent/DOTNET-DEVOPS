using aspnet.Models;

namespace aspnet.Data
{
    public interface ICarBrandRepository
    {
        public IEnumerable<CarBrand> Brands { get; }
        public bool Add(CarBrand brand);
        public bool Update(CarBrand brand);
        public bool Delete(CarBrand brand);
        public List<string> GetAllBrands();
    }
}
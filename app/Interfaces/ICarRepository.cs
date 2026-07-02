using aspnet.Models;

namespace aspnet.Interfaces
{
    public interface ICarRepository
    {
        public IEnumerable<Car> Cars { get; }
        public bool Add(Car car);
        public bool Update(Car car);
        public bool Delete(Car car);
        public List<Car> GetAllCars();
    }
}
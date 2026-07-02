using aspnet.Models;

namespace aspnet.Data
{
    public interface ICarModelRepository
    {
        public IEnumerable<CarModel> Models { get; }
        public bool Add(CarModel model);
        public bool Update(CarModel model);
        public bool Delete(CarModel model);
        public List<string> GetAllModels();
    }
}
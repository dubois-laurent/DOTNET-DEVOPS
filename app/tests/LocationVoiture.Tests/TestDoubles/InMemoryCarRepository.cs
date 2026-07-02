using aspnet.Interfaces;
using aspnet.Models;

namespace LocationVoiture.Tests.TestDoubles;

internal sealed class InMemoryCarRepository : ICarRepository
{
    private readonly List<Car> _cars;

    public InMemoryCarRepository(IEnumerable<Car>? cars = null)
    {
        _cars = cars?.ToList() ?? new List<Car>();
    }

    public IEnumerable<Car> Cars => _cars;

    public bool Add(Car car)
    {
        _cars.Add(car);
        return true;
    }

    public bool Update(Car car)
    {
        var existing = _cars.FirstOrDefault(existingCar => existingCar.Id == car.Id);
        if (existing is null)
        {
            return false;
        }

        existing.PlateNumber = car.PlateNumber;
        existing.CarModelId = car.CarModelId;
        existing.CarModel = car.CarModel;
        return true;
    }

    public bool Delete(Car car)
    {
        return _cars.RemoveAll(existingCar => existingCar.Id == car.Id) > 0;
    }

    public List<Car> GetAllCars() => _cars.ToList();
}
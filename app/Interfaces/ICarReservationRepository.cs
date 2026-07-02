using aspnet.Models;

namespace aspnet.Data
{
    public interface ICarReservationRepository
    {
        public IEnumerable<CarReservation> Reservations { get; }
        public bool Add(CarReservation reservation);
        public bool Update(CarReservation reservation);
        public bool Delete(CarReservation reservation);
        public List<string> GetAllReservations();
    }
}
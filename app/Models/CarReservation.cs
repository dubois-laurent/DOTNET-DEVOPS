namespace aspnet.Models
{
    public class CarReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
        public int CarCustomerId { get; set; }
        public CarCustomer CarCustomer { get; set; } = null!;
    }
}
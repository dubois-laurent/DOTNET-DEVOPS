namespace aspnet.Models
{
    public class CarCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<Car> Cars { get; set; } = new();
    }
}
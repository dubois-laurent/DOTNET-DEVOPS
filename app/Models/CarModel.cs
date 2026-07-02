namespace aspnet.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CarBrandId { get; set; }
        public decimal DailyPrice { get; set; }
        public int NbSeats { get; set; }
        public string? FuelType { get; set; }
        public CarBrand CarBrand { get; set; } = null!;
        public List<Car> Cars { get; set; } = new();
    }
}
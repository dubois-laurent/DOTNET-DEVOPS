namespace aspnet.Models
{
    public class CarBrand
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<CarModel> CarModels { get; set; } = new();
    }
}
namespace aspnet.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Brand
        {
            get
            {
                return CarModel.CarBrand.Name;
            }
        }
        public string Model
        {
            get
            {
                return CarModel.Name;
            }
        }
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; } = null!;
    }
}
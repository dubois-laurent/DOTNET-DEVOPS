using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    public class CarModelUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du modèle est obligatoire.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La marque est obligatoire.")]
        public int CarBrandId { get; set; }

        [Required(ErrorMessage = "Le prix journalier est obligatoire.")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix journalier doit être supérieur ou égal à 0.")]
        public decimal DailyPrice { get; set; }

        public int NbSeats { get; set; }

        public string? FuelType { get; set; }
    }
}

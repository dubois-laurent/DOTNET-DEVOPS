using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    public class CarCreateVM
    {
        [Required(ErrorMessage = "La plaque d'immatriculation est obligatoire.")]
        public string PlateNumber { get; set; } = null!;

        [Required(ErrorMessage = "Le modèle est obligatoire.")]
        public int CarModelId { get; set; }
    }
}
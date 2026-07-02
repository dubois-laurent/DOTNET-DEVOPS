using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    public class CarBrandCreateVM
    {
        [Required(ErrorMessage = "Le nom de la marque est obligatoire.")]
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    public class CarBrandUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la marque est obligatoire.")]
        public string Name { get; set; } = null!;
    }
}

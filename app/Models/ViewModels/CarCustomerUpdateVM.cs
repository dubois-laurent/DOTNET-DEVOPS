using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    public class CarCustomerUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string Name { get; set; } = null!;

        [EmailAddress(ErrorMessage = "L'adresse e-mail n'est pas valide.")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public string PhoneNumber { get; set; } = null!;
    }
}

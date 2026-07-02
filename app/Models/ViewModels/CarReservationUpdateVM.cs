using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    public class CarReservationUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La voiture est obligatoire.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Le client est obligatoire.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La date de fin est obligatoire.")]
        public DateTime EndDate { get; set; }
    }
}

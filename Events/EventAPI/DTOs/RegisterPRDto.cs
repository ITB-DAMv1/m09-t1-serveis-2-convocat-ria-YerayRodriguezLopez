using System.ComponentModel.DataAnnotations;

namespace EventAPI.DTOs
{
    public class RegisterPRDto
    {
        [Required(ErrorMessage = "El nom és obligatori")]
        [StringLength(100, ErrorMessage = "El nom no pot superar els 100 caràcters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Els cognoms són obligatoris")]
        [StringLength(100, ErrorMessage = "Els cognoms no poden superar els 100 caràcters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "L'email és obligatori")]
        [EmailAddress(ErrorMessage = "Format d'email invàlid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contrasenya és obligatòria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contrasenya ha de tenir entre 6 i 100 caràcters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmació de contrasenya és obligatòria")]
        [Compare("Password", ErrorMessage = "Les contrasenyes no coincideixen")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

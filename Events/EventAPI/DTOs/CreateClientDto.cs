using System.ComponentModel.DataAnnotations;

namespace EventAPI.DTOs
{
    public class CreateClientDto
    {
        [Required(ErrorMessage = "El nom de l'empresa és obligatori")]
        [StringLength(200, ErrorMessage = "El nom de l'empresa no pot superar els 200 caràcters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "El nom del CEO és obligatori")]
        [StringLength(100, ErrorMessage = "El nom no pot superar els 100 caràcters")]
        public string CeoFirstName { get; set; }

        [Required(ErrorMessage = "Els cognoms del CEO són obligatoris")]
        [StringLength(100, ErrorMessage = "Els cognoms no poden superar els 100 caràcters")]
        public string CeoLastName { get; set; }

        [Required(ErrorMessage = "L'email és obligatori")]
        [EmailAddress(ErrorMessage = "Format d'email invàlid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre d'assistents és obligatori")]
        [Range(1, 1000, ErrorMessage = "El nombre d'assistents ha d'estar entre 1 i 1000")]
        public int AttendeeCount { get; set; }

        public bool IsVip { get; set; }

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

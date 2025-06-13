using System.ComponentModel.DataAnnotations;

namespace EventClient.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "L'email és obligatori")]
        [EmailAddress(ErrorMessage = "Format d'email invàlid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasenya és obligatòria")]
        public string Password { get; set; } = string.Empty;
    }
}

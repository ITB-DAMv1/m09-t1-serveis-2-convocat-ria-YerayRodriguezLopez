using System.ComponentModel.DataAnnotations;

namespace EventAPI.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "L'email és obligatori")]
        [EmailAddress(ErrorMessage = "Format d'email invàlid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contrasenya és obligatòria")]
        [DataType(DataType.Password)]
        public string Password
        {
            get; set;
        }
    }
}

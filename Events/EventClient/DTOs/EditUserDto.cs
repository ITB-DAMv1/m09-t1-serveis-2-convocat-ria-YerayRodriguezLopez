using System.ComponentModel.DataAnnotations;

namespace EventClient.DTOs
{
    public class EditUserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nom de l'empresa és obligatori")]
        [StringLength(100, ErrorMessage = "El nom de l'empresa no pot superar els 100 caràcters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nom d'usuari és obligatori")]
        [StringLength(50, ErrorMessage = "El nom d'usuari no pot superar els 50 caràcters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email és obligatori")]
        [EmailAddress(ErrorMessage = "Format d'email invàlid")]
        public string Email { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "El nombre de persones ha d'estar entre 1 i 100")]
        public int NumberOfPeople { get; set; }

        public bool IsVip
        {
            get; set;
        }
    }
}

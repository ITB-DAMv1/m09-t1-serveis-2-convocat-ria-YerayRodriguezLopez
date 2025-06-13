using System.ComponentModel.DataAnnotations;

namespace EventAPI.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }

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
        public DateTime RegisterDate { get; set; }
    }
}

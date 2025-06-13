using System.ComponentModel.DataAnnotations;

namespace EventAPI.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string CeoFirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string CeoLastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(1, 1000)]
        public int AttendeeCount { get; set; }

        public bool IsVip { get; set; }

        public DateTime RegisterDate { get; set; }

        // Navigation property
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

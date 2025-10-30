using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Optional role/level for admin distinction
        public string Role { get; set; }
    }
}

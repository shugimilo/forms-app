using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class UserCreateViewModel
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        public string Role { get; set; } = "User";
    }
}
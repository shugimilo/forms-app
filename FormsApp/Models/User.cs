using System;
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models 
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "User"; // or "Admin"
    }
}

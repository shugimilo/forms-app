using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class FormEditViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool RequireLogin { get; set; }
    }
}
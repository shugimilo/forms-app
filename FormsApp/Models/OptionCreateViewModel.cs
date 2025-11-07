using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class OptionCreateViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Text { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}

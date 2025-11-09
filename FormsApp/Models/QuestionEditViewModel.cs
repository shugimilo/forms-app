using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class QuestionEditViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(512)]
        public string Text { get; set; }

        [Required]
        public string Type { get; set; }  // e.g. "ShortText", "LongText", "SingleChoice", "MultipleChoice", "Numeric", "Date", "Time"

        public bool IsRequired { get; set; } = false;

        // Optional: path or URL to an image attached to the question
        [StringLength(1024)]
        public string? ImagePath { get; set; }

        // For numeric type questions
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? Step { get; set; }

        // The form this question belongs to
        [Required]
        public int FormId { get; set; }
    }
}

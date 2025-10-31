using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required, StringLength(512)]
        public string Text { get; set; }

        [Required]
        public string Type { get; set; }  // e.g. "ShortText", "LongText", "SingleChoice", "MultipleChoice", "Numeric", "Date", "Time"

        public bool IsRequired { get; set; }

        // Optional: path or URL to an image attached to the question
        public string? ImagePath { get; set; }

        // For numeric type: range definition (optional)
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? Step { get; set; }

        // Relationship: which form it belongs to
        [Required]
        public int FormId { get; set; }
        public Form Form { get; set; }

        // For choice-based questions
        public ICollection<Option>? Options { get; set; }
    }
}
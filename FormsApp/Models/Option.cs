using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Option
    {
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Text { get; set; }

        // Optional image per option
        public string? ImagePath { get; set; }

        // Relationship
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
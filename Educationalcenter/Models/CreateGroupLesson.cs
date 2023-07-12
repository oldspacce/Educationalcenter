using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Educationalcenter.Models
{
    public class CreateGroupLesson
    {
        [Required]
        public int ClientAmount { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}

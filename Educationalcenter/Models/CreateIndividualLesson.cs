using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Educationalcenter.Models
{
    public class CreateIndividualLesson
    {
        [Required]
        public string TeacherLogin { get; set; } = null;
        [Required]
        public string ClientLogin { get; set; } = null;
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}

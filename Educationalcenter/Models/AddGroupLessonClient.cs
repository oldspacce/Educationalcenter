using System.ComponentModel.DataAnnotations;

namespace Educationalcenter.Models
{
    public class AddGroupLessonClient
    {
        [Required]
        public string TeacherLogin { get; set; }
        [Required]
        public string Subjectlesson { get; set; }
    }
}

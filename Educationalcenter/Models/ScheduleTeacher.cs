using System.Text.Json.Serialization;

namespace Educationalcenter.Models
{
    public class ScheduleTeacher
    {
        public Teacher teacher { get; set; }
        public IEnumerable<Scheduledate> Scheduledates { get; set; }

    }
}

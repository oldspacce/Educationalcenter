using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Educationalcenter
{
    public class TeacherRep : ControllerBase
    {
        public ActionResult AddScheduleLesson(EducationalcenterContext context,DateOnly date, TimeOnly starttime, TimeOnly endtime)
        {
            try
            {
                Schedulelesson schedulelesson = new Schedulelesson();
                schedulelesson.Schedulelessonid = Guid.NewGuid();
                schedulelesson.Date = date;
                schedulelesson.Starttime = starttime;
                schedulelesson.Endtime = endtime;
                context.Add(schedulelesson);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

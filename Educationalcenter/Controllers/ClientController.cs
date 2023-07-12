using Educationalcenter.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace Educationalcenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "client, admin")]
    public class ClientController : ControllerBase
    {
        private readonly EducationalcenterContext _context;

        public ClientController(EducationalcenterContext context)
        {
            _context = context;
        }
        [HttpPost("AddingToGroupLesson")]
        public ActionResult AddingToGroupLesson(AddGroupLessonClient lessonClient)
        {
            try
            {
                if(!_context.Grouplessons.Any(item => item.Subject.Subjectname == lessonClient.Subjectlesson
                && item.Teacher.User.Login == lessonClient.TeacherLogin))
                {
                    return BadRequest("Not found group lesson");
                }
                Guid id = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

                Client client = _context.Clients.First(item => item.Userid == id);
                var grouplesson = _context.Grouplessons.First(item => item.Subject.Subjectname == lessonClient.Subjectlesson
                && item.Teacher.User.Login == lessonClient.TeacherLogin);

                if (_context.Grouplessonclients.Any(item => item.Clientid == client.Clientid
                && item.Grouplessonid == grouplesson.Grouplessonid))
                {
                    return BadRequest("you are already registered");
                }

                Grouplessonclient grouplessonclient = new Grouplessonclient();
                grouplessonclient.Grouplessonclientid = id;
                grouplessonclient.Clientid = client.Clientid;



                grouplessonclient.Grouplessonid = grouplesson.Grouplessonid;
                _context.Add(grouplessonclient);
                _context.SaveChanges();
                return Ok(grouplesson);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetScheduleIndividuallessonclient")]
        public ActionResult GetScheduleIndividuallesson()
        {
            try
            {
                Guid id = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

                Client client = _context.Clients.First(item => item.Userid == id);

                var schedule = _context.Individualeschedules.Where(item => item.Individuallesson.Clientid == client.Clientid)
                    .Include(item => item.Schedulelesson).OrderBy(i => i.Schedulelesson.Date);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetScheduleAllGroupLesson")]
        public ActionResult GetScheduleGrouplesson()
        {
            try
            {
                var schedule = _context.Groupschedules.Include(i => i.Grouplesson)
                    .Include(i => i.Grouplesson.Subject).Include(i => i.Schedulelesson);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetGrouplessonclientschedule")]
        public ActionResult GetGrouplesson()
        {
            try
            {
                Guid id = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

                Client client = _context.Clients.First(item => item.Userid == id);

                var schedule = _context.Grouplessonclients.Include(i => i.Grouplesson).Where(i => i.Clientid == client.Clientid)
                    .Include(i => i.Grouplesson.Groupschedules).ThenInclude(i => i.Schedulelesson).Include(i => i.Grouplesson.Subject);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetTeachersSchedule")]
        public ActionResult GetTeachersschedule()
        {
            try
            {
                var teacher = _context.Teachers;

                List<ScheduleTeacher> teachers = new List<ScheduleTeacher>();

                foreach (var item in teacher)
                {
                    var schedule = new ScheduleTeacher();
                    schedule.teacher = item;
                    schedule.Scheduledates = _context.Scheduledates
                        .Include(id => id.Schedule).Include(id => id.Dateschedule).Include(i => i.Dateschedule.Teacher).OrderBy(i => i.Dateschedule.Teacher.Surname);
                    teachers.Add(schedule);
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

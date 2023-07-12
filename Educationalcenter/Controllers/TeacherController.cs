using Educationalcenter.Models;
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
    [Authorize(Roles = "teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly EducationalcenterContext _context;

        public TeacherController(EducationalcenterContext context)
        {
            _context = context;
        }

        [HttpPost("AddIndividualLesson")]
        public ActionResult CreateIndividualLesson(CreateIndividualLesson lesson)
        {
            try
            {
                User teacherid = _context.Users.FirstOrDefault(item => item.Login == lesson.TeacherLogin && item.Role == "teacher");
                User clientid = _context.Users.FirstOrDefault(item => item.Login == lesson.ClientLogin && item.Role == "client");

                if (teacherid != null && clientid != null)
                {

                    Teacher teacher = _context.Teachers.First(item => item.Userid == teacherid.Userid);
                    Client client = _context.Clients.First(item => item.Userid == clientid.Userid);

                    var scheduleDate = _context.Scheduledates.FirstOrDefault(item => item.Dateschedule.Teacherid == teacher.Teacherid
                    && item.Dateschedule.Date == lesson.Date
                    && item.Schedule.Time == lesson.StartTime);

                    if(scheduleDate != null && scheduleDate.Busy)
                    {
                        return BadRequest("Time busy");
                    }
                    Individuallesson ilesson = new Individuallesson();
                    ilesson.Individuallessonid = Guid.NewGuid();
                    ilesson.Teacherid = teacher.Teacherid;
                    ilesson.Clientid = client.Clientid;
                    ilesson.Cost = lesson.Cost;
                    _context.Add(ilesson);
                    //_context.SaveChanges();
                    //Добавление даты и времени в расписание занятий
                    if (!_context.Schedulelessons.Any(item => item.Date == lesson.Date
                    && item.Starttime == lesson.StartTime
                    && item.Endtime == lesson.EndTime))
                    {
                        AddScheduleLesson(lesson.Date, lesson.StartTime, lesson.EndTime);
                    }
                    //Добавление связи между расписанием и занятием
                    Individualeschedule individualeschedule = new Individualeschedule();
                    individualeschedule.Individualschedule = Guid.NewGuid();
                    individualeschedule.Individuallessonid = ilesson.Individuallessonid;
                    individualeschedule.Schedulelesson = _context.Schedulelessons.First(item => item.Date == lesson.Date
                    && item.Starttime == lesson.StartTime
                    && item.Endtime == lesson.EndTime);
                    _context.Add(individualeschedule);
                    //Добавление занятости преподавателю
                    AddTeacherBusy(teacher, lesson.Date, lesson.StartTime, lesson.EndTime);
                    return Ok(lesson);
                }
                else {
                    return Ok("Not found teacher or client");
                }

            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost("AddGroupLesson")]
        public ActionResult CreateGroupLesson(CreateGroupLesson lesson)
        {

            Guid teacherid = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

            Teacher teacher = _context.Teachers.First(item => item.Userid == teacherid);
            try
            {
                var scheduleDate = _context.Scheduledates.FirstOrDefault(item => item.Dateschedule.Teacherid == teacher.Teacherid
                && item.Dateschedule.Date == lesson.Date
                && item.Schedule.Time == lesson.StartTime);

                if (scheduleDate != null && scheduleDate.Busy)
                {
                    return BadRequest("Time busy");
                }

                Grouplesson grouplesson = new Grouplesson();
                grouplesson.Grouplessonid = Guid.NewGuid();
                grouplesson.Teacherid = teacher.Teacherid;
                grouplesson.Cost = lesson.Cost;
                grouplesson.Clientamount = lesson.ClientAmount;
                Subject subject = _context.Subjects.First(item => item.Subjectname == lesson.Subject);
                grouplesson.Subjectid = subject.Subjectid;
                if (!_context.Schedulelessons.Any(item => item.Date == lesson.Date
                        && item.Starttime == lesson.StartTime
                        && item.Endtime == lesson.EndTime))
                {
                    AddScheduleLesson(lesson.Date, lesson.StartTime, lesson.EndTime);
                }
                Groupschedule groupschedule = new Groupschedule();
                groupschedule.Groupscheduleid = Guid.NewGuid();
                groupschedule.Schedulelesson = _context.Schedulelessons.First(item => item.Date == lesson.Date
                    && item.Starttime == lesson.StartTime
                    && item.Endtime == lesson.EndTime);
                groupschedule.Grouplessonid = grouplesson.Grouplessonid;
                _context.Add(grouplesson);
                _context.Add(groupschedule);
                AddTeacherBusy(teacher, lesson.Date, lesson.StartTime, lesson.EndTime);
                _context.SaveChanges();
                return Ok(lesson);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost("AddTeacherSubject")]
        public ActionResult AddTeacherSubject(string subject)
        {
            try
            {
                Guid teacherid = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

                Teacher teacher = _context.Teachers.First(item => item.Userid == teacherid);
                string[] subgects = subject.Split(' ');
                foreach (string s in subgects)
                {
                    Teachersubject teachersubject = new Teachersubject();
                    teachersubject.Teachersubjectid = Guid.NewGuid();
                    teachersubject.Teacherid = teacher.Teacherid;
                    var newSubject = _context.Subjects.First(item => item.Subjectname == s);
                    teachersubject.Subjectid = newSubject.Subjectid;
                    _context.Add(teachersubject);
                }
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetSchedule")]
        public ActionResult GetTeacherSchedule()
        {
            try
            {
                Guid id = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

                Teacher teacher = _context.Teachers.First(item => item.Userid == id);

                var schedule = new ScheduleTeacher();
                schedule.teacher = teacher;
                schedule.Scheduledates = _context.Scheduledates.Where(item => item.Dateschedule.Teacherid == teacher.Teacherid)
                    .Include(id => id.Schedule).Include(id => id.Dateschedule).OrderBy(i => i.Dateschedule.Date);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        private ActionResult AddScheduleLesson(DateOnly date, TimeOnly starttime, TimeOnly endtime)
        {
            try
            {
                Schedulelesson schedulelesson = new Schedulelesson();
                schedulelesson.Schedulelessonid = Guid.NewGuid();
                schedulelesson.Date = date;
                schedulelesson.Starttime = starttime;
                schedulelesson.Endtime = endtime;
                _context.Add(schedulelesson);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        private ActionResult AddDateTeacher(DateOnly date, Guid teacherid)
        {
            try
            {
                Dateschedule dateschedule = new Dateschedule();
                dateschedule.Datescheduleid = Guid.NewGuid();
                dateschedule.Date = date;
                dateschedule.Teacherid = teacherid;
                _context.Add(dateschedule);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        private ActionResult AddTimeSchedule(TimeOnly time)
        {
            try
            {
                Schedule schedule = new Schedule();
                schedule.Scheduleid = Guid.NewGuid();
                schedule.Time = time;
                _context.Add(schedule);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private ActionResult AddTeacherBusy(Teacher teacher, DateOnly date, TimeOnly starttime, TimeOnly endtime)
        {
            try
            {
                if (!_context.Dateschedules.Any(item => item.Teacherid == teacher.Teacherid
                && item.Date == date))
                {
                    AddDateTeacher(date, teacher.Teacherid);
                }
                if (!_context.Schedules.Any(item => item.Time == starttime))
                {
                    AddTimeSchedule(starttime);
                }

                Scheduledate scheduledate = new Scheduledate();
                scheduledate.Scheduledateid = Guid.NewGuid();

                Schedule schedule1 = _context.Schedules.First(item => item.Time == starttime);
                Dateschedule dateschedule1 = _context.Dateschedules.First(item => item.Teacherid == teacher.Teacherid
                && item.Date == date);

                scheduledate.Datescheduleid = dateschedule1.Datescheduleid;
                scheduledate.Scheduleid = schedule1.Scheduleid;
                scheduledate.Busy = true;
                _context.Add(scheduledate);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

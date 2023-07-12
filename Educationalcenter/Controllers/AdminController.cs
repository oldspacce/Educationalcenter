
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Educationalcenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly EducationalcenterContext _context;
        
        public AdminController(EducationalcenterContext context)
        {
            _context = context;
        }

        [HttpPost("AddSubject")]
        public ActionResult AddSubject(string subjectname)
        {
            try
            {
                Subject subject = new Subject();
                subject.Subjectid = Guid.NewGuid();
                subject.Subjectname = subjectname;
                _context.Add(subject);
                _context.SaveChanges();
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

using Educationalcenter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using Educationalcenter;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Educationalcenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly EducationalcenterContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, EducationalcenterContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public ActionResult CreateUser(UserRegistration user)
        {
            try
            {
                user.Userid = Guid.NewGuid();
                User user1 = new User();
                user1.Userid = user.Userid;
                user1.Role = user.Role;
                user1.Login = user.Login;
                UserRep.UserPassword(user1, user.Password);
                if (user.Role == "teacher")
                {
                    Teacher teacher = new Teacher();
                    teacher.Teacherid = Guid.NewGuid();
                    teacher.Surname = user.Surname;
                    teacher.Name = user.Name;
                    teacher.Patronymic = user.Patronymic;
                    teacher.Experience = user.Experience;
                    teacher.Phone = user.Phone;
                    teacher.Userid = user.Userid;
                    _context.Add(teacher);
                }
                else if (user.Role == "client")
                {
                    Client client = new Client();
                    client.Clientid = Guid.NewGuid();
                    client.Surname = user.Surname;
                    client.Name = user.Name;
                    client.Patronymic = user.Patronymic;
                    client.Phone = user.Phone;
                    client.Userid = user.Userid;
                    _context.Add(client);
                }
                _context.Add(user1);
                _context.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public ActionResult Login(UserLogin user)
        {
            if (!UserRep.IsExistUser(_context, user))
            {
                return BadRequest("Wrong email or password");
            }

            User finduser = _context.Users.First(item => item.Login == user.Login);

            Autentificate(user, finduser.Userid, finduser.Role);
            return Ok(finduser);
        }
        [HttpPost("LogoutUser")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();    
        }

        [Authorize]
        [HttpGet("info")]
        public ActionResult GetInfo()
        {
            Guid id = new Guid(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).Single());

            try
            {
                if (User.IsInRole("teacher"))
                {
                    Teacher teacher = _context.Teachers.First(item => item.Userid == id);
                    
                    return Ok(teacher);
                }
                else if (User.IsInRole("client"))
                {
                    Client client = _context.Clients.First(item => item.Userid == id);
                    return Ok(client);
                }
                return Ok();
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        private void Autentificate(UserLogin model, Guid guid, string role)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, model.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
            new Claim("Id", guid.ToString())};

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
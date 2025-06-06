﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitiApiBackend.DataAccess;
using UniversitiApiBackend.Helpers;
using UniversitiApiBackend.Models.DataModels;

namespace UniversitiApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings, UniversityDBContext context)
        {
           _jwtSettings = jwtSettings;
            _context = context;
        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "martin@gmail.com",
                Name = "Admin",
                Password = "admin"
            },
             new User()
            {
                Id = 2,
                Email = "pepe@gmail.com",
                Name = "User1",
                Password = "user1"
            }
        };

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokents();
                // Search a user in context with LINQ
                var searchUser = (from user in _context.Users
                                 where user.Name == userLogins.UserName && user.Password == userLogins.Password
                                 select user).FirstOrDefault();

                // TODO: Change to searchUser
                // var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                if (searchUser != null)
                {
                    // var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokents()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                    return Ok(Token);
                }
                else
                    return BadRequest("Invalid UserName or Password");
            }
            catch (Exception ex)
            {
                throw new Exception("Gettoken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")] // RBAC => Role Based Access Control
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}

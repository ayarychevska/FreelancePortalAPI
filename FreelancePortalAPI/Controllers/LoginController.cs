using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using FreelancePortalAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Models.Login;
using Services.Services.ApplicationUsers;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private ApplicationUsersService ApplicationUsersService { get; }

        private List<UserModel> appUsers = new List<UserModel>
        {
            new UserModel { UserName = "Vaibhav Bhapkar", Login = "admin", Password = "1234", UserType = "Admin" },
            new UserModel { UserName = "Test User", Login = "user", Password = "1234", UserType = "User" }
        };

        public LoginController(ApplicationUsersService applicationUsersService)
        {
            ApplicationUsersService = applicationUsersService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            try
            {
                var user = ApplicationUsersService.AuthenticateUser(login);
                var tokenString = ApplicationUsersService.GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });

                return response;
            }
            catch (NullReferenceException)
            {
                return BadRequest("No user found");
            }
            catch (ArgumentException)
            {
                return BadRequest("Wrong password");
            }
        }


        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }

        [HttpGet]
        [Route("GetAdminData")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdminData()
        {
            return Ok("This is a response from Admin method");
        }
    }
}

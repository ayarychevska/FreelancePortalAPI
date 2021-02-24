using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.ApplicationUsers;
using Services.Services.ApplicationUsers;

namespace FreelancePortalAPI.Controllers
{
    [Authorize]
    [Route("api/application-users")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<ApplicationUser> Repository { get; }
        private ApplicationUsersService ApplicationUsersService { get; }

        public ApplicationUsersController(IRepository<ApplicationUser> repository, ApplicationUsersService applicationUsersService, IMapper mapper)
        {
            Repository = repository;
            ApplicationUsersService = applicationUsersService;
            Mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ViewModel>> Create([FromBody] CreateModel createModel)
        {
            if (!createModel.Password.Equals(createModel.RepeatPassword))
                return BadRequest("Password not match");

            var result = ApplicationUsersService.Create(createModel);

            ViewModel userViewModel = Mapper.Map<ViewModel>(result);

            return Ok(userViewModel);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ListViewModel>>> GetUsers()
        {
            var users = Repository.GetAll();

            return Ok(Mapper.Map<List<ListViewModel>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewModel>> GetUserById([FromRoute] string id)
        {
            try
            {
                var user = ApplicationUsersService.GetUserById(id);
                return Ok(Mapper.Map<ViewModel>(user));
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/raw")]
        public async Task<ActionResult<CreateModel>> GetUserByIdRaw([FromRoute] string id)
        {
            try
            {
                var user = ApplicationUsersService.GetUserById(id);
                return Ok(Mapper.Map<CreateModel>(user));
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CreateModel>> Update([FromBody] CreateModel createModel)
        {
            var result = ApplicationUsersService.Update(createModel);

            CreateModel userViewModel = Mapper.Map<CreateModel>(result);

            return Ok(userViewModel);
        }

        [HttpPut("password")]
        public async Task<ActionResult<CreateModel>> UpdatePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (!changePasswordModel.Password.Equals(changePasswordModel.RepeatedPassword))
                return BadRequest("Password not match");

            try
            {
                var result = ApplicationUsersService.ChangePassword(changePasswordModel);
                CreateModel userViewModel = Mapper.Map<CreateModel>(result);
                return Ok(userViewModel);
            }
            catch (ArgumentException)
            {
                return BadRequest("Previous password is not correct");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var user = Repository.GetSingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            Repository.Remove(user);
            return Ok();
        }
    }
}

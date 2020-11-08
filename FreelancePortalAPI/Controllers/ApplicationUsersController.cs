using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.ApplicationUsers;
using Services.Services.ApplicationUsers;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<ActionResult<ViewModel>> Create([FromBody] CreateModel createModel)
        {
            var result = ApplicationUsersService.Create(createModel);

            ViewModel userViewModel = Mapper.Map<ViewModel>(result);

            return Ok(userViewModel);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ViewModel>>> GetUsers()
        {
            var users = Repository.GetAll();

            return Ok(Mapper.Map<List<ViewModel>>(users));
        }

        [HttpPut]
        public async Task<ActionResult<ViewModel>> Update([FromBody] CreateModel createModel)
        {
            var result = ApplicationUsersService.Update(createModel);

            ViewModel userViewModel = Mapper.Map<ViewModel>(result);

            return Ok(userViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]string id)
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

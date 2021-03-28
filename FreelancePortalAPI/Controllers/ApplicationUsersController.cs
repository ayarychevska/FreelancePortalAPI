using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IMapper _mapper;
        private readonly IRepository<ApplicationUser> _repository;
        private readonly ApplicationUsersService _applicationUsersService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ApplicationUsersController(IRepository<ApplicationUser> repository,
                ApplicationUsersService applicationUsersService,
                IMapper mapper,
                IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _applicationUsersService = applicationUsersService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ViewModel>> Create([FromBody] CreateModel createModel)
        {
            if (!createModel.Password.Equals(createModel.RepeatPassword))
                return BadRequest("Password not match");

            var result = _applicationUsersService.Create(createModel);

            ViewModel userViewModel = _mapper.Map<ViewModel>(result);

            return Ok(userViewModel);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ListViewModel>>> GetUsers()
        {
            var users = _repository.GetAll();

            return Ok(_mapper.Map<List<ListViewModel>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewModel>> GetUserById([FromRoute] string id)
        {
            try
            {
                var user = _applicationUsersService.GetUserById(id);
                return Ok(_mapper.Map<ViewModel>(user));
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
                var user = _applicationUsersService.GetUserById(id);
                return Ok(_mapper.Map<CreateModel>(user));
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
            var result = _applicationUsersService.Update(createModel);

            CreateModel userViewModel = _mapper.Map<CreateModel>(result);

            return Ok(userViewModel);
        }

        [HttpPut("password")]
        public async Task<ActionResult<CreateModel>> UpdatePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (!changePasswordModel.Password.Equals(changePasswordModel.RepeatedPassword))
                return BadRequest("Password not match");

            try
            {
                var result = _applicationUsersService.ChangePassword(changePasswordModel);
                CreateModel userViewModel = _mapper.Map<CreateModel>(result);
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
            var user = _repository.GetSingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _repository.Remove(user);
            return Ok();
        }

        [HttpPost("{id}/avatar")]
        public async Task<string> UploadedFile(string id, IFormFile avatar)
        {
            string uniqueFileName = null;

            if (avatar != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString();
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    avatar.CopyTo(fileStream);
                }

                ApplicationUser targetUser = _applicationUsersService.GetUserById(id);
                targetUser.Avatar = uniqueFileName;

                _repository.Update(targetUser);
            }

            return uniqueFileName;
        }

        [AllowAnonymous]
        [HttpGet("{fileName}/avatar")]
        public async Task<FileContentResult> GetImage([FromRoute] string fileName)
        {
            byte[] avatar = null;

            string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, fileName);

            avatar = ReadFile(filePath);


            return File(avatar, "image/jpeg");
        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
    }
}

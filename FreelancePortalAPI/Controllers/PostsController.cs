using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Posts;
using Services.Services.Posts;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Post> Repository { get; }
        private PostsService PostsService { get; }

        public PostsController(IRepository<Post> repository, PostsService postsService, IMapper mapper)
        {
            Repository = repository;
            PostsService = postsService;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateModel>> Create([FromBody] CreateModel createModel)
        {
            createModel.DateUTC = DateTime.UtcNow;
            var result = PostsService.Create(createModel);

            CreateModel postViewModel = Mapper.Map<CreateModel>(result);

            return Ok(postViewModel);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CreateModel>>> GetUsers()
        {
            var posts = Repository.GetAll();

            return Ok(Mapper.Map<List<CreateModel>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CreateModel>> GetPostById([FromRoute] int id)
        {
            var post = Repository.GetSingleOrDefault(x => x.Id == id);
            if (post == null)
                return NotFound();
            return Ok(Mapper.Map<CreateModel>(post));
        }

        [HttpPut]
        public async Task<ActionResult<CreateModel>> Update([FromBody] CreateModel createModel)
        {
            var result = PostsService.Update(createModel);

            CreateModel postViewModel = Mapper.Map<CreateModel>(result);

            return Ok(postViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var post = Repository.GetSingleOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            Repository.Remove(post);
            return Ok();
        }
    }
}

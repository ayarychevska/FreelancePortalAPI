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

        [HttpGet("my-posts")]
        public async Task<ActionResult<List<ViewModel>>> GetMyPosts([FromQuery]string userId)
        {
            var myPosts = PostsService.GetMyPosts(userId);

            return Ok(Mapper.Map<List<ViewModel>>(myPosts));
        }

        [HttpGet("{id}/view")]
        public async Task<ActionResult<ViewModel>> GetPostViewModel([FromRoute] long id)
        {
            var post = PostsService.GetPostViewModel(id);

            return Ok(Mapper.Map<ViewModel>(post));
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ViewModel>>> GetPosts()
        {
            var posts = PostsService.GetList();

            return Ok(Mapper.Map<List<ViewModel>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CreateModel>> GetPostById([FromRoute] long id)
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

        [HttpPut("{id}/set-status")]
        public async Task<ActionResult<CreateModel>> UpdateStatus([FromRoute] long id, [FromQuery] int status)
        {
            var result = PostsService.UpdateStatus(id, status);

            CreateModel postViewModel = Mapper.Map<CreateModel>(result);

            return Ok(postViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
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

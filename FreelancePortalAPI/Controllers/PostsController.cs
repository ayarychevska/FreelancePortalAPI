using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Posts;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Post> Repository { get; }
        private PostsService ApplicationUsersService { get; }

        public PostsController(IRepository<Post> repository, PostsService applicationUsersService, IMapper mapper)
        {
            Repository = repository;
            ApplicationUsersService = applicationUsersService;
            Mapper = mapper;
        }
    }
}

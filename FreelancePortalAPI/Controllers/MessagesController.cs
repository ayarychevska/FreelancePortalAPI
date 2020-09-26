using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Messages;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Message> Repository { get; }
        private MessagesService ApplicationUsersService { get; }

        public MessagesController(IRepository<Message> repository, MessagesService applicationUsersService, IMapper mapper)
        {
            Repository = repository;
            ApplicationUsersService = applicationUsersService;
            Mapper = mapper;
        }
    }
}

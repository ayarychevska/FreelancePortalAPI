using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Messages;
using Services.Services.Messages;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Message> Repository { get; }
        private MessagesService MessagesService { get; }

        public MessagesController(IRepository<Message> repository, MessagesService messagesService, IMapper mapper)
        {
            Repository = repository;
            MessagesService = messagesService;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateModel>> Create([FromBody] CreateModel createModel)
        {
            createModel.DateTimeSendedUTC = DateTime.UtcNow;
            var result = MessagesService.Create(createModel);

            CreateModel createViewModel = Mapper.Map<CreateModel>(result);

            return Ok(createViewModel);
        }

        [HttpGet("conversations/{id}")]
        public async Task<ActionResult<List<ViewModel>>> GetConversations([FromRoute] string id)
        {
            var conversations = MessagesService.GetConversations(id);

            return Ok(Mapper.Map<List<ViewModel>>(conversations));
        }

        [HttpGet("conversation")]
        public async Task<ActionResult<List<ViewModel>>> GetConversation([FromQuery] string who, [FromQuery] string withWho)
        {
            var conversation = MessagesService.GetConversation(who, withWho);

            return Ok(Mapper.Map<List<ViewModel>>(conversation));
        }
    }
}

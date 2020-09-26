using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Services.Services.Messages
{
    public class MessagesService : BaseService<Message>
    {
        public MessagesService(IRepository<Message> repository, IMapper mapper) : base(repository, mapper) { }
    }
}

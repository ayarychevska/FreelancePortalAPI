using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Services.Models.Messages;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using System.Linq;

namespace Services.Services.Messages
{
    public class MessagesService : BaseService<Message>
    {
        public MessagesService(IRepository<Message> repository, IMapper mapper) : base(repository, mapper) { }

        public Message Create(CreateModel createModel)
        {
            Message message = Mapper.Map<Message>(createModel);

            var result = Repository.Add(message);
            return result;
        }

        public List<Message> GetConversation(string whoId, string withWhoId)
        {
            var predicate = PredicateBuilder.New<Message>(false);

            predicate.And(x => (x.SenderId == whoId && x.ReceiverId == withWhoId) || (x.ReceiverId == whoId && x.SenderId == withWhoId));

            return Repository
                .FindQuery(predicate)
                .Include(s => s.Sender)
                .Include(m => m.Receiver)
                .ToList();
        }

        public List<Message> GetConversations(string userId)
        {
            List<Message> conversationsList = new List<Message>();

            var messageList = Repository
                .FindQuery(x => x.SenderId == userId || x.ReceiverId == userId)
                .Include(s => s.Sender)
                .Include(m => m.Receiver)
                .ToList();

            List<string> conversationsWith = new List<string>();

            foreach (var message in messageList)
            {
                if (message.ReceiverId != userId && !conversationsWith.Contains(message.ReceiverId))
                {
                    conversationsWith.Add(message.ReceiverId);
                }
                else if (message.SenderId != userId && !conversationsWith.Contains(message.SenderId))
                {
                    conversationsWith.Add(message.SenderId);
                }
            }

            List<Message> lastMessages = new List<Message>();

            foreach (var with in conversationsWith)
            {
                lastMessages.Add(messageList.OrderByDescending(x => x.DateTimeSendedUTC).FirstOrDefault(x => (x.ReceiverId == with || x.SenderId == with)));
            }

            return lastMessages;
        }
    }
}

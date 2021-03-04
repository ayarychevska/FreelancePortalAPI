using FreelancePortalAPI.Managers;
using Newtonsoft.Json;
using Services.Factories;
using Services.Models.Messages;
using Services.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePortalAPI.Handlers
{
    public class ChatMessageHandler : WebSocketHandler
    {
        private MessageServiceFactory _msf;

        public ChatMessageHandler(ConnectionManager webSocketConnectionManager, MessageServiceFactory msf) : base(webSocketConnectionManager)
        {
            _msf = msf;
        }

        public override async Task OnConnected(WebSocket socket, string issuer)
        {
            await base.OnConnected(socket, issuer);

            var socketId = WebSocketConnectionManager.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer, MessagesService messagesService)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var messageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var messageDefiniton = new { To = "", Text = "" };
            var data = JsonConvert.DeserializeAnonymousType(messageJson, messageDefiniton);
            
            var internalMessage = $"{socketId} said to {data.To}: {data.Text}";

            messagesService.Create(new CreateModel {
                DateTimeSendedUTC = DateTime.UtcNow,
                Text = data.Text,
                Status = 0,
                SenderId = socketId,
                ReceiverId = data.To
            });

            Console.WriteLine(internalMessage);

            await SendMessageAsync(data.To, "UPDATE_REQUESTED");
            await SendMessageAsync(socketId, "UPDATE_REQUESTED");
            //await SendMessageToAllAsync(internalMessage);
        }
    }
}

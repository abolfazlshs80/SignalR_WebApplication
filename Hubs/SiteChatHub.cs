using Microsoft.AspNetCore.SignalR;
using SignalR.Bugeto.Models.Services;
using SignalR_WebApplication.Models.Message;
using SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;

namespace SignalR_WebApplication.Hubs
{
    public class SiteChatHub(IChatRoomService _chatRoomService,IMessageService _messageService) : Hub
    {
        public async Task SendNewMessage(string Sender, string Message)
        {
            var roomId = await _chatRoomService.GetChatRoomForConnection(Context.ConnectionId);
            MessageDto messageDto = new MessageDto()
            {
                Message = Message,
                Sender = Sender,
                Time = DateTime.Now,
            };

            await _messageService.SaveChatMessage(roomId, messageDto);
            await Clients.Groups(roomId.ToString())
                .SendAsync("getNewMessage", messageDto.Sender, messageDto.Message, messageDto.Time.ToShortDateString());


        }

        public override async Task OnConnectedAsync()
        {

            var roomId = await _chatRoomService.CreateChatRoom(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await Clients.Caller.
                SendAsync("getNewMessage", "پشتیبانی ", "سلام وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", DateTime.Now.ToShortTimeString());
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }



  


    }
}

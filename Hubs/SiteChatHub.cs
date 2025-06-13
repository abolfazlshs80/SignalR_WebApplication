using Microsoft.AspNetCore.SignalR;
using SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;

namespace SignalR_WebApplication.Hubs
{
    public class SiteChatHub(IChatRoomService _chatRoomService) : Hub
    {
        public async Task SendNewMessage(string Sender, string Message)
        {
            var roomId = await _chatRoomService.GetChatRoomForConnection(Context.ConnectionId);

            await Clients.Groups(roomId.ToString())
                .SendAsync("GetNewMessage", Sender, Message, DateTime.Now.ToShortDateString());
          
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

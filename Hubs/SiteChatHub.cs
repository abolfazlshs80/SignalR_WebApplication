using Microsoft.AspNetCore.Authorization;
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
            if (Context.User.Identity.IsAuthenticated)
            {
                await base.OnConnectedAsync();
                return;
            }
            var roomId = await _chatRoomService.CreateChatRoom(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await Clients.Caller.
                SendAsync("getNewMessage", "پشتیبانی ", "سلام وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", DateTime.Now.ToShortTimeString());
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// پیوستن پشتیبان ها به گروه
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        public async Task JoinRoom(Guid roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        /// <summary>
        /// ترک گروه توسط پشتیبان
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Authorize]
        public async Task LeaveRoom(Guid roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }



  


    }
}

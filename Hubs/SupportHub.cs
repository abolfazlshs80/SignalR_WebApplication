using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.Bugeto.Models.Services;
using SignalR_WebApplication.Models.Message;
using SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;

namespace SignalR_WebApplication.Hubs
{
    [Authorize]
    public class SupportHub(IChatRoomService _chatRoomService,IMessageService _messageService,IHubContext<SiteChatHub> _siteChathub) :Hub
    {




        public async override Task OnConnectedAsync()
        {
            var rooms = await _chatRoomService.GetAllrooms();
            await Clients.Caller.SendAsync("GetRooms", rooms);
            await base.OnConnectedAsync();
        }


        public async Task LoadMessage(Guid roomId)
        {
            var message = await _messageService.GetChatMessage(roomId);
            await Clients.Caller.SendAsync("getNewMessage", message);
        }

        public async Task SendMessage(Guid roomId, string text)
        {
            var message = new MessageDto
            {
                Sender = Context.User.Identity.Name,
                Message = text,
                Time = DateTime.Now,
            };

            await _messageService.SaveChatMessage(roomId, message);

            await _siteChathub.Clients.Group(roomId.ToString())
                .SendAsync("getNewMessage", message.Sender, message.Message, message.Time.ToShortTimeString());

        }
    }
}

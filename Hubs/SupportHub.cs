using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;

namespace SignalR_WebApplication.Hubs
{
    [Authorize]
    public class SupportHub:Hub
    {
        private readonly IChatRoomService _chatRoomService;

        public SupportHub(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }
        public async override Task OnConnectedAsync()
        {
            var rooms = await _chatRoomService.GetAllrooms();
            await Clients.Caller.SendAsync("GetRooms", rooms);
            await base.OnConnectedAsync(); 
        }
    }
}

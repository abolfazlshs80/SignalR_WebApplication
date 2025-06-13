using Microsoft.AspNetCore.SignalR;

namespace SignalR_WebApplication.Hubs
{
    public class SiteChatHub : Hub
    {
        public async Task SendNewMesaage(string Sender, string Message)
        {
            await Clients.All.SendAsync("GetNewMessage", Sender, Message, DateTime.Now.ToShortDateString());
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

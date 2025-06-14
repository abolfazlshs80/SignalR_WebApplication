namespace SignalR_WebApplication.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace SignalR.Bugeto.Models.Services
    {
        public interface IChatRoomService
        {
            Task<List<Guid>> GetAllrooms();
            Task<Guid> CreateChatRoom(string ConnectionId);
            Task<Guid> GetChatRoomForConnection(string CoonectionId);
        }
    }

}

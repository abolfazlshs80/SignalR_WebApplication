using Microsoft.EntityFrameworkCore;
using SignalR_WebApplication.Data.Context;
using SignalR_WebApplication.Data.Models;

namespace SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;

public class ChatRoomService : IChatRoomService
{
    private readonly AppDbContext _context;
    public ChatRoomService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateChatRoom(string ConnectionId)
    {
        var existChatRoom = _context.ChatRooms.SingleOrDefault(p => p.ConnectionId == ConnectionId);
        if (existChatRoom != null)
        {
            return await Task.FromResult(existChatRoom.Id);
        }

        ChatRoom chatRoom = new ChatRoom()
        {
            ConnectionId = ConnectionId,
            Id = Guid.NewGuid(),
        };
        _context.ChatRooms.Add(chatRoom);
        _context.SaveChanges();
        return await Task.FromResult(chatRoom.Id);
    }

    public async Task<Guid> GetChatRoomForConnection(string CoonectionId)
    {
        var chatRoom = _context.ChatRooms.SingleOrDefault(p => p.ConnectionId == CoonectionId);
        return await Task.FromResult(chatRoom.Id);
    }
    public async Task<List<Guid>> GetAllrooms()
    {
        var rooms = _context.ChatRooms
            .Include(p => p.ChatMessages)
            .Where(p => p.ChatMessages.Any())
            .Select(p =>
                p.Id).ToList();
        return await Task.FromResult(rooms);
        return await Task.FromResult(rooms);
    }
}
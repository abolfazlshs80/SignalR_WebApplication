using Microsoft.EntityFrameworkCore;
using SignalR_WebApplication.Data.Models;

namespace SignalR_WebApplication.Data.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}

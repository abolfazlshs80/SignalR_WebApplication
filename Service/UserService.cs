using Microsoft.EntityFrameworkCore;
using SignalR_WebApplication.Data.Context;
using SignalR_WebApplication.Data.Models;

namespace SignalR_WebApplication.Service
{

    public interface IUserService
    {
        Task<User> ValidateUser((string userName, string password) model);
    }
    public class UserService(AppDbContext _context): IUserService
    {
        public async Task<User> ValidateUser((string userName, string password) model)
        {
            return await _context.User.SingleOrDefaultAsync(p => p.UserName == model.userName && p.Password == model.password);

        }
    }
}

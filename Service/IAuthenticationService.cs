using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SignalR_WebApplication.Data.Models;

namespace SignalR_WebApplication.Service
{
    public interface IAuthenticationService
    {
        Task<(ClaimsPrincipal, AuthenticationProperties, string)> LoginAsync(User user);
    }

    public class AuthenticationService(IUserService _userService) : IAuthenticationService
    {
        public async Task<(ClaimsPrincipal, AuthenticationProperties,string)> LoginAsync(User user)
        {
          
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
                };

                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties
                {
                    RedirectUri = "/support"
                };
                return (new ClaimsPrincipal(identity),
                        properties,
                        CookieAuthenticationDefaults.AuthenticationScheme);

            

           
        }
    }

}

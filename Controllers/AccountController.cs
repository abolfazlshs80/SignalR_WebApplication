using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SignalR_WebApplication.Service;
using System.Security.Claims;
using SignalR_WebApplication.Data.Models;
using IAuthenticationService = SignalR_WebApplication.Service.IAuthenticationService;

namespace SignalR_WebApplication.Controllers
{
    public class AccountController(IUserService _userService,IAuthenticationService _authenticationService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {

            var finduser = await _userService.ValidateUser(new(model.UserName,model.Password));
            if (finduser != null)
            {
                (ClaimsPrincipal, AuthenticationProperties, string) login = await _authenticationService.LoginAsync(finduser);
                return SignIn(login.Item1,login.Item2,login.Item3
                );
            }

            return View();
        }
    }
}

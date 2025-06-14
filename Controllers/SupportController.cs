using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalR_WebApplication.Controllers
{
    [Authorize]
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

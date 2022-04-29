using Microsoft.AspNetCore.Mvc;

namespace Jim.Core.Authentication.API.Controllers
{
    public class LoginController : Controller
    {
        private IUserManagerService _usersService;

        public IActionResult Index()
        {
            return View();
        }
    }
}

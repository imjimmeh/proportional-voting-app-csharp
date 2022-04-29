using Jim.Core.Authentication.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jim.Core.Authentication.API.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserManagerService _usersService;

        public LoginController(IUserManagerService usersService, ILogger<LoginController> logger)
        {
            _logger = logger;
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

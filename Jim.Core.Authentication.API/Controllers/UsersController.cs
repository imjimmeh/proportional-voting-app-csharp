using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jim.Core.Authentication.API.Controllers
{
    public class UsersController : Controller
    {
        private IUserManagerService _usersService;

        public UsersController(IUserManagerService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [Authorize(Roles = "GetUsers")]
        public async Task<IActionResult> GetUsers(long take, long skip)
        {
            return View();
        }

        [Authorize(Roles = "GetUsers")]
        public async Task<IActionResult> CreateUser(CreateUserDTO newUser)
        {
            return View();
        }
    }
}

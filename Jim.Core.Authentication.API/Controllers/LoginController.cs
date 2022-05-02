using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Tokens.Service;
using Microsoft.AspNetCore.Mvc;

namespace Jim.Core.Authentication.API.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            try
            {
                var result = await _usersService.()
            }
            catch(Exception ex)
            {

            }
        }
    }
}

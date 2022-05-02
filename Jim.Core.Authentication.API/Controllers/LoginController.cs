using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Shared.APIs;
using Jim.Core.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Jim.Core.Authentication.API.Controllers
{
    [ApiController]
    public class LoginController : BaseController<LoginController>
    {
        private readonly IUserTokenService _userTokenService;

        public LoginController(IUserTokenService userTokenService, ILogger<LoginController> logger) : base(logger)
        {
            _userTokenService = userTokenService ?? throw new ArgumentNullException(nameof(userTokenService));
        }

        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            try
            {
                var result = await _userTokenService.TryGenerateUserToken(request);

                if (result == null)
                    throw new Exception($"No result returned");

                return result.IsSuccess ? Ok(result) : Unauthorized(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new TokenLoginResponse(new[] { new ResponseError(ex.Message) }));
            }
        }
    }
}

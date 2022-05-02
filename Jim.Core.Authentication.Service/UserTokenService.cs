using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Exceptions;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Authentication.Service
{
    public class UserTokenService : IUserTokenService
    {
        private ILoginService _loginService;
        private ITokenService _tokenService;

        public UserTokenService(ITokenService tokenService, ILoginService loginService)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<TokenLoginResponse?> TryGenerateUserToken(LoginRequest login)
        {
            try
            {
                var user = await _loginService.TryLoginAsync(login);

                if (user == null)
                    throw new JimAuthenticationException(AuthenticationFailureReason.Unauthorised, "Unknown error logging in - please try again");

                var token = await _tokenService.GenerateToken(user);

                return new TokenLoginResponse(token);
            }
            catch (JimAuthenticationException ex)
            {
                return new TokenLoginResponse(new[] { new ResponseError(ex.Message) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

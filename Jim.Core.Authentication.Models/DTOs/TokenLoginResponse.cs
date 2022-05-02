using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Shared.Models.DTOs;

namespace Jim.Core.Authentication.Models.DTOs
{
    public record TokenLoginResponse : ApiResponse, ITokenResult
    {
        public string? GeneratedToken { get; init; }

        public DateTime? ExpiresAt { get; init; }

        public TokenLoginResponse()
        {
        }

        public TokenLoginResponse(bool isSuccess) : base(isSuccess)
        {
        }

        public TokenLoginResponse(IEnumerable<ResponseError> errorMessages) : base(errorMessages)
        {
        }

        protected TokenLoginResponse(ApiResponse original) : base(original)
        {
        }

        public TokenLoginResponse(ITokenResult? result)
        {
            var isSuccess = result != null && !string.IsNullOrEmpty(result.GeneratedToken);

            if(isSuccess)
            {
                GeneratedToken = result.GeneratedToken;
                ExpiresAt = result.ExpiresAt;
                IsSuccess = true;
            }
            else
            {
                ErrorMessages = new List<ResponseError> { new ResponseError("Did not receive a valid token result") };
            }
        }
    }
}

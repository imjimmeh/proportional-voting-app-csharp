using Jim.Core.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Authentication.Models.DTOs
{
    public record ValidateTokenResult : ApiResponse
    {
        public ValidateTokenResult()
        {
        }

        public ValidateTokenResult(bool isSuccess) : base(isSuccess)
        {
        }

        public ValidateTokenResult(IEnumerable<ResponseError> errorMessages) : base(errorMessages)
        {
        }

        protected ValidateTokenResult(ApiResponse original) : base(original)
        {
        }

        public bool IsValidToken { get; init; }
    }
}

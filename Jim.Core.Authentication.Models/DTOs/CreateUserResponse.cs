using Jim.Core.Shared.Models.DTOs;

namespace Jim.Core.Authentication.Models.DTOs
{
    public record CreateUserResponse : ApiResponse
    {
        public CreateUserResponse()
        {
        }

        public CreateUserResponse(bool isSuccess) : base(isSuccess)
        {
        }

        public CreateUserResponse(IEnumerable<ResponseError>? errorMessages) : base(errorMessages)
        {
        }

        public CreateUserResponse(long createdUserId) : base(true)
        {
            CreatedUserId = createdUserId;
        }

        protected CreateUserResponse(ApiResponse original) : base(original)
        {
        }

        public long? CreatedUserId { get; init; }
    }
}

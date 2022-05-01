namespace Jim.Core.Shared.Models.DTOs
{
    public record ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public ApiResponse(IEnumerable<ResponseError> errorMessages)
        {
            switch(errorMessages)
            {
                case IList<ResponseError> list:
                    ErrorMessages = list;
                    break;
                default:
                    ErrorMessages = errorMessages.ToList();
                    break;
            }

            IsSuccess = false;
        }

        public bool IsSuccess { get; set; } = false;

        public DateTime ResponseTime { get; init; } = DateTime.UtcNow;

        public IList<ResponseError>? ErrorMessages { get; set; } = null;
    }
}
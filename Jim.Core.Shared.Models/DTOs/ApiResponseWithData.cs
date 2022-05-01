namespace Jim.Core.Shared.Models.DTOs
{
    public record ApiResponseWithData<TData> : ApiResponse
        where TData : class
    {
        public TData? Data { get; init; } = null!;
    }
}
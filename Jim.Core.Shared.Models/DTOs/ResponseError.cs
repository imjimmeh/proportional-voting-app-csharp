namespace Jim.Core.Shared.Models.DTOs
{
    public record ResponseError
    {
        public ResponseError(string errorMessage)
        {
            ErrorMessage = !string.IsNullOrEmpty(errorMessage)? errorMessage : throw new ArgumentNullException(nameof(errorMessage));
        }

        public string ErrorMessage { get; init; } = null!;
    }
}

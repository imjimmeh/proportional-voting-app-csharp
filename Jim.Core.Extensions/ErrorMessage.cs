namespace Jim.Core.Extensions
{
    public record struct ErrorMessage
    {
        public ErrorMessage(string key, string message)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public string Key { get; init; }

        public string Message { get; init; }
    }
}

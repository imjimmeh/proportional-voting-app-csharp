namespace Jim.Core.Extensions
{
    public static class StringExtensions
    {
        public static void SetValueIfNotNullOrEmpty(ref string toChange, string newValue, string nameOfNewValue)
            => toChange = ValidString(newValue) ? toChange : throw new ArgumentNullException(nameOfNewValue);

        private static bool ValidString(string newValue)
            => !string.IsNullOrEmpty(newValue);
    }
}
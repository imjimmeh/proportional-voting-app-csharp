﻿using System.Security;

namespace Jim.Core.Extensions
{
    public static class StringExtensions
    {
        public static void SetValueIfNotNullOrEmpty(ref string toChange, string newValue, string nameOfNewValue)
            => toChange = newValue.IsValid() ? toChange : throw new ArgumentNullException(nameOfNewValue);

        private static bool IsValid(this string newValue)
            => !string.IsNullOrEmpty(newValue);

        public static SecureString? ToSecureString(this string plainString)
        {
            if (plainString == null)
                return null;

            var secureString = new SecureString();

            foreach (char c in plainString.ToCharArray())
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        public static byte[] ToByteArray(this string value) => Convert.FromBase64String(value);
    }
}
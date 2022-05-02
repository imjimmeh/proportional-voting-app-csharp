using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Authentication.Models.Exceptions
{
    public class JimAuthenticationException : UnauthorizedAccessException
    {
        public readonly AuthenticationFailureReason FailureReason;

        public JimAuthenticationException()
        {
        }

        public JimAuthenticationException(AuthenticationFailureReason failureReason) : base(failureReason.GetErrorMessageForReason())
        {
            FailureReason = failureReason;
        }


        public JimAuthenticationException(AuthenticationFailureReason failureReason, string? message) : base(message)
        {
            FailureReason = failureReason;
        }

        public JimAuthenticationException(AuthenticationFailureReason failureReason, string? message, Exception? innerException) : base(message, innerException)
        {
            FailureReason = failureReason;
        }

        protected JimAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    internal static class AuthenticationErrorMessages
    {
        private static Dictionary<AuthenticationFailureReason, string> _errorMessages = new Dictionary<AuthenticationFailureReason, string>
        {
            { AuthenticationFailureReason.InvalidPassword, "Password does not match" }, { AuthenticationFailureReason.InvalidUsername, "Username could not be found" },
            { AuthenticationFailureReason.AccessRevoked, "Access has been revoked for this user" }, { AuthenticationFailureReason.Unauthorised, "User does not have access" },
        };

        public static string? GetErrorMessageForReason(this AuthenticationFailureReason failureReason)
        {
            if(_errorMessages.TryGetValue(failureReason, out string errorMessage))
                return errorMessage;

            return null;
        }
    }
}

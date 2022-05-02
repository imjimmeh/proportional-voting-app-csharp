using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Authentication.Models.Exceptions
{
    public enum AuthenticationFailureReason
    {
        None,
        InvalidUsername,
        InvalidPassword,
        Unauthorised,
        AccessRevoked
    }
}

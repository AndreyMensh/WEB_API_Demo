using System;

namespace Helpers.Exceptions
{
    public class RefreshTokenNotFoundException : ArgumentException
    {
        public RefreshTokenNotFoundException(string message)
            : base(message)
        {
        }
    }

    public class RefreshTokenExpiredException : ArgumentException
    {
        public RefreshTokenExpiredException(string message)
            : base(message)
        {
        }
    }
}

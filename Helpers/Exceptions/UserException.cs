using System;

namespace Helpers.Exceptions
{
    public class UserAlreadyExistsException : ArgumentException
    {
        public UserAlreadyExistsException(string message)
            : base(message)
        {
        }
    }

    public class UserNotFoundException : ArgumentException
    {
        public UserNotFoundException(string message = "User not found!")
            : base(message)
        {
        }
    }

    public class UserBlockedException : ArgumentException
    {
        public UserBlockedException(string message)
            : base(message)
        {
        }
    }
}

using System;

namespace Helpers.Exceptions
{
    public class RoleAlreadyExistExceptions : ArgumentException
    {
        public RoleAlreadyExistExceptions(string message)
            : base(message)
        {
        }
    }

    public class RoleNotFounExceptions : ArgumentException
    {
        public RoleNotFounExceptions(string message)
            : base(message)
        {
        }
    }
}

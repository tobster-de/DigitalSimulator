using System;
using System.Collections.Generic;

namespace DigitalClasses.Exceptions
{
    class NotAUniqueNameException : Exception
    {
        public NotAUniqueNameException()
            : base()
        {
        }

        public NotAUniqueNameException(string message)
            : base(message)
        {
        }
    }
}

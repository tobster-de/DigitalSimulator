using System;
using System.Collections.Generic;

namespace DigitalClasses.Exceptions
{
    class MacroReferenceTypeMismatchException : Exception
    {
        public MacroReferenceTypeMismatchException()
            : base()
        {
        }

        public MacroReferenceTypeMismatchException(string message)
            : base(message)
        {
        }
    }
}

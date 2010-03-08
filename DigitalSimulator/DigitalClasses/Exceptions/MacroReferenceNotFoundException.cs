using System;
using System.Collections.Generic;

namespace DigitalClasses.Exceptions
{
    class MacroReferenceNotFoundException : Exception
    {
        public MacroReferenceNotFoundException()
            : base()
        {
        }

        public MacroReferenceNotFoundException(string message)
            : base(message)
        {
        }
    }
}

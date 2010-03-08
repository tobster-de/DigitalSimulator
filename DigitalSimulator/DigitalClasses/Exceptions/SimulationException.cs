using System;
using System.Collections.Generic;

namespace DigitalClasses.Exceptions
{
    class SimulationException : Exception
    {
        public SimulationException()
            : base()
        {
        }

        public SimulationException(string message)
            : base(message)
        {
        }
    }
}

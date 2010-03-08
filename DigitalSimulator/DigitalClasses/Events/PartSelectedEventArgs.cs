using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Events
{
    public class PartSelectedEventArgs
    {
        private SymbolPart m_Part;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="part">The selected part</param>
        public PartSelectedEventArgs(SymbolPart part)
        {
            m_Part = part;
        }

        /// <summary>
        /// The element to be placed
        /// </summary>
        public SymbolPart Part
        {
            get
            {
                return m_Part;
            }
        }
    }
}

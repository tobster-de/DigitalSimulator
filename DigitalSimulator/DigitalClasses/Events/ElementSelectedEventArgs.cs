using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;

namespace DigitalClasses.Events
{
    public class ElementSelectedEventArgs
    {
        private GraphicBaseElement m_Element;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">The selected element</param>
        public ElementSelectedEventArgs(GraphicBaseElement element)
        {
            m_Element = element;
        }

        /// <summary>
        /// The element to be placed
        /// </summary>
        public GraphicBaseElement Element
        {
            get
            {
                return m_Element;
            }
        }
    }
}

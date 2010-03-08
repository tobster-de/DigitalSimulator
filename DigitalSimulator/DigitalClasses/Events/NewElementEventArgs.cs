using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;

namespace DigitalClasses.Events
{
    public class NewElementEventArgs
    {
        private GraphicBaseElement m_Element;
        //private PointF m_Location;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">The element to be placed</param>
        //// <param name="location">The location the element was placed</param>
        public NewElementEventArgs(GraphicBaseElement element/*, PointF location*/)
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

        ///// <summary>
        ///// The location the element was placed
        ///// </summary>
        //public PointF Location
        //{
        //    get
        //    {
        //        return m_Location;
        //    }
        //}
    }
}

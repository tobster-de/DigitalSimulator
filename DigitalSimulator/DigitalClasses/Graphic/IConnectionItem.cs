using System;
using System.Collections.Generic;
using System.Drawing;


namespace DigitalClasses.Graphic
{
    /// <summary>
    /// Common interface for connection items, i.e. ConnectionLines or ConnectionNodes
    /// </summary>
    internal interface IConnectionItem
    {
        /// <summary>
        /// Gets resp. sets the color of this object
        /// </summary>
        Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether a location is near this object
        /// </summary>
        /// <param name="location">The location of interest</param>
        /// <returns>True if the point is near this object</returns>
        bool IsNear(PointF location);
    }
}

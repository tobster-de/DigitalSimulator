using System;
using System.Collections.Generic;
using System.Drawing;

namespace DigitalClasses.Events
{
    public class LocationChangedEventArgs : EventArgs
    {
        private PointF m_old;
        private PointF m_new;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oldLocation">The old location</param>
        /// <param name="newLocation">The new location</param>
        public LocationChangedEventArgs(PointF oldLocation, PointF newLocation)
            : base()
        {
            m_old = oldLocation;
            m_new = newLocation;
        }

        /// <summary>
        /// The previous location
        /// </summary>
        public PointF OldLocation
        {
            get
            {
                return m_old;
            }
        }

        /// <summary>
        /// The new location that was set
        /// </summary>
        public PointF NewLocation
        {
            get
            {
                return m_new;
            }
        }

    }
}

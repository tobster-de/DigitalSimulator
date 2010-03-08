using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;
using DigitalClasses.Logic;

namespace DigitalClasses.Events
{
    public class TerminalCountChangedEventArgs : EventArgs
    {
        private int m_old;
        private int m_new;
        private DirectionType m_direction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="direction">Direction of terminals that count has changed</param>
        /// <param name="oldCount">The old count </param>
        /// <param name="newCount">The new count of the terminals</param>
        public TerminalCountChangedEventArgs(DirectionType direction, int oldCount, int newCount)
            : base()
        {
            m_old = oldCount;
            m_new = newCount;
            m_direction = direction;
        }

        /// <summary>
        /// The previous terminal count of the element
        /// </summary>
        public int OldCount
        {
            get
            {
                return m_old;
            }
        }

        /// <summary>
        /// The new terminal count that was set
        /// </summary>
        public int NewCount
        {
            get
            {
                return m_new;
            }
        }

        /// <summary>
        /// The direction of terminals which count has changed
        /// </summary>
        public DirectionType Direction
        {
            get
            {
                return m_direction;
            }
        }
    }
}

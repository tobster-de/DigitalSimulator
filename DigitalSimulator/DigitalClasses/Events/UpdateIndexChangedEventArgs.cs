using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;

namespace DigitalClasses.Events
{
    public class UpdateIndexChangedEventArgs : EventArgs
    {
        private int m_old;
        private int m_new;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newIndex">old update index</param>
        /// <param name="oldIndex">new update index</param>
        public UpdateIndexChangedEventArgs(int oldIndex, int newIndex)
            : base()
        {
            m_old = oldIndex;
            m_new = newIndex;
        }

        /// <summary>
        /// The previous index of the element
        /// </summary>
        public int OldIndex
        {
            get
            {
                return m_old;
            }
        }

        /// <summary>
        /// The new index that was set
        /// </summary>
        public int NewIndex
        {
            get
            {
                return m_new;
            }
        }
    }
}

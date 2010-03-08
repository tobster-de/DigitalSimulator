using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;
using System.Windows.Forms;

namespace DigitalClasses.Controls
{
    public abstract class CircuitTool
    {
        #region Fields

        protected CircuitEditor m_Editor;

        #endregion

        #region Properties

        /// <summary>
        /// The editor for this tool
        /// </summary>
        internal CircuitEditor CircuitEditor
        {
            get
            {
                return m_Editor;
            }
            set
            {
                m_Editor = value;
            }
        }

        #endregion

        #region Construction

        protected CircuitTool()
        {
        }

        #endregion

        #region Prototypes

        public virtual void MouseClick(PointF location, Keys controlKeys)
        {
        }

        public virtual void MouseDown(PointF location, Keys controlKeys)
        {
        }

        public virtual void MouseUp(PointF location, Keys controlKeys)
        {
        }

        public virtual void MouseMove(MouseButtons button, PointF location, Keys controlKeys)
        {
        }

        public virtual void Reset()
        {
        }

        #endregion
    }
}

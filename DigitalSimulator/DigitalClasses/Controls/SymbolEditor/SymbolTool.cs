using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Events;

namespace DigitalClasses.Controls
{
    public abstract class SymbolTool
    {
        #region Fields

        protected SymbolEditor m_Editor;

        #endregion

        #region Properties

        /// <summary>
        /// The editor for this tool
        /// </summary>
        internal SymbolEditor SymbolEditor
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

        internal abstract event NewSymbolPart OnNewSymbolPart;

        protected SymbolTool()
        {
        }

        internal abstract void MouseClick(PointF location);

        internal abstract void Reset();
    }
}

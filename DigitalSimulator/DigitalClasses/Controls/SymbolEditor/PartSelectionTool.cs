using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Events;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Controls
{
    public class PartSelectionTool : SymbolTool
    {
        #region Events

        public event PartSelected OnPartSelected;

        #endregion

        private static SymbolTool m_Instance;

        internal override event NewSymbolPart OnNewSymbolPart;

        /// <summary>
        /// Returns the Singleton Instance of this tool
        /// </summary>
        public static SymbolTool Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new PartSelectionTool();
                }
                return m_Instance;
            }
        }

        internal override void MouseClick(PointF location)
        {
            SymbolPart part = m_Editor.GetPartAt(location);
            if (OnPartSelected != null)
            {
                OnPartSelected(this, new PartSelectedEventArgs(part));
            }
        }

        internal override void Reset()
        {
        }
    }
}

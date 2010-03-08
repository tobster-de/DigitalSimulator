using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Events;

namespace DigitalClasses.Controls
{
    public class PartDeletionTool: SymbolTool
    {
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
                    m_Instance = new PartDeletionTool();
                }
                return m_Instance;
            }
        }

        internal override void MouseClick(PointF location)
        {
        }

        internal override void Reset()
        {
        }
    }
}

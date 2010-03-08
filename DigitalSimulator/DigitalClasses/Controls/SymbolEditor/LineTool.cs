using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DigitalClasses.Events;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Controls
{
    public class LineTool : SymbolTool
    {
        private static SymbolTool m_Instance;

        private int m_ClickNumber;
        private PointF m_FirstClick;

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
                    m_Instance = new LineTool();
                }
                return m_Instance;
            }
        }

        internal override void MouseClick(PointF location)
        {
            switch (m_ClickNumber)
            {
                case 0:
                    {
                        m_FirstClick = location;
                    }
                    break;
                case 1:
                    {
                        if (OnNewSymbolPart != null)
                        {
                            //GraphicsPath path = new GraphicsPath();
                            //path.AddLine(m_FirstClick, location);
                            SymbolPart part = new LinePart(m_FirstClick, location);
                            OnNewSymbolPart(this, new NewSymbolPartEventArgs(part));
                        }
                    }
                    break;
                default:
                    break;
            }
            m_ClickNumber = ++m_ClickNumber % 2;
        }

        internal override void Reset()
        {
            m_ClickNumber = 0;
            m_FirstClick = Point.Empty;
        }

    }
}

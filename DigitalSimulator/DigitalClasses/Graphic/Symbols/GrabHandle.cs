using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;
using ToolBox;

namespace DigitalClasses.Graphic.Symbols
{
    public class GrabHandle
    {
        public delegate void HandleMoved(GrabHandle sender, PointF oldLocation, PointF newLocation);

        public event HandleMoved OnHandleMoved;

        private PointF m_Location;
        private int m_Index;

        /// <summary>
        /// Index 
        /// </summary>
        public int Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
            }
        }

        /// <summary>
        /// Location
        /// </summary>
        public PointF Location
        {
            get
            {
                return new PointF(m_Location.X, m_Location.Y);
            }
            set
            {
                if (m_Location.X != value.X || m_Location.Y != value.Y)
                {
                    if (OnHandleMoved != null)
                    {
                        OnHandleMoved(this, m_Location, value);
                    }
                    m_Location = value;
                }
            }
        }

        public GrabHandle(PointF location)
        {
            m_Location = location;
        }

        public void DrawHandle(Graphics g, PointF offset)
        {
            RectangleF rect = new RectangleF(m_Location.X + offset.X - 2, m_Location.Y + offset.Y - 2, 5, 5);
            g.FillRectangle(GraphicConstants.GrabHandleBrush, rect);
        }

        internal bool IsNear(PointF location)
        {
            Vector origin = new Vector(m_Location.X, m_Location.Y);
            Vector test = new Vector(location.X, location.Y);
            test = test - origin;
            return test.Length < 4;
        }
    }
}

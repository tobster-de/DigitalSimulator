using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;
using DigitalClasses.Logic;

namespace DigitalClasses.Graphic.Symbols
{
    public class PortPart : SymbolPart
    {
        private GraphicTerminal m_GraphicTerminal;
        private PointF m_Location;
        private string m_Name;

        /// <summary>
        /// Name of this port
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        /// <summary>
        /// Direction of this port. Input or Output
        /// </summary>
        public DirectionType Direction
        {
            get
            {
                return (m_GraphicTerminal.LinkedObject as Terminal).Direction;
            }
        }

        /// <summary>
        /// Angle is automatically determined by the direction of a port
        /// </summary>
        public float Angle
        {
            get
            {
                return m_GraphicTerminal.Angle;
            }
        }

        /// <summary>
        /// The location of the port within the symbol
        /// </summary>
        public override PointF Location
        {
            get
            {
                return m_Location;
            }
            set
            {
                m_Location = value;
                m_GraphicTerminal.Location = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">location of the port</param>
        /// <param name="direction">direction of the port</param>
        /// <param name="name">name of the port</param>
        public PortPart(PointF location, float angle, DirectionType direction, string name)
        {
            Terminal terminal = new Terminal(direction);
            m_GraphicTerminal = new GraphicTerminal(terminal);
            if (angle != 0)
            {
                m_GraphicTerminal.Angle = angle;
            }
            m_GraphicTerminal.Location = location;
            m_Location = location;
            m_Name = name;
        }

        public PortPart(PointF location, DirectionType direction, string name)
        {
            Terminal terminal = new Terminal(direction);
            m_GraphicTerminal = new GraphicTerminal(terminal);
            if (direction == DirectionType.Input)
            {
                m_GraphicTerminal.Angle = 180;
            }
            m_GraphicTerminal.Location = location;
            m_Location = location;
            m_Name = name;
        }

        #region Overrides

        public override bool IsNear(PointF location)
        {
            return m_GraphicTerminal.Bounds.Contains(location);
        }

        public override GrabHandle GetGrabHandle(PointF offset)
        {
            return null;
        }

        public override void DrawPart(System.Drawing.Graphics g, PointF offset)
        {
            m_GraphicTerminal.Location = new PointF(m_Location.X + offset.X, m_Location.Y + offset.Y);
            m_GraphicTerminal.Paint(g);
            m_GraphicTerminal.Location = m_Location;
        }

        public override void DrawSelection(Graphics g, PointF offset)
        {
            RectangleF bounds = m_GraphicTerminal.Bounds;
            bounds.Location = DoOffset(bounds.Location, offset);
            g.FillRectangle(GraphicConstants.HighlightBrush, bounds);
        }

        #endregion
    }
}

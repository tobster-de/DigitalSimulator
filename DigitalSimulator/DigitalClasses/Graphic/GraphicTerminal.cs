using System;
using System.Collections.Generic;
using DigitalClasses.Logic;
using System.Drawing.Drawing2D;
using System.Drawing;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    public class GraphicTerminal : GraphicBaseElement
    {
        #region Fields

        private ConnectionNode m_Node;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        public override RectangleF Bounds
        {
            get
            {
                RectangleF result = new RectangleF(Location, Size);
                if (result.Width == 0)
                {
                    result.X -= 2;
                    result.Width += 5;
                }
                if (result.Height == 0)
                {
                    result.Y -= 2;
                    result.Height += 5;
                }
                return result;
            }
        }

        public override PointF Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                if (base.Location != value)
                {
                    base.Location = value;
                    m_Node.Location = m_Body.Path.GetLastPoint();
                }
            }
        }

        internal ConnectionNode ConnectionNode
        {
            get
            {
                return m_Node;
            }
        }

        #endregion

        #region Construction

        internal GraphicTerminal(BaseElement linkedObject)
            : base(linkedObject)
        {
            BuildBody();
            m_Node = new ConnectionNode(m_Body.Path.GetLastPoint());
            AddChild(m_Node);
        }

        #endregion

        #region Private Implementation

        private void BuildBody()
        {
            GraphicsPath bodypath = new GraphicsPath();
            Terminal terminal = (Terminal)m_LinkedObject;
            if (terminal.Negated)
            {
                bodypath.AddEllipse(0, 0, 5, 5);
                bodypath.AddLine(5, 3, GraphicConstants.GraphicTerminalWidth, 3);
            }
            else
            {
                bodypath.AddLine(0, 0, GraphicConstants.GraphicTerminalWidth, 0);
            }
            m_Body = new GraphicShape(bodypath);
        }

        #endregion

        #region Overrides

        public override void PaintBackground(Graphics graphics)
        {
            if (m_Highlighted)
            {
                graphics.FillRectangle(GraphicConstants.HighlightBrush, Bounds);
            }
        }

        #endregion
    }
}

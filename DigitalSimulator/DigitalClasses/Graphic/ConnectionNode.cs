using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ToolBox;
using DigitalClasses.Logic;

namespace DigitalClasses.Graphic
{
    internal class ConnectionNode : GraphicBaseElement, IConnectionItem
    {
        #region Fields

        private List<ConnectionLine> m_Lines;

        #endregion

        #region Properties

        public override GraphicBaseElement Parent
        {
            get
            {
                return base.Parent;
            }
            set
            {
                if (value != m_Parent)
                {
                    if (m_Parent != null && m_Parent is GraphicTerminal)
                    {
                        (m_Parent.LinkedObject as Terminal).ParentIO.OnNameChanged -= IOElement_OnNameChanged;
                    }
                    base.Parent = value;
                    if (m_Parent != null)
                    {
                        if (m_Parent is GraphicTerminal)
                        {
                            InputOutputElement termParentIO = (m_Parent.LinkedObject as Terminal).ParentIO;
                            if (termParentIO != null)
                            {
                                termParentIO.OnNameChanged += new DigitalClasses.Events.NotifyEvent(IOElement_OnNameChanged);
                            }
                        }
                        Name = GenerateNodeName();
                    }
                }
            }
        }

        /// <summary>
        /// Color of this connection line
        /// </summary>
        public Color Color
        {
            get
            {
                return m_Body.Color;
            }
            set
            {
                m_Body.Color = value;
            }
        }

        /// <summary>
        /// Returns the Lines this Node is connected to
        /// </summary>
        public ConnectionLine[] Lines
        {
            get
            {
                return m_Lines.ToArray();
            }
        }

        #endregion

        #region Construction

        public ConnectionNode(PointF location)
        {
            BuildBody();
            Location = location;
            m_Lines = new List<ConnectionLine>();
        }

        public ConnectionNode(float X1, float Y1)
        {
            BuildBody();
            Location = new PointF(X1, Y1);
            m_Lines = new List<ConnectionLine>();
        }

        #endregion

        #region Overrides

        public override void Paint(Graphics graphics)
        {
            if (m_Body != null && m_Lines.Count > 2)
            {
                graphics.FillEllipse(new SolidBrush(m_Body.Color), Location.X - 3, Location.Y - 3, 7, 7);
                //m_Body.BackgroundColor = m_Body.Color;
                //m_Body.Paint(graphics);
            }
        }

        public override void PaintBackground(Graphics graphics)
        {
            if (m_Parent.Highlighted)
            {
                if (m_Lines.Count > 2)
                {
                    graphics.FillEllipse(new SolidBrush(GraphicConstants.HighlightColor), Location.X - 5, Location.Y - 5, 11, 11);
                }
                else if (m_Lines.Count == 2)
                {
                    graphics.FillRectangle(new SolidBrush(GraphicConstants.HighlightColor), Location.X - 2, Location.Y - 2, 5, 5);
                }
            }
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Build the body of this Connection
        /// </summary>
        private void BuildBody()
        {
            //GraphicsPath bodyPath = new GraphicsPath();
            //bodyPath.AddEllipse(0, 0, 5, 5);
            //m_Body = new GraphicShape(bodyPath);
            m_Body = new GraphicShape(); //dummy
        }

        private double Distance(PointF location, PointF point)
        {
            Vector ap = new Vector(location.X, location.Y);
            Vector p = new Vector(point.X, point.Y);

            return (p - ap).Length;
        }

        private string GenerateNodeName()
        {
            if (m_Parent == null)
            {
                return String.Empty;
            }
            if (m_Parent is GraphicConnection)
            {
                return String.Format("{0:x4}{1:x4}", (int)Location.X, (int)Location.Y);
            }
            else if (m_Parent is GraphicTerminal)
            {
                GraphicTerminal terminal = m_Parent as GraphicTerminal;
                string ioName = String.Empty;
                if ((m_Parent.LinkedObject as Terminal).ParentIO != null)
                {
                    ioName = (m_Parent.LinkedObject as Terminal).ParentIO.Name;
                }
                return String.Format("{0}/{1}", ioName, terminal.Name);
            }
            throw new NotSupportedException("Parent type not supported");
        }

        private void IOElement_OnNameChanged(object sender)
        {
            Name = GenerateNodeName();
        }

        #endregion

        #region Public Implementation

        public bool IsNear(PointF location)
        {
            return Distance(Location, location) < 6;
        }

        internal void AttachLine(ConnectionLine line)
        {
            if (m_Lines.Contains(line))
            {
                return;
            }
            m_Lines.Add(line);
        }

        internal void DetachLine(ConnectionLine line)
        {
            if (m_Lines.Contains(line))
            {
                m_Lines.Remove(line);
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ToolBox;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    internal enum LineStyle
    {
        lmNone,
        lmHorizontal,
        lmVertical
    }

    internal class ConnectionLine : GraphicBaseElement, IConnectionItem
    {
        #region Fields

        private List<ConnectionNode> m_Nodes;

        #endregion

        #region Properties

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
        /// Returns the nodes this line is connected to
        /// </summary>
        public ConnectionNode[] Nodes
        {
            get
            {
                return m_Nodes.ToArray();
            }
        }

        /// <summary>
        /// Returns the style of this Line
        /// </summary>
        public LineStyle LineStyle
        {
            get
            {
                return DetermineLineStyle(m_Nodes[0].Location, m_Nodes[1].Location);
            }
        }

        #endregion

        #region Construction

        public ConnectionLine(ConnectionNode first, ConnectionNode second)
        {
            m_Nodes = new List<ConnectionNode>(2);
            AttachNode(first);
            AttachNode(second);
            BuildBody();
        }

        #endregion

        #region Overrides

        public override void PaintBackground(Graphics graphics)
        {
            if (m_Parent.Highlighted)
            {
                graphics.DrawLine(new Pen(GraphicConstants.HighlightColor, 5), m_Nodes[0].Location, m_Nodes[1].Location);
            }
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Build the body of this Connection
        /// </summary>
        private void BuildBody()
        {
            GraphicsPath bodyPath = new GraphicsPath();
            bodyPath.AddLine(m_Nodes[0].Location, m_Nodes[1].Location);
            m_Body = new GraphicShape(bodyPath);
        }

        private double DistanceLinePoint(PointF point)
        {
            Vector ap = Vector.FromPointF(m_Nodes[0].Location);
            Vector ep = Vector.FromPointF(m_Nodes[1].Location);
            Vector rv = ep - ap;
            Vector p = Vector.FromPointF(point);

            return Vector.DistanceLinePoint(ap, rv, p);
        }

        private void Node_OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            BuildBody();
        }

        /// <summary>
        /// Attaches this line to a node
        /// </summary>
        /// <param name="node">Node to attach to</param>
        private void AttachNode(ConnectionNode node)
        {
            if (m_Nodes.Contains(node) || m_Nodes.Count >= 2)
            {
                return;
            }
            m_Nodes.Add(node);
            node.AttachLine(this);
            node.OnLocationChanged += new LocationChangeEvent(Node_OnLocationChanged);
        }

        /// <summary>
        /// Detaches this line from a node
        /// </summary>
        /// <param name="node">Node to detach from</param>
        private void DetachNode(ConnectionNode node)
        {
            if (m_Nodes.Contains(node))
            {
                m_Nodes.Remove(node);
                node.DetachLine(this);
                node.OnLocationChanged -= Node_OnLocationChanged;
            }
        }

        #endregion

        #region Public Implementation


        internal static LineStyle DetermineLineStyle(PointF point1, PointF point2)
        {
            if (point1.X == point2.X)
            {
                return LineStyle.lmVertical;
            }
            if (point1.Y == point2.Y)
            {
                return LineStyle.lmHorizontal;
            }
            return LineStyle.lmNone;
        }

        /// <summary>
        /// Detaches from all ConnectionNodes - Use it only when line is not neaded anymore
        /// </summary>
        internal void DetachNodes()
        {
            for (int i = m_Nodes.Count - 1; i >= 0; i--)
            {
                DetachNode(m_Nodes[i]);
            }
        }

        /// <summary>
        /// Determines whether a location is near this line
        /// </summary>
        /// <param name="location">The location of interest</param>
        /// <returns>True if the point is near this line</returns>
        public bool IsNear(PointF location)
        {
            return DistanceLinePoint(location) < 4;
        }

        /// <summary>
        /// Calculates a point on the line with least distance to the given point
        /// </summary>
        /// <param name="point">a given point</param>
        /// <returns>nearest point on the line</returns>
        public PointF NearestPointOnLine(PointF point)
        {
            Vector ap = Vector.FromPointF(m_Nodes[0].Location);
            Vector ep = Vector.FromPointF(m_Nodes[1].Location);
            Vector rv = ep - ap;
            Vector p = Vector.FromPointF(point);

            return Vector.NearestPointOnLine(ap, rv, p).ToPointF();
        }

        #endregion
    }
}

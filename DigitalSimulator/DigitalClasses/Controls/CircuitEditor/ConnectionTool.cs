using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DigitalClasses.Graphic;
using DigitalClasses.Events;
using DigitalClasses.Logic;

namespace DigitalClasses.Controls
{
    public class ConnectionTool : CircuitTool
    {
        #region Fields

        private PointF m_LastMouseLocation;
        private GraphicBaseElement m_FromElement;
        private GraphicBaseElement m_ParentElement;
        private GraphicBaseElement m_HighLightElement;

        #endregion

        #region Overrides

        public override void MouseClick(PointF location, Keys controlKeys)
        {
            GraphicBaseElement element = m_Editor.GetElementAt(location);
            if (element == null)
            {
                if (m_FromElement != null)
                {
                    //floating connection - create a new connection line
                    FloatingConnection(location);
                    m_LastMouseLocation = location;
                }
                return;
            }
            if (element is GraphicInputOutputElement == false && element is GraphicConnection == false)
            {
                return;
            }
            //user clicked a terminal of an element
            GraphicInputOutputElement ioelem = element as GraphicInputOutputElement;
            GraphicTerminal graphicTerminal = null;
            if (ioelem != null)
            {
                graphicTerminal = ioelem.GetTerminalAt(location);

                if (graphicTerminal == null || graphicTerminal.Equals(m_FromElement))
                {
                    return;
                }
                if (m_FromElement == null)
                {
                    m_FromElement = graphicTerminal;
                    m_LastMouseLocation = location;
                    return;
                }
                TryConnectToTerminal(graphicTerminal, location);
                m_LastMouseLocation = location;
            }
            //user clicked a connection
            GraphicConnection connection = element as GraphicConnection;
            if (connection != null)
            {
                if (connection.Equals(m_FromElement))
                {
                    return;
                }
                if (m_FromElement == null)
                {
                    m_FromElement = connection;
                    m_LastMouseLocation = location;
                    return;
                }
                TryConnectToConnection(connection, location);
                m_LastMouseLocation = location;
            }
        }

        public override void MouseMove(MouseButtons button, PointF location, Keys controlKeys)
        {
            if (m_Editor == null)
            {
                return;
            }
            GraphicBaseElement element = m_Editor.GetElementAt(location);
            if (m_HighLightElement != null)
            {
                if (element == null
                    || (element.Equals(m_ParentElement) == false && m_HighLightElement is GraphicTerminal)
                    || (element.Equals(m_HighLightElement) == false && m_HighLightElement is GraphicConnection))
                {
                    //Un-Hover the element
                    UnHighlightElement(m_HighLightElement);
                    return;
                }
            }
            if (element is GraphicInputOutputElement)
            {
                GraphicInputOutputElement ioelem = element as GraphicInputOutputElement;
                GraphicTerminal graphicTerminal = ioelem.GetTerminalAt(location);
                if (graphicTerminal != null)
                {
                    if ((graphicTerminal.LinkedObject as Terminal).IsConnected == false)
                    {
                        //Hover an element
                        m_ParentElement = element;
                        HighlightElement(graphicTerminal);
                    }
                }
                else if (m_HighLightElement != null)
                {
                    //Un-Hover the element
                    UnHighlightElement(m_HighLightElement);
                }
            }
            if (element is GraphicConnection)
            {
                if (m_HighLightElement == null)
                {
                    //Hover an element
                    HighlightElement(element);
                }
            }
        }

        public override void Reset()
        {
            m_HighLightElement = null;
            m_FromElement = null;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Determines whether a line through the given points would be horizontal or vertical
        /// </summary>
        /// <param name="point1">first point of the line</param>
        /// <param name="point2">second point of the line</param>
        /// <returns></returns>
        private bool IsOrthogonal(PointF point1, PointF point2)
        {
            return point1.X == point2.X || point1.Y == point2.Y;
        }

        /// <summary>
        /// Enforces that a line through the given point is either horizontal or vertical
        /// </summary>
        /// <param name="fixPoint">a fixed point</param>
        /// <param name="varPoint">a point that needs to adjusted</param>
        /// <returns>correct point</returns>
        private PointF ForceOrthogonality(PointF fixPoint, PointF varPoint)
        {
            if (IsOrthogonal(fixPoint, varPoint))
            {
                return varPoint;
            }
            if (Math.Abs(fixPoint.X - varPoint.X) > Math.Abs(fixPoint.Y - varPoint.Y))
            {
                //Y-difference is lower - so line should be horizontal
                return new PointF(varPoint.X, fixPoint.Y);
            }
            else
            {
                //X-difference is lower - so line should be vertical
                return new PointF(fixPoint.X, varPoint.Y);
            }
        }

        /// <summary>
        /// Splits the connection line at the given point and places a node there
        /// </summary>
        /// <param name="line">the line to split</param>
        /// <param name="atPoint">the point to place a node</param>
        /// <returns>the placed node</returns>
        private ConnectionNode SplitConnectionLine(ConnectionLine line, PointF atPoint)
        {
            GraphicConnection connection = line.Parent as GraphicConnection;

            ConnectionNode node = new ConnectionNode(line.NearestPointOnLine(m_Editor.AlignToGrid(atPoint)));
            ConnectionLine line1 = new ConnectionLine(line.Nodes[0], node);
            ConnectionLine line2 = new ConnectionLine(line.Nodes[1], node);
            connection.RemoveChild(line);
            connection.AddChild(node);
            connection.AddChild(line1);
            connection.AddChild(line2);

            return node;
        }

        /// <summary>
        /// Create a floating connnection, i.e. not completely connected everywhere
        /// </summary>
        /// <param name="location"></param>
        private void FloatingConnection(PointF location)
        {
            if (m_FromElement is GraphicConnection)
            {
                GraphicConnection graphicConnection = m_FromElement as GraphicConnection;
                IConnectionItem connectionItem = graphicConnection.GetItemAt(m_LastMouseLocation);
                if (connectionItem is ConnectionNode)
                {
                    //user clicked near a node at fist, connect to this
                    ConnectionNode prevNode = connectionItem as ConnectionNode;
                    location = ForceOrthogonality(prevNode.Location, m_Editor.AlignToGrid(location));
                    bool skip = false;
                    //check whether the new line would just lenghten the previous line
                    if (prevNode.Lines.Length == 1)
                    {
                        ConnectionLine prevLine = prevNode.Lines[0];
                        if (prevLine.LineStyle == ConnectionLine.DetermineLineStyle(prevNode.Location, location))
                        {
                            //place the node at the new location in this case
                            prevNode.Location = location;
                            skip = true;
                        }
                    }
                    if (skip == false)
                    {
                        //place a new line
                        ConnectionNode node = new ConnectionNode(location);
                        ConnectionLine line = new ConnectionLine(prevNode, node);

                        m_FromElement.AddChild(node);
                        m_FromElement.AddChild(line);

                        m_FromElement = node;
                    }
                }
                else if (connectionItem is ConnectionLine)
                {
                    //user clicked near a line at first
                    ConnectionLine prevLine = connectionItem as ConnectionLine;
                    //place a connection node at location - split connection line
                    ConnectionNode node = SplitConnectionLine(prevLine, m_LastMouseLocation);

                    //the new node/line
                    location = ForceOrthogonality(node.Location, m_Editor.AlignToGrid(location));
                    ConnectionNode fnode = new ConnectionNode(location);
                    ConnectionLine line = new ConnectionLine(node, fnode);
                    m_FromElement.AddChild(fnode);
                    m_FromElement.AddChild(line);

                    m_FromElement = fnode;
                }
            }
            else if (m_FromElement is GraphicTerminal)
            {
                //floating connection from a terminal
                GraphicTerminal graphicTerminal = m_FromElement as GraphicTerminal;
                location = ForceOrthogonality(graphicTerminal.ConnectionNode.Location, m_Editor.AlignToGrid(location));
                ConnectionNode node = new ConnectionNode(location);
                ConnectionLine line = new ConnectionLine(node, graphicTerminal.ConnectionNode);

                GraphicConnection graphicConnection =
                    GraphicObjectFactory.CreateInstance(typeof(Connection), new Connection()) as GraphicConnection;
                graphicConnection.Name = UniqueName.GetUniqueName(m_Editor.Circuit, typeof(Connection));
                graphicConnection.AddChild(node);
                graphicConnection.AddChild(line);

                graphicConnection.ConnectTerminal(graphicTerminal);
                m_Editor.AddElement(graphicConnection);

                //proceed connecting with this node
                m_FromElement = node;
            }
            else if (m_FromElement is ConnectionNode)
            {
                //special case -> could be covered by "is GraphicConnection"
                //sequential floating lines
                ConnectionNode prevNode = m_FromElement as ConnectionNode;
                location = ForceOrthogonality(prevNode.Location, m_Editor.AlignToGrid(location));
                bool skip = false;
                //check whether the new line would just lenghten the previous line
                if (prevNode.Lines.Length == 1)
                {
                    ConnectionLine prevLine = prevNode.Lines[0];
                    if (prevLine.LineStyle == ConnectionLine.DetermineLineStyle(prevNode.Location, location))
                    {
                        //place the node at the new location in this case
                        prevNode.Location = location;
                        skip = true;
                    }
                }
                if (skip == false)
                {
                    //place a new line
                    ConnectionNode node = new ConnectionNode(location);
                    ConnectionLine line = new ConnectionLine(prevNode, node);

                    GraphicConnection graphicConnection = prevNode.Parent as GraphicConnection;
                    graphicConnection.AddChild(node);
                    graphicConnection.AddChild(line);

                    m_FromElement = node;
                }
            }
            m_Editor.UpdateDrawing();
            m_Editor.RaiseChangedEvent();
            m_Editor.Invalidate();
        }

        /// <summary>
        /// Connect to a Connection
        /// </summary>
        /// <param name="connection"></param>
        private void TryConnectToConnection(GraphicConnection connection, PointF location)
        {
            if (m_FromElement is GraphicTerminal)
            {
                Terminal fromTerminal = m_FromElement.LinkedObject as Terminal;
                if (fromTerminal.Connection != null)
                {
                    MergeConnections(fromTerminal.Connection.LinkedObject as GraphicConnection, m_LastMouseLocation, connection, location);
                }
                else
                {
                    IConnectionItem connectionItem = connection.GetItemAt(location);
                    if (connectionItem is ConnectionNode)
                    {
                        //user clicked near a node - connect to this
                        ConnectionLine line = new ConnectionLine((m_FromElement as GraphicTerminal).ConnectionNode, connectionItem as ConnectionNode);
                        connection.AddChild(line);
                    }
                    else if (connectionItem is ConnectionLine)
                    {
                        //user clicked near a line
                        ConnectionLine prevLine = connectionItem as ConnectionLine;
                        //place a connection node at location - split connection line
                        ConnectionNode node = SplitConnectionLine(prevLine, location);
                        ConnectionLine line = new ConnectionLine(node, (m_FromElement as GraphicTerminal).ConnectionNode);
                        connection.AddChild(line);
                    }
                    connection.ConnectTerminal(m_FromElement as GraphicTerminal);
                    m_Editor.UpdateDrawing();
                    m_Editor.RaiseChangedEvent();
                }
            }
            else if (m_FromElement is GraphicConnection)
            {
                MergeConnections(m_FromElement as GraphicConnection, m_LastMouseLocation, connection, location);
            }
            else if (m_FromElement is ConnectionNode)
            {
                MergeConnections(m_FromElement.Parent as GraphicConnection, m_LastMouseLocation, connection, location);
                //GraphicConnection fromConnection = m_FromElement.Parent as GraphicConnection;
            }
            m_FromElement = null;
            m_Editor.Invalidate();
        }

        /// <summary>
        /// Connect to a Terminal (resp. its connection)
        /// </summary>
        /// <param name="graphicTerminal"></param>
        private void TryConnectToTerminal(GraphicTerminal graphicTerminal, PointF location)
        {
            Terminal toTerminal = graphicTerminal.LinkedObject as Terminal;
            if (toTerminal.Connection != null)
            {
                //merging connections not supported by clicking terminals
                return;
            }
            if (m_FromElement is GraphicTerminal)
            {
                Terminal fromTerminal = m_FromElement.LinkedObject as Terminal;
                //if (fromTerminal.Connection != null && toTerminal.Connection != null)
                //{
                //    MergeConnections(fromTerminal.Connection.LinkedObject as GraphicConnection, m_LastMouseLocation,
                //        toTerminal.Connection.LinkedObject as GraphicConnection, location);
                //}
                //else if (fromTerminal.Connection != null)
                //{
                //    //IDEE: suche über allen linien die kürzeste entfernung vom ziel zur verbindung
                //    GraphicConnection graphicConnection = fromTerminal.Connection.LinkedObject as GraphicConnection;

                //    ConnectionLine line = new ConnectionLine((m_FromElement as GraphicTerminal).ConnectionNode, graphicTerminal.ConnectionNode);
                //    graphicConnection.AddChild(line);

                //    graphicConnection.ConnectTerminal(graphicTerminal);
                //    m_Editor.UpdateDrawing();
                //    m_Editor.RaiseChangedEvent();
                //}
                //else if (toTerminal.Connection != null)
                //{
                //    GraphicConnection graphicConnection = toTerminal.Connection.LinkedObject as GraphicConnection;
                //    graphicConnection.ConnectTerminal(m_FromElement as GraphicTerminal);
                //    m_Editor.UpdateDrawing();
                //    m_Editor.RaiseChangedEvent();
                //}
                //else
                if (fromTerminal.Connection == null && toTerminal.Connection == null)
                {
                    GraphicTerminal fromGraphicTerminal = m_FromElement as GraphicTerminal;
                    if (IsOrthogonal(fromGraphicTerminal.ConnectionNode.Location, graphicTerminal.ConnectionNode.Location) == false)
                    {
                        return;
                    }
                    GraphicConnection graphicConnection
                        = GraphicObjectFactory.CreateInstance(typeof(Connection), new Connection()) as GraphicConnection;
                    graphicConnection.Name = UniqueName.GetUniqueName(m_Editor.Circuit, typeof(Connection));
                    graphicConnection.ConnectTerminal(fromGraphicTerminal);
                    graphicConnection.ConnectTerminal(graphicTerminal);

                    ConnectionLine line = new ConnectionLine(fromGraphicTerminal.ConnectionNode, graphicTerminal.ConnectionNode);
                    graphicConnection.AddChild(line);

                    m_Editor.AddElement(graphicConnection);
                    m_Editor.UpdateDrawing();
                    m_Editor.RaiseChangedEvent();
                }
            }
            if (m_FromElement is GraphicConnection)
            {
                GraphicConnection fromConnection = m_FromElement as GraphicConnection;
                IConnectionItem connectionItem = fromConnection.GetItemAt(m_LastMouseLocation);

                if (connectionItem is ConnectionNode)
                {
                    ConnectionLine line = new ConnectionLine(connectionItem as ConnectionNode, graphicTerminal.ConnectionNode);
                    fromConnection.AddChild(line);
                }
                else if (connectionItem is ConnectionLine)
                {
                    ConnectionLine prevLine = connectionItem as ConnectionLine;
                    ConnectionNode node = new ConnectionNode(prevLine.NearestPointOnLine(m_LastMouseLocation));
                    ConnectionLine line1 = new ConnectionLine(prevLine.Nodes[0], node);
                    ConnectionLine line2 = new ConnectionLine(prevLine.Nodes[1], node);
                    fromConnection.RemoveChild(prevLine);
                    fromConnection.AddChild(node);
                    fromConnection.AddChild(line1);
                    fromConnection.AddChild(line2);

                    ConnectionLine line = new ConnectionLine(node, graphicTerminal.ConnectionNode);
                    fromConnection.AddChild(line);
                }

                fromConnection.ConnectTerminal(graphicTerminal);
                m_Editor.UpdateDrawing();
                m_Editor.RaiseChangedEvent();
            }
            if (m_FromElement is ConnectionNode)
            {
                m_FromElement.Location = ForceOrthogonality(graphicTerminal.ConnectionNode.Location, m_FromElement.Location);
                ConnectionLine line = new ConnectionLine(m_FromElement as ConnectionNode, graphicTerminal.ConnectionNode);
                m_FromElement.Parent.AddChild(line);

                (m_FromElement.Parent as GraphicConnection).ConnectTerminal(graphicTerminal);
                m_Editor.UpdateDrawing();
                m_Editor.RaiseChangedEvent();
            }
            m_FromElement = null;
            m_Editor.Invalidate();
        }

        /// <summary>
        /// Merges two connection objects into one
        /// </summary>
        /// <param name="fromConnection">the remaining connection</param>
        /// <param name="toConnection">the connection that gets merged</param>
        private void MergeConnections(GraphicConnection fromConnection, PointF fromPoint, GraphicConnection toConnection, PointF toPoint)
        {
            if (fromConnection.Equals(toConnection))
            {
                return;
            }
            if (MessageBox.Show(String.Format("Sollen die Verbindungen \"{0}\" und \"{1}\" miteinander verbunden werden?",
                fromConnection.Name, toConnection.Name), "Frage", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //derive all Terminals and ConnectionItems (lines, nodes)
                fromConnection.Merge(toConnection);

                IConnectionItem item1 = fromConnection.GetItemAt(fromPoint);
                IConnectionItem item2 = fromConnection.GetItemAt(toPoint);
                if (item1 is ConnectionLine)
                {
                    item1 = SplitConnectionLine(item1 as ConnectionLine, fromPoint);
                }
                if (item2 is ConnectionLine)
                {
                    item2 = SplitConnectionLine(item2 as ConnectionLine, fromPoint);
                }
                ConnectionLine line = new ConnectionLine(item1 as ConnectionNode, item2 as ConnectionNode);
                fromConnection.AddChild(line);

                m_Editor.RemoveElement(toConnection);
                m_Editor.UpdateDrawing();
                m_Editor.RaiseChangedEvent();
            }
        }

        /// <summary>
        /// Highlights an  element
        /// </summary>
        /// <param name="element">element to highlight</param>
        private void HighlightElement(GraphicBaseElement element)
        {
            if (element != null)
            {
                if (!element.Highlighted)
                {
                    m_HighLightElement = element;
                    m_HighLightElement.Highlighted = true;
                    m_Editor.UpdateBackground();
                    m_Editor.Invalidate();
                }
                else
                {
                    m_HighLightElement = element;
                }
            }
        }

        /// <summary>
        /// Removes the highlight from an element
        /// </summary>
        /// <param name="element">element that has a highlight</param>
        private void UnHighlightElement(GraphicBaseElement element)
        {
            if (element != null && element.Highlighted)
            {
                m_ParentElement = null;
                m_HighLightElement = null;
                element.Highlighted = false;
                m_Editor.UpdateBackground();
                m_Editor.Invalidate();
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    class GraphicConnection : GraphicBaseElement
    {
        #region Events

        public override event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        #region Construction

        internal GraphicConnection(BaseElement linkedObject)
            : base(linkedObject)
        {
            Connection connection = (Connection)m_LinkedObject;
            if (connection.Terminals != null && connection.Terminals.Count > 0)
            {
                foreach (Terminal terminal in connection.Terminals)
                {
                    (terminal.LinkedObject as GraphicTerminal).OnLocationChanged += new LocationChangeEvent(GraphicTerminal_OnLocationChanged);
                    //new NotifyEvent(GraphicTerminal_OnLocationChanged);
                }
            }
            connection.OnStateChanged += new DigitalClasses.Events.NotifyEvent(connection_OnStateChanged);
            connection.OnTerminalCountChanged += new NotifyEvent(Connection_OnTerminalCountChanged);
            BuildBody();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Connects a Terminal to this Connection
        /// </summary>
        /// <param name="graphicTerminal">the Terminal to connect</param>
        public void ConnectTerminal(GraphicTerminal graphicTerminal)
        {
            Terminal terminal = graphicTerminal.LinkedObject as Terminal;
            Connection connection = LinkedObject as Connection;
            connection.ConnectTerminal(terminal);
            graphicTerminal.OnLocationChanged += new LocationChangeEvent(GraphicTerminal_OnLocationChanged);
            //new NotifyEvent(GraphicTerminal_OnLocationChanged);
            //BuildBody();
        }

        /// <summary>
        /// Disconnects a Terminal from this Connection
        /// </summary>
        /// <param name="graphicTerminal">the Terminal to disconnect</param>
        public void DisconnectTerminal(GraphicTerminal graphicTerminal)
        {
            Terminal terminal = graphicTerminal.LinkedObject as Terminal;
            Connection connection = LinkedObject as Connection;
            connection.DisconnectTerminal(terminal);
            graphicTerminal.OnLocationChanged -= GraphicTerminal_OnLocationChanged;
            //BuildBody();
        }

        /// <summary>
        /// Merges all connected Terminals of the given connection to this one
        /// </summary>
        /// <param name="graphicConnection">connection to derive connection items from</param>
        internal void Merge(GraphicConnection graphicConnection)
        {
            Connection otherConnection = graphicConnection.LinkedObject as Connection;
            Connection connection = LinkedObject as Connection;
            foreach (Terminal terminal in otherConnection.Terminals)
            {
                otherConnection.DisconnectTerminal(terminal);
                connection.ConnectTerminal(terminal);
            }
            //clone all nodes of the other connection
            foreach (GraphicBaseElement child in graphicConnection.Children)
            {
                if (child is ConnectionNode)
                {
                    ConnectionNode clone = new ConnectionNode(child.Location);
                    AddChild(clone);
                }
            }
            //clone all lines of the other connection attached to the previously cloned nodes
            foreach (GraphicBaseElement child in graphicConnection.Children)
            {
                if (child is ConnectionLine)
                {
                    ConnectionLine orig = child as ConnectionLine;
                    ConnectionNode node1, node2;
                    //select cloned nodes or terminal nodes
                    if (orig.Nodes[0].Parent is GraphicConnection)
                    {
                        node1 = GetItemAt(orig.Nodes[0].Location, typeof(ConnectionNode)) as ConnectionNode;
                    }
                    else
                    {
                        node1 = orig.Nodes[0];
                    }
                    if (orig.Nodes[1].Parent is GraphicConnection)
                    {
                        node2 = GetItemAt(orig.Nodes[1].Location, typeof(ConnectionNode)) as ConnectionNode;
                    }
                    else
                    {
                        node2 = orig.Nodes[1];
                    }
                    if (node1 != null && node2 != null)
                    {
                        graphicConnection.RemoveChild(child);
                        ConnectionLine clone = new ConnectionLine(node1, node2);
                        AddChild(clone);
                    }
                }
            }
            foreach (GraphicBaseElement child in graphicConnection.Children)
            {
                graphicConnection.RemoveChild(child);
            }
            BuildBody();
        }

        /// <summary>
        /// Checks whether the given location is part of this connection
        /// </summary>
        /// <param name="location">the location of interest</param>
        /// <returns>true if object is near the location</returns>
        internal bool BelongsTo(PointF location)
        {
            if (m_Children == null)
            {
                return false;
            }
            foreach (IConnectionItem item in m_Children)
            {
                if (item.IsNear(location))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the item that is near the location
        /// </summary>
        /// <param name="location">the location of interest</param>
        /// <returns>the item near the location</returns>
        internal IConnectionItem GetItemAt(PointF location)
        {
            if (m_Children == null)
            {
                return null;
            }
            //nodes and lines share some space, so return nodes first
            foreach (IConnectionItem item in m_Children)
            {
                if (item.IsNear(location) && item is ConnectionNode)
                    return item;
            }
            //only return lines if no node is near location
            foreach (IConnectionItem item in m_Children)
            {
                if (item.IsNear(location) && item is ConnectionLine)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// Returns the item of a desired type that is near the location
        /// </summary>
        /// <param name="location">the location of interest</param>
        /// <param name="type">the type the item has to be</param>
        /// <returns>the item near the location</returns>
        internal IConnectionItem GetItemAt(PointF location, Type type)
        {
            if (m_Children == null)
            {
                return null;
            }
            //nodes and lines share some space, so return nodes first
            foreach (IConnectionItem item in m_Children)
            {
                if (item.IsNear(location) && item.GetType().Equals(type))
                    return item;
            }
            return null;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Event handler on location change of a terminal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicTerminal_OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            //BuildBody();
            if (OnUpdateDrawing != null)
            {
                OnUpdateDrawing(this, false);
            }
        }

        /// <summary>
        /// Event handler on state change of linked connection object
        /// </summary>
        /// <param name="sender"></param>
        private void connection_OnStateChanged(object sender)
        {
            if (OnUpdateDrawing != null)
            {
                OnUpdateDrawing(this, false);
            }
        }

        /// <summary>
        /// Event handler on change of the terminal count of linked connection object
        /// </summary>
        /// <param name="sender"></param>
        void Connection_OnTerminalCountChanged(object sender)
        {
            BuildBody();
        }

        /// <summary>
        /// Change color of the shape based on the state
        /// </summary>
        /// <param name="state">the corresponding state</param>
        private void ChangeColor(State state)
        {
            Color color;
            if (state.Equals(State.High))
            {
                //m_Body.Color = GraphicConstants.StateHighColor;
                color = GraphicConstants.StateHighColor;
            }
            else
            {
                //m_Body.Color = GraphicConstants.StateLowColor;
                color = GraphicConstants.StateLowColor;
            }
            if (m_Children != null)
                foreach (IConnectionItem item in m_Children)
                {
                    item.Color = color;
                }
        }

        /// <summary>
        /// Build the body of this Connection
        /// </summary>
        private void BuildBody()
        {
            //TODO: build up lines routed from terminal to terminal
            //Connection connection = LinkedObject as Connection;
            //List<Terminal> terminals = connection.Terminals;
            //if (m_Children != null)
            //{
            //    m_Children.Clear();
            //}


            m_Body = new GraphicShape(); //dummy
        }
        #endregion

        #region Overrides

        internal override void AddChild(GraphicBaseElement child)
        {
            base.AddChild(child);
            m_Children.Sort(delegate(GraphicBaseElement a, GraphicBaseElement b)
            {
                //Lines are 'greater' than Nodes
                if (a is ConnectionNode)
                {
                    if (b is ConnectionNode)
                    {
                        return 0;
                    }
                    return -1;
                }
                else if (b is ConnectionNode)
                {
                    return 1;
                }
                return 0;
            });
        }

        internal override void RemoveChild(GraphicBaseElement child)
        {
            base.RemoveChild(child);
            if (child is ConnectionLine)
            {
                ConnectionLine line = child as ConnectionLine;
                line.DetachNodes();
            }
        }

        public override void Paint(Graphics graphics)
        {
            Connection connection = m_LinkedObject as Connection;
            ChangeColor(connection.State);
            base.Paint(graphics);
        }

        public override void PaintBackground(Graphics graphics)
        {
            if (m_Highlighted && m_Children != null && m_Children.Count > 0)
            {
                foreach (GraphicBaseElement gbe in m_Children)
                {
                    gbe.PaintBackground(graphics);
                }
            }
        }

        public override void Update()
        {
            (LinkedObject as Connection).Update();
        }

        #endregion
    }
}

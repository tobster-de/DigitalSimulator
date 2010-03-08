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
    public class DisconnectionTool : CircuitTool
    {
        #region Fields

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
                return;
            }
            if (element is GraphicInputOutputElement == false && element is GraphicConnection == false)
            {
                return;
            }
            GraphicConnection graphicConnection = null;
            //user clicked a terminal of an element
            GraphicInputOutputElement ioelem = element as GraphicInputOutputElement;
            if (ioelem != null)
            {
                GraphicTerminal graphicTerminal = ioelem.GetTerminalAt(location);
                if (graphicTerminal != null)
                {
                    Terminal terminal = (graphicTerminal.LinkedObject as Terminal);
                    if (terminal.IsConnected)
                    {
                        Connection connection = terminal.Connection;
                        graphicConnection = terminal.Connection.LinkedObject as GraphicConnection;
                        graphicConnection.RemoveChild(graphicTerminal.ConnectionNode.Lines[0]);

                        //terminal.Disconnect();
                        connection.DisconnectTerminal(terminal);
                        if (connection.Terminals.Count == 0)
                        {
                            m_Editor.RemoveElement(connection.LinkedObject as GraphicConnection);
                        }
                        m_Editor.UpdateDrawing();
                        m_Editor.RaiseChangedEvent();
                        m_Editor.Invalidate();
                    }
                }
            }
            //user clicked a connection
            graphicConnection = element as GraphicConnection;
            if (graphicConnection != null)
            {
                foreach (GraphicBaseElement child in graphicConnection.Children)
                {
                    graphicConnection.RemoveChild(child);
                }
                Connection connection = graphicConnection.LinkedObject as Connection;
                foreach (Terminal terminal in connection.Terminals)
                {
                    //terminal.Disconnect();
                    connection.DisconnectTerminal(terminal);
                }
                m_Editor.RemoveElement(graphicConnection);
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
                    //Hover an element
                    m_ParentElement = element;
                    HighlightElement(graphicTerminal);
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

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
    public class ElementDeletionTool : CircuitTool
    {
        #region Fields

        protected GraphicBaseElement m_SelectedElement;
        protected GraphicBaseElement m_HighLightElement;

        #endregion

        #region Private Implementation

        private void DeleteElement(GraphicBaseElement element)
        {
            if (m_Editor == null)
            {
                return;
            }
            if (element is GraphicInputOutputElement)
            {
                //disconnect the element from all all connections
                InputOutputElement logicIO = (element.LinkedObject as InputOutputElement);
                foreach (Terminal terminal in logicIO.Inputs)
                {
                    if (terminal.IsConnected == false)
                    {
                        continue;
                    }
                    Connection connection = terminal.Connection;
                    //terminal.Disconnect();
                    connection.DisconnectTerminal(terminal);
                    if (connection.Terminals.Count == 0)
                    {
                        m_Editor.RemoveElement(connection.LinkedObject as GraphicConnection);
                    }
                }
                foreach (Terminal terminal in logicIO.Outputs)
                {
                    if (terminal.IsConnected == false)
                    {
                        continue;
                    }
                    Connection connection = terminal.Connection;
                    terminal.Disconnect();
                    if (connection.Terminals.Count == 0)
                    {
                        m_Editor.RemoveElement(connection.LinkedObject as GraphicConnection);
                    }
                }
                //delete the element
                m_Editor.RemoveElement(element);
            }
        }
        
        #endregion

        #region Overrides

        public override void MouseClick(PointF location, Keys controlKeys)
        {
            if (m_Editor == null)
            {
                return;
            }
            GraphicBaseElement element = m_Editor.GetElementAt(location);
            if (element != null)
            {
                DeleteElement(element);
            }
        }

        public override void MouseDown(PointF location, Keys controlKeys)
        {
            if (m_Editor == null)
            {
                return;
            }
            GraphicBaseElement element = m_Editor.GetElementAt(location);
            if (element != null)
            {
                //Select the element
                m_SelectedElement = element;
            }
        }

        public override void MouseUp(PointF location, Keys controlKeys)
        {
            if (m_Editor == null)
            {
                return;
            }
            if (m_SelectedElement != null)
            {
                DeleteElement(m_SelectedElement);
            }
            m_SelectedElement = null;
        }

        public override void MouseMove(MouseButtons button, PointF location, Keys controlKeys)
        {
            if (m_Editor == null)
            {
                return;
            }
            GraphicBaseElement element = m_Editor.GetElementAt(location);
            if (element != m_SelectedElement && element is GraphicInputOutputElement)
            {
                m_SelectedElement = element;
            }
            if (m_HighLightElement != null && !m_HighLightElement.Equals(element) && m_HighLightElement.Highlighted)
            {
                //Un-Hover the element
                m_HighLightElement.Highlighted = false;
                m_HighLightElement = null;
                m_Editor.UpdateBackground();
                m_Editor.Invalidate();
            }
            if (element != null && !element.Highlighted && element is GraphicInputOutputElement)
            {
                //Hover an element
                m_HighLightElement = element;
                m_HighLightElement.Highlighted = true;
                m_Editor.UpdateBackground();
                m_Editor.Invalidate();
            }

        }

        public override void Reset()
        {
            m_SelectedElement = null;
        }

        #endregion
    }
}

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
    public class GateTool : CircuitTool
    {
        #region Fields

        private GraphicBaseElement m_GraphicElement;

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Element">The new Element to be placed</param>
        public GateTool(GraphicBaseElement Element)
        {
            m_GraphicElement = Element;
        }

        #endregion

        #region Overrides

        public override void MouseClick(PointF location, Keys controlKeys)
        {
            if (m_Editor != null)
            {
                if (m_Editor.GetElementAt(location) == null)
                {
                    GraphicBaseElement element = GraphicObjectFactory.CreateClone(m_GraphicElement);
                    element.Name = UniqueName.GetUniqueName(m_Editor.Circuit, m_GraphicElement.LinkedObject.GetType());
                    if (element is GraphicInput)
                    {
                        (element as GraphicInput).SignalName = UniqueName.GetUniqueSignalName(m_Editor.Circuit);
                    }
                    if (element is GraphicOutput)
                    {
                        (element.LinkedObject as SignalOutput).SignalName = UniqueName.GetUniqueSignalName(m_Editor.Circuit);
                    }
                    element.Location = m_Editor.AlignToGrid(location);
                    m_Editor.AddElement(element);
                }
            }
        }

        #endregion
    }
}

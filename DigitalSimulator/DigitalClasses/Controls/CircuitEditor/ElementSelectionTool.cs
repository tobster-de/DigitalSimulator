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
    public class ElementSelectionTool : CircuitTool
    {
        #region Events

        public event ElementSelected OnElementSelected;

        #endregion

        #region Fields

        protected GraphicBaseElement m_HighLightElement;
        protected GraphicBaseElement m_SelectedElement;
        protected PointF m_SelectionOffset;

        #endregion

        #region Private Implementation

        private void SelectElementAt(PointF location)
        {
            if (m_Editor != null)
            {
                GraphicBaseElement element = m_Editor.GetElementAt(location);
                if (element != null)
                {
                    //Select the element
                    m_SelectedElement = element;
                    m_SelectionOffset = new PointF(location.X - m_SelectedElement.Location.X, location.Y - m_SelectedElement.Location.Y);
                    if (OnElementSelected != null)
                    {
                        OnElementSelected(this, new ElementSelectedEventArgs(element));
                    }
                }
            }
        }

        #endregion

        #region Overrides

        public override void MouseClick(PointF location, Keys controlKeys)
        {
            SelectElementAt(location);
        }

        public override void MouseDown(PointF location, Keys controlKeys)
        {
            SelectElementAt(location);
        }

        public override void MouseUp(PointF location, Keys controlKeys)
        {
            m_SelectedElement = null;
        }

        public override void MouseMove(MouseButtons button, PointF location, Keys controlKeys)
        {
            if (m_Editor == null)
            {
                return;
            }
            if (button == MouseButtons.Left && m_SelectedElement != null && m_SelectedElement is GraphicConnection == false)
            {

                PointF newloc = m_Editor.AlignToGrid(new PointF(location.X - m_SelectionOffset.X, location.Y - m_SelectionOffset.Y));
                if (newloc.Equals(m_SelectedElement.Location) == false)
                {
                    //m_HighLightElement.Highlighted = false;
                    m_SelectedElement.Location = newloc;
                    m_Editor.RaiseChangedEvent();
                    m_Editor.UpdateBackground();
                    m_Editor.UpdateDrawing();
                    m_Editor.Invalidate();
                }
                return;
            }
            GraphicBaseElement element = m_Editor.GetElementAt(location);
            if (m_HighLightElement != null && !m_HighLightElement.Equals(element) && m_HighLightElement.Highlighted)
            {
                //Un-Hover the element
                m_HighLightElement.Highlighted = false;
                m_HighLightElement = null;
                m_Editor.UpdateBackground();
                m_Editor.Invalidate();
            }
            if (element != null && !element.Highlighted)
            {
                //Hover an element
                m_HighLightElement = element;
                m_HighLightElement.Highlighted = true;
                m_Editor.UpdateBackground();
                m_Editor.Invalidate();
            }
        }

        #endregion
    }
}

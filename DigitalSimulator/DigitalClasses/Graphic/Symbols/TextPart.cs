using System;
using System.Collections.Generic;
using System.Drawing;
using ToolBox;
using DigitalClasses.Graphic;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic.Symbols
{
    public class TextPart : SymbolPart
    {
        #region Fields

        PointF m_Location;
        string m_Text;
        SizeF m_Size;

        #endregion

        #region Events

        public override event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        #region Properties

        public override PointF Location
        {
            get
            {
                return m_Location;
            }
            set
            {
                m_Location = value;
            }
        }

        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                if (OnUpdateDrawing != null)
                {
                    OnUpdateDrawing(this, false);
                }
            }
        }

        #endregion

        public TextPart(PointF location, string text)
        {
            m_Location = location;
            m_Text = text;
        }

        public override bool IsNear(PointF location)
        {
            if (m_Size != null)
            {
                RectangleF rect = new RectangleF(m_Location, m_Size);
                return rect.Contains(location);
            }
            Vector origin = new Vector(m_Location.X, m_Location.Y);
            Vector vloca = new Vector(location.X, location.Y);
            double distance = (origin - vloca).Length;
            return distance < 5;
        }

        public override GrabHandle GetGrabHandle(PointF offset)
        {
            return null;
        }

        public override void DrawPart(Graphics g, PointF offset)
        {
            Font font = new Font("Arial", 10);
            g.DrawString(m_Text, font, new SolidBrush(Color.Black), DoOffset(m_Location, offset));
            m_Size = g.MeasureString(m_Text, font);
        }

        public override void DrawSelection(Graphics g, PointF offset)
        {
            Font font = new Font("Arial", 10);
            m_Size = g.MeasureString(m_Text, font);
            RectangleF rect = new RectangleF(DoOffset(m_Location, offset), m_Size);
            g.FillRectangle(GraphicConstants.HighlightBrush, rect);
        }
    }
}

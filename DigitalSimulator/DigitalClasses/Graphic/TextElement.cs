using System;
using System.Collections.Generic;
using System.Drawing;

namespace DigitalClasses.Graphic
{
    /// <summary>
    /// This class is used to display some text within an element
    /// </summary>
    internal class TextElement : GraphicBaseElement
    {
        #region Fields

        private string m_Text;
        private SizeF m_Size;
        private PointF m_Location;
        private string m_FontName;
        private int m_FontSize;
        private Font m_Font;

        #endregion

        #region Properties

        /// <summary>
        /// Text which is displayed in the Symbol
        /// </summary>
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
            }
        }

        /// <summary>
        /// The name of the font that is used to draw the text
        /// </summary>
        public string FontName
        {
            get
            {
                return m_FontName;
            }
            set
            {
                if (!m_FontName.Equals(value))
                {
                    m_FontName = value;
                    m_Font = new Font(m_FontName, m_FontSize);
                }
            }
        }

        /// <summary>
        /// The Size of the Font to be used to draw the text
        /// </summary>
        public int FontSize
        {
            get
            {
                return m_FontSize;
            }
            set
            {
                if (!m_FontSize.Equals(value))
                {
                    m_FontSize = value;
                    m_Font = new Font(m_FontName, m_FontSize);
                }
            }
        }

        /// <summary>
        /// Gets this object's dimensions.
        /// </summary>
        public override SizeF Size
        {
            get
            {
                return m_Size;
            }
        }

        /// <summary>
        /// Gets this elements position.
        /// </summary>
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

        /// <summary>
        /// Gets the bounding rectangle af this text
        /// To return a correct value, a TextElement needs to be drawn first.
        /// </summary>
        public override RectangleF Bounds
        {
            get
            {
                PointF location = m_Location;
                location.X += Parent.Location.X;
                location.Y += Parent.Location.Y;
                RectangleF result = new RectangleF(location, Size);

                return result;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Create a new TextElement
        /// </summary>
        /// <param name="text">the text that should be displayed</param>
        /// <param name="relativeLocation">relative location to the parent</param>
        internal TextElement(string text, PointF relativeLocation)
            : base()
        {
            m_Text = text;
            m_Size = new SizeF();
            m_Location = relativeLocation;
            m_FontName = "Arial";
            m_FontSize = 10;
            m_Font = new Font(m_FontName, m_FontSize);
        }

        #endregion

        #region Public Implementation

        #endregion

        #region Overrides

        public override void Paint(Graphics graphics)
        {
            m_Size = graphics.MeasureString(m_Text, m_Font);
            PointF location = m_Location;
            location.X += Parent.Location.X;
            location.Y += Parent.Location.Y;
            graphics.DrawString(Text, m_Font, new SolidBrush(GraphicConstants.DefaultColor), location);
        }

        /// <summary>
        /// No children are supported for TextElements
        /// </summary>
        /// <param name="child"></param>
        /// <exception cref="NotSupportedException"></exception>
        internal override void AddChild(GraphicBaseElement child)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// No children are supported for TextElements
        /// </summary>
        /// <param name="child"></param>
        /// <exception cref="NotSupportedException"></exception>
        internal override void RemoveChild(GraphicBaseElement child)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    /// <summary>
    /// This class is some special TextElement used for the name of an element
    /// </summary>
    internal class NameTextElement : TextElement
    {
        #region Construction

        /// <summary>
        /// Create a new NameTextElement
        /// </summary>
        /// <param name="text">the text that should be displayed</param>
        /// <param name="relativeLocation">relative location to the parent</param>
        internal NameTextElement(string text, PointF relativeLocation)
            : base(text, relativeLocation)
        {
        }

        #endregion
    }
}

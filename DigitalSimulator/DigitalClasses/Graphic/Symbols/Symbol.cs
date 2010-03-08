using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace DigitalClasses.Graphic.Symbols
{
    /// <summary>
    /// This class holds data representing symbols for macros
    /// </summary>
    public class Symbol : IEnumerable
    {
        #region Fields

        private List<SymbolPart> m_Parts;
        private string m_Name;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the count of the parts this symbol contains of
        /// </summary>
        public int PartCount
        {
            get
            {
                return m_Parts.Count;
            }
        }

        /// <summary>
        /// The name of this symbol
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        #endregion

        #region Constructor

        public Symbol()
        {
            m_Parts = new List<SymbolPart>();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Adds a part to this symbol
        /// </summary>
        /// <param name="part">Part to add</param>
        public virtual void AddPart(SymbolPart part)
        {
            if (m_Parts.Contains(part) == false)
            {
                m_Parts.Add(part);
            }
        }

        /// <summary>
        /// Removes an Part from this container
        /// </summary>
        /// <param name="part">the Part to remove</param>
        /// <returns>true when removing was successful</returns>
        public virtual bool RemovePart(SymbolPart part)
        {
            return m_Parts.Remove(part);
        }

        /// <summary>
        /// Checks whether an Part is part of this container
        /// </summary>
        /// <param name="part">the Part to check</param>
        /// <returns>true if the Part is contained</returns>
        public virtual bool ContainsPart(SymbolPart part)
        {
            return m_Parts.Contains(part);
        }

        /// <summary>
        /// Generates a GraphicShape based on the symbol data
        /// </summary>
        /// <returns></returns>
        public GraphicShape GenerateShape()
        {
            GraphicsPath path = new GraphicsPath();
            foreach (SymbolPart part in m_Parts)
            {
                if (part is LinePart)
                {
                    LinePart line = (LinePart)part;
                    path.StartFigure();
                    path.AddLine(line.GetPoint(0), line.GetPoint(1));
                }
                if (part is RectanglePart)
                {
                    RectanglePart rect = (RectanglePart)part;
                    path.StartFigure();
                    path.AddRectangle(new RectangleF(rect.Location.X, rect.Location.Y, rect.Width, rect.Height));
                }
            }
            return new GraphicShape(path);
        }

        #endregion

        #region IEnumerable Member

        public IEnumerator GetEnumerator()
        {
            return m_Parts.GetEnumerator();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// Represents data about a rectangular part of a symbol
    /// </summary>
    public class SymbolRectData : SymbolPartData
    {
        /// <summary>
        /// X co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("width")]
        public float Width;

        /// <summary>
        /// Y co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("height")]
        public float Height;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SymbolRectData()
        {

        }
        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="x">X co-ordinate</param>
        /// <param name="y">Y co-ordinate</param>
        /// <param name="height">height of the rectangle</param>
        /// <param name="width">width of the recangle</param>
        public SymbolRectData(float x, float y, float width, float height)
            : base(x, y)
        {
            Width = width;
            Height = height;
        }
    }
}

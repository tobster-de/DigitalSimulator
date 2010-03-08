using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// Represents data about terminals of a symbol
    /// </summary>
    public class SymbolPartData
    {
        /// <summary>
        /// X co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("X")]
        public float X;

        /// <summary>
        /// Y co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("Y")]
        public float Y;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SymbolPartData()
        {

        }
        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="x">X coordinate value.</param>
        /// <param name="y">Y coordinate value.</param>
        public SymbolPartData(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}

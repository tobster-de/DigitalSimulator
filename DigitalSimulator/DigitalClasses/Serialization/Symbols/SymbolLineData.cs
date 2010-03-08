using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// Represents data about terminals of a symbol
    /// </summary>
    public class SymbolLineData : SymbolPartData
    {
        /// <summary>
        /// X co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("X2")]
        public float X2;

        /// <summary>
        /// Y co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("Y2")]
        public float Y2;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SymbolLineData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="x1">first X co-ordinate</param>
        /// <param name="y1">first Y co-ordinate</param>
        /// <param name="x2">second X co-ordinate</param>
        /// <param name="y2">second Y co-ordinate</param>
        public SymbolLineData(float x1, float y1, float x2, float y2)
            : base(x1, y1)
        {
            X2 = x2;
            Y2 = y2;
        }
    }
}

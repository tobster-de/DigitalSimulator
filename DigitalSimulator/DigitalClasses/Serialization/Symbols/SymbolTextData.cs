using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// Represents data about terminals of a symbol
    /// </summary>
    public class SymbolTextData : SymbolPartData
    {
        /// <summary>
        /// Angle of the terminal
        /// </summary>
        [XmlAttribute("text")]
        public string Text;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SymbolTextData()
        {

        }
        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="x">X co-ordinate value</param>
        /// <param name="y">Y co-ordinate value</param>
        /// <param name="text">Displayed text</param>
        public SymbolTextData(float x, float y, string text):
            base(x,y)
        {
            Text = text;
        }
    }
}

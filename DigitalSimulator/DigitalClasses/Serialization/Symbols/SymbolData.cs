using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    public class SymbolData
    {
        /// <summary>
        /// Name of the symbol
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        ///// <summary>
        ///// Width of symbol
        ///// </summary>
        //[XmlAttribute("width")]
        //public float Width;

        ///// <summary>
        ///// Height of symbol
        ///// </summary>
        //[XmlAttribute("height")]
        //public float Height;

        /// <summary>
        /// Array of terminals present in the given symbol
        /// </summary>
        [XmlArray("Parts")]
        [XmlArrayItem("Port", Type = typeof(SymbolPortData)),
         XmlArrayItem("Text", Type = typeof(SymbolTextData)),
         XmlArrayItem("Rectangle", Type = typeof(SymbolRectData)),
         XmlArrayItem("Line", Type= typeof(SymbolLineData))]
        public SymbolPartData[] SymbolParts;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SymbolData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        //// <param name="width">Width of Symbol.</param>
        //// <param name="height">Height of the Symbol.</param>
        public SymbolData(string name/*, float width, float height*/)
        {
            Name = name;
            //Width = width;
            //Height = height;
        }
    }
}

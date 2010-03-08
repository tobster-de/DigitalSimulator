using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// Data class for macro elements within circuits
    /// </summary>
    public class MacroElementData : IOElementData
    {
        /// <summary>
        /// References to the filename of the macro
        /// </summary>
        [XmlAttribute("reference")]
        public string Reference;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public MacroElementData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the macro</param>
        /// <param name="type">Type name of the macro</param>
        /// <param name="reference">Reference file name</param>
        public MacroElementData(string name, string type, string reference)
            : base(name, type)
        {
            Reference = reference;
        }
    }
}

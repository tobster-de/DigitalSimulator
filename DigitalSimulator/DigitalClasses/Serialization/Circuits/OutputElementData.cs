using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    public class OutputElementData : BaseElementData
    {
        /// <summary>
        /// Array of terminals in input direction
        /// </summary>
        [XmlArray("Inputs", IsNullable = false)]
        [XmlArrayItem("Input")]
        public TerminalData[] Inputs;

        [XmlAttribute("SignalName")]
        public string SignalName;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public OutputElementData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        public OutputElementData(string name, string signalName)
            : base(name)
        {
            SignalName = signalName;
        }
    }
}

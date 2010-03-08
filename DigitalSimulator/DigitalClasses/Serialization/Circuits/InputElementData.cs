using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    public class InputElementData : BaseElementData
    {
        /// <summary>
        /// Array of terminals in output direction
        /// </summary>
        [XmlArray("Outputs", IsNullable = false)]
        [XmlArrayItem("Output")]
        public TerminalData[] Outputs;

        [XmlAttribute("Signal")]
        public string SignalName;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public InputElementData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        /// <param name="signalName">name of the signal of this element</param>
        public InputElementData(string name, string signalName)
            : base(name)
        {
            SignalName = signalName;
        }
    }
}

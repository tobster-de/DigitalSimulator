using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    public class IOElementData : BaseElementData
    {
        /// <summary>
        /// Type of the element
        /// </summary>
        [XmlAttribute("type")]
        public string Type;

        /// <summary>
        /// Index of the element in processing order
        /// </summary>
        [XmlAttribute("index")]
        public int Index;

        /// <summary>
        /// Unit delay of this element
        /// </summary>
        [XmlAttribute("unitdelay")]
        public int UnitDelay;

        /// <summary>
        /// Positive edge delay of this element
        /// </summary>
        [XmlAttribute("posedge")]
        public int PositiveEdgeDelay;

        /// <summary>
        /// Negative edge delay of this element
        /// </summary>
        [XmlAttribute("negedge")]
        public int NegativeEdgeDelay;

        /// <summary>
        /// Array of terminals in input direction
        /// </summary>
        [XmlArray("Inputs", IsNullable = false)]
        [XmlArrayItem("Input")]
        public TerminalData[] Inputs;

        /// <summary>
        /// Array of terminals in output direction
        /// </summary>
        [XmlArray("Outputs", IsNullable = false)]
        [XmlArrayItem("Output")]
        public TerminalData[] Outputs;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public IOElementData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">name of the element.</param>
        /// <param name="type">type name of the element</param>
        public IOElementData(string name, string type)
            : base(name)
        {
            Type = type;
        }
    }
}

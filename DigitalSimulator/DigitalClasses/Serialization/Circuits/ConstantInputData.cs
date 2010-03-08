using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    public class ConstantInputData : BaseElementData
    {
        /// <summary>
        /// Array of terminals in output direction
        /// </summary>
        [XmlArray("Outputs", IsNullable = false)]
        [XmlArrayItem("Output")]
        public TerminalData[] Outputs;

        /// <summary>
        /// constant state of the input
        /// </summary>
        [XmlAttribute("State")]
        public string State;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public ConstantInputData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the element</param>
        /// <param name="state">constant state of the input</param>
        public ConstantInputData(string name, string state)
            : base(name)
        {
            State = state;
        }
    }
}

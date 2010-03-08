using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    public class CircuitData
    {
        /// <summary>
        /// Name of the circuit
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        /// <summary>
        /// Array of elements present in the circuit
        /// </summary>
        [XmlArray("Elements")]
        [XmlArrayItem("IOElement", Type = typeof(IOElementData)),
         XmlArrayItem("Clock", Type = typeof(ClockData)),
         XmlArrayItem("InputElement", Type = typeof(InputElementData)),
         XmlArrayItem("OutputElement", Type = typeof(OutputElementData)),
         XmlArrayItem("ConstantInput", Type = typeof(ConstantInputData)),
         XmlArrayItem("MacroElement", Type = typeof(MacroElementData))]
        public BaseElementData[] Elements;

        /// <summary>
        /// Array of connections present in the circuit
        /// </summary>
        [XmlArray("Connections")]
        [XmlArrayItem("Connection")]
        public ConnectionData[] Connections;

        /// <summary>
        /// Array of signals used for simulation
        /// </summary>
        [XmlArray("Signals", IsNullable = false)]
        [XmlArrayItem("Signal")]
        public SignalData[] Signals;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public CircuitData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        public CircuitData(string name)
        {
            Name = name;
        }
    }
}

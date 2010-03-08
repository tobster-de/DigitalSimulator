using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class represents a macro
    /// </summary>
    public class MacroData
    {
        /// <summary>
        /// Name of the circuit
        /// </summary>
        [XmlElement("Name")]
        public string Name;

        /// <summary>
        /// Array of terminals present in the given symbol
        /// </summary>
        [XmlElement("Circuit")]
        public CircuitData Circuit;

        /// <summary>
        /// Array of connections present in the given symbol
        /// </summary>
        [XmlElement("Symbol")]
        public SymbolData Symbol;

        /// <summary>
        /// Array of signals used for simulation
        /// </summary>
        [XmlArray("PortMatching", IsNullable = false)]
        [XmlArrayItem("Matching")]
        public MatchingData[] Matching;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public MacroData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        public MacroData(string name)
        {
            Name = name;
        }
    }
}

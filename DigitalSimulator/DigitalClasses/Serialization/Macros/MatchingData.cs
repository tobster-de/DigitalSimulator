using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class matches names of IO elements to ports of a symbol to create a macro
    /// </summary>
    public class MatchingData
    {
        /// <summary>
        /// Name of the port of the symbol
        /// </summary>
        [XmlAttribute("portname")]
        public string PortName;

        /// <summary>
        /// Name of the IOElement within the circuit
        /// </summary>
        [XmlAttribute("ioelement")]
        public string IOElementName;

        /// <summary>
        /// The direction of the terminal
        /// </summary>
        [XmlAttribute("direction")]
        public DirectionType Direction;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public MatchingData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="portname">Name of the port</param>
        /// <param name="direction">Direction of the port</param>
        /// <param name="ioelement">Name of the element</param>
        public MatchingData(string portname, string ioelement, DirectionType direction)
        {
            PortName = portname;
            IOElementName = ioelement;
            Direction = direction;
        }
    }
}

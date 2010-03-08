using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// Represents data about ports of a symbol
    /// </summary>
    public class SymbolPortData : SymbolPartData
    {
        /// <summary>
        /// Name of the port
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        /// <summary>
        /// Angle of the port
        /// </summary>
        [XmlAttribute("angle")]
        public float Angle;

        /// <summary>
        /// The type of Direction of the symbol port
        /// </summary>
        [XmlAttribute("direction")]
        public DirectionType Direction;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SymbolPortData()
        {

        }
        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="x">
        /// X co-ordinate value.
        /// </param>
        /// <param name="y">
        /// Y co-ordinate value.
        /// </param>
        /// <param name="angle">angle of the port</param>
        /// <param name="direction">direction of the port</param>
        /// <param name="name">name of the port</param>
        public SymbolPortData(float x, float y, float angle, DirectionType direction, string name)
            : base(x, y)
        {
            Angle = angle;
            Direction = direction;
            Name = name;
        }
    }
}

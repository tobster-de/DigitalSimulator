using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This is a serializable representation of a Terminal object
    /// </summary>
    public class TerminalData
    {
        /// <summary>
        /// Name of the terminal
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        /// <summary>
        /// Name of the connection the terminal is connected to
        /// </summary>
        [XmlAttribute("connection")]
        public string Connection;

        ///// <summary>
        ///// The index the terminal has
        ///// </summary>
        //[XmlAttribute("index")]
        //public int Index;

        /// <summary>
        /// The direction of the terminal
        /// </summary>
        [XmlAttribute("direction")]
        public DirectionType Direction;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public TerminalData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Terminal.</param>
        /// <param name="direction">Direction of the Terminal</param>
        public TerminalData(string name, DirectionType direction)
        {
            Name = name;
            Direction = direction;
            //Index = 0;
            Connection = String.Empty;
        }
    }
}

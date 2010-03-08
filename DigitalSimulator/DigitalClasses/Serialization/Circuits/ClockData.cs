using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    public class ClockData : BaseElementData
    {
        /// <summary>
        /// Array of terminals in output direction
        /// </summary>
        [XmlArray("Outputs", IsNullable = false)]
        [XmlArrayItem("Output")]
        public TerminalData[] Outputs;

        [XmlAttribute("lowduration")]
        public uint LowDuration;

        [XmlAttribute("highduration")]
        public uint HighDuration;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public ClockData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        /// <param name="highDuration">duration of high pulse</param>
        /// <param name="lowDuration">duration of low pulse</param>
        public ClockData(string name, uint lowDuration, uint highDuration)
            : base(name)
        {
            LowDuration = lowDuration;
            HighDuration = highDuration;
        }
    }
}

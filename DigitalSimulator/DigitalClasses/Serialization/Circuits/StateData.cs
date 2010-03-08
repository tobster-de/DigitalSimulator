using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This is a serializable representation of a State of a Signal
    /// </summary>
    public class StateData
    {
        /// <summary>
        /// Name of the connection
        /// </summary>
        [XmlAttribute("level")]
        public string Level;

        /// <summary>
        /// The signal data
        /// </summary>
        [XmlAttribute("duration")]
        public int Duration;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public StateData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="level"></param>
        /// <param name="duration"></param>
        public StateData(string level, int duration)
        {
            Level = level;
            Duration = duration;
        }
    }
}

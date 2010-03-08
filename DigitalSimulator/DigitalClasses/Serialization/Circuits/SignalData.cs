using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This is a serializable representation of a SignalList
    /// </summary>
    public class SignalData
    {
        /// <summary>
        /// Name of the connection
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        /// <summary>
        /// The signal data
        /// </summary>
        [XmlArray("States"),
         XmlArrayItem("State", typeof(StateData))]
        public StateData[] States;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public SignalData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Connection.</param>
        public SignalData(string name/*, string states*/)
        {
            Name = name;
            //States = states;
        }
    }
}

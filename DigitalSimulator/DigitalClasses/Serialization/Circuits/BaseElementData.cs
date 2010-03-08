using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    public abstract class BaseElementData
    {
        /// <summary>
        /// Name of the element
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        /// <summary>
        /// X co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("X")]
        public float X;

        /// <summary>
        /// Y co-ordinate value of Location.
        /// </summary>
        [XmlAttribute("Y")]
        public float Y;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public BaseElementData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        public BaseElementData(string name)
        {
            Name = name;
        }
    }
}

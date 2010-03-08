using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This is a serializable representation of a Connection object
    /// </summary>
    public class ConnectionData
    {
        /// <summary>
        /// Name of the connection
        /// </summary>
        [XmlAttribute("name")]
        public string Name;

        /// <summary>
        /// Array of terminals present in the given symbol
        /// </summary>
        [XmlArray("Items", IsNullable = false)]
        [XmlArrayItem("NodeItem", Type = typeof(NodeItemData)),
         XmlArrayItem("LineItem", Type = typeof(LineItemData))]
        public ConnectionItemData[] Items;

        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public ConnectionData()
        {

        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Connection.</param>
        public ConnectionData(string name)
        {
            Name = name;
        }
    }
}

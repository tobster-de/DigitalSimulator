using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    public class LineItemData : ConnectionItemData
    {
        /// <summary>
        /// Name of the first node
        /// </summary>
        [XmlAttribute("Node1")]
        public string Node1;
        
        /// <summary>
        /// Name of the second node
        /// </summary>
        [XmlAttribute("Node2")]
        public string Node2;
        
        /// <summary>
        /// Public Constructor for deserialization
        /// </summary>
        public LineItemData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization
        /// </summary>
        /// <param name="name">Name of the Symbol.</param>
        /// <param name="signalName">name of the signal of this element</param>
        public LineItemData(string node1, string node2)
        {
            Node1 = node1;
            Node2 = node2;
        }
    }
}

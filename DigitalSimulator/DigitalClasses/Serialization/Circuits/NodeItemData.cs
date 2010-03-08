using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    public class NodeItemData : ConnectionItemData
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
        public NodeItemData()
        {
        }

        /// <summary>
        /// Public Constructor used for serialization (general)
        /// </summary>
        /// <param name="name">name of the node</param>
        public NodeItemData(string name)
        {
            Name = name;
        }

        ///// <summary>
        ///// Public Constructor used for serialization (floating node)
        ///// </summary>
        ///// <param name="name">Name of the Symbol.</param>
        ///// <param name="x">x-coordinate</param>
        ///// <param name="y">y-coordinate</param>
        //public NodeItemData(float x, float y)
        //{
        //    Name = String.Format("{0:x4}{1:x4}", x, y);
        //    X = x;
        //    Y = y;
        //}

        ///// <summary>
        ///// Public Constructor used for serialization - (terminal noda)
        ///// </summary>
        ///// <param name="elementName"></param>
        ///// <param name="terminalName"></param>
        //public NodeItemData(string elementName, string terminalName)
        //{
        //    Name = String.Format("{0}/{1}", elementName, terminalName);
        //}
    }
}

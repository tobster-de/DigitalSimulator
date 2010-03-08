using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigitalClasses.Logic
{
    public enum DirectionType
    {
        [XmlEnum(Name="Input")]
        Input,
        [XmlEnum(Name = "Output")]
        Output
    }
    
}

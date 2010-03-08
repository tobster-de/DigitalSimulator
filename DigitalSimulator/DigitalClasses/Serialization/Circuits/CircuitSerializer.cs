using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using DigitalClasses.Logic;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class is a helper class responsible for serializing circuits
    /// </summary>
    public static class CircuitSerializer
    {
        /// <summary>
        /// Serializes a Circuits converted to CircuitData objects to a XML file
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <param name="circuitData">The CircuitData object to serialize</param>
        public static void SerializeCircuit(string fileName, CircuitData circuitData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CircuitData));
            FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, circuitData);
            fs.Close();
        }

        /// <summary>
        /// Deserializes a XML file to a CircuitData object, which can be converted to Circuits.
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <returns>The deserialized CircuitData object</returns>
        public static CircuitData DeserializeCircuit(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                return new CircuitData();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(CircuitData));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            CircuitData circuitData = (CircuitData)serializer.Deserialize(fs);
            fs.Close();
            return circuitData;
        }
    }
}

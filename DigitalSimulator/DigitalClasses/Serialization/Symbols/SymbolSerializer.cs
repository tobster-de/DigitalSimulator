using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class is a helper class responsible for serializing symbols
    /// </summary>
    public static class SymbolSerializer
    {
        /// <summary>
        /// Serializes a SymbolData object to a XML file
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <param name="symbolData">The symbol to serialize</param>
        public static void SerializeSymbol(string fileName, SymbolData symbolData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SymbolData));
            FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, symbolData);
            fs.Close();
        }

        /// <summary>
        /// Deserializes a XML file to a SymbolCollection
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <returns>The deserialized SymbolData object</returns>
        public static SymbolData DeserializeSymbol(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                return new SymbolData();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(SymbolData));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            SymbolData symbolData = (SymbolData)serializer.Deserialize(fs);
            fs.Close();
            return symbolData;
        }
    }
}

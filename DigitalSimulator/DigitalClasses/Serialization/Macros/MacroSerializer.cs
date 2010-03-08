using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class is a helper class responsible for serializing macros
    /// </summary>
    public static class MacroSerializer
    {
        /// <summary>
        /// Serializes a MacroData object to a XML file
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <param name="macroData">The Macro to serialize</param>
        public static void SerializeMacro(string fileName, MacroData macroData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MacroData));
            FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, macroData);
            fs.Close();
        }

        /// <summary>
        /// Deserializes a XML file to a MacroData
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <returns>The deserialized MacroData object</returns>
        public static MacroData DeserializeMacro(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                return new MacroData();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(MacroData));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            MacroData macroData = (MacroData)serializer.Deserialize(fs);
            fs.Close();
            return macroData;
        }
    }
}

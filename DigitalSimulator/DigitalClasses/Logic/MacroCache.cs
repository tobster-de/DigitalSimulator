using System;
using System.Collections.Generic;
using System.IO;
using DigitalClasses.Serialization;
using DigitalClasses.Graphic.Symbols;
using System.Windows.Forms;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// MacroCache holds macros once loaded
    /// </summary>
    public class MacroCache
    {
        #region Fields

        private static MacroCache m_Instance;
        private Dictionary<string, string> m_FileNames;
        private Dictionary<string, MacroData> m_Data;
        private Dictionary<string, Macro> m_Macros;
        private Dictionary<string, Symbol> m_Symbols;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the singleton instance of the macro cache
        /// </summary>
        public static MacroCache Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new MacroCache();
                }
                return m_Instance;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Private Constructor to create the singleton
        /// </summary>
        private MacroCache()
        {
            string path = Application.StartupPath + @"\Macros";
            //string path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + @"\Macros";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            m_Data = new Dictionary<string, MacroData>();
            m_FileNames = new Dictionary<string, string>();
            m_Macros = new Dictionary<string, Macro>();
            m_Symbols = new Dictionary<string, Symbol>();

            string[] files = Directory.GetFiles(path, @"*.xmac");
            foreach (string fileName in files)
            {
                LoadMacro(fileName);
            }
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Returns a list with the names of all macros contained
        /// </summary>
        /// <returns>list with the names of all macros contained</returns>
        public List<string> GetMacroNames()
        {
            List<string> result = new List<string>(m_Macros.Count);
            foreach (string key in m_Macros.Keys)
            {
                result.Add(key);
            }
            foreach (string key in m_Data.Keys)
            {
                result.Add(key);
            }
            return result;
        }

        /// <summary>
        /// Preloads data of a macro from a file. Returns the type name of the loaded macro on success.
        /// </summary>
        /// <param name="fileName">the name of the file</param>
        /// <returns>the typename of the macro if loading was successful</returns>
        public string LoadMacro(string fileName)
        {
            fileName = Application.StartupPath + @"\Macros\" + Path.GetFileName(fileName);
            if (File.Exists(fileName) == false)
            {
                return String.Empty;
            }
            MacroData macroData = MacroSerializer.DeserializeMacro(fileName);
            if (m_Data.ContainsKey(macroData.Name))
            {
                m_Data.Remove(macroData.Name);
            }
            if (m_FileNames.ContainsKey(macroData.Name))
            {
                m_FileNames.Remove(macroData.Name);
            }
            m_FileNames.Add(macroData.Name, fileName);
            m_Data.Add(macroData.Name, macroData);
            if (m_Macros.ContainsKey(macroData.Name))
            {
                //if it's already present, replace it with the new one
                m_Macros.Remove(macroData.Name);
                m_Symbols.Remove(macroData.Name);
            }
            //Macro macro = new Macro(CircuitConverter.Instance.ConvertToCircuit(macroData.Circuit), macroData.Matching);
            //macro.FileReference = Path.GetFileName(fileName);
            //macro.TypeName = macroData.Name;
            //m_Macros.Add(macroData.Name, macro);
            //m_Symbols.Add(macroData.Name, SymbolConverter.Instance.ConvertToSymbol(macroData.Symbol));
            return macroData.Name;
        }

        /// <summary>
        /// Creates the Macro based of preloaded data
        /// </summary>
        /// <param name="macroName">the name of the macro to build</param>
        private void CreateMacro(string macroName)
        {
            if (m_Data.ContainsKey(macroName))
            {
                Macro macro = new Macro(CircuitConverter.Instance.ConvertToCircuit(m_Data[macroName].Circuit), m_Data[macroName].Matching);
                macro.FileReference = Path.GetFileName(m_FileNames[macroName]);
                macro.TypeName = macroName;
                m_Macros.Add(macroName, macro);
                m_Symbols.Add(macroName, SymbolConverter.Instance.ConvertToSymbol(m_Data[macroName].Symbol));

                m_Data.Remove(macroName);
                m_FileNames.Remove(macroName);
            }
        }

        /// <summary>
        /// Returns the a clone of the macro with the given type name
        /// </summary>
        /// <param name="macroName">type name of the desired macro</param>
        /// <returns>clone of the macro with the given type name</returns>
        public Macro GetMacro(string macroName)
        {
            if (m_Macros.ContainsKey(macroName))
            {
                //Macro template was already build previously
                return m_Macros[macroName].Clone() as Macro;
            }
            else if (m_Data.ContainsKey(macroName))
            {
                //Macro template was not built, but we have the data already
                CreateMacro(macroName);
                return m_Macros[macroName].Clone() as Macro;
            }
            return null;
        }

        /// <summary>
        /// Returns the symbol for the macro with the given type name used for graphical representation
        /// </summary>
        /// <param name="macroName">type name of the desired macro</param>
        /// <returns>symbol for the macro with the given type name</returns>
        public Symbol GetSymbol(string macroName)
        {
            if (m_Symbols.ContainsKey(macroName))
            {
                //Symbol was already build previously
                return m_Symbols[macroName];
            }
            else if (m_Data.ContainsKey(macroName))
            {
                //Symbol was not built, but we have the data already
                CreateMacro(macroName);
                return m_Symbols[macroName];
            }
            return null;
        }

        #endregion
    }
}

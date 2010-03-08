using System;
using System.Collections.Generic;
using DigitalClasses.Logic;
using DigitalClasses.Graphic;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Controls
{
    /// <summary>
    /// Static class that generates or checks unique names for elements or signals within CircuitEditors
    /// </summary>
    internal static class UniqueName
    {
        /// <summary>
        /// Generates a unique name for elements within a Circuit
        /// </summary>
        /// <param name="circuit">the Circuit the element resides</param>
        /// <param name="type">the type of the linked logic element determined with the GetType method</param>
        /// <returns>a unique name for an element</returns>
        public static string GetUniqueName(Circuit circuit, Type type)
        {
            int count = 0;
            foreach (BaseElement be in circuit)
            {
                if (be.GetType().Equals(type))
                    count++;
            }
            string name = String.Format("{0}{1}", type.Name, ++count);
            while (!NameIsUnique(circuit, name))
            {
                name = String.Format("{0}{1}", type.Name, ++count);
            }
            return name;
        }

        /// <summary>
        /// Checks whether a given name is unique in the also given Circuit
        /// </summary>
        /// <param name="circuit">the Circuit the element resides</param>
        /// <param name="name">the name the element should get</param>
        /// <returns>true if the given name will be unique</returns>
        public static bool NameIsUnique(Circuit circuit, string name)
        {
            foreach (BaseElement be in circuit)
            {
                if (be.Name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Generates a unique name for ports within a Symbol
        /// </summary>
        /// <param name="symbol">the Symbol the port is contained</param>
        /// <returns>a unique name for a port</returns>
        public static string GetUniquePortName(Symbol symbol)
        {
            int count = 0;
            foreach (SymbolPart part in symbol)
            {
                if (part is PortPart)
                    count++;
            }
            string name;
            do
            {
                name = String.Format("Port{0}", ++count);
            } while (!PortNameIsUnique(symbol, name));
            return name;
        }

        /// <summary>
        /// Checks whether a given name is unique in the also given symbol
        /// </summary>
        /// <param name="symbol">the symbol the port is contained</param>
        /// <param name="name">the name the port should get</param>
        /// <returns>true if the given name will be unique</returns>
        public static bool PortNameIsUnique(Symbol symbol, string name)
        {
            foreach (SymbolPart part in symbol)
            {
                if (part is PortPart && (part as PortPart).Name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Generates a unique Name for a Signal using the pattern [A..Z][1..0]*
        /// </summary>
        /// <param name="circuit">the Circuit the Signal will be used in</param>
        /// <returns>the unique name for a signal</returns>
        public static string GetUniqueSignalName(Circuit circuit)
        {
            int num = 1;
            string name;
            while (true)
            {
                for (char ch = 'A'; ch < 'Z'; ch++)
                {
                    name = String.Format("{0}{1}", ch, num);
                    if (SignalNameIsUnique(circuit, name))
                    {
                        return name;
                    }
                }
                num++;
            }
        }

        /// <summary>
        /// Checks whether a given name for a signal is unique in the also given circuit
        /// </summary>
        /// <param name="circuit">the Circuit the signal is used in</param>
        /// <param name="name">the name the element should get</param>
        /// <returns>true if the given name for the signal will be unique</returns>
        public static bool SignalNameIsUnique(Circuit circuit, string name)
        {
            foreach (BaseElement be in circuit)
            {
                if (be is SignalInput && (be as SignalInput).SignalName.Equals(name))
                {
                    return false;
                }
                if (be is SignalOutput && (be as SignalOutput).SignalName.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }

    }
}

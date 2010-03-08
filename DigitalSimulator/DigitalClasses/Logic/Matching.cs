using System;
using System.Collections.Generic;
using DigitalClasses.Serialization;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class matches outbound ports of symbols to internal signal IOs of circuits.
    /// Cannot be cloned due to different referenced objects!
    /// </summary>
    internal class Matching
    {
        #region Fields

        private Dictionary<Terminal, InputOutputElement> m_MatchingDict;

        #endregion

        #region Construction

        public Matching()
        {
            m_MatchingDict = new Dictionary<Terminal, InputOutputElement>();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Adds a new Matching
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="ioElement"></param>
        public void AddMatching(Terminal terminal, InputOutputElement ioElement)
        {
            if (ioElement is SignalInput == false && ioElement is SignalOutput == false)
            {
                throw new ArgumentException("Type of IOElement not supported for matching.");
            }
            m_MatchingDict.Add(terminal, ioElement);
        }

        /// <summary>
        /// Find corresponding Terminal in reverse direction
        /// </summary>
        /// <param name="ioElement">IO to find Terminal for</param>
        /// <returns>Corresponsing Terminal or null if not found</returns>
        public Terminal FindTerminal(InputOutputElement ioElement)
        {
            InputOutputElement[] ioArray = new InputOutputElement[m_MatchingDict.Values.Count];
            m_MatchingDict.Values.CopyTo(ioArray, 0);
            Terminal[] termArray = new Terminal[m_MatchingDict.Keys.Count];
            m_MatchingDict.Keys.CopyTo(termArray, 0);

            if (m_MatchingDict.ContainsValue(ioElement))
            {
                for (int i = 0; i < m_MatchingDict.Values.Count; i++)
                {
                    if (ioElement.Equals(ioArray[i]))
                    {
                        return termArray[i];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find a corresponding SignalIO for a Terminal
        /// </summary>
        /// <param name="terminal">Terminal to find the IO for</param>
        /// <returns>Corresponding IO or null if not found</returns>
        public InputOutputElement FindIOElement(Terminal terminal)
        {
            if (m_MatchingDict.ContainsKey(terminal))
            {
                return m_MatchingDict[terminal];
            }
            return null;
        }

        #endregion

    }
}

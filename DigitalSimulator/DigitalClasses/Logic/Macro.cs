using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Serialization;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents an element that can contain more complex circuitry
    /// </summary>
    public class Macro : InputOutputElement
    {
        #region Fields

        private string m_TypeName;
        private string m_FileReference;
        private Circuit m_Circuit;
        private Matching m_Matching;
        private State[] m_PreStates;

        /// <summary>
        /// MatchingData is held for cloning
        /// </summary>
        private MatchingData[] m_MatchingData;

        #endregion

        #region Properties

        protected override int MaxInputCount
        {
            get
            {
                return CircuitInputCount();
            }
        }

        protected override int MaxOutputCount
        {
            get
            {
                return CircuitOutputCount();
            }
        }

        /// <summary>
        /// Holds the name of the file data of this macro is stored in
        /// </summary>
        [Browsable(false)]
        public string FileReference
        {
            get
            {
                return m_FileReference;
            }
            set
            {
                m_FileReference = value;
            }
        }

        /// <summary>
        /// Holds the name of the type of this macro
        /// </summary>
        [Browsable(false)]
        public string TypeName
        {
            get
            {
                return m_TypeName;
            }
            set
            {
                m_TypeName = value;
            }
        }

        /// <summary>
        /// Displays the name of the type to the user. Use the not visible property TypeName for assignment.
        /// </summary>
        [Description("Gibt den Typen des Makros an.")]
        public string Type
        {
            get
            {
                return m_TypeName;
            }
        }

        #endregion

        #region Construction

        internal Macro(Circuit circuit, MatchingData[] matching)
            : base()
        {
            m_Circuit = circuit;
            InputCount = CircuitInputCount();
            OutputCount = CircuitOutputCount();
            m_Matching = new Matching();
            m_MatchingData = matching;
            m_PreStates = new State[OutputCount];
            CreateMatchings(m_MatchingData);
        }

        private Macro(Macro original)
        {
            if (!String.IsNullOrEmpty(original.m_Name))
                this.Name = (string)original.Name.Clone();
            if (!String.IsNullOrEmpty(original.m_TypeName))
                this.TypeName = (string)original.TypeName.Clone();
            if (!String.IsNullOrEmpty(original.m_FileReference))
                this.FileReference = (string)original.FileReference.Clone();
            this.m_Circuit = original.m_Circuit.Clone();
            InputCount = CircuitInputCount();
            OutputCount = CircuitOutputCount();
            m_Matching = new Matching();
            m_MatchingData = original.m_MatchingData;
            m_PreStates = new State[OutputCount];
            CreateMatchings(m_MatchingData);
        }

        #endregion

        #region Overrides

        public override void Logic()
        {
            //use matching to deploy states of Terminals to the SignalInputs within circuit
            foreach (Terminal terminal in Inputs)
            {
                InputOutputElement io = m_Matching.FindIOElement(terminal);
                if (io != null)
                {
                    terminal.Update();
                    (io as SignalInput).State = terminal.State;
                    //io.Logic();
                }
            }
            //run circuit
            m_Circuit.Update();
            //use matching to deploy states of SignalOutputs within circuit to the Terminals
            for (int index = 0; index < Outputs.Length; index++)
            {
                InputOutputElement io = m_Matching.FindIOElement(Outputs[index]);
                if (io != null)
                {
                    //io.Logic();
                    m_PreStates[index] = (io as SignalOutput).State;
                }
            }
        }

        public override void Propagate()
        {
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i].State = m_PreStates[i];
            }
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns>Clone</returns>
        public override InputOutputElement Clone()
        {
            return new Macro(this);
        }

        #endregion

        #region Public Implmementation

        /// <summary>
        /// Returns the output Terminal with the given name        
        /// </summary>
        /// <param name="name">name of the Terminal</param>
        /// <returns></returns>
        public Terminal GetInputByName(string name)
        {
            return FindTerminal(m_Inputs, name);
        }

        /// <summary>
        /// Returns the output Terminal with the given name        
        /// </summary>
        /// <param name="name">name of the Terminal</param>
        /// <returns></returns>
        public Terminal GetOutputByName(string name)
        {
            return FindTerminal(m_Outputs, name);
        }

        /// <summary>
        /// Searches for a Terminal with the given name. Searches Inputs first, then Outputs.
        /// </summary>
        /// <param name="name">name of the Terminal</param>
        /// <returns></returns>
        public Terminal GetTerminalByName(string name)
        {
            Terminal result = FindTerminal(m_Inputs, name);
            if (result == null)
            {
                result = FindTerminal(m_Outputs, name);
            }
            return result;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Counts the SignalInputs within the circuit
        /// </summary>
        /// <returns>Count of SignalInputs</returns>
        private int CircuitInputCount()
        {
            int count = 0;
            if (m_Circuit == null)
            {
                return 0;
            }
            foreach (BaseElement be in m_Circuit)
            {
                if (be is SignalInput)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Counts the SignalOutputs within the circuit
        /// </summary>
        /// <returns>Count of SignalOutputs</returns>
        private int CircuitOutputCount()
        {
            int count = 0;
            if (m_Circuit == null)
            {
                return 0;
            }
            foreach (BaseElement be in m_Circuit)
            {
                if (be is SignalOutput)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Creates the matching
        /// </summary>
        /// <param name="matching">matching data</param>
        private void CreateMatchings(MatchingData[] matching)
        {
            //symbol port names are used for the terminals as well
            int input = 0;
            int output = 0;
            for (int i = 0; i < matching.Length; i++)
            {
                if (matching[i].Direction == DirectionType.Input)
                {
                    Inputs[input].Name = matching[i].PortName;
                    input++;
                }
                if (matching[i].Direction == DirectionType.Output)
                {
                    Outputs[output].Name = matching[i].PortName;
                    output++;
                }
            }
            //finally find the correct objects for the matchings
            foreach (BaseElement be in m_Circuit)
            {
                if (be is SignalInput || be is SignalOutput)
                {
                    Terminal terminal = FindTerminal(matching, be.Name);
                    if (terminal != null)
                    {
                        m_Matching.AddMatching(terminal, be as InputOutputElement);
                    }
                }
            }
        }

        /// <summary>
        /// Find the terminal that matches a given IOElement
        /// </summary>
        /// <param name="matching">matching data</param>
        /// <param name="IOName">name of the IOElement</param>
        /// <returns>found Terminal or null of not found</returns>
        private Terminal FindTerminal(MatchingData[] matching, string IOName)
        {
            //at first find the right index
            bool found = false;
            int index = -1;
            for (int i = 0; i < matching.Length; i++)
            {
                if (matching[i].IOElementName.Equals(IOName))
                {
                    found = true;
                    index = i;
                    break;
                }
            }
            if (found == false)
            {
                return null;
            }
            //then find the corresponding terminal
            if (matching[index].Direction == DirectionType.Input)
            {
                return FindTerminal(m_Inputs, matching[index].PortName);
            }
            if (matching[index].Direction == DirectionType.Output)
            {
                return FindTerminal(m_Outputs, matching[index].PortName);
            }
            return null;
        }

        private Terminal FindTerminal(List<Terminal> collection, string name)
        {
            return collection.Find(
                delegate(Terminal terminal)
                {
                    return terminal.Name.Equals(name);
                }
            );
        }

        #endregion
    }
}

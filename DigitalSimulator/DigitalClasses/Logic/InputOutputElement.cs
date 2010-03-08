using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// Base class for elements with input and/or output <see cref="Terminal">Terminals</see>
    /// </summary>
    public abstract class InputOutputElement : BaseElement
    {
        #region Events

        public event UpdateIndexChanged OnUpdateIndexChanged;
        public event TerminalCountChanged OnTerminalCountChanged;

        #endregion

        #region Fields

        protected List<Terminal> m_Inputs;
        protected List<Terminal> m_Outputs;
        protected int m_UpdateIndex;
        protected int m_UnitDelay;
        protected int m_PosEdgeDelay;
        protected int m_NegEdgeDelay;

        #endregion

        #region Properties

        /// <summary>
        /// The count of input terminals this element has. 
        /// Not visible to the user by default. Visibility may be changed by ancestors.
        /// </summary>
        [Category("Funktion"),
         Description("Bestimmt die Anzahl der Eingänge des Elements."),
         Browsable(false)]
        public virtual int InputCount
        {
            get
            {
                return m_Inputs.Count;
            }
            set
            {
                if (m_Inputs.Count != value)
                {
                    int oldvalue = m_Inputs.Count;
                    if (value > m_Inputs.Count)
                    {
                        int count = value - m_Inputs.Count;
                        for (int i = 0; i < count; i++)
                        {
                            AddInput();
                        }
                    }
                    else if (value < m_Inputs.Count)
                    {
                        int count = m_Inputs.Count - value;
                        for (int i = 0; i < count; i++)
                        {
                            RemoveInput();
                        }
                    }
                    if (OnTerminalCountChanged != null)
                    {
                        OnTerminalCountChanged(this, new TerminalCountChangedEventArgs(DirectionType.Input, oldvalue, m_Inputs.Count));
                    }
                }
            }
        }

        /// <summary>
        /// The count of output terminals this element has.
        /// Not visible to the user by default. Visibility may be changed by ancestors.
        /// </summary>
        [Category("Funktion"),
         Description("Bestimmt die Anzahl der Ausgänge des Elements."),
         Browsable(false)]
        public virtual int OutputCount
        {
            get
            {
                return m_Outputs.Count;
            }
            set
            {
                if (m_Outputs.Count != value)
                {
                    int oldvalue = m_Inputs.Count;
                    if (value > m_Outputs.Count)
                    {
                        int count = value - m_Outputs.Count;
                        for (int i = 0; i < count; i++)
                        {
                            AddOutput();
                        }
                    }
                    else if (value < m_Outputs.Count)
                    {
                        int count = m_Outputs.Count - value;
                        for (int i = 0; i < count; i++)
                        {
                            RemoveOutput();
                        }
                    }
                    if (OnTerminalCountChanged != null)
                    {
                        OnTerminalCountChanged(this, new TerminalCountChangedEventArgs(DirectionType.Output, oldvalue, m_Outputs.Count));
                    }
                }
            }
        }

        /// <summary>
        /// Depicts the unit delay in steps. A zero meens no delay at all.
        /// Not visible to the user by default. Visibility may be changed by ancestors.
        /// </summary>
        [Category("Funktion"),
         Description("Bestimmt die Gatterverzögerungszeit in Schritten."),
         Browsable(false)]
        public virtual int UnitDelay
        {
            get
            {
                return m_UnitDelay;
            }
            set
            {
                if (m_UnitDelay != value)
                {
                    m_UnitDelay = value;
                    foreach (Terminal terminal in m_Outputs)
                    {
                        if (m_UnitDelay == 0)
                        {
                            terminal.Delay = new NoDelay();
                        }
                        else
                        {
                            terminal.Delay = new GateDelay(m_UnitDelay);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Depicts the positive edge delay in steps. A zero meens no delay at all.
        /// Not visible to the user by default. Visibility may be changed by ancestors.
        /// </summary>
        [Category("Funktion"),
         Description("Bestimmt die Verzögerungszeit für positive Flanken in Schritten."),
         Browsable(false)]
        public virtual int PosEdgeDelay
        {
            get
            {
                return m_PosEdgeDelay;
            }
            set
            {
                if (m_PosEdgeDelay != value)
                {
                    m_PosEdgeDelay = value;
                    foreach (Terminal terminal in m_Inputs)
                    {
                        if (m_PosEdgeDelay == 0 && m_NegEdgeDelay == 0)
                        {
                            terminal.Delay = new NoDelay();
                        }
                        else
                        {
                            terminal.Delay = new EdgeDelay(m_PosEdgeDelay, m_NegEdgeDelay);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Depicts the negative edge delay in steps. A zero meens no delay at all.
        /// Not visible to the user by default. Visibility may be changed by ancestors.
        /// </summary>
        [Category("Funktion"),
         Description("Bestimmt die Verzögerungszeit für negative Flanken in Schritten."),
         Browsable(false)]
        public virtual int NegEdgeDelay
        {
            get
            {
                return m_NegEdgeDelay;
            }
            set
            {
                if (m_NegEdgeDelay != value)
                {
                    m_NegEdgeDelay = value;
                    foreach (Terminal terminal in m_Inputs)
                    {
                        if (m_PosEdgeDelay == 0 && m_NegEdgeDelay == 0)
                        {
                            terminal.Delay = new NoDelay();
                        }
                        else
                        {
                            terminal.Delay = new EdgeDelay(m_PosEdgeDelay, m_NegEdgeDelay);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Depicts the order of the update process of all elements.
        /// </summary>
        [Category("Funktion"),
         Description("Bestimmt die Reihenfolge in der die Elemente abgearbeitet werden.")]
        public virtual int UpdateIndex
        {
            get
            {
                return m_UpdateIndex;
            }
            set
            {
                if (m_UpdateIndex != value)
                {
                    int oldvalue = m_UpdateIndex;
                    m_UpdateIndex = value;
                    if (OnUpdateIndexChanged != null)
                    {
                        OnUpdateIndexChanged(this, new UpdateIndexChangedEventArgs(oldvalue, value));
                    }
                }
            }
        }

        /// <summary>
        /// The maximum count of input terminals this element could have.
        /// </summary>
        protected virtual int MaxInputCount
        {
            get
            {
                return Int32.MaxValue;
            }
        }

        /// <summary>
        /// The maximum count of output terminals this element could have.
        /// </summary>
        protected virtual int MaxOutputCount
        {
            get
            {
                return Int32.MaxValue;
            }
        }

        /// <summary>
        /// The minimum count of input terminals this element must have.
        /// </summary>
        protected virtual int MinInputCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// The minimum count of output terminals this element must have.
        /// </summary>
        protected virtual int MinOutputCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Input Terminals. 
        /// </summary>
        [Browsable(false)]
        public Terminal[] Inputs
        {
            get
            {
                return m_Inputs.ToArray();
            }
        }

        /// <summary>
        /// Output Terminals. 
        /// </summary>
        [Browsable(false)]
        public Terminal[] Outputs
        {
            get
            {
                return m_Outputs.ToArray();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default-Constructor
        /// </summary>
        public InputOutputElement()
        {
            m_UnitDelay = 0;
            m_PosEdgeDelay = 0;
            m_NegEdgeDelay = 0;
            m_UpdateIndex = -1;
            m_Inputs = new List<Terminal>(MinInputCount);
            m_Outputs = new List<Terminal>(MinOutputCount);
            //automatically instatiates inputs/outputs (use the predefined values above), so do this at last 
            InputCount = MinInputCount;
            OutputCount = MinOutputCount;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Adds an input with the given name
        /// </summary>
        /// <param name="name">the name of the new input</param>
        /// <returns>true on success, i.e. if the count of inputs is below the maximum</returns>
        protected bool AddInput(string name)
        {
            if (InputCount < MaxInputCount)
            {
                Terminal terminal = new Terminal(DirectionType.Input);
                terminal.Direction = DirectionType.Input;
                terminal.Name = name;
                if (m_PosEdgeDelay == 0 && m_NegEdgeDelay == 0)
                {
                    terminal.Delay = new NoDelay();
                }
                else
                {
                    terminal.Delay = new EdgeDelay(m_PosEdgeDelay, m_NegEdgeDelay);
                }
                terminal.ParentIO = this;
                m_Inputs.Add(terminal);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an input with a generated name
        /// </summary>
        /// <returns>true on success, i.e. if the count of inputs is below the maximum</returns>        
        protected bool AddInput()
        {
            return AddInput(String.Format("I{0}", InputCount + 1));
        }

        /// <summary>
        /// Removes the input with the given name
        /// </summary>
        /// <param name="name">name of the input to remove</param>
        /// <returns>true on success, i.e. the input with the name was found</returns>
        protected bool RemoveInput(string name)
        {
            if (m_Inputs.Count > MinInputCount)
            {
                for (int i = m_Inputs.Count - 1; i >= 0; i--)
                {
                    if (m_Inputs[i].Name.Equals(name))
                    {
                        if (m_Inputs[i].IsConnected)
                        {
                            m_Inputs[i].Disconnect();
                        }
                        m_Inputs[i].ParentIO = null;
                        m_Inputs.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the last input
        /// </summary>
        /// <returns>true on success</returns>
        protected bool RemoveInput()
        {
            if (m_Inputs.Count > MinInputCount)
            {
                if (m_Inputs[m_Inputs.Count - 1].IsConnected)
                {
                    m_Inputs[m_Inputs.Count - 1].Disconnect();
                }
                m_Inputs[m_Inputs.Count - 1].ParentIO = null;
                m_Inputs.RemoveAt(m_Inputs.Count - 1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an output with the given name
        /// </summary>
        /// <param name="name">the name of the new output</param>
        /// <returns>true on success, i.e. if the count of outputs is below the maximum</returns>
        protected bool AddOutput(string name)
        {
            if (OutputCount < MaxOutputCount)
            {
                Terminal terminal = new Terminal(DirectionType.Output);
                terminal.Name = name;
                if (m_UnitDelay == 0)
                {
                    terminal.Delay = new NoDelay();
                }
                else
                {
                    terminal.Delay = new GateDelay(m_UnitDelay);
                }
                terminal.ParentIO = this;
                m_Outputs.Add(terminal);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an output with a generated name
        /// </summary>
        /// <returns>true on success, i.e. if the count of outputs is below the maximum</returns>        
        protected bool AddOutput()
        {
            return AddOutput(String.Format("O{0}", OutputCount + 1));
        }

        /// <summary>
        /// Removes the output with the given name
        /// </summary>
        /// <param name="name">name of the output to remove</param>
        /// <returns>true on success, i.e. the output with the name was found</returns>
        protected bool RemoveOutput(string name)
        {
            if (m_Outputs.Count > MinOutputCount)
            {
                for (int i = m_Outputs.Count - 1; i >= 0; i--)
                {
                    if (m_Outputs[i].Name.Equals(name))
                    {
                        if (m_Outputs[i].IsConnected)
                        {
                            m_Outputs[i].Disconnect();
                        }
                        m_Outputs[i].ParentIO = null;
                        m_Outputs.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the last output
        /// </summary>
        /// <returns>true on success</returns>
        protected bool RemoveOutput()
        {
            if (m_Outputs.Count > MinOutputCount)
            {
                if (m_Outputs[m_Outputs.Count - 1].IsConnected)
                {
                    m_Outputs[m_Outputs.Count - 1].Disconnect();
                }
                m_Outputs[m_Outputs.Count - 1].ParentIO = null;
                m_Outputs.RemoveAt(m_Outputs.Count - 1);
                return true;
            }
            return false;
        }

        #endregion

        #region Prototypes

        /// <summary>
        /// First part of processing step. Performs the logic of this element.
        /// </summary>
        public abstract void Logic();

        /// <summary>
        /// Second part of processing step. Propagates the calculated output
        /// </summary>
        public abstract void Propagate();

        /// <summary>
        /// Clones this element
        /// </summary>
        /// <returns>Clone</returns>
        public abstract InputOutputElement Clone();

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This Class represents the connection between multiple <see cref="Terminal">Terminals</see>
    /// </summary>
    public class Connection : BaseElement
    {
        #region Events

        /// <summary>
        /// Event on change of the state held by this terminal
        /// </summary>
        public event NotifyEvent OnStateChanged;
        /// <summary>
        /// Event on change of the count of terminals connected to this connection
        /// </summary>
        public event NotifyEvent OnTerminalCountChanged;

        #endregion

        #region Fields

        private List<Terminal> m_Terminals;
        private State m_State;

        #endregion

        #region Properties

        /// <summary>
        /// The current state of this Connection: Low or High
        /// </summary>
        public State State
        {
            get
            {
                return m_State;
            }
            internal set
            {
                if (m_State != value)
                {
                    m_State = value;
                    if (OnStateChanged != null)
                    {
                        OnStateChanged(this);
                    }
                }
            }
        }

        /// <summary>
        /// Returns currently connected Terminals
        /// </summary>
        [Browsable(false)]
        public List<Terminal> Terminals
        {
            get
            {
                return new List<Terminal>(m_Terminals);
            }
        }

        #endregion

        #region Constructor

        public Connection()
        {
            m_State = State.Low;
            m_Terminals = new List<Terminal>(2);
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Updates the State of this Connection from connected output <see cref="Terminal">Terminals</see>.
        /// If more than a single output is connected together, this simulates a wired OR.
        /// </summary>
        public void Update()
        {
            State nextState = State.Low;
            foreach (Terminal terminal in m_Terminals)
            {
                if (terminal.Direction == DirectionType.Output)
                {
                    nextState |= terminal.State;
                }
            }
            State = nextState;
        }

        /// <summary>
        /// Connects a Terminal to this Connection
        /// </summary>
        /// <param name="terminal">the Terminal to connect</param>
        public void ConnectTerminal(Terminal terminal)
        {
            if (m_Terminals.Contains(terminal) == false)
            {
                m_Terminals.Add(terminal);
                terminal.Connection = this;
                if (terminal.Direction.Equals(DirectionType.Output))
                {
                    terminal.OnStateChanged += new NotifyEvent(terminal_OnStateChanged);
                    Update();
                }
                else
                {
                    terminal.State = m_State;
                }
                if (OnTerminalCountChanged != null)
                {
                    OnTerminalCountChanged(this);
                }
            }
        }

        /// <summary>
        /// Disconnects a Terminal from this Connection
        /// </summary>
        /// <param name="terminal">the Terminal to disconnect</param>
        public void DisconnectTerminal(Terminal terminal)
        {
            if (m_Terminals.Contains(terminal))
            {
                m_Terminals.Remove(terminal);
                terminal.Connection = null;
                if (terminal.Direction.Equals(DirectionType.Output))
                {
                    terminal.OnStateChanged -= terminal_OnStateChanged;
                    Update();
                }
                else
                {
                    terminal.State = State.Low;
                }
                if (OnTerminalCountChanged != null)
                {
                    OnTerminalCountChanged(this);
                }
            }
        }

        #endregion

        #region Private Implementation

        void terminal_OnStateChanged(object sender)
        {
            Terminal terminal = sender as Terminal;
            if (terminal != null && terminal.Direction.Equals(DirectionType.Output))
            {
                Update();
            }
        }

        #endregion
    }
}

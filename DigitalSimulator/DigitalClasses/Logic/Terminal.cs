using System;
using System.Collections.Generic;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents a terminal of a gate to connect to a <see cref="Connection">Connection</see> with.
    /// </summary>
    public class Terminal : BaseElement
    {
        #region Events

        /// <summary>
        /// Event on change of the state held by this terminal - works only for output direction terminals
        /// </summary>
        public event NotifyEvent OnStateChanged;

        #endregion

        #region Fields

        private DirectionType m_Direction;
        private Connection m_Connection;
        private InputOutputElement m_ParentIO;
        private State m_State;
        private bool m_Negated;
        private IDelay m_Delay;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the parent input output element this terminal is part of
        /// </summary>
        public InputOutputElement ParentIO
        {
            get
            {
                return m_ParentIO;
            }
            internal set
            {
                m_ParentIO = value;
            }
        }

        /// <summary>
        /// The direction of this terminal: Input or Output
        /// </summary>
        public DirectionType Direction
        {
            get
            {
                return m_Direction;
            }
            set
            {
                m_Direction = value;
            }
        }

        /// <summary>
        /// The <see cref="Connection">Connection</see> this Terminal is connected to
        /// </summary>
        public Connection Connection
        {
            get
            {
                return m_Connection;
            }
            set
            {
                if (m_Connection != null && !m_Connection.Equals(value))
                {
                    m_Connection.OnStateChanged -= connection_OnStateChanged;
                }
                if (m_Connection == null || !m_Connection.Equals(value))
                {
                    m_Connection = value;
                    if (m_Connection != null)
                    {
                        m_Connection.OnStateChanged += new DigitalClasses.Events.NotifyEvent(connection_OnStateChanged);
                    }
                    Update();
                }
            }
        }

        /// <summary>
        /// Indicates whether this terminal is connected to a Connection
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return m_Connection != null;
            }
        }

        /// <summary>
        /// The current external state of this terminal: Low or High
        /// External State is influenced by Negation. 
        /// </summary>
        public State State
        {
            get
            {
                State result = m_Delay.Peek();
                if (m_Negated)
                {
                    return !result;
                }
                return result;
            }
            set
            {
                if (m_Direction == DirectionType.Output)
                {
                    m_Delay.Enqueue(value);
                    State last = m_State;
                    m_State = m_Delay.Dequeue();
                    if (m_State != last)
                    {
                        if (OnStateChanged != null)
                        {
                            OnStateChanged(this);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether this terminal is negated or not
        /// </summary>
        public bool Negated
        {
            get
            {
                return m_Negated;
            }
            set
            {
                m_Negated = value;
            }
        }

        /// <summary>
        /// Gives access to the delay behavior of this terminal
        /// </summary>
        internal IDelay Delay
        {
            get
            {
                return m_Delay;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Delay object cannot be null!");
                }
                m_Delay = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="direction">the direction of this terminal</param>
        public Terminal(DirectionType direction)
        {
            m_Direction = direction;
            m_Delay = new NoDelay();
            //switch (direction)
            //{
            //    case DirectionType.Input:
            //        m_Delay = new EdgeDelay(1, 2);
            //        break;
            //    case DirectionType.Output:
            //        m_Delay = new GateDelay(1);
            //        break;
            //}
            m_State = State.Low;
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Updates the state of this Terminal from a connected Connection. Applies to Input Terminals only.
        /// </summary>
        public void Update()
        {
            if (m_Direction == DirectionType.Input)
            {
                m_Delay.Dequeue();
                if (m_Connection != null)
                {
                    m_State = m_Connection.State;
                    m_Delay.Enqueue(m_Connection.State);
                }
                else
                {
                    //without connection this will be undefined
                    //atm there's only a 2 state logic so its low
                    m_Delay.Enqueue(State.Low);
                    m_State = State.Low;        
                }
            }
        }

        /// <summary>
        /// Disconnect from the connection
        /// </summary>
        public void Disconnect()
        {
            if (m_Connection != null)
            {
                m_Connection.DisconnectTerminal(this);
                m_Connection = null;
            }
        }

        #endregion

        #region Private Implementation

        private void connection_OnStateChanged(object sender)
        {
            if (m_Direction.Equals(DirectionType.Input))
            {
                m_State = (sender as Connection).State;
            }
        }

        #endregion
    }
}

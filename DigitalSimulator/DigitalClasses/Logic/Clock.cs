using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents a clock input
    /// </summary>
    public class Clock : InputOutputElement
    {
        #region Events

        public event NotifyEvent OnStateChanged;

        #endregion

        #region Fields

        private State m_State;
        private uint m_HighDuration;
        private uint m_LowDuration;
        private uint m_Position;
        private State m_PreResult;

        #endregion

        #region Properties

        protected override int MaxOutputCount
        {
            get
            {
                return 1;
            }
        }

        protected override int MinInputCount
        {
            get
            {
                return 0;
            }
        }

        protected override int MaxInputCount
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// State this input propagates as signal
        /// </summary>
        [Category("Funktion"), Description("Den Zustand des Ausgangs des Elements wird durch die Position bestimmt.")]
        public State State
        {
            get
            {
                return m_State;
            }
        }

        /// <summary>
        /// Depicts how long a high level should be
        /// </summary>
        [Category("Funktion"), Description("Bestimmt die Dauer eines High-Pegels in Simulationsschritten.")]
        public uint HighDuration
        {
            get
            {
                return m_HighDuration;
            }
            set
            {
                m_HighDuration = value;
                if (m_Position >= m_HighDuration + m_LowDuration)
                {
                    m_Position = 0;
                }
                LogicWithoutIncrement();
            }
        }

        /// <summary>
        /// Depicts how long a low level should be
        /// </summary>
        [Category("Funktion"), Description("Bestimmt die Dauer eines Low-Pegels in Simulationsschritten.")]
        public uint LowDuration
        {
            get
            {
                return m_LowDuration;
            }
            set
            {
                m_LowDuration = value;
                if (m_Position >= m_HighDuration + m_LowDuration)
                {
                    m_Position = 0;
                }
                LogicWithoutIncrement();
            }
        }

        /// <summary>
        /// Depicts the position of simulated clock.
        /// </summary>
        [Category("Funktion"), Description("Aktuelle Position. Kann nicht größer als LowDuration + HighDuration sein.")]
        public uint Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
                if (m_Position >= m_HighDuration + m_LowDuration)
                {
                    m_Position = 0;
                }
                LogicWithoutIncrement();
            }
        }

        [Browsable(false)]
        public override int UpdateIndex
        {
            get
            {
                return base.UpdateIndex;
            }
            set
            {
                base.UpdateIndex = value;
            }
        }

        #endregion

        #region Construction

        public Clock()
            : base()
        {
            m_State = State.Low;
            m_HighDuration = 2;
            m_LowDuration = 2;
            m_Position = 0;
        }

        private Clock(Clock original)
        {
            if (!String.IsNullOrEmpty(original.m_Name))
                Name = (string)original.Name.Clone();
            m_State = original.m_State;
            m_HighDuration = original.m_HighDuration;
            m_LowDuration = original.m_LowDuration;
            m_Position = 0;
        }

        #endregion

        #region Public Implementation

        public override void Logic()
        {
            LogicWithoutIncrement();
            m_Position++;
            if (m_Position >= m_HighDuration + m_LowDuration)
                m_Position = 0;
        }

        public override void Propagate()
        {
            m_Outputs[0].State = m_PreResult;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Performs logic but does not increment current position
        /// </summary>
        private void LogicWithoutIncrement()
        {
            State oldState = m_State;
            if (m_Position < m_LowDuration)
            {
                m_State = State.Low;
            }
            else
            {
                m_State = State.High;
            }
            if (oldState.Equals(m_State) == false && OnStateChanged != null)
            {
                OnStateChanged(this);
            }
            m_PreResult = m_State;
        }

        #endregion

        #region Overrides

        public override InputOutputElement Clone()
        {
            return new Clock(this);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents elements to input signals into the simulation. In macros these objects
    /// are used as ports in input direction.
    /// </summary>
    public class SignalInput : ConstantInput
    {
        #region Fields

        private Signal m_Signal;

        #endregion

        #region Properties

        /// <summary>
        /// Attached Signal
        /// </summary>
        [Browsable(false)]
        public Signal Signal
        {
            get
            {
                return m_Signal;
            }
            internal set
            {
                m_Signal = value;
            }
        }

        /// <summary>
        /// Name of the attached Signal
        /// </summary>
        [Description("Bestimmt den Namen des Signals.")]
        public string SignalName
        {
            get
            {
                return m_Signal.Name;
            }
            set
            {
                m_Signal.Name = value;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        public SignalInput()
            : base()
        {
            m_Signal = new Signal();
        }

        private SignalInput(SignalInput original)
        {
            if (!String.IsNullOrEmpty(original.m_Name))
                Name = (string)original.Name.Clone();
            m_State = original.m_State;
            UnitDelay = 0;
            m_Signal = new Signal();
        }

        #endregion

        #region Overrides

        public override InputOutputElement Clone()
        {
            return new SignalInput(this);
        }

        #endregion
    }
}
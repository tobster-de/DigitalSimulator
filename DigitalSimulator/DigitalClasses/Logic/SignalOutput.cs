using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents elements to output signals from the simulation. In macros these objects
    /// are used as ports in output direction.
    /// </summary>
    public class SignalOutput : InputOutputElement
    {
        #region Events

        public event NotifyEvent OnStateChanged;
        public event NotifyEvent OnSignalNameChanged;

        #endregion

        #region Fields

        private State m_LastState;
        private string m_SignalName;

        #endregion

        #region Properties

        protected override int MaxOutputCount
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
                return 1;
            }
        }

        /// <summary>
        /// State this output received as signal
        /// </summary>
        [Category("Funktion"), Description("Stellt den Zustand des Elements dar.")]
        public State State
        {
            get
            {
                return m_Inputs[0].State;
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

        /// <summary>
        /// Name of the attached Signal
        /// </summary>
        [Description("Bestimmt den Namen des ausgegebenen Signals.")]
        public string SignalName
        {
            get
            {
                return m_SignalName;
            }
            set
            {
                if (m_SignalName != value)
                {
                    m_SignalName = value;
                    if (OnSignalNameChanged != null)
                    {
                        OnSignalNameChanged(this);
                    }
                }
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        public SignalOutput()
            : base()
        {
            m_LastState = m_Inputs[0].State;
            //uncommented -> event only for outputs!
            //m_Inputs[0].OnStateChanged += new DigitalClasses.Events.NotifyEvent(StateChangedEventHandler);
            m_Inputs[0].Delay = new NoDelay();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="original">original object</param>
        private SignalOutput(SignalOutput original)
        {
            if (!String.IsNullOrEmpty(original.m_Name))
                Name = (string)original.Name.Clone();
            m_LastState = m_Inputs[0].State;
            m_Inputs[0].Delay = new NoDelay();
        }

        #endregion

        #region Overrides

        public override void Logic()
        {
            //dummy
        }

        public override void Propagate()
        {
            m_Inputs[0].Update();
            if (m_LastState.Equals(m_Inputs[0].State) == false && OnStateChanged != null)
            {
                OnStateChanged(this);
            }
            m_LastState = m_Inputs[0].State;
        }

        public override InputOutputElement Clone()
        {
            return new SignalOutput(this);
        }

        #endregion

        #region Private Implementation

        //private void StateChangedEventHandler(object sender)
        //{
        //    if (OnStateChanged != null)
        //    {
        //        OnStateChanged(this);
        //    }
        //}
        
        #endregion
    }
}

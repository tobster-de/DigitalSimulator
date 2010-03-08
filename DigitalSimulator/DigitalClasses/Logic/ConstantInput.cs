using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents elements to input a constant state into the simulation. 
    /// </summary>
    public class ConstantInput : InputOutputElement
    {
        #region Events

        public event NotifyEvent OnStateChanged;

        #endregion

        #region Fields

        protected State m_State;

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
        [Category("Funktion"), Description("Bestimmt den Zustand des Ausgangs des Elements.")]
        public virtual State State
        {
            get
            {
                return m_State;
            }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                    if (OnStateChanged != null)
                    {
                        OnStateChanged(this);
                    }
                    Logic();
                }
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

        /// <summary>
        /// Constructor
        /// </summary>
        public ConstantInput()
            : base()
        {
            m_State = State.Low;
            UnitDelay = 0;
        }

        private ConstantInput(ConstantInput original)
        {
            if (!String.IsNullOrEmpty(original.m_Name))
                Name = (string)original.Name.Clone();
            m_State = original.m_State;
            UnitDelay = 0;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Propagates the state to the output
        /// </summary>
        public override void Logic()
        {
            //inputs propagate their logic state immediatly, to cause no delay
            m_Outputs[0].State = m_State;
        }

        public override void Propagate()
        {
            //dummy, nothing to do            
        }

        public override InputOutputElement Clone()
        {
            return new ConstantInput(this);
        }

        #endregion
    }
}
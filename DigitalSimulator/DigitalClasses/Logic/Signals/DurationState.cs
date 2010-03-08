using System;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// Objects of this class aggregate to a Signal.
    /// </summary>
    public class DurationState
    {
        #region Fields

        private State m_State;
        private int m_Duration;

        #endregion

        #region Properties

        /// <summary>
        /// The state
        /// </summary>
        public State State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }

        /// <summary>
        /// Determines the duration of the state
        /// </summary>
        public int Duration
        {
            get
            {
                return m_Duration;
            }
            set
            {
                m_Duration = value;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="state">the state</param>
        /// <param name="duration">determines how long (i.e. simulation steps) the state endures</param>
        public DurationState(State state, int duration)
        {
            m_State = state;
            m_Duration = duration;
        }

        #endregion
    }
}

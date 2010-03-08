using System;
using System.Collections.Generic;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class is responsible for realisation of the gate delay.
    /// </summary>
    internal class NoDelay : IDelay
    {
        #region Fields

        private State m_State;

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        public NoDelay()
        {
            m_State = State.Low;
        }

        #endregion

        #region IDelay Member

        /// <summary>
        /// Indexer to change delay value. In NoDelay this has no effect at all.
        /// </summary>
        /// <param name="index">index of the dalay value</param>
        /// <returns>value of the index</returns>
        public int this[int index]
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        /// <summary>
        /// Enqueues a new state
        /// </summary>
        /// <param name="state">state to enqueue</param>
        public void Enqueue(State state)
        {
            m_State = state;
        }

        /// <summary>
        /// Dequeues a state
        /// </summary>
        /// <returns>dequeued state</returns>
        public State Dequeue()
        {
            return m_State;
        }

        /// <summary>
        /// Returns the next state of the delay without removing it
        /// </summary>
        /// <returns>next state</returns>
        public State Peek()
        {
            return m_State;
        }

        #endregion
    }
}

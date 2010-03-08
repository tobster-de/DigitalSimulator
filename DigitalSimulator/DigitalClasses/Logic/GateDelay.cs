using System;
using System.Collections.Generic;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class is responsible for realisation of the gate delay.
    /// </summary>
    internal class GateDelay : IDelay
    {
        #region Fields

        private int m_Delay;
        private Queue<State> m_StateQueue;
        private State m_NoDelay;

        #endregion

        #region Properties

        /// <summary>
        /// The length of the delay in steps
        /// </summary>
        public int Delay
        {
            get
            {
                return m_Delay;
            }
            set
            {
                if (m_Delay != value && value >= 0)
                {
                    m_Delay = value;
                    //add steps
                    if (m_StateQueue.Count < value)
                    {
                        for (int i = m_StateQueue.Count; i < m_Delay; i++)
                        {
                            m_StateQueue.Enqueue(State.Low);
                        }
                    }
                    //remove steps
                    if (m_StateQueue.Count > value)
                    {
                        for (int i = m_StateQueue.Count - 1; i >= m_Delay; i--)
                        {
                            m_StateQueue.Dequeue();
                        }
                    }
                }
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="delay">duration of the delay</param>
        public GateDelay(int delay)
        {
            m_Delay = delay;
            m_NoDelay = State.Low;
            m_StateQueue = new Queue<State>();
            for (int i = 0; i < m_Delay; i++)
            {
                m_StateQueue.Enqueue(State.Low);
            }
        }

        #endregion

        #region IDelay Member

        /// <summary>
        /// Indexer to change delay value. Here the index has not effect.
        /// </summary>
        /// <param name="index">index of the dalay value</param>
        /// <returns>value of the index</returns>
        public int this[int index]
        {
            get
            {
                return m_Delay;
            }
            set
            {
                Delay = value;
            }
        }

        /// <summary>
        /// Enqueues a new state
        /// </summary>
        /// <param name="state">state to enqueue</param>
        public void Enqueue(State state)
        {
            if (m_Delay == 0)
            {
                m_NoDelay = state;
                return;
            }
            m_StateQueue.Enqueue(state);
        }

        /// <summary>
        /// Dequeues a state
        /// </summary>
        /// <returns>dequeued state</returns>
        public State Dequeue()
        {
            if (m_StateQueue.Count == 0)
            {
                return m_NoDelay;
            }
            return m_StateQueue.Dequeue();
        }

        /// <summary>
        /// Returns the next state of the delay without removing it
        /// </summary>
        /// <returns>next state</returns>
        public State Peek()
        {
            if (m_StateQueue.Count == 0 && m_Delay == 0)
            {
                return m_NoDelay;
            }
            return m_StateQueue.Peek();
        }

        #endregion
    }
}

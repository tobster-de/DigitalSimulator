using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class helps to delay edges in the signal
    /// </summary>
    public class EdgeDelay : IDelay
    {
        #region Fields

        private Queue<bool> m_PositiveEdges;
        private Queue<bool> m_NegativeEdges;
        private bool m_PosEdge;
        private bool m_NegEdge;
        private int m_PositiveDelay;
        private int m_NegativeDelay;
        private State m_LastState;
        private State m_LastOutput;

        #endregion

        #region Properties

        /// <summary>
        /// The length of the delay on a positive edge in steps
        /// </summary>
        [Description("Bestimmt die Länge der Verzögerung bei positiver Flanke.")]
        public int PositiveEdgeDelay
        {
            get
            {
                return m_PositiveDelay;
            }
            set
            {
                if (m_PositiveDelay != value && value >= 0)
                {
                    m_PositiveDelay = value;
                    //add steps
                    if (m_PositiveEdges.Count < value)
                    {
                        for (int i = m_PositiveEdges.Count; i < m_PositiveDelay; i++)
                        {
                            m_PositiveEdges.Enqueue(false);
                        }
                    }
                    //remove steps
                    if (m_PositiveEdges.Count > value)
                    {
                        for (int i = m_PositiveEdges.Count - 1; i >= m_PositiveDelay; i--)
                        {
                            m_PositiveEdges.Dequeue();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The length of the delay on a negative edge in steps
        /// </summary>
        [Description("Bestimmt die Länge der Verzögerung bei negativer Flanke.")]
        public int NegativeEdgeDelay
        {
            get
            {
                return m_NegativeDelay;
            }
            set
            {
                if (m_NegativeDelay != value && value >= 0)
                {
                    m_NegativeDelay = value;
                    //add steps
                    if (m_NegativeEdges.Count < value)
                    {
                        for (int i = m_NegativeEdges.Count; i < m_NegativeDelay; i++)
                        {
                            m_NegativeEdges.Enqueue(false);
                        }
                    }
                    //remove steps
                    if (m_NegativeEdges.Count > value)
                    {
                        for (int i = m_NegativeEdges.Count - 1; i >= m_NegativeDelay; i--)
                        {
                            m_NegativeEdges.Dequeue();
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
        /// <param name="negativeEdgeDelay">length of the delay of a negative edge</param>
        /// <param name="positiveEdgeDelay">length of the delay of a positive edge</param>
        public EdgeDelay(int positiveEdgeDelay, int negativeEdgeDelay)
        {
            m_PositiveDelay = positiveEdgeDelay;
            m_NegativeDelay = negativeEdgeDelay;

            m_PositiveEdges = new Queue<bool>();
            for (int i = 0; i < m_PositiveDelay; i++)
            {
                m_PositiveEdges.Enqueue(false);
            }
            m_NegativeEdges = new Queue<bool>();
            for (int i = 0; i < m_NegativeDelay; i++)
            {
                m_NegativeEdges.Enqueue(false);
            }
            m_LastState = State.Low;
            m_LastOutput = State.Low;
        }

        #endregion

        #region IDelay Member

        /// <summary>
        /// Indexer to change delay values. index 0 = delay on positive edge, index 1 = delay on negative edge
        /// </summary>
        /// <param name="index">index of the dalay value</param>
        /// <returns>value of the index</returns>
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return m_PositiveDelay;
                    case 1:
                        return m_NegativeDelay;
                    default:
                        return -1;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        PositiveEdgeDelay = value;
                        break;
                    case 1:
                        NegativeEdgeDelay = value;
                        break;
                }
            }
        }

        /// <summary>
        /// Enqueues a new state
        /// </summary>
        /// <param name="state">state to enqueue</param>
        public void Enqueue(State state)
        {
            //detect positive edge and enqueue the value
            bool edge = false;
            if (m_LastState.Equals(State.Low) && state.Equals(State.High))
            {
                edge = true;
            }
            m_PosEdge = edge;
            m_PositiveEdges.Enqueue(edge);
            //detect negative edge and enqueue the value
            edge = false;
            if (m_LastState.Equals(State.High) && state.Equals(State.Low))
            {
                edge = true;
            }
            m_NegEdge = edge;
            m_NegativeEdges.Enqueue(edge);
            m_LastState = state;
        }

        /// <summary>
        /// Dequeues a new state
        /// </summary>
        /// <returns>dequeued state</returns>
        public State Dequeue()
        {
            //dequeue the delayed values
            bool positive = m_PosEdge;
            if (m_PositiveDelay > 0)
            {
                positive = m_PositiveEdges.Dequeue();
            }
            bool negative = m_NegEdge;
            if (m_NegativeDelay > 0)
            {
                negative = m_NegativeEdges.Dequeue();
            }
            if (positive && !negative)
            {
                //only delayed positive edge
                m_LastOutput = State.High;
            }
            else if (!positive && negative)
            {
                //only delayed negative edge
                m_LastOutput = State.Low;
            }
            else if (positive && negative)
            {
                //TODO: correct both edges handling
                /*
                if (m_PositiveDelay > m_NegativeDelay)
                {
                    m_LastOutput = State.High;
                }
                else
                {
                    m_LastOutput = State.Low;
                }
                System.Windows.Forms.MessageBox.Show(String.Format("Beide Flanken eingetreten.\n {0} setzt sich durch.", m_LastOutput));
                //throw new NotImplementedException("Cannot handle both edges at the same time so far.");
                //m_LastOutput = !m_LastOutput;
                 */
                System.Windows.Forms.MessageBox.Show(String.Format("Beide Flanken eingetreten.\n {0} beibehalten.", m_LastOutput));
            }
            return m_LastOutput;
        }

        /// <summary>
        /// Returns the next state of the delay without removing it
        /// </summary>
        /// <returns>next state</returns>
        public State Peek()
        {
            //peek the delayed values
            bool positive = m_PosEdge;
            if (m_PositiveDelay > 0)
            {
                positive = m_PositiveEdges.Peek();
            }
            bool negative = m_NegEdge;
            if (m_NegativeDelay > 0)
            {
                negative = m_NegativeEdges.Peek();
            }
            if (positive && !negative)
            {
                //only delayed positive edge
                return State.High;
            }
            else if (!positive && negative)
            {
                //only delayed negative edge
                return State.Low;
            }
            else if (positive && negative)
            {
                /*
                if (m_PositiveDelay > m_NegativeDelay)
                {
                    return State.High;
                }
                else
                {
                    return State.Low;
                }
                //return !m_LastOutput;
                //return m_LastOutput;
                 */
            }
            return m_LastOutput;
        }

        #endregion
    }
}

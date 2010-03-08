using System;
using System.Collections.Generic;
using System.Collections;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class is a composition of one or more States representing a complete signal.
    /// </summary>
    public sealed class Signal : IList<DurationState>, ICloneable
    {
        #region Events

        public event SignalChanged OnSignalChanged;

        #endregion

        #region Fields

        private List<DurationState> m_States;
        private string m_Name;
        private bool m_ReadOnly;

        #endregion

        #region Properties

        /// <summary>
        /// The Name of this Signal
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (String.IsNullOrEmpty(m_Name) || !m_Name.Equals(value))
                {
                    m_Name = value;
                    RaiseChangeEvent(SignalChangeType.SignalName);
                }
            }
        }

        /// <summary>
        /// The length of this signal in simulation steps.
        /// This is not equivalent to the count of items contained.
        /// </summary>
        public int SignalLength
        {
            get
            {
                int sum = 0;
                m_States.ForEach(
                    delegate(DurationState state)
                    {
                        sum += state.Duration;
                    }
                );
                return sum;
            }
        }

        /// <summary>
        /// Determines whether this Signal can be edited by the user in the editor.
        /// Functionality of the methods is not affected by this value.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return m_ReadOnly;
            }
            set
            {
                m_ReadOnly = value;
            }
        }

        #endregion

        #region Construction

        public Signal()
        {
            m_States = new List<DurationState>(2);
        }

        private Signal(Signal original)
        {
            m_States = new List<DurationState>(2);
            m_Name = original.m_Name;
            foreach (DurationState state in original.m_States)
            {
                Add(state);
            }
        }

        #endregion

        #region IList<DurationState> Member

        /// <summary>
        /// Returns the first index of the given state. May be not very informative.
        /// </summary>
        /// <param name="item">state of interest</param>
        /// <returns>first index of a state</returns>
        public int IndexOf(DurationState item)
        {
            return m_States.IndexOf(item);
        }

        /// <summary>
        /// Inserts a state at the given index into this signal.
        /// </summary>
        /// <param name="index">the index the state should be inserted</param>
        /// <param name="item">the state that should be inserted</param>
        public void Insert(int index, DurationState item)
        {
            if (index > 0 && m_States[index - 1].State.Equals(item.State))
            {
                m_States[index - 1].Duration += item.Duration;
            }
            else if (index < m_States.Count - 1 && m_States[index + 1].State.Equals(item.State))
            {
                m_States[index + 1].Duration += item.Duration;
            }
            else
            {
                m_States.Insert(index, item);
            }
            RaiseChangeEvent(SignalChangeType.SignalGraph);
        }

        /// <summary>
        /// Removes a State at a concrete index
        /// </summary>
        /// <param name="index">the index of a state which should be removed</param>
        public void RemoveAt(int index)
        {
            m_States.RemoveAt(index);
            RaiseChangeEvent(SignalChangeType.SignalGraph);
        }

        /// <summary>
        /// Array accessor
        /// </summary>
        /// <param name="index">index of the state of interest</param>
        /// <returns>returns the state at the given index</returns>
        public DurationState this[int index]
        {
            get
            {
                return m_States[index];
            }
            set
            {
                if (!m_States[index].Equals(value))
                {
                    m_States[index] = value;
                    RaiseChangeEvent(SignalChangeType.SignalGraph);
                }
            }
        }

        #endregion

        #region ICollection<DurationState> Member

        /// <summary>
        /// Adds a state to this signal.
        /// </summary>
        /// <param name="item">the state to add to the end of this signal</param>
        public void Add(DurationState item)
        {
            if (m_States.Count > 0 && m_States[m_States.Count - 1].State.Equals(item.State))
            {
                m_States[m_States.Count - 1].Duration += item.Duration;
            }
            else
            {
                m_States.Add(item);
            }
            RaiseChangeEvent(SignalChangeType.SignalGraph);
        }

        /// <summary>
        /// Clears this signal
        /// </summary>
        public void Clear()
        {
            m_States.Clear();
            RaiseChangeEvent(SignalChangeType.SignalGraph);
        }

        /// <summary>
        /// Determines if a state is part of this signal.
        /// </summary>
        /// <param name="item">state of interest</param>
        /// <returns>true if state is contained in this signal</returns>
        public bool Contains(DurationState item)
        {
            return m_States.Contains(item);
        }

        public void CopyTo(DurationState[] array, int arrayIndex)
        {
            m_States.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns the count of states contained in this signal.
        /// This is not equivalent to the length of the signal in simulation steps.
        /// </summary>
        public int Count
        {
            get
            {
                return m_States.Count;
            }
        }

        /// <summary>
        /// Removes the first index of the given State. 
        /// This may be bot very useful. Use RemoveAt for your sake.
        /// </summary>
        /// <param name="item">the state to remove</param>
        /// <returns></returns>
        public bool Remove(DurationState item)
        {
            bool result = m_States.Remove(item);
            if (result)
            {
                RaiseChangeEvent(SignalChangeType.SignalGraph);
            }
            return result;
        }

        #endregion

        #region IEnumerable<DurationState> Member

        public IEnumerator<DurationState> GetEnumerator()
        {
            return m_States.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_States.GetEnumerator();
        }

        #endregion

        #region ICloneable Member

        public object Clone()
        {
            return new Signal(this);
        }

        #endregion

        #region Public Implementation

        internal int GetBeginOfStateAtIndex(int index)
        {
            int stepsum = 0;
            for (int i = 0; i < m_States.Count; i++)
            {
                if (i == index)
                {
                    return stepsum;
                }
                stepsum += m_States[i].Duration;
            }
            return -1;
        }

        internal int GetIndexAtStep(int step)
        {
            int stepsum = 0;
            for (int i = 0; i < m_States.Count; i++)
            {
                if (step > stepsum && m_States[i].Duration + stepsum > step)
                {
                    return i;
                }
                stepsum += m_States[i].Duration;
            }
            return -1;
        }

        /// <summary>
        /// Determines the state value at a given simulation step
        /// </summary>
        /// <param name="step">the simulation step state value is wished for</param>
        /// <returns>state value at a given simulation step</returns>
        internal State GetStateAtStep(int step)
        {
            int stepsum = 0;
            foreach (DurationState state in m_States)
            {
                if (step >= stepsum && state.Duration + stepsum > step)
                {
                    return state.State;
                }
                stepsum += state.Duration;
            }
            return null;
        }

        /// <summary>
        /// Inserts a new State a simulation step. May split the state at this step.
        /// </summary>
        /// <param name="newState">The State with a duration</param>
        /// <param name="step">The simulation step to insert the state</param>
        internal void InsertStateAtStep(DurationState newState, int step)
        {
            int stepsum = 0;
            foreach (DurationState state in m_States)
            {
                if (step >= stepsum && state.Duration + stepsum > step && state.State != newState.State)
                {
                    int index = m_States.IndexOf(state);
                    int newDuration = step - stepsum;
                    m_States.Insert(index + 1, newState);
                    m_States.Insert(index + 2, new DurationState(state.State, state.Duration - newDuration));
                    state.Duration = newDuration;
                    RaiseChangeEvent(SignalChangeType.SignalGraph);
                    return;
                }
                stepsum += state.Duration;
            }
            if (stepsum < step)
            {
                if (m_States.Count > 0 && m_States[m_States.Count - 1].State != newState.State)
                    m_States[m_States.Count - 1].Duration += step - stepsum;
                else
                    m_States.Add(new DurationState(!newState.State, step - stepsum));
                m_States.Add(newState);
                RaiseChangeEvent(SignalChangeType.SignalGraph);
            }
        }

        /// <summary>
        /// Removes an impulse (positive or negative) to merge with the surrounding.
        /// </summary>
        /// <param name="step">the simulation step at which the impulse resides</param>
        internal void MergeStep(int step)
        {
            int stepsum = 0;
            foreach (DurationState state in m_States)
            {
                if (step >= stepsum && state.Duration + stepsum > step)
                {
                    int index = m_States.IndexOf(state);
                    int newDuration = m_States[index].Duration;
                    if (index > 0)
                        newDuration += m_States[index - 1].Duration;
                    if (index < m_States.Count - 1)
                        newDuration += m_States[index + 1].Duration;
                    if (index > 0)
                    {
                        //not the first impulse
                        if (index + 1 < m_States.Count)
                        {
                            //next one can be enlarged
                            m_States[index + 1].Duration = newDuration;
                            m_States.RemoveAt(index);
                            m_States.RemoveAt(index - 1);
                        }
                        else
                        {
                            //enlarge the previous one instead
                            m_States[index - 1].Duration = newDuration;
                            m_States.RemoveAt(index);
                        }
                    }
                    else if (index < m_States.Count - 1)
                    {
                        //not the last impulse
                        if (index - 1 > 0)
                        {
                            //enlarge the previous one
                            m_States.RemoveAt(index + 1);
                            m_States.RemoveAt(index);
                            m_States[index - 1].Duration = newDuration;
                        }
                        else
                        {
                            //enlarge the next one instead
                            m_States[index + 1].Duration = newDuration;
                            m_States.RemoveAt(index);
                        }
                    }
                    else
                    {
                        //only one impulse, so change its state
                        m_States[index].State = !m_States[index].State;
                    }
                    RaiseChangeEvent(SignalChangeType.SignalGraph);
                    return;
                }
                stepsum += state.Duration;
            }
        }

        /// <summary>
        /// Determines whether the given simulation step is an edge of an impulse
        /// </summary>
        /// <param name="step">the simulation step to check for an edge</param>
        /// <returns>true if there is an edge at the given simulation step</returns>
        internal bool IsEdge(int step)
        {
            int stepsum = 0;
            foreach (DurationState state in m_States)
            {
                if (step == state.Duration + stepsum)
                {
                    return true;
                }
                stepsum += state.Duration;
            }
            return false;
        }

        /// <summary>
        /// Enforces this signal to be longer. Can not be shortened.
        /// </summary>
        /// <param name="length">New length. Should be higher than current length.</param>
        internal void ForceLength(int length)
        {
            if (SignalLength < length)
            {
                int diff = length - SignalLength;
                if (m_States.Count == 0 || m_States[m_States.Count - 1].State == State.High)
                    m_States.Add(new DurationState(State.Low, diff));
                else
                    m_States[m_States.Count - 1].Duration += diff;
                RaiseChangeEvent(SignalChangeType.SignalGraph);
            }
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Trigger for the change event
        /// </summary>
        private void RaiseChangeEvent(SignalChangeType change)
        {
            if (OnSignalChanged != null)
            {
                OnSignalChanged(this, change);
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return this.GetType().Name + String.Format(" {0} (#{1})", Name, Count);
        }

        public override bool Equals(object obj)
        {
            if (obj is Signal == false)
            {
                return false;
            }
            Signal other = (Signal)obj;
            if (other.Count != this.Count)
            {
                return false;
            }
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Equals(other[i]) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}

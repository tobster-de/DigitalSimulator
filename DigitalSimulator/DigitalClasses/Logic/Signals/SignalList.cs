using System;
using System.Collections.Generic;
using System.Collections;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class is a composition of one or more Signals used for the simulation process.
    /// </summary>
    public sealed class SignalList : IList<Signal>
    {
        #region Events

        public event NotifyEvent OnListChanged;

        #endregion

        #region Fields

        private List<Signal> m_Signals;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the maximum length of the contained Signals.
        /// Check IsConsistent to check if this value applies to all contained Signals.
        /// </summary>
        public int MaxSignalLength
        {
            get
            {
                int result = 0;
                foreach (Signal signal in m_Signals)
                {
                    result = signal.SignalLength > result ? signal.SignalLength : result;
                }
                return result;
            }
        }


        /// <summary>
        /// Returns whether all Signals contained have the same duration
        /// </summary>
        public bool IsConsistent
        {
            get
            {
                int count = -1;
                bool result = true;
                foreach (Signal signal in m_Signals)
                {
                    result = result && (count == -1 || signal.SignalLength == count);
                    count = signal.SignalLength;
                }
                return result;
            }
        }

        #endregion

        #region Construction

        public SignalList()
        {
            m_Signals = new List<Signal>(1);
        }

        #endregion

        #region IList<Signal> Member

        public int IndexOf(Signal item)
        {
            return m_Signals.IndexOf(item);
        }

        public void Insert(int index, Signal item)
        {
            m_Signals.Insert(index, item);
            RaiseChangeEvent();
        }

        public void RemoveAt(int index)
        {
            m_Signals.RemoveAt(index);
            RaiseChangeEvent();
        }

        public Signal this[int index]
        {
            get
            {
                return m_Signals[index];
            }
            set
            {
                if (!m_Signals[index].Equals(value))
                {
                    m_Signals[index] = value;
                    RaiseChangeEvent();
                }
            }
        }

        #endregion

        #region ICollection<Signal> Member

        public void Add(Signal item)
        {
            m_Signals.Add(item);
            RaiseChangeEvent();
        }

        public void Clear()
        {
            m_Signals.Clear();
            RaiseChangeEvent();
        }

        public bool Contains(Signal item)
        {
            return m_Signals.Contains(item);
        }

        public void CopyTo(Signal[] array, int arrayIndex)
        {
            m_Signals.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return m_Signals.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(Signal item)
        {
            bool result = m_Signals.Remove(item);
            if (result)
            {
                RaiseChangeEvent();
            }
            return result;
        }

        #endregion

        #region IEnumerable<Signal> Member

        public IEnumerator<Signal> GetEnumerator()
        {
            return m_Signals.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_Signals.GetEnumerator();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Forces all signals to have the same length
        /// </summary>
        public void ForceConsistency()
        {
            if (IsConsistent)
            {
                return;
            }
            int max = MaxSignalLength;
            foreach (Signal signal in m_Signals)
            {
                signal.ForceLength(max);
            }
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Trigger for the change event
        /// </summary>
        private void RaiseChangeEvent()
        {
            if (OnListChanged != null)
            {
                OnListChanged(this);
            }
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj is SignalList == false)
            {
                return false;
            }
            SignalList other = (SignalList)obj;
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

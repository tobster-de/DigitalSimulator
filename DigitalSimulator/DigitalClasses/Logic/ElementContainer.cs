using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DigitalClasses.Logic
{
    public abstract class ElementContainer : BaseElement, IEnumerable
    {
        #region Fields

        protected List<BaseElement> m_Elements;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the count of contained elements
        /// </summary>
        public int ElementCount
        {
            get
            {
                return m_Elements.Count;
            }
        }

        #endregion

        #region Constructor

        public ElementContainer()
        {
            m_Elements = new List<BaseElement>();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Adds an element to this container
        /// </summary>
        /// <param name="element">Element to add</param>
        public virtual void AddElement(BaseElement element)
        {
            if (m_Elements.Contains(element) == false)
            {
                m_Elements.Add(element);
            }
        }

        /// <summary>
        /// Removes an element from this container
        /// </summary>
        /// <param name="element">the element to remove</param>
        /// <returns>true when removing was successful</returns>
        public virtual bool RemoveElement(BaseElement element)
        {
            return m_Elements.Remove(element);
        }

        /// <summary>
        /// Checks whether an element is part of this container
        /// </summary>
        /// <param name="element">the element to check</param>
        /// <returns>true if the element is contained</returns>
        public virtual bool ContainsElement(BaseElement element)
        {
            return m_Elements.Contains(element);
        }

        #endregion

        #region IEnumerable Member

        public IEnumerator GetEnumerator()
        {
            return m_Elements.GetEnumerator();
        }

        #endregion
    }
}

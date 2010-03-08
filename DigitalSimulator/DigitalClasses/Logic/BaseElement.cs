using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class is the common base class. It defines common features for all derived classes.
    /// </summary>
    public abstract class BaseElement
    {
        #region Events

        public event NotifyEvent OnNameChanged;

        #endregion

        #region Fields

        protected string m_Name;
        protected object m_LinkedObject;

        #endregion

        #region Properties

        /// <summary>
        /// The Name of this Element.
        /// </summary>
        [Description("Bestimmt den Namen des Elements.")]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    if (OnNameChanged != null)
                    {
                        OnNameChanged(this);
                    }
                }
            }
        }

        /// <summary>
        /// A linked object (graphical object)
        /// </summary>
        [Browsable(false)]
        public object LinkedObject
        {
            get
            {
                return m_LinkedObject;
            }
            set
            {
                m_LinkedObject = value;
            }
        }

        #endregion
    }
}

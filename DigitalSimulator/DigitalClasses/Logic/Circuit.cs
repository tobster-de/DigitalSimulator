using System;
using System.Collections.Generic;
using DigitalClasses.Events;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents a collection of logic elements to form a circuit.
    /// </summary>
    public class Circuit : ElementContainer
    {
        #region Fields

        private int m_NextIndex;
        private bool m_IgnoreEvents;
        private bool m_ReadOnly;

        #endregion

        #region Properties

        /// <summary>
        /// Holds a value that states whether this circuit is read only. Used by CircuitEditor when simulating.
        /// </summary>
        public bool ReadOnly
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

        public Circuit() :
            base()
        {
            m_NextIndex = 0;
        }

        /// <summary>
        /// Cloning Constructor.
        /// Cloning the whole circuit is pretty difficult somehow!
        /// </summary>
        /// <param name="original">original object to create clone from</param>
        private Circuit(Circuit original)
        {
            //create connections to connect the cloned elements to
            List<Connection> connlist = new List<Connection>();
            foreach (BaseElement element in original.m_Elements)
            {
                if (element is Connection)
                {
                    Connection cloned = new Connection();
                    cloned.Name = element.Name;
                    connlist.Add(cloned);
                }
            }
            //clone elements
            foreach (BaseElement element in original.m_Elements)
            {
                if (element is InputOutputElement)
                {
                    InputOutputElement io = element as InputOutputElement;
                    InputOutputElement cloned = io.Clone();
                    ConnectClonedTerminals(io, cloned, connlist);
                    AddElement(cloned);
                }
            }
            // add the connections to the circuit clone
            foreach (Connection connection in connlist)
            {
                if (connection.Terminals.Count > 0)
                {
                    AddElement(connection);
                }
            }
        }

        #endregion

        #region Public Implmementation

        /// <summary>
        /// Updates all InputOutputElements within the circuit. Therefore their logic method will be run.
        /// </summary>
        public void Update()
        {
            //elements are sorted by type and the UpdateIndex, 
            //when run the logic method is run, the result is stored internally
            foreach (BaseElement element in m_Elements)
            {
                InputOutputElement io = element as InputOutputElement;
                if (io != null)
                {
                    io.Logic();
                }
            }
            //upon propagation the internal result is set to their outputs
            foreach (BaseElement element in m_Elements)
            {
                InputOutputElement io = element as InputOutputElement;
                if (io != null)
                {
                    io.Propagate();
                }
            }
        }

        #endregion

        #region Overrides

        public override void AddElement(BaseElement element)
        {
            if (element is InputOutputElement)
            {
                InputOutputElement io = (element as InputOutputElement);
                io.UpdateIndex = m_NextIndex;
                m_NextIndex++;
                io.OnUpdateIndexChanged += new DigitalClasses.Events.UpdateIndexChanged(UpdateIndexChangedEventHandler);
            }
            base.AddElement(element);
            SortElements();
        }

        public override bool RemoveElement(BaseElement element)
        {
            if (element is InputOutputElement)
            {
                InputOutputElement io = (element as InputOutputElement);
                io.OnUpdateIndexChanged -= UpdateIndexChangedEventHandler;
                m_IgnoreEvents = true;
                foreach (BaseElement be in m_Elements)
                {
                    if (be is InputOutputElement)
                    {
                        InputOutputElement beio = be as InputOutputElement;
                        if (beio.UpdateIndex > io.UpdateIndex)
                        {
                            beio.UpdateIndex--;
                        }
                    }
                }
                m_IgnoreEvents = false;
            }
            return base.RemoveElement(element);
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Handles index change by user. Those changes may need change in other indices.
        /// </summary>
        /// <param name="sender">element whose index was changed</param>
        /// <param name="e">EventArgs containing further information</param>
        private void UpdateIndexChangedEventHandler(object sender, UpdateIndexChangedEventArgs e)
        {
            if (m_IgnoreEvents)
            {
                return;
            }
            InputOutputElement senderio = sender as InputOutputElement;
            #region Old > New
            if (e.OldIndex > e.NewIndex)
            {
                //increase all indices between the new and the old value
                m_IgnoreEvents = true;
                foreach (BaseElement be in m_Elements)
                {
                    if (be is InputOutputElement)
                    {
                        InputOutputElement beio = be as InputOutputElement;
                        if (!beio.Equals(senderio) && beio.UpdateIndex >= e.NewIndex && beio.UpdateIndex < e.OldIndex)
                        {
                            beio.UpdateIndex++;
                        }
                    }
                }
                m_IgnoreEvents = false;
            }
            #endregion
            #region New > Old
            if (e.OldIndex < e.NewIndex)
            {
                //decrease all indices between the new and the old value
                m_IgnoreEvents = true;
                foreach (BaseElement be in m_Elements)
                {
                    if (be is InputOutputElement)
                    {
                        InputOutputElement beio = be as InputOutputElement;
                        if (!beio.Equals(senderio) && beio.UpdateIndex > e.OldIndex && beio.UpdateIndex <= e.NewIndex)
                        {
                            beio.UpdateIndex--;
                        }
                    }
                }
                m_IgnoreEvents = false;
            }
            #endregion
            SortElements();
        }

        /// <summary>
        /// Sort the Elements: 1. Inputs, 2. IO according to the index, 3. Outputs
        /// </summary>
        private void SortElements()
        {
            m_Elements.Sort(delegate(BaseElement a, BaseElement b)
            {
                //everything is 'greater' than a Connection or a Clock
                if (a is Connection || a is Clock)
                {
                    if (b is Connection || b is Clock)
                    {
                        return 0;
                    }
                    return -1;
                }
                else if (b is SignalInput || b is Clock)
                {
                    return 1;
                }
                //everything is 'greater' than a SignalInput
                if (a is SignalInput)
                {
                    if (b is SignalInput)
                    {
                        return (a as SignalInput).UpdateIndex.CompareTo((b as SignalInput).UpdateIndex);
                    }
                    return -1;
                }
                else if (b is SignalInput)
                {
                    return 1;
                }
                //everything is 'lower' than a SignalOutput
                if (a is SignalOutput)
                {
                    if (b is SignalOutput)
                    {
                        return (a as SignalOutput).UpdateIndex.CompareTo((b as SignalOutput).UpdateIndex);
                    }
                    return 1;
                }
                else if (b is SignalOutput)
                {
                    return -1;
                }
                //order of IOs depends on their update index
                if (a is InputOutputElement && b is InputOutputElement)
                {
                    int cp = (a as InputOutputElement).UpdateIndex.CompareTo((b as InputOutputElement).UpdateIndex);
                    return cp;
                }
                return 0;
            });
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        public Circuit Clone()
        {
            return new Circuit(this);
        }

        /// <summary>
        /// Part of the cloning constructor.
        /// Connects the cloned terminals to the cloned connections.
        /// </summary>
        /// <param name="io">original element object</param>
        /// <param name="cloned">cloned element object</param>
        /// <param name="connlist">list with connections</param>
        private void ConnectClonedTerminals(InputOutputElement io, InputOutputElement cloned, List<Connection> connlist)
        {
            //inputs
            for (int i = 0; i < io.Inputs.Length; i++)
            {
                if (io.Inputs[i].IsConnected == false)
                {
                    continue;
                }
                string connectionName = io.Inputs[i].Connection.Name;
                Connection destConn = connlist.Find(
                    delegate(Connection connection)
                    {
                        if (connection.Name.Equals(connectionName))
                            return true;
                        return false;
                    }
                );
                if (destConn != null)
                {
                    destConn.ConnectTerminal(cloned.Inputs[i]);
                }
            }
            //outputs
            for (int i = 0; i < io.Outputs.Length; i++)
            {
                if (io.Outputs[i].IsConnected == false)
                {
                    continue;
                }
                string connectionName = io.Outputs[i].Connection.Name;
                Connection destConn = connlist.Find(
                    delegate(Connection connection)
                    {
                        if (connection.Name.Equals(connectionName))
                            return true;
                        return false;
                    }
                );
                if (destConn != null)
                {
                    destConn.ConnectTerminal(cloned.Outputs[i]);
                }
            }
        }


        #endregion
    }
}

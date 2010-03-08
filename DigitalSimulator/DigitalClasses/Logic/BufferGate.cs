using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents a buffer gate.
    /// </summary>
    public class BufferGate : InputOutputElement
    {
        #region Fields

        private State m_PreResult;

        #endregion

        #region Properties

        protected override int MaxInputCount
        {
            get
            {
                return 1;
            }
        }

        protected override int MaxOutputCount
        {
            get
            {
                return 1;
            }
        }

        [Browsable(true)]
        public override int UnitDelay
        {
            get
            {
                return base.UnitDelay;
            }
            set
            {
                base.UnitDelay = value;
            }
        }

        [Browsable(true)]
        public override int PosEdgeDelay
        {
            get
            {
                return base.PosEdgeDelay;
            }
            set
            {
                base.PosEdgeDelay = value;
            }
        }

        [Browsable(true)]
        public override int NegEdgeDelay
        {
            get
            {
                return base.NegEdgeDelay;
            }
            set
            {
                base.NegEdgeDelay = value;
            }
        }

        #endregion

        #region Constructor

        public BufferGate()
            : base()
        {
        }

        #endregion

        #region Public Implementation

        public override void Logic()
        {
            m_Inputs[0].Update();
            m_PreResult = m_Inputs[0].State;
        }

        public override void Propagate()
        {
            m_Outputs[0].State = m_PreResult;
        }


        #endregion

        #region Overrides

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns>Clone</returns>
        public override InputOutputElement Clone()
        {
            BufferGate io = new BufferGate();
            if (!String.IsNullOrEmpty(m_Name))
                io.Name = (string)Name.Clone();
            io.UnitDelay = UnitDelay;
            io.NegEdgeDelay = NegEdgeDelay;
            io.PosEdgeDelay = PosEdgeDelay;
            io.InputCount = InputCount;
            for (int i = 0; i < InputCount; i++)
            {
                io.Inputs[i].Direction = Inputs[i].Direction;
                io.Inputs[i].Negated = Inputs[i].Negated;
            }
            io.OutputCount = OutputCount;
            for (int i = 0; i < OutputCount; i++)
            {
                io.Outputs[i].Direction = Outputs[i].Direction;
                io.Outputs[i].Negated = Outputs[i].Negated;
            }
            return io;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents an AND gate.
    /// </summary>
    public class AndGate : InputOutputElement
    {
        #region Fields

        private State m_PreResult;

        #endregion

        #region Properties

        protected override int MaxOutputCount
        {
            get
            {
                return 1;
            }
        }

        protected override int MinInputCount
        {
            get
            {
                return 2;
            }
        }

        [Browsable(true)]
        public override int InputCount
        {
            get
            {
                return base.InputCount;
            }
            set
            {
                base.InputCount = value;
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

        #region Construction

        public AndGate(int inputs)
            : base()
        {
            if (inputs < MinInputCount)
            {
                throw new ArgumentOutOfRangeException(String.Format("Count of Inputs must not be lower than {0}.", MinInputCount));
            }
            InputCount = inputs;
        }

        public AndGate()
            : base()
        {
        }

        #endregion

        #region Overrides

        public override void Logic()
        {
            State output = State.High;
            foreach (Terminal terminal in m_Inputs)
            {
                terminal.Update();
                output &= terminal.State;
            }
            m_PreResult = output;
        }

        public override void Propagate()
        {
            m_Outputs[0].State = m_PreResult;
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns>Clone</returns>
        public override InputOutputElement Clone()
        {
            AndGate io = new AndGate();
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

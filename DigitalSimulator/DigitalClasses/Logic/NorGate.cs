using System;
using System.Collections.Generic;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents an NOR gate. 
    /// </summary>
    public class NorGate : OrGate
    {
        #region Constructor

        public NorGate(int inputs)
            : base(inputs)
        {
            m_Outputs[0].Negated = true;
        }

        public NorGate()
            : base()
        {
            m_Outputs[0].Negated = true;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns>Clone</returns>
        public override InputOutputElement Clone()
        {
            NorGate io = new NorGate();
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

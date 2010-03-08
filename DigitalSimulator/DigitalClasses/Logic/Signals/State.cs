using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalClasses.Controls;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This class represents the States Connections or Terminals can take.
    /// It's possible to calculate with States like Boolean.
    /// </summary>
    [TypeConverter(typeof(StateTypeConverter))]
    public sealed class State
    {
        /// <summary>
        /// Low State
        /// </summary>
        public static readonly State Low = new State(false);
        /// <summary>
        /// High State
        /// </summary>
        public static readonly State High = new State(true);

        #region Fields

        /// <summary>
        /// The only two instances this class is able to have
        /// </summary>
        private static State[] m_Instances = new State[] { Low, High };
        private bool m_Value;

        #endregion

        #region Construction/Instanciation

        private State(bool value)
        {
            m_Value = value;
        }

        private static State Instance(bool value)
        {
            foreach (State state in m_Instances)
            {
                if (state.m_Value == value)
                {
                    return state;
                }
            }
            return null;
        }

        #endregion

        #region Operators

        /// <summary>
        /// NOT operator
        /// </summary>
        /// <param name="other">unary operand</param>
        /// <returns>State</returns>
        public static State operator !(State other)
        {
            return Instance(!other.m_Value);
        }

        /// <summary>
        /// AND operator
        /// </summary>
        /// <param name="a">first operand</param>
        /// <param name="b">second operand</param>
        /// <returns>State</returns>
        public static State operator &(State a, State b)
        {
            return Instance(a.m_Value & b.m_Value);
        }

        /// <summary>
        /// OR operator
        /// </summary>
        /// <param name="a">first operand</param>
        /// <param name="b">second operand</param>
        /// <returns>State</returns>
        public static State operator |(State a, State b)
        {
            return Instance(a.m_Value | b.m_Value);
        }

        /// <summary>
        /// Equation operator
        /// </summary>
        /// <param name="a">first operand</param>
        /// <param name="b">second operand</param>
        /// <returns>true on equity</returns>
        public static bool operator ==(State a, State b)
        {
            if ((a as object) == null && (b as object) == null)
                return true;
            return (a as object) != null && (b as object) != null && a.m_Value == b.m_Value;
        }

        /// <summary>
        /// Negative equation operator
        /// </summary>
        /// <param name="a">first operand</param>
        /// <param name="b">second operand</param>
        /// <returns>false on equity</returns>
        public static bool operator !=(State a, State b)
        {
            if ((a as object) == null && (b as object) != null)
                return true;
            if ((a as object) != null && (b as object) == null)
                return true;
            if ((a as object) == null && (b as object) == null)
                return false;
            return a.m_Value != b.m_Value;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether this State equals an other one
        /// </summary>
        /// <param name="other">an other state</param>
        /// <returns>Whether the two states are equal</returns>
        public bool Equals(State other)
        {
            return (other as object) != null && other.m_Value == m_Value;
        }

        /// <summary>
        /// Determines whether this State equals an other object
        /// </summary>
        /// <param name="obj">an other object</param>
        /// <returns>Whether the two objects are equal</returns>
        public override bool Equals(object obj)
        {
            return (obj is State) && (obj as State).m_Value == m_Value;
        }

        public override int GetHashCode()
        {
            return m_Value.GetHashCode();
        }

        /// <summary>
        /// Converts the State to a readable string
        /// </summary>
        /// <returns>The State converted to a string</returns>
        public override string ToString()
        {
            if (this.Equals(High))
            {
                return "High";
            }
            return "Low";
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using DigitalClasses.Logic;
using DigitalClasses.Graphic;
using DigitalClasses.Controls;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class is responsible for converting SignalList objects to SignalData arrays
    /// </summary>
    public class SignalConverter
    {
        #region Fields

        private delegate object ConvertMethod(object Input);

        private static SignalConverter m_Instance = new SignalConverter();

        #endregion

        #region Properties

        /// <summary>
        /// Returns the singleton Instance for this class
        /// </summary>
        public static SignalConverter Instance
        {
            get
            {
                return m_Instance;
            }
        }

        #endregion

        #region Construction

        private SignalConverter()
        {
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Converts a SignalList to an array of SignalData
        /// </summary>
        /// <param name="signals">SignalList to convert</param>
        /// <returns>converted Array of SignalData</returns>
        public SignalData[] ConvertFromSignalList(SignalList signals)
        {
            List<SignalData> signalData = new List<SignalData>(signals.Count);
            foreach (Signal signal in signals)
            {
                signalData.Add(ConvertSignal(signal));
            }
            return signalData.ToArray();
        }

        /// <summary>
        /// Converts an array of SignalData to a SignalList
        /// </summary>
        /// <param name="signalDataArray">Array of SignalData to convert</param>
        /// <returns>converted SignalList</returns>
        public SignalList ConvertToSignalList(SignalData[] signalDataArray)
        {
            SignalList signalList = new SignalList();
            foreach (SignalData signalData in signalDataArray)
            {
                signalList.Add(ConvertSignalData(signalData));
            }
            return signalList;
        }

        #endregion

        #region ConvertMethods

        /// <summary>
        /// Converts Signal to SignalData
        /// </summary>
        /// <param name="signal">the Signal to convert</param>
        /// <returns>the converted SignalData</returns>
        private SignalData ConvertSignal(Signal signal)
        {
            List<StateData> states = new List<StateData>();
            foreach (DurationState state in signal)
            {
                states.Add(new StateData(state.State.ToString(), state.Duration));
            }
            SignalData signalData = new SignalData(signal.Name);
            signalData.States = states.ToArray();
            return signalData;
        }

        /// <summary>
        /// Converts a SignalData to a Signal
        /// </summary>
        /// <param name="signalData">the SignalData to convert</param>
        /// <returns>the converted Signal</returns>
        private Signal ConvertSignalData(SignalData signalData)
        {
            Signal signal = new Signal();
            signal.Name = signalData.Name;
            StateTypeConverter stateconv = new StateTypeConverter();
            foreach (StateData stateData in signalData.States)
            {
                State state = (State)stateconv.ConvertFrom(stateData.Level.Trim());
                signal.Add(new DurationState(state, stateData.Duration));
            }
            return signal;
        }

        #endregion
    }
}

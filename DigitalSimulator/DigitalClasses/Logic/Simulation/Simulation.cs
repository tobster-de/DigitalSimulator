using System;
using System.Collections.Generic;
using System.Timers;

using ToolBox;
using DigitalClasses.Events;
using DigitalClasses.Exceptions;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This enumeration is used to depict the state of a Simulation
    /// </summary>
    public enum SimulationState
    {
        Unknown,
        Ready,
        Paused,
        Running
    }

    /// <summary>
    /// This class is responsible for the simulation of a digital circuit
    /// </summary>
    public class Simulation
    {
        #region Events

        public event SimulationStateChanged OnSimulationStateChanged;
        public event NotifyEvent OnStepProcessed;

        #endregion

        #region Fields

        private Timer m_Timer;
        private Circuit m_Circuit;
        private SignalList m_EditorSignals;
        private SignalList m_SignalList;
        private int m_CurrentStep;
        private SimulationState m_SimState;
        private bool m_LoopSimulation;
        private AverageValue m_StepTimeSpan;

        #endregion

        #region Properties

        /// <summary>
        /// Interval on which simulation steps are processed
        /// </summary>
        public int Interval
        {
            get
            {
                return (int)m_Timer.Interval;
            }
            set
            {
                m_Timer.Interval = value;
            }
        }

        /// <summary>
        /// Depicts whether the simulation should begin from start when the signal stimuli end
        /// </summary>
        public bool LoopSimulation
        {
            get
            {
                return m_LoopSimulation;
            }
            set
            {
                m_LoopSimulation = value;
            }
        }

        /// <summary>
        /// Hold the current step for the simulation
        /// </summary>
        public int CurrentStep
        {
            get
            {
                return m_CurrentStep;
            }
            //set
            //{
            //    if (value - m_CurrentStep == 1 || (m_CurrentStep == m_SignalList.MaxSignalLength && value == 0))
            //    {
            //        m_CurrentStep = value;
            //    }
            //}
        }

        /// <summary>
        /// Depicts the current state of this Simulation
        /// </summary>
        public SimulationState SimulationState
        {
            get
            {
                return m_SimState;
            }
            private set
            {
                //private setter for event generation
                if (m_SimState != value)
                {
                    m_SimState = value;
                    if (OnSimulationStateChanged != null)
                    {
                        OnSimulationStateChanged(this, m_SimState);
                    }
                }
            }
        }

        /// <summary>
        /// The collection of signals that is used by this Simulation. Note that assignment does not
        /// apply to the used list directly!
        /// </summary>
        public SignalList Signals
        {
            get
            {
                return m_SignalList;
            }
            set
            {
                m_EditorSignals = value;
                GenerateSignals();
            }
        }

        /// <summary>
        /// The collection of signals that is set by the user in the editor. 
        /// </summary>
        public SignalList EditorSignals
        {
            get
            {
                return m_EditorSignals;
            }
        }

        /// <summary>
        /// This is the circuit that is simulated
        /// </summary>
        public Circuit Circuit
        {
            get
            {
                return m_Circuit;
            }
            set
            {
                m_Circuit = value;
            }
        }

        /// <summary>
        /// Returns the average measured runtime of a simulation step in milliseconds
        /// </summary>
        public double StepRuntime
        {
            get
            {
                return m_StepTimeSpan;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="circuit">the circuit to simulate</param>
        /// <param name="signalList">signals to stimulate the circuit with</param>
        public Simulation(Circuit circuit, SignalList signalList)
        {
            m_Timer = new Timer(50);
            //leave this false to prevent a step to take action when another one is still processing
            m_Timer.AutoReset = false; 
            m_Timer.Elapsed += new ElapsedEventHandler(SimulationTimerElapsed);
            m_Circuit = circuit;
            m_EditorSignals = signalList;
            //signalList.OnListChanged
            m_SignalList = new SignalList();
            m_CurrentStep = 0;
            m_StepTimeSpan = new AverageValue();
            GenerateSignals();
            SimulationState = SimulationState.Ready;
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Performs a single step of the simulaion
        /// </summary>
        public void Step()
        {
            if (SimulationState != SimulationState.Paused)
            {
                GenerateSignals();
            }
            MeasureSimulationStep();
            
            SimulationState = SimulationState.Paused;
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        public void Start()
        {
            if (SimulationState != SimulationState.Paused)
            {
                GenerateSignals();
            }
            m_Timer.Start();
            SimulationState = SimulationState.Running;
            //reset all clocks
            foreach (BaseElement element in m_Circuit)
            {
                Clock clock = element as Clock;
                if (clock != null)
                {
                    clock.Position = 0;
                }
            }
            m_Circuit.ReadOnly = true;
        }

        /// <summary>
        /// Stops the simulaion
        /// </summary>
        public void Stop()
        {
            m_Timer.Stop();
            m_CurrentStep = 0;
            SimulationState = SimulationState.Ready;
            m_Circuit.ReadOnly = false;
            m_StepTimeSpan.Reset();
        }

        /// <summary>
        /// Pauses the simulation
        /// </summary>
        public void Pause()
        {
            m_Timer.Stop();
            SimulationState = SimulationState.Paused;
        }

        #endregion

        #region Private Implementation

        private void SimulationTimerElapsed(object sender, ElapsedEventArgs e)
        {
            MeasureSimulationStep();

            if (m_SimState == SimulationState.Running) {
                m_Timer.Start();
            }
        }

        private void MeasureSimulationStep()
        {
            DateTime start = DateTime.Now;
            ProcessSimulationStep();
            DateTime end = DateTime.Now;
            TimeSpan span = new TimeSpan((end.Subtract(start).Ticks));
            m_StepTimeSpan.Add(span.TotalMilliseconds);

            if (OnStepProcessed != null)
            {
                OnStepProcessed(this);
            }
        }

        private void GenerateSignals()
        {
            m_SignalList.Clear();
            //add a signal for all clocks
            foreach (BaseElement element in m_Circuit)
            {
                Clock clock = element as Clock;
                if (clock != null)
                {
                    Signal clkSignal = new Signal();
                    clkSignal.IsReadOnly = true;
                    clkSignal.Name = element.Name;
                    m_SignalList.Add(clkSignal);
                }
            }
            //add the stimuli
            foreach (Signal signal in m_EditorSignals)
            {
                //Signal clone = signal.Clone() as Signal;
                //clone.IsReadOnly = true;
                //m_SignalList.Add(clone);
                Signal inSignal = new Signal();
                inSignal.IsReadOnly = true;
                inSignal.Name = signal.Name;
                m_SignalList.Add(inSignal);
            }
            //add a signal for all outputs
            foreach (BaseElement element in m_Circuit)
            {
                SignalOutput output = element as SignalOutput;
                if (output != null)
                {
                    Signal outSignal = new Signal();
                    outSignal.IsReadOnly = true;
                    outSignal.Name = output.SignalName;
                    if (string.IsNullOrEmpty(outSignal.Name))
                    {
                        outSignal.Name = element.Name;
                    }
                    m_SignalList.Add(outSignal);
                }
            }
            if (m_SignalList.IsConsistent == false)
            {
                m_SignalList.ForceConsistency();
            }
        }

        private void ProcessSimulationStep()
        {
            //set the inputs appropriate
            foreach (BaseElement element in m_Circuit)
            {
                SignalInput input = element as SignalInput;
                if (input != null)
                {
                    State state = input.Signal.GetStateAtStep(m_CurrentStep);
                    if (state == null)
                    {
                        if (m_LoopSimulation || m_CurrentStep < m_EditorSignals.MaxSignalLength)
                        {
                            throw new SimulationException(
                                String.Format("Missing State for Signal {0} at step {1}.",
                                input.SignalName, m_CurrentStep));
                        }
                        //else don't change the signal
                    }
                    else
                    {
                        input.State = state;
                    }
                }
            }
            //perform an update
            m_Circuit.Update();
            foreach (BaseElement element in m_Circuit)
            {
                //get data from clocks
                Clock clock = element as Clock;
                if (clock != null)
                {
                    AddStateToSignal(clock.Name, clock.State);
                }
                //get data from inputs
                SignalInput input = element as SignalInput;
                if (input != null)
                {
                    AddStateToSignal(input.SignalName, input.State);
                }
                //get data from outputs
                SignalOutput output = element as SignalOutput;
                if (output != null)
                {
                    string name = output.SignalName;
                    if (string.IsNullOrEmpty(name))
                    {
                        name = output.Name;
                    }
                    AddStateToSignal(name, output.State);
                }
            }
            m_CurrentStep++;
            if (m_LoopSimulation && m_CurrentStep >= m_EditorSignals.MaxSignalLength)
            {
                m_CurrentStep = 0;
                foreach (Signal signal in m_SignalList)
                {
                    signal.Clear();
                }
            }
        }

        private void AddStateToSignal(string signalName, State state)
        {
            foreach (Signal signal in m_SignalList)
            {
                if (signal.Name.Equals(signalName))
                {
                    signal.Add(new DurationState(state, 1));
                }
            }
        }

        #endregion
    }
}

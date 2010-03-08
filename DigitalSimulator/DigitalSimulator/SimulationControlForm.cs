using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using DigitalClasses.Graphic;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace DigitalSimulator
{
    public partial class SimulationControlForm : DockContent
    {
        #region Fields

        private Simulation m_Simulation;

        #endregion

        public SimulationControlForm()
        {
            InitializeComponent();
            CheckSimulationState();
        }

        #region Public Implementation

        /// <summary>
        /// Sets the simulation object to control
        /// </summary>
        /// <param name="simulation">simulation object to control</param>
        internal void SetSimulation(Simulation simulation)
        {
            if (m_Simulation != null)
            {
                m_Simulation.OnSimulationStateChanged -= SimulationStateChangedHandler;
                m_Simulation.OnStepProcessed -= SimulationStepProcessedHandler;
            }
            m_Simulation = simulation;
            if (m_Simulation != null)
            {
                m_Simulation.OnSimulationStateChanged += new SimulationStateChanged(SimulationStateChangedHandler);
                m_Simulation.OnStepProcessed += new NotifyEvent(SimulationStepProcessedHandler);
            }
            CheckSimulationState();
        }

        #endregion

        #region Private Implementation

        void SimulationStateChangedHandler(object sender, SimulationState newState)
        {
            CheckSimulationState();
        }

        void SimulationStepProcessedHandler(object sender)
        {
            if (InvokeRequired)
            {
                NotifyEvent d = new NotifyEvent(SimulationStepProcessedHandler);
                Invoke(d, new object[] { sender });
            }
            else
            {
                toolStripLabel_ProcessRate.Text = String.Format("Ø {0:f} ms", m_Simulation.StepRuntime);
            }
        }

        private void CheckSimulationState()
        {
            if (m_Simulation == null)
            {
                btn_Stop.Enabled = false;
                btn_Step.Enabled = false;
                btn_Start.Enabled = false;
                btn_Pause.Enabled = false;
                trackBar_Interval.Value = 50;
                trackBar_Interval.Enabled = false;
                checkBox_LoopSim.Enabled = false;
            }
            else
            {
                btn_Pause.Enabled = m_Simulation.SimulationState == SimulationState.Running;
                btn_Start.Enabled = m_Simulation.SimulationState == SimulationState.Ready
                    || m_Simulation.SimulationState == SimulationState.Paused;
                btn_Step.Enabled = m_Simulation.SimulationState == SimulationState.Paused
                    || m_Simulation.SimulationState == SimulationState.Ready;
                btn_Stop.Enabled = m_Simulation.SimulationState == SimulationState.Running
                    || m_Simulation.SimulationState == SimulationState.Paused;
                trackBar_Interval.Value = m_Simulation.Interval;
                trackBar_Interval.Enabled = m_Simulation.SimulationState != SimulationState.Running;
                checkBox_LoopSim.Enabled = true;
                checkBox_LoopSim.Checked = m_Simulation.LoopSimulation;
            }
            //label_IntervalValue.Text = String.Format("{0} ms ({1:f} Hz)", trackBar_Interval.Value,
            //    1000f / trackBar_Interval.Value);
            label_IntervalValue.Text = String.Format("{0} ms", trackBar_Interval.Value);
        }

        private void trackBar_Interval_Scroll(object sender, EventArgs e)
        {
            //label_IntervalValue.Text = String.Format("{0} ms ({1:f} Hz)", trackBar_Interval.Value,
            //    1000f / trackBar_Interval.Value);
            label_IntervalValue.Text = String.Format("{0} ms", trackBar_Interval.Value);
            m_Simulation.Interval = trackBar_Interval.Value;
        }

        private void checkBox_LoopSim_CheckedChanged(object sender, EventArgs e)
        {
            m_Simulation.LoopSimulation = checkBox_LoopSim.Checked;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (m_Simulation == null)
            {
                return;
            }
            if (m_Simulation.EditorSignals != null && m_Simulation.EditorSignals.IsConsistent == false)
            {
                if (MessageBox.Show("Die zu verwendeten Signale haben nicht die selbe Dauer.\nSollen die Signale erweitert werden?",
                    "Hinweis", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
                m_Simulation.EditorSignals.ForceConsistency();
            }
            m_Simulation.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (m_Simulation == null)
            {
                return;
            }
            m_Simulation.Stop();
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (m_Simulation == null)
            {
                return;
            }
            m_Simulation.Pause();
        }

        private void btn_Step_Click(object sender, EventArgs e)
        {
            if (m_Simulation == null)
            {
                return;
            }
            m_Simulation.Step();
        }

        #endregion

    }
}

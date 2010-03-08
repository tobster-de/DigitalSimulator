using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DigitalClasses.Controls;
using DigitalClasses.Graphic;
using DigitalClasses.Logic;
using DigitalClasses.Events;
using WeifenLuo.WinFormsUI.Docking;
using DigitalClasses.Serialization;
using System.IO;

namespace DigitalSimulator
{
    public partial class CircuitEditorForm : DockContent, ISaveableDocument
    {
        #region Fields

        private String m_FileName;
        private Simulation m_Simulation;
        private bool m_Persistent;

        #endregion

        #region Properties

        /// <summary>
        /// The circuit editor control displayed by this form
        /// </summary>
        public CircuitEditor CircuitEditor
        {
            get
            {
                return circuitEditor;
            }
        }

        /// <summary>
        /// Depicts the Simulation object used to simulate the circuit
        /// </summary>
        public Simulation Simulation
        {
            get
            {
                return m_Simulation;
            }
        }

        /// <summary>
        /// Indicates if there is signaling data for simulation available
        /// </summary>
        public bool HasSignalingData
        {
            get
            {
                return (circuitEditor.SignalList != null && circuitEditor.SignalList.Count > 0 && circuitEditor.SignalList[0].Count > 0);
            }
        }

        /// <summary>
        /// Designates the filename used to store the circuit of this editor form
        /// </summary>
        public string FileName
        {
            get
            {
                return (string)m_FileName.Clone();
            }
            private set
            {
                if (m_FileName == null || m_FileName.Equals(value) == false)
                {
                    m_FileName = value;
                    Text = Path.GetFileName(value);
                }
                if (m_Persistent == false)
                {
                    if (Text.EndsWith("*") == false)
                    {
                        Text += "*";
                    }
                }
                else
                {
                    if (Text.EndsWith("*"))
                    {
                        Text = Text.TrimEnd('*');
                    }
                }
            }
        }

        #endregion

        public CircuitEditorForm()
        {
            InitializeComponent();
            m_FileName = "";
            m_Persistent = true;
            m_Simulation = new Simulation(circuitEditor.Circuit, circuitEditor.SignalList);
        }

        private void SaveAs(string fileName)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName))
                {
                    FileSelector fileSelector = new FileSelector(FileSelectorFilters.Circuits);
                    fileSelector.FileName = fileName;
                    if (fileSelector.ExecuteSaveDialog())
                    {
                        fileName = fileSelector.FileName;
                    }
                }
                if (String.IsNullOrEmpty(fileName) == false)
                {
                    CircuitData data = CircuitConverter.Instance.ConvertFromCircuit(circuitEditor.Circuit);
                    data.Signals = SignalConverter.Instance.ConvertFromSignalList(circuitEditor.SignalList);
                    CircuitSerializer.SerializeCircuit(fileName, data);
                    m_Persistent = true;
                    FileName = fileName;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                if (e.InnerException != null)
                {
                    msg += "\n" + e.InnerException.Message;
                }
                MessageBox.Show(msg + "\n-----\n" + e.StackTrace, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void SaveDocumentAs()
        {
            SaveAs(null);
        }

        public void SaveDocument()
        {
            SaveAs(m_FileName);
        }

        public bool LoadDocument(string fileName)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName) == false)
                {
                    CircuitData circuitData = CircuitSerializer.DeserializeCircuit(fileName);
                    circuitEditor.SignalList = SignalConverter.Instance.ConvertToSignalList(circuitData.Signals);
                    circuitEditor.Circuit = CircuitConverter.Instance.ConvertToCircuit(circuitData, circuitEditor.SignalList, true);
                    m_Simulation.Signals = circuitEditor.SignalList;
                    m_Simulation.Circuit = circuitEditor.Circuit;

                    FileName = fileName;
                    return true;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                if (e.InnerException != null)
                {
                    msg += "\n" + e.InnerException.Message;
                }
                MessageBox.Show(msg + "\n-----\n" + e.StackTrace, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }

        private void HandleSizeChanged(object sender, EventArgs e)
        {
            circuitEditor.Size = this.Size;
            this.AutoScrollMinSize = this.Size;
        }

        private void CircuitEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Persistent == false)
            {
                string msg = String.Copy(Text);
                if (MessageBox.Show(String.Format("Die Schaltung \"{0}\" wurde verändert.\nMöchten Sie die Änderungen speichern?", msg.TrimEnd('*')),
                    "Hinweis", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    SaveDocument();
                }
            }
        }

        private void circuitEditor_OnCircuitChanged(object sender)
        {
            m_Persistent = false;
            if (Text.EndsWith("*") == false)
            {
                Text += "*";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace DigitalClasses.Controls
{
    public partial class SignalPanel : UserControl
    {
        #region Fields

        private SignalList m_SignalList;
        private List<SignalEditor> m_EditorControls = new List<SignalEditor>();
        private int m_ScaleStep = SignalEditor.c_GridStep;
        private bool m_IgnoreEvent;

        #endregion

        #region Properties

        /// <summary>
        /// The signals displayed by the contained editors
        /// </summary>
        [Browsable(false)]
        public SignalList SignalList
        {
            get
            {
                return m_SignalList;
            }
            set
            {
                if (m_SignalList == null || !m_SignalList.Equals(value))
                {
                    if (m_SignalList != null)
                        m_SignalList.OnListChanged -= SignalList_OnListChanged;
                    m_SignalList = value;
                    if (m_SignalList != null)
                        m_SignalList.OnListChanged += new DigitalClasses.Events.NotifyEvent(SignalList_OnListChanged);
                    ApplyListToControls();
                }
            }
        }

        /// <summary>
        /// Defines the StepSize of the editors
        /// </summary>
        [DefaultValue(SignalEditor.c_GridStep), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Appearance"), Description("Bestimmt die Breite des Rasters in den Editoren.")]
        public int ScaleStep
        {
            get
            {
                return m_ScaleStep;
            }
            set
            {
                if (m_ScaleStep != value && value > 0)
                {
                    if (m_ScaleStep == 1 && value > 2)
                    {
                        m_ScaleStep = 2;
                    }
                    else
                    {
                        if (value == 0)
                        {
                            m_ScaleStep = 1;
                        }
                        else
                        {
                            m_ScaleStep = value;
                        }
                    }
                    ApplyScale();
                }
            }
        }

        #endregion

        #region Construction

        public SignalPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        #endregion

        #region Overrides

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (SignalEditor editor in m_EditorControls)
            {
                editor.Width = panel.Width;
                editor.Invalidate();
            }
            Invalidate();
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Adds an impulse to all signals
        /// </summary>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            int newmax = m_SignalList.MaxSignalLength + 1;
            foreach (Signal signal in m_SignalList)
            {
                while (signal.SignalLength < newmax)
                {
                    signal.Add(new DurationState(State.Low, 2));
                }
            }
        }

        /// <summary>
        /// Removes the last impulse from all signals
        /// </summary>
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            m_IgnoreEvent = true;
            int newmax = m_SignalList.MaxSignalLength - 1;
            foreach (Signal signal in m_SignalList)
            {
                while (signal.Count > newmax)
                {
                    signal.RemoveAt(signal.Count - 1);
                }
            }
            m_IgnoreEvent = false;
        }

        /// <summary>
        /// Reduces the scale step
        /// </summary>
        private void btn_ScaleDown_Click(object sender, EventArgs e)
        {
            ScaleStep -= 2;
        }

        /// <summary>
        /// Increases the scale step
        /// </summary>
        private void btn_ScaleUp_Click(object sender, EventArgs e)
        {
            ScaleStep += 2;
        }

        /// <summary>
        /// Event handler when the SignalList changes
        /// </summary>
        /// <param name="sender"></param>
        private void SignalList_OnListChanged(object sender)
        {
            if (InvokeRequired)
            {
                //handle thread safety
                NotifyEvent d = new NotifyEvent(SignalList_OnListChanged);
                Invoke(d, new object[] { sender });
            }
            else
            {
                ApplyListToControls();
            }
        }

        /// <summary>
        /// Applies changes in the List to be correctly represented by editors
        /// </summary>
        private void ApplyListToControls()
        {
            int count = 0;
            if (m_SignalList != null)
            {
                count = m_SignalList.Count;
            }
            if (count < m_EditorControls.Count)
            {
                //remove controls
                for (int i = m_EditorControls.Count - 1; i >= count; i--)
                {
                    SignalEditor editor = m_EditorControls[i];
                    editor.OnStepChanged -= editor_OnStepChanged;
                    editor.Signal.OnSignalChanged -= SignalChangedEventHandler;
                    m_EditorControls.RemoveAt(i);
                    editor.Hide();
                    editor.Dispose();
                }
            }
            if (m_EditorControls.Count < count)
            {
                //add controls
                for (int i = m_EditorControls.Count; i < count; i++)
                {
                    SignalEditor editor = CreateEditorControl();
                    editor.Signal = m_SignalList[i];
                    editor.Signal.OnSignalChanged += new SignalChanged(SignalChangedEventHandler);
                    m_EditorControls.Add(editor);
                }
                m_IgnoreEvent = true;
                m_SignalList.ForceConsistency();
                m_IgnoreEvent = false;
            }
            for (int i = 0; i < m_EditorControls.Count; i++)
            {
                if (!m_EditorControls[i].Signal.Equals(m_SignalList[i]))
                {
                    m_EditorControls[i].Signal.OnSignalChanged -= SignalChangedEventHandler;
                    m_EditorControls[i].Signal = m_SignalList[i];
                    m_EditorControls[i].Signal.OnSignalChanged += new SignalChanged(SignalChangedEventHandler);
                }
            }
        }

        /// <summary>
        /// Event handler on signal change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="change">type of the changes</param>
        void SignalChangedEventHandler(object sender, SignalChangeType change)
        {
            //if (m_IgnoreEvent == false && change == SignalChangeType.SignalGraph)
            //{
            //    bool old = m_IgnoreEvent;
            //    m_IgnoreEvent = true;
            //    m_SignalList.ForceConsistency();
            //    m_IgnoreEvent = old;
            //}
        }

        /// <summary>
        /// Creates a new editor control
        /// </summary>
        /// <returns>created control</returns>
        private SignalEditor CreateEditorControl()
        {
            int location = 0;
            if (m_EditorControls.Count > 0)
            {
                SignalEditor last = m_EditorControls.Last();
                location = last.Top + last.Height;
            }
            SignalEditor editor = new SignalEditor();
            editor.Parent = panel;
            editor.BackColor = BackColor;
            editor.GridColor = Color.FromArgb(64, 64, 64);
            editor.Font = new Font(editor.Font, FontStyle.Bold);
            editor.StepSize = m_ScaleStep;
            editor.Top = location;
            editor.Width = panel.Width;
            editor.OnStepChanged += new DigitalClasses.Events.NotifyEvent(editor_OnStepChanged);
            return editor;
        }

        /// <summary>
        /// Event handler when the scale within an editor was changed
        /// </summary>
        void editor_OnStepChanged(object sender)
        {
            SignalEditor editor = sender as SignalEditor;
            if (editor != null && m_EditorControls.Contains(editor))
            {
                ScaleStep = editor.StepSize;
            }
        }

        /// <summary>
        /// Applies the new width of the grid to all editors
        /// </summary>
        private void ApplyScale()
        {
            foreach (SignalEditor editor in m_EditorControls)
            {
                editor.StepSize = m_ScaleStep;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DigitalClasses.Logic;
using System.Drawing.Drawing2D;
using DigitalClasses.Events;

namespace DigitalClasses.Controls
{
    public partial class SignalEditor : UserControl
    {
        #region Events

        public event NotifyEvent OnStepChanged;

        #endregion

        #region Constants

        // Drawing Constants
        private const int c_GridTop = 20;       // margin from top
        private const int c_GridBottom = 14;    // margin from bottom
        private const int c_GridLeft = 12;      // margin from left
        private const int c_GridRight = 5;      // margin from right
        internal const int c_GridStep = 4;      // default grid size

        private static Cursor PositiveEdge = new Cursor(new System.IO.MemoryStream(Properties.Resources.PositiveEdge));
        private static Cursor NegativeEdge = new Cursor(new System.IO.MemoryStream(Properties.Resources.NegativeEdge));
        private static Cursor RemoveHighState = new Cursor(new System.IO.MemoryStream(Properties.Resources.RemoveHighState));
        private static Cursor RemoveLowState = new Cursor(new System.IO.MemoryStream(Properties.Resources.RemoveLowState));
        private static Cursor MoveEdge = new Cursor(new System.IO.MemoryStream(Properties.Resources.MoveEdge));

        #endregion

        #region Fields

        private Image m_Background;
        private Image m_Drawing;
        private Color m_GridColor = Color.Black;
        private Color m_SignalColor = Color.Lime;
        private Color m_ScaleColor = Color.Silver;
        private int m_StepSize = 30;
        private int m_MaxSteps = 0;
        private Signal m_Signal;
        private bool m_controlPressed;

        private int m_EdgeDragIndex = -1;
        private int m_EdgeDragStep = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Defines the color of the Grid
        /// </summary>
        [DefaultValue(typeof(Color), "0x000000"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Appearance"), Description("Bestimmt die Farbe des Rasters.")]
        public Color GridColor
        {
            get
            {
                return m_GridColor;
            }
            set
            {
                if (m_GridColor != value && value != null)
                {
                    m_GridColor = value;
                    UpdateBackground();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Defines the color of the impulse diagram
        /// </summary>
        [DefaultValue(typeof(Color), "0x00FF00"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Appearance"), Description("Bestimmt die Farbe des Diagramms.")]
        public Color SignalColor
        {
            get
            {
                return m_SignalColor;
            }
            set
            {
                if (m_SignalColor != value && value != null)
                {
                    m_SignalColor = value;
                    UpdateDrawing();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Defines the color of the scale
        /// </summary>
        [DefaultValue(typeof(Color), "0xC0C0C0"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Appearance"), Description("Bestimmt die Farbe der Skala.")]
        public Color ScaleColor
        {
            get
            {
                return m_ScaleColor;
            }
            set
            {
                if (m_ScaleColor != value && value != null)
                {
                    m_ScaleColor = value;
                    UpdateBackground();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Defines the width of one step of the grid
        /// </summary>
        [DefaultValue(c_GridStep), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Appearance"), Description("Bestimmt die Breite eines Schritts des Rasters.")]
        public int StepSize
        {
            get
            {
                return m_StepSize;
            }
            set
            {
                if (m_StepSize != value && value > 0)
                {
                    if (m_StepSize == 1 && value > 2)
                    {
                        m_StepSize = 2;
                    }
                    else
                    {
                        if (value == 0)
                        {
                            m_StepSize = 1;
                        }
                        else
                        {
                            m_StepSize = value;
                        }
                    }
                    if (m_Signal != null && Width < m_Signal.Count * m_StepSize + c_GridLeft + c_GridRight)
                    {
                        Width = m_Signal.Count * m_StepSize + c_GridLeft + c_GridRight;
                    }
                    UpdateBackground();
                    UpdateDrawing();
                    Invalidate();
                    if (OnStepChanged != null)
                    {
                        OnStepChanged(this);
                    }
                }
            }
        }

        /// <summary>
        /// Changes the size of the control
        /// </summary>
        new public Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = CheckSize(value);
                int newSteps = CalcMaxSteps();
                if (newSteps != m_MaxSteps)
                {
                    m_MaxSteps = newSteps;
                    UpdateBackground();
                    UpdateDrawing();
                    Invalidate();
                }
            }
        }

        new public int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                Size size = CheckSize(new Size(value, Height));
                base.Width = size.Width;
            }
        }

        new public int Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                Size size = CheckSize(new Size(Width, value));
                base.Height = size.Height;
            }
        }

        /// <summary>
        /// Returns the minimum dimensions of this editor to display the whole signal
        /// </summary>
        new public Size MinimumSize
        {
            get
            {
                int count = 0;
                if (m_Signal != null)
                {
                    count = m_Signal.Count;
                }
                return new Size(count * m_StepSize + c_GridLeft + c_GridRight,
                    c_GridTop + c_GridStep + c_GridBottom);
            }
        }

        /// <summary>
        /// The signal displayed by this editor
        /// </summary>
        [Browsable(false)]
        public Signal Signal
        {
            get
            {
                return m_Signal;
            }
            set
            {
                if (m_Signal == null || !m_Signal.Equals(value))
                {
                    if (m_Signal != null)
                        m_Signal.OnSignalChanged -= signal_OnSignalChanged;
                    m_Signal = value;
                    if (m_Signal != null)
                        m_Signal.OnSignalChanged += new SignalChanged(signal_OnSignalChanged);
                    if (m_Signal != null && Width < m_Signal.Count * m_StepSize + c_GridLeft + c_GridRight)
                    {
                        Width = m_Signal.Count * m_StepSize + c_GridLeft + c_GridRight;
                    }
                    UpdateBackground();
                    UpdateDrawing();
                    Invalidate();
                }
            }
        }

        #endregion

        #region Construction

        public SignalEditor()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Checks the Size for minimum requirements
        /// </summary>
        /// <param name="value">size value</param>
        /// <returns>correct size</returns>
        private Size CheckSize(Size value)
        {
            Size newsize = value;
            if (m_Signal != null && m_Signal.Count > 0)
            {
                if (newsize.Width < m_Signal.SignalLength * m_StepSize + c_GridLeft + c_GridRight)
                {
                    newsize.Width = m_Signal.SignalLength * m_StepSize + c_GridLeft + c_GridRight;
                }
                if (newsize.Height < c_GridTop + c_GridStep + c_GridBottom)
                {
                    newsize.Height = c_GridTop + c_GridStep + c_GridBottom;
                }
            }
            return newsize;
        }

        private int CalcMaxSteps()
        {
            return (Width - c_GridLeft - c_GridRight) / m_StepSize;
        }

        /// <summary>
        /// Updates the background drawing
        /// </summary>
        protected virtual void UpdateBackground()
        {
            if (Width == 0 || Height == 0)
            {
                return;
            }
            m_Background = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(m_Background);
            //g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
            Pen pen = new Pen(m_GridColor);
            if (StepSize > 0)
            {
                int count = (Width - c_GridLeft - c_GridRight) / m_StepSize;
                if (m_Signal != null && m_Signal.SignalLength > count)
                {
                    count = m_Signal.SignalLength;
                }
                Bitmap backbmp = (m_Background as Bitmap);
                for (int i = c_GridLeft + m_StepSize; i < Width - c_GridRight; i += m_StepSize)
                {
                    for (int j = c_GridTop; j < Height - c_GridBottom; j += 4)
                    {
                        backbmp.SetPixel(i, j, m_GridColor);
                    }
                }
                g.DrawLine(pen, c_GridLeft, c_GridTop, Width - c_GridRight, c_GridTop);
                g.DrawLine(pen, c_GridLeft, Height - c_GridBottom, Width - c_GridRight, Height - c_GridBottom);
            }
            if (m_Signal != null)
                g.DrawString(m_Signal.Name, Font, new SolidBrush(Color.White), new PointF(c_GridLeft, 4));
            pen = new Pen(m_ScaleColor);
            g.DrawLine(pen, c_GridLeft - 4, c_GridTop - 4, c_GridLeft - 4, Height - (c_GridBottom - 4));
            g.DrawLine(pen, c_GridLeft - 4, Height - (c_GridBottom - 4), Width - c_GridRight, Height - (c_GridBottom - 4));
            Font small = new Font(Font.FontFamily, 5.0f);
            g.DrawString("H", small, new SolidBrush(m_ScaleColor), new PointF(1, c_GridTop));
            g.DrawString("L", small, new SolidBrush(m_ScaleColor), new PointF(1, Height - (c_GridBottom + 6)));
        }

        /// <summary>
        /// Updates the signal drawing
        /// </summary>
        protected virtual void UpdateDrawing()
        {
            if (Width == 0 || Height == 0)
            {
                return;
            }
            m_Drawing = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(m_Drawing);
            //g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, Width, Height);

            if (m_Signal != null)
            {
                Pen pen = new Pen(m_SignalColor);
                int stepsum = 0;
                for (int i = 0; i < m_Signal.Count; i++)
                {
                    int y = Height - c_GridBottom;
                    if (m_Signal[i].State.Equals(State.High))
                    {
                        y = c_GridTop;
                    }
                    g.DrawLine(pen, stepsum * m_StepSize + c_GridLeft, y, (stepsum + m_Signal[i].Duration) * m_StepSize + c_GridLeft, y);

                    if (i > 0 && i <= m_Signal.Count)
                    {
                        y = Height - c_GridBottom;
                        int y2 = c_GridTop;
                        g.DrawLine(pen, stepsum * m_StepSize + c_GridLeft, y, stepsum * m_StepSize + c_GridLeft, y2);
                    }

                    stepsum += m_Signal[i].Duration;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateBackground();
            UpdateDrawing();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_Drawing != null)
            {
                e.Graphics.DrawImage(m_Drawing, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (m_Background != null)
            {
                e.Graphics.DrawImage(m_Background, 0, 0);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_Signal.IsReadOnly || e.X < c_GridLeft || e.X > Width - c_GridRight || e.Y < c_GridTop || e.Y > Height - c_GridBottom)
            {
                Cursor = Cursors.Default;
                return;
            }
            int step = (int)Math.Round((e.X - c_GridLeft) / (double)m_StepSize);

            //move edge
            if (m_EdgeDragIndex > -1 && m_EdgeDragStep != step)
            {
                int newDuration = step - m_Signal.GetBeginOfStateAtIndex(m_EdgeDragIndex);
                int newSuccDuration = -1;
                bool successor = false;
                if (m_controlPressed & m_EdgeDragIndex + 1 < m_Signal.Count)
                {
                    successor = true;
                    newSuccDuration = m_Signal[m_EdgeDragIndex + 1].Duration + (m_Signal[m_EdgeDragIndex].Duration - newDuration);
                }
                if ((newDuration > 0 && newSuccDuration > 0 && successor) || (newDuration > 0 && successor == false))
                {
                    if (successor && newSuccDuration > 0)
                    {
                        m_Signal[m_EdgeDragIndex + 1].Duration = newSuccDuration;
                    }
                    m_Signal[m_EdgeDragIndex].Duration = newDuration;
                    UpdateDrawing();
                    Invalidate();
                }
                Cursor = MoveEdge;
                return;
            }

            //change cursor
            State atstep = m_Signal.GetStateAtStep(step);
            if (State.High.Equals(atstep))
            {
                if (e.Y - c_GridTop < 6)
                    Cursor = RemoveHighState;
                else
                {
                    if (Math.Abs(e.X - c_GridLeft - step * m_StepSize) < 2 && m_Signal.IsEdge(step))
                        Cursor = MoveEdge;
                    else
                        Cursor = NegativeEdge;
                }
            }
            else
            {
                if (atstep != null && (Height - c_GridBottom) - e.Y < 6)
                    Cursor = RemoveLowState;
                else
                {
                    if (Math.Abs(e.X - c_GridLeft - step * m_StepSize) < 2 && m_Signal.IsEdge(step))
                        Cursor = MoveEdge;
                    else
                        Cursor = PositiveEdge;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (m_Signal.IsReadOnly || e.Button != MouseButtons.Left || e.X < c_GridLeft
                || e.X > Width - c_GridRight || e.Y < c_GridTop || e.Y > Height - c_GridBottom)
            {
                return;
            }
            int step = (int)Math.Round((e.X - c_GridLeft) / (double)m_StepSize);
            if (Math.Abs(e.X - c_GridLeft - step * m_StepSize) < 2 && m_Signal.IsEdge(step))
            {
                m_EdgeDragStep = step;
                m_EdgeDragIndex = m_Signal.GetIndexAtStep(step - 1);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (m_Signal.IsReadOnly || e.Button != MouseButtons.Left || e.X < c_GridLeft
                || e.X > Width - c_GridRight || e.Y < c_GridTop || e.Y > Height - c_GridBottom)
            {
                return;
            }
            if (m_EdgeDragStep > -1)
            {
                //deselect current edge
                m_EdgeDragStep = -1;
                m_EdgeDragIndex = -1;
            }
            else
            {
                //place a new edge
                int step = (int)Math.Round((e.X - c_GridLeft) / (double)m_StepSize);
                State atstep = m_Signal.GetStateAtStep(step);
                if (State.High.Equals(atstep))
                {
                    if (e.Y - c_GridTop < 6)
                    {
                        m_Signal.MergeStep(step);
                    }
                    else
                    {
                        m_Signal.InsertStateAtStep(new DurationState(State.Low, 2), step);
                    }
                }
                else
                {
                    if (atstep != null && (Height - c_GridBottom) - e.Y < 6)
                    {
                        m_Signal.MergeStep(step);
                    }
                    else
                    {
                        m_Signal.InsertStateAtStep(new DurationState(State.High, 2), step);
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!m_controlPressed)
            {
                base.OnMouseWheel(e);
                return;
            }
            //if (e.Delta > 0)
            //{
            //    StepSize -= 5;
            //}
            //else if (e.Delta < 0)
            //{
            //    StepSize += 5;
            //}
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            m_controlPressed = e.Control;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            m_controlPressed = e.Control;
        }

        private void signal_OnSignalChanged(object sender, SignalChangeType change)
        {
            if (InvokeRequired)
            {
                //handle thread safety
                SignalChanged d = new SignalChanged(signal_OnSignalChanged);
                try
                {
                    if (IsDisposed == false)
                        Invoke(d, new object[] { sender, change });
                }
                catch (ObjectDisposedException)
                {
                    //although it should be not possible, catch it here
                }
            }
            else
            {
                Size = CheckSize(Size);
                if (change == SignalChangeType.SignalGraph)
                    UpdateDrawing();
                else
                    UpdateBackground();
                Invalidate();
            }
        }

        #endregion
    }
}

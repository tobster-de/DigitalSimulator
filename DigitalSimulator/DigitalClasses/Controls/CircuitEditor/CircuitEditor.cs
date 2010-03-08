using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DigitalClasses.Graphic;
using DigitalClasses.Events;
using DigitalClasses.Logic;

namespace DigitalClasses.Controls
{
    public partial class CircuitEditor : UserControl
    {
        public enum GridModes
        {
            Dots,
            Lines
        }

        #region Events

        [Category("Events"), Description("Bei Änderungen an der Schaltung ausgelöst.")]
        public event NotifyEvent CircuitChanged;

        #endregion

        #region Fields

        private Point m_GridSize;
        private Color m_GridColor;
        private GridModes m_GridMode;

        private Image m_Background;
        private Image m_Drawing;

        private CircuitTool m_CurrentTool;
        private SignalList m_SignalList;
        private Circuit m_Circuit;

        private Keys m_ControlKeys;

        #endregion

        #region Properties

        [DefaultValue(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
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
                Size newsize = value;
                //TODO: check the size

                base.Size = newsize;
            }
        }

        [DefaultValue(typeof(Point), "50; 50"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Data"), Description("Bestimmt Höhe und Breite des Rasters.")]
        public Point GridSize
        {
            get
            {
                return m_GridSize;
            }
            set
            {
                if (m_GridSize != value)
                {
                    m_GridSize = value;
                    UpdateBackground();
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(GridModes), "Lines"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Data"), Description("Bestimmt Art des Rasters.")]
        public GridModes GridMode
        {
            get
            {
                return m_GridMode;
            }
            set
            {
                if (m_GridMode != value)
                {
                    m_GridMode = value;
                    UpdateBackground();
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "Empty"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Data"), Description("Bestimmt die Farbe des Rasters.")]
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

        ///// <summary>
        ///// The elements of a circuit that is drawn
        ///// </summary>
        //[Browsable(false)]
        //public List<GraphicBaseElement> Elements
        //{
        //    get
        //    {
        //        return new List<GraphicBaseElement>(m_Elements);
        //    }
        //}

        /// <summary>
        /// The circuit that is drawn by this editor
        /// </summary>
        [Browsable(false)]
        public Circuit Circuit
        {
            get
            {
                return m_Circuit;
            }
            set
            {
                if (m_Circuit.Equals(value) == false)
                {
                    foreach (BaseElement element in m_Circuit)
                    {
                        (element.LinkedObject as GraphicBaseElement).OnUpdateDrawing -= element_OnUpdateDrawing;
                    }
                    m_Circuit = value;
                    foreach (BaseElement element in m_Circuit)
                    {
                        (element.LinkedObject as GraphicBaseElement).OnUpdateDrawing += new UpdateDrawingEvent(element_OnUpdateDrawing);
                    }
                    UpdateDrawing();
                }
            }
        }

        /// <summary>
        /// The current tool used in the editor
        /// </summary>
        [Browsable(false)]
        public CircuitTool CurrentTool
        {
            get
            {
                return m_CurrentTool;
            }
            set
            {
                //if (m_CurrentTool != null)
                //{
                //    m_CurrentTool.OnNewElement -= NewElementHandler;
                //}
                m_CurrentTool = value;
                if (m_CurrentTool != null)
                {
                    m_CurrentTool.CircuitEditor = this;
                    m_CurrentTool.Reset();
                    //m_CurrentTool.OnNewElement += new NewElement(NewElementHandler);
                }
            }
        }

        public SignalList SignalList
        {
            get
            {
                return m_SignalList;
            }
            set
            {
                m_SignalList = value;
            }
        }

        #endregion

        #region Construction

        public CircuitEditor()
        {
            InitializeComponent();
            //m_Elements = new List<GraphicBaseElement>();
            m_Circuit = new Circuit();
            m_GridMode = GridModes.Lines;
            m_SignalList = new SignalList();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Align a location to the grid
        /// </summary>
        /// <param name="location">location to align</param>
        /// <returns>aligned location</returns>
        public PointF AlignToGrid(PointF location)
        {
            return new PointF((float)(Math.Round(location.X / (float)m_GridSize.X)) * m_GridSize.X,
                ((float)Math.Round(location.Y / (float)m_GridSize.Y)) * m_GridSize.Y);
        }

        /// <summary>
        /// Align a location to the grid
        /// </summary>
        /// <param name="location">location to align</param>
        /// <returns>aligned location</returns>
        public Point AlignToGrid(Point location)
        {
            return new Point(((int)(location.X / m_GridSize.X)) * m_GridSize.X,
                ((int)(location.Y / m_GridSize.Y)) * m_GridSize.Y);
        }

        /// <summary>
        /// Adds a new element to the circuit
        /// </summary>
        /// <param name="element">the element to add</param>
        public void AddElement(GraphicBaseElement element)
        {
            if (element != null)
            {
                m_Circuit.AddElement(element.LinkedObject as BaseElement);
                element.OnUpdateDrawing += new UpdateDrawingEvent(element_OnUpdateDrawing);
                if (element is GraphicInput && element.LinkedObject is SignalInput)
                {
                    m_SignalList.Add((element.LinkedObject as SignalInput).Signal);
                }
                RaiseChangedEvent();
                UpdateDrawing();
                Invalidate();
            }
        }

        /// <summary>
        /// Removes an element from the circuit
        /// </summary>
        /// <param name="element">the element to remove</param>
        public void RemoveElement(GraphicBaseElement element)
        {
            if (element != null && m_Circuit.ContainsElement(element.LinkedObject as BaseElement))
            {
                m_Circuit.RemoveElement(element.LinkedObject as BaseElement);
                element.OnUpdateDrawing -= element_OnUpdateDrawing;
                if (element is GraphicInput && element.LinkedObject is SignalInput)
                {
                    m_SignalList.Remove((element.LinkedObject as SignalInput).Signal);
                }
                RaiseChangedEvent();
                UpdateDrawing();
                Invalidate();
            }
        }

        /// <summary>
        /// Returns the element at a given location
        /// </summary>
        /// <param name="location">Location of interest</param>
        /// <returns>Element at the given location or null respectively</returns>
        public GraphicBaseElement GetElementAt(PointF location)
        {
            //foreach (GraphicBaseElement element in m_Elements)
            foreach (BaseElement logic in m_Circuit)
            {
                GraphicBaseElement element = logic.LinkedObject as GraphicBaseElement;
                RectangleF bounds = element.Bounds;
                if (element is GraphicConnection)
                {
                    GraphicConnection connection = element as GraphicConnection;
                    if (connection.BelongsTo(location))
                    {
                        return element;
                    }
                }
                if (bounds.Contains(location))
                {
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Updates the circuit drawing
        /// </summary>
        public virtual void UpdateDrawing()
        {
            if (Width == 0 || Height == 0)
            {
                return;
            }
            m_Drawing = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(m_Drawing);
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, Width, Height);
            foreach (BaseElement logic in m_Circuit)
            {
                GraphicBaseElement element = logic.LinkedObject as GraphicBaseElement;
                element.Paint(g);
            }
        }

        /// <summary>
        /// Updates the background drawing
        /// </summary>
        public virtual void UpdateBackground()
        {
            if (Width == 0 || Height == 0)
            {
                return;
            }
            m_Background = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(m_Background);
            g.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
            Pen pen = new Pen(m_GridColor);
            if (GridSize.X > 0 && GridSize.Y > 0)
            {
                for (int i = m_GridSize.X; i < Width; i += m_GridSize.X)
                {
                    for (int j = m_GridSize.X; j < Height; j += m_GridSize.Y)
                    {
                        if (m_GridMode.Equals(GridModes.Dots))
                        {
                            (m_Background as Bitmap).SetPixel(i, j, m_GridColor);
                        }
                        if (m_GridMode.Equals(GridModes.Lines))
                        {
                            g.DrawLine(pen, 0, j, Width, j);
                        }
                    }
                    if (m_GridMode.Equals(GridModes.Lines))
                    {
                        g.DrawLine(pen, i, 0, i, Height);
                    }
                }
            }
            foreach (BaseElement logic in m_Circuit)
            {
                GraphicBaseElement element = logic.LinkedObject as GraphicBaseElement;
                //if (element.Highlighted)
                element.PaintBackground(g);
            }
        }

        /// <summary>
        /// Updates the circuit
        /// </summary>
        public void UpdateCircuit()
        {
            m_Circuit.Update();
        }

        #endregion

        #region Private Implementation

        internal void RaiseChangedEvent()
        {
            if (CircuitChanged != null)
            {
                CircuitChanged(this);
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
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            m_ControlKeys = e.Modifiers;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            m_ControlKeys = e.Modifiers;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (m_CurrentTool != null && m_Circuit.ReadOnly == false)
            {
                m_CurrentTool.MouseClick(e.Location, m_ControlKeys);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (m_CurrentTool != null && m_Circuit.ReadOnly == false)
            {
                m_CurrentTool.MouseDown(e.Location, m_ControlKeys);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (m_CurrentTool != null && m_Circuit.ReadOnly == false)
            {
                m_CurrentTool.MouseUp(e.Location, m_ControlKeys);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_CurrentTool != null && m_Circuit.ReadOnly == false)
            {
                m_CurrentTool.MouseMove(e.Button, e.Location, m_ControlKeys);
            }
        }

        /// <summary>
        /// Event handler when changes in elements occur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="completeDrawing">update complete drawing</param>
        private void element_OnUpdateDrawing(object sender, bool completeDrawing)
        {
            if (InvokeRequired)
            {
                //handle thread safety
                UpdateDrawingEvent d = new UpdateDrawingEvent(element_OnUpdateDrawing);
                Invoke(d, new object[] { sender, completeDrawing });
                return;
            }
            if (completeDrawing)
            {
                UpdateDrawing();
                Invalidate();
            }
            else
            {
                // only update particular object
                GraphicBaseElement element = sender as GraphicBaseElement;
                if (element != null)
                {
                    Graphics g = Graphics.FromImage(m_Drawing);
                    element.Paint(g);
                    Graphics control = this.CreateGraphics();
                    control.DrawImage(m_Drawing, 0, 0);
                }
            }
        }

        #endregion
    }
}


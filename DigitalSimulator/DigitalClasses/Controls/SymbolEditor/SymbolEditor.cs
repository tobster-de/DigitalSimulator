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
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Controls
{
    public partial class SymbolEditor : UserControl
    {
        #region Events

        [Category("Events"), Description("Bei Änderungen an des Symbols ausgelöst.")]
        public event NotifyEvent SymbolChanged;

        #endregion

        #region Fields

        private Point m_Offset;
        private Point m_GridSize;
        private Color m_GridColor;

        private Image m_Background;
        private Image m_Drawing;
        //private List<SymbolPart> m_Parts;
        private Symbol m_Symbol;
        private SymbolPart m_SelectedPart;
        private PointF m_SelectionOffset;
        private SymbolTool m_CurrentTool;
        private GrabHandle m_GrabHandle;

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

        [DefaultValue(typeof(Point), "4; 4"), RefreshProperties(RefreshProperties.Repaint)]
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

        [DefaultValue(typeof(Point), "0; 0"), RefreshProperties(RefreshProperties.Repaint)]
        [Category("Data"), Description("Versetzt den Koordinaten-Urspung um diese Werte.")]
        public Point Offset
        {
            get
            {
                return m_Offset;
            }
            set
            {
                if (m_Offset != value)
                {
                    m_Offset = value;
                    UpdateBackground();
                    UpdateDrawing();
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
        ///// The GraphicsPath of a symbol that is drawn
        ///// </summary>
        //[Browsable(false)]
        //public GraphicsPath Path
        //{
        //    get
        //    {
        //        return m_Path;
        //    }
        //    set
        //    {
        //        m_Path.Reset();
        //        if (value != null && value.PointCount > 0)
        //        {
        //            m_Path.AddPath(value, false);
        //        }
        //        UpdateBackground();
        //        UpdateDrawing();
        //        Invalidate();
        //    }
        //}

        ///// <summary>
        ///// The GraphicsShape of a symbol that is drawn
        ///// </summary>
        //[Browsable(false)]
        //public GraphicShape Shape
        //{
        //    get
        //    {
        //        return new GraphicShape(m_Path);
        //    }
        //    set
        //    {
        //        m_Path.Reset();
        //        if (value != null && value.Path.PointCount > 0)
        //        {
        //            m_Path.AddPath(value.Path, false);
        //        }
        //        UpdateBackground();
        //        UpdateDrawing();
        //        Invalidate();
        //    }
        //}

        /// <summary>
        /// The current tool used in the editor
        /// </summary>
        [Browsable(false)]
        public SymbolTool CurrentTool
        {
            get
            {
                return m_CurrentTool;
            }
            set
            {
                if (m_CurrentTool != null)
                {
                    m_CurrentTool.OnNewSymbolPart -= NewPartElementHandler;
                    m_CurrentTool.SymbolEditor = null;
                }
                m_CurrentTool = value;
                if (m_CurrentTool != null)
                {
                    m_CurrentTool.Reset();
                    m_CurrentTool.OnNewSymbolPart += new NewSymbolPart(NewPartElementHandler);
                    m_CurrentTool.SymbolEditor = this;
                }
            }
        }

        /// <summary>
        /// The symbol that is drawn by this editor
        /// </summary>
        [Browsable(false)]
        public Symbol Symbol
        {
            get
            {
                return m_Symbol;
            }
            set
            {
                if (m_Symbol.Equals(value) == false)
                {
                    m_Symbol = value;
                    UpdateDrawing();
                    Invalidate();
                }
            }
        }

        #endregion

        #region Construction

        public SymbolEditor()
        {
            InitializeComponent();
            //m_Parts = new List<SymbolPart>();
            m_Symbol = new Symbol();
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
            return new PointF(((int)(location.X / m_GridSize.X)) * m_GridSize.X,
                ((int)(location.Y / m_GridSize.Y)) * m_GridSize.Y);
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
        /// Translate a coordinate with the values of the current offset
        /// </summary>
        /// <param name="location">the location to translate</param>
        /// <param name="alignToGrid">depicts if the location additionally should be aligned to the grid</param>
        /// <returns>translated location</returns>
        public PointF Translate(PointF location, bool alignToGrid)
        {
            PointF point = new PointF(location.X - m_Offset.X, location.Y - m_Offset.Y);
            if (alignToGrid)
            {
                return AlignToGrid(point);
            }
            return point;
        }

        /// <summary>
        /// Translate a coordinate with the values of the current offset
        /// </summary>
        /// <param name="location">the location to translate</param>
        /// <param name="alignToGrid">depicts if the location additionally should be aligned to the grid</param>
        /// <returns>translated location</returns>
        public Point Translate(Point location, bool alignToGrid)
        {
            PointF point = Translate((PointF)location, alignToGrid);
            return new Point((int)Math.Truncate(point.X), (int)Math.Truncate(point.Y));
        }

        /// <summary>
        /// Translate a coordinate to deny the current offset
        /// </summary>
        /// <param name="location">the location to translate</param>
        /// <param name="alignToGrid">depicts if the location additionally should be aligned to the grid</param>
        /// <returns>translated location</returns>
        public PointF ReverseTranslate(PointF location, bool alignToGrid)
        {
            PointF point = new PointF(location.X + m_Offset.X, location.Y + m_Offset.Y);
            if (alignToGrid)
            {
                return AlignToGrid(point);
            }
            return point;
        }

        /// <summary>
        /// Translate a coordinate to deny the current offset
        /// </summary>
        /// <param name="location">the location to translate</param>
        /// <param name="alignToGrid">depicts if the location additionally should be aligned to the grid</param>
        /// <returns>translated location</returns>
        public Point ReverseTranslate(Point location, bool alignToGrid)
        {
            Point point = new Point(location.X + m_Offset.X, location.Y + m_Offset.Y);
            if (alignToGrid)
            {
                return AlignToGrid(point);
            }
            return point;
        }

        /// <summary>
        /// Selects a part at the given location
        /// </summary>
        /// <param name="location">location of interest</param>
        /// <returns></returns>
        public SymbolPart GetPartAt(PointF location)
        {
            int found = 0;
            SymbolPart foundPart = null;
            foreach (SymbolPart part in m_Symbol)
            {
                if (part.IsNear(location))
                {
                    found++;
                    foundPart = part;
                }
            }
            if (found == 1)
            {
                return foundPart;
            }
            return null;

        }

        public int SelectPartAt(PointF location)
        {
            int found = 0;
            foreach (SymbolPart part in m_Symbol)
            {
                if (part.IsNear(location))
                {
                    found++;
                    m_GrabHandle = part.GetGrabHandle(location);
                    if (part.Equals(m_SelectedPart) == false)
                    {
                        if (m_SelectedPart != null)
                            m_SelectedPart.IsSelected = false;
                        part.IsSelected = true;
                        m_SelectedPart = part;
                        UpdateBackground();
                        Invalidate();
                    }
                }
            }
            if (found == 0 && m_SelectedPart != null)
            {
                m_SelectedPart.IsSelected = false;
                m_SelectedPart = null;
                m_GrabHandle = null;
                UpdateBackground();
                Invalidate();
            }
            return found;
        }

        #endregion

        #region Private Implementation

        internal void RaiseChangedEvent()
        {
            if (SymbolChanged != null)
            {
                SymbolChanged(this);
            }
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
            g.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
            if (GridSize.X > 0 && GridSize.Y > 0)
            {
                for (int i = m_GridSize.X; i < Width; i += m_GridSize.X)
                {
                    for (int j = m_GridSize.X; j < Height; j += m_GridSize.Y)
                    {
                        (m_Background as Bitmap).SetPixel(i, j, m_GridColor);
                    }
                }
            }
            g.DrawLine(new Pen(m_GridColor), 0, m_Offset.Y, Width, m_Offset.Y);
            g.DrawLine(new Pen(m_GridColor), m_Offset.X, 0, m_Offset.X, Height);
            if (m_SelectedPart != null)
                m_SelectedPart.DrawSelection(g, m_Offset);
        }

        /// <summary>
        /// Updates the symbol drawing
        /// </summary>
        protected virtual void UpdateDrawing()
        {
            if (Width == 0 || Height == 0)
            {
                return;
            }
            m_Drawing = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(m_Drawing);
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, Width, Height);
            if (m_Symbol.PartCount > 0)
            {
                foreach (SymbolPart part in m_Symbol)
                {
                    part.DrawPart(g, m_Offset);
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (m_CurrentTool == null)
            {
                return;
            }
            m_CurrentTool.MouseClick(Translate(e.Location, true));
            if (m_CurrentTool is PartDeletionTool)
            {
                if (SelectPartAt(Translate(e.Location, false)) == 1)
                {
                    m_Symbol.RemovePart(m_SelectedPart);
                    m_SelectedPart = null;
                    RaiseChangedEvent();
                    UpdateDrawing();
                    UpdateBackground();
                    Invalidate();
                }
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || m_CurrentTool == null)
            {
                return;
            }
            if (m_CurrentTool is PartSelectionTool)
            {
                SelectPartAt(Translate(e.Location, false));
                if (m_SelectedPart != null)
                {
                    if (m_GrabHandle == null)
                    {
                        m_SelectionOffset = new PointF(e.X - m_SelectedPart.Location.X, e.Y - m_SelectedPart.Location.Y);
                    }
                    else
                    {
                        m_SelectionOffset = new PointF(e.X - m_GrabHandle.Location.X, e.Y - m_GrabHandle.Location.Y);
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (m_CurrentTool != null && m_CurrentTool is PartSelectionTool && m_SelectedPart != null)
            {
                if (m_GrabHandle == null)
                {
                    m_SelectedPart.Location = AlignToGrid(new PointF(e.X - m_SelectionOffset.X, e.Y - m_SelectionOffset.Y));
                }
                else
                {
                    m_GrabHandle.Location = AlignToGrid(new PointF(e.X - m_SelectionOffset.X, e.Y - m_SelectionOffset.Y));
                }
                m_GrabHandle = null;
                UpdateBackground();
                UpdateDrawing();
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_CurrentTool != null && m_CurrentTool is PartSelectionTool && m_SelectedPart != null)
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                PointF newloc = AlignToGrid(new PointF(e.X - m_SelectionOffset.X, e.Y - m_SelectionOffset.Y));
                if (newloc.Equals(m_SelectedPart.Location) == false)
                {
                    if (m_GrabHandle == null)
                    {
                        m_SelectedPart.Location = AlignToGrid(new PointF(e.X - m_SelectionOffset.X, e.Y - m_SelectionOffset.Y));
                    }
                    else
                    {
                        m_GrabHandle.Location = AlignToGrid(new PointF(e.X - m_SelectionOffset.X, e.Y - m_SelectionOffset.Y));
                    }
                    RaiseChangedEvent();
                    UpdateBackground();
                    UpdateDrawing();
                    Invalidate();
                }
            }
        }

        private void NewPartElementHandler(object sender, NewSymbolPartEventArgs e)
        {
            if (e.SymbolPart != null)
            {
                if (e.SymbolPart is PortPart)
                    (e.SymbolPart as PortPart).Name = UniqueName.GetUniquePortName(m_Symbol);
                e.SymbolPart.OnUpdateDrawing += new UpdateDrawingEvent(SymbolPart_OnUpdateDrawing);
                m_Symbol.AddPart(e.SymbolPart);
                RaiseChangedEvent();
                UpdateDrawing();
                Invalidate();
            }
        }

        private void SymbolPart_OnUpdateDrawing(object sender, bool completeDrawing)
        {
            UpdateBackground();
            UpdateDrawing();
            Invalidate();            
        }

        /// <summary>
        /// Computes and returns Translation Matrix. This will be called
        /// whenever there is change in bounds.
        /// </summary>
        /// <returns>
        /// computed matrix
        /// </returns>
        private Matrix ComputeTranslationMatrix()
        {
            Matrix translationMatrix = new Matrix();
            translationMatrix.Translate(m_Offset.X, m_Offset.Y, MatrixOrder.Append);
            return translationMatrix;
        }

        #endregion
    }
}

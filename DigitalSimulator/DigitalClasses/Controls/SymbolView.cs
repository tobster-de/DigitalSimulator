using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DigitalClasses.Graphic;

namespace DigitalClasses.Controls
{
    public partial class SymbolView : UserControl
    {
        #region Fields

        //private Point m_Offset;

        //private Image m_Background;
        private Image m_Drawing;
        private GraphicsPath m_Path;
        private GraphicBaseElement m_Element;

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
        /// The GraphicsPath of a symbol that is drawn
        /// </summary>
        [Browsable(false)]
        public GraphicsPath Path
        {
            get
            {
                return m_Path;
            }
            set
            {
                m_Path.Reset();
                if (value != null && value.PointCount > 0)
                    m_Path.AddPath(value, false);
                //UpdateBackground();
                UpdateDrawing();
                Invalidate();
            }
        }

        /// <summary>
        /// The GraphicsShape of a symbol that is drawn
        /// </summary>
        [Browsable(false)]
        public GraphicShape Shape
        {
            get
            {
                return new GraphicShape(m_Path);
            }
            set
            {
                Path = value.Path;
                //m_Path.Reset();
                //if (value != null && value.Path.PointCount > 0)
                //{
                //    m_Path.AddPath(value.Path, false);
                //}
                //UpdateDrawing();
                //Invalidate();
            }
        }

        ///// <summary>
        ///// The GraphicsShape of a symbol that is drawn
        ///// </summary>
        //[Browsable(false)]
        //public GraphicBaseElement Element
        //{
        //    get
        //    {
        //        return m_Element;
        //    }
        //    set
        //    {
        //        m_Element = value;
        //        Shape = value.m_Body;
        //        //m_Path.Reset();
        //        //if (value != null && value.Path.PointCount > 0)
        //        //{
        //        //    m_Path.AddPath(value.Path, false);
        //        //}
        //        //UpdateDrawing();
        //        //Invalidate();
        //    }
        //}

        #endregion

        public SymbolView()
        {
            InitializeComponent();
            m_Path = new GraphicsPath();
        }

        #region Private Implementation

        ///// <summary>
        ///// Updates the background drawing
        ///// </summary>
        //protected virtual void UpdateBackground()
        //{
        //    if (Width == 0 || Height == 0)
        //    {
        //        return;
        //    }
        //    m_Background = new Bitmap(Width, Height);
        //    Graphics g = Graphics.FromImage(m_Background);
        //    g.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
        //    if (GridSize.X > 0 && GridSize.Y > 0)
        //    {
        //        for (int i = m_GridSize.X; i < Width; i += m_GridSize.X)
        //        {
        //            for (int j = m_GridSize.X; j < Height; j += m_GridSize.Y)
        //            {
        //                (m_Background as Bitmap).SetPixel(i, j, m_GridColor);
        //            }
        //        }
        //    }
        //    g.DrawLine(new Pen(m_GridColor), 0, m_Offset.Y, Width, m_Offset.Y);
        //    g.DrawLine(new Pen(m_GridColor), m_Offset.X, 0, m_Offset.X, Height);
        //}

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
            if (m_Element != null)
            {
                m_Element.Paint(g);
            }
            else
            {
                if (m_Path != null)
                {
                    GraphicsPath path = (GraphicsPath)m_Path.Clone();
                    Matrix translation = ComputeTranslationMatrix();
                    path.Transform(translation);
                    g.DrawPath(Pens.Black, path);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            //UpdateBackground();
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

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    if (m_Background != null)
        //    {
        //        e.Graphics.DrawImage(m_Background, 0, 0);
        //    }
        //}

        /// <summary>
        /// Computes and returns Translation Matrix. This will be called
        /// whenever there is change in bounds.
        /// </summary>
        /// <returns>
        /// computed matrix
        /// </returns>
        private Matrix ComputeTranslationMatrix()
        {
            RectangleF rect = m_Path.GetBounds();

            float offsetX = -rect.Left+5; //m_Offset.X;
            float offsetY = -rect.Top+5; //m_Offset.Y;

            Matrix translationMatrix = new Matrix();
            translationMatrix.Translate(offsetX, offsetY, MatrixOrder.Append);
            return translationMatrix;
        }

        #endregion
    }
}

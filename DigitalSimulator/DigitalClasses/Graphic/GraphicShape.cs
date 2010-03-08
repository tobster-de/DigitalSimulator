using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace DigitalClasses.Graphic
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphicShape : ICloneable
    {
        #region Fields

        ///// <summary>
        ///// Object to which this shape belongs to, used for drawing
        ///// </summary>
        //private GraphicBaseElement m_Parent;
        /// <summary>
        /// Orientation angle which can be 0, 90, 180 and 270.
        /// </summary>
        private float m_Angle;
        /// <summary>
        /// Represents an instance of GraphicsPath which represents the visual appearance
        /// of body of a Graphical element.
        /// </summary>
        private GraphicsPath m_Path;
        /// <summary>
        /// Rectancle around this shape, used for location setting
        /// </summary>
        private RectangleF m_Bounds;
        /// <summary>
        /// Instance of Pen which will be used for painting graphical representation.
        /// </summary>
        private Pen m_Pen;
        /// <summary>
        /// Instance of Brush which will be used for painting graphical representation.
        /// </summary>
        private Brush m_Brush;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the location of the shape
        /// </summary>
        public PointF Location
        {
            get
            {
                return new PointF(m_Bounds.X, m_Bounds.Y);
            }
            set
            {
                m_Bounds.Location = value;
                if (m_Pen != null && m_Path != null && m_Path.PointCount > 0)
                {
                    //m_Bounds.Location = value;
                    Matrix translate = ComputeTranslationMatrix();
                    m_Path.Transform(translate);
                    m_Bounds = m_Path.GetBounds();
                }
            }
        }

        /// <summary>
        /// Returns the size of the shape
        /// </summary>
        public SizeF Size
        {
            get
            {
                m_Bounds = m_Path.GetBounds();
                return m_Bounds.Size;
            }
        }

        /// <summary>
        /// Returns the drawn path of this shape
        /// </summary>
        public GraphicsPath Path
        {
            get
            {
                return (GraphicsPath)m_Path.Clone();
            }
        }

        /// <summary>
        /// Gets or sets the rotation angle of this shape
        /// </summary>
        public float Angle
        {
            get
            {
                return m_Angle;
            }
            set
            {
                //calculate new rotation relative to old one
                Matrix rotate = ComputeRotationMatrix(value);
                m_Path.Transform(rotate);
                m_Angle = value;
                m_Bounds = m_Path.GetBounds();
            }
        }

        /// <summary>
        /// Gets center position of this shape. 
        /// </summary>
        public PointF Center
        {
            get
            {
                return new PointF(m_Bounds.Width / 2 + m_Bounds.Left, m_Bounds.Height / 2 + m_Bounds.Top);
            }
        }

        /// <summary>
        /// The Color of the shape
        /// </summary>
        public Color Color
        {
            get
            {
                return m_Pen.Color;
            }
            set
            {
                m_Pen.Color = value;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return ((SolidBrush)m_Brush).Color;
            }
            set
            {
                ((SolidBrush)m_Brush).Color = value;
            }
        }

        #endregion

        #region Construction/Initialization

        /// <summary>
        /// Public constructor which constructs the GraphicShape using the information
        /// provided with PathData parameter.
        /// </summary>
        /// <param name="pathData">
        /// Contains data about shape body.
        /// </param>
        public GraphicShape(PathData pathData)
        {
            Initialize();
            m_Path = new GraphicsPath(pathData.Points, pathData.Types);
            m_Bounds = m_Path.GetBounds();
        }

        /// <summary>
        /// Private constructor, used for cloning
        /// </summary>
        /// <param name="shape">
        /// Reference to another shape whose deep copy has to be created.
        /// </param>
        private GraphicShape(GraphicShape shape)
        {
            Initialize();
            m_Angle = shape.m_Angle;
            m_Pen = (Pen)shape.m_Pen.Clone();
            m_Brush = (Brush)shape.m_Brush.Clone(); 
            m_Path = (GraphicsPath)shape.m_Path.Clone();
            m_Bounds = shape.m_Bounds;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GraphicShape()
        {
            Initialize();
            // initializing Graphics path
            m_Path = new GraphicsPath();
        }

        /// <summary>
        /// Constructs Shape from GraphicsPath
        /// </summary>
        /// <param name="bodyPath">
        /// Reference to GraphicsPath
        /// </param>
        public GraphicShape(GraphicsPath bodyPath)
        {
            Initialize();
            m_Path = (GraphicsPath)bodyPath.Clone();
            m_Bounds = m_Path.GetBounds();
        }

        /// <summary>
        /// Initializes attributes and styles.
        /// </summary>
        private void Initialize()
        {
            m_Angle = GraphicConstants.DefaultAngle;
            m_Pen = new Pen(GraphicConstants.DefaultColor, GraphicConstants.DefaultPenWidth);
            m_Brush = new SolidBrush(GraphicConstants.BackgroundColor);
        }

        #endregion

        #region ICloneable Member

        /// <summary>
        /// Provides deep copy implementation.
        /// </summary>
        /// <returns>
        /// Instance of the cloned object.
        /// </returns>
        public object Clone()
        {
            return new GraphicShape(this);
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Renders the graphics path. 
        /// </summary>
        /// <param name="graphics">
        /// The Graphics to draw to.
        /// </param>
        public void Paint(Graphics graphics)
        {
            if (m_Pen != null && m_Path != null && m_Path.PointCount > 0)
            {
                graphics.FillPath(m_Brush, m_Path);
                graphics.DrawPath(m_Pen, m_Path);
            }
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Computes and returns Translation Matrix. This will be called
        /// whenever there is change in bounds.
        /// </summary>
        /// <returns>computed Matrix</returns>
        private Matrix ComputeTranslationMatrix()
        {
            RectangleF rect = m_Path.GetBounds();

            float offsetX = m_Bounds.X - rect.X;
            float offsetY = m_Bounds.Y - rect.Y;

            Matrix translationMatrix = new Matrix();
            translationMatrix.Translate(offsetX, offsetY, MatrixOrder.Append);
            return translationMatrix;
        }

        /// <summary>
        /// Computes and return Rotation Matrix. This is called
        /// on change in Angle.
        /// </summary>
        /// <param name="angle">Angle</param>
        /// <returns>computed Matrix</returns>
        private Matrix ComputeRotationMatrix(float angle)
        {
            float diffAngle = (float)Math.Truncate(angle / 90) * 90 - m_Angle;

            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(diffAngle, Center);
            return rotationMatrix;
        }

        #endregion
    }
}

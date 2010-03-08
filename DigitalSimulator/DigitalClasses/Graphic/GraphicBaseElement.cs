using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    /// <summary>
    /// Abstract base class for all graphic elements
    /// </summary>
    public abstract class GraphicBaseElement
    {
        #region Events

        public event LocationChangeEvent OnLocationChanged;
        public virtual event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        #region Fields

        protected string m_Name;
        protected GraphicShape m_Body;
        protected object m_LinkedObject;
        protected GraphicBaseElement m_Parent;
        protected List<GraphicBaseElement> m_Children;
        protected bool m_Highlighted;
        protected bool m_Selected;
        protected Color m_BackgroundColor;

        #endregion

        #region Properties

        /// <summary>
        /// Returns name of the diagram object
        /// </summary>
        public virtual string Name
        {
            get
            {
                if (m_LinkedObject != null && m_LinkedObject is BaseElement)
                {
                    return (m_LinkedObject as BaseElement).Name;
                }
                return m_Name;
            }
            set
            {
                if (m_LinkedObject != null && m_LinkedObject is BaseElement)
                {
                    (m_LinkedObject as BaseElement).Name = value;
                }
                m_Name = value;
            }
        }

        /// <summary>
        /// Gets or sets this elements position.
        /// </summary>
        virtual public PointF Location
        {
            get
            {
                return m_Body.Location;
            }
            set
            {
                PointF oldLocation = m_Body.Location;
                if (oldLocation != value)
                {
                    m_Body.Location = value;
                    if (OnLocationChanged != null)
                    {
                        OnLocationChanged(this, new LocationChangedEventArgs(oldLocation, value));
                    }
                }
            }
        }

        /// <summary>
        /// Gets this elements dimensions.
        /// </summary>
        virtual public SizeF Size
        {
            get
            {
                return m_Body.Size;
            }
        }

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        virtual public RectangleF Bounds
        {
            get
            {
                return new RectangleF(Location, Size);
            }
        }

        /// <summary>
        /// Gets this object's center position. 
        /// </summary>
        public PointF Center
        {
            get
            {
                //return new PointF(m_Center.X, m_Center.Y);
                return m_Body.Center;
            }
        }

        /// <summary>
        /// A linked object (logical object)
        /// </summary>
        public object LinkedObject
        {
            get
            {
                return m_LinkedObject;
            }
            set
            {
                m_LinkedObject = value;
            }
        }

        /// <summary>
        /// The Angle this Element is rotated by
        /// </summary>
        public float Angle
        {
            get
            {
                return m_Body.Angle;
            }
            set
            {
                m_Body.Angle = value;
            }
        }

        /// <summary>
        /// Refers to the Parent object
        /// </summary>
        virtual public GraphicBaseElement Parent
        {
            get
            {
                return m_Parent;
            }
            set
            {
                if (m_Parent != null && m_Parent != value)
                {
                    m_Parent.RemoveChild(this);
                }
                m_Parent = value;
                if (m_Parent != null)
                {
                    m_Parent.AddChild(this);
                }
            }
        }

        /// <summary>
        /// Returns the list of the contained child objects. Readonly
        /// </summary>
        public List<GraphicBaseElement> Children
        {
            get
            {
                if (m_Children != null)
                {
                    List<GraphicBaseElement> result = new List<GraphicBaseElement>(m_Children);
                    return result;
                }
                return null;
            }
        }

        /// <summary>
        /// Stated whether this element is highlighted in the editor
        /// </summary>
        public bool Highlighted
        {
            get
            {
                return m_Highlighted;
            }
            set
            {
                if (m_Highlighted != value)
                {
                    m_Highlighted = value;
                    RaiseUpdateDrawingEvent(false);
                }
            }
        }

        /// <summary>
        /// States whether this element is selected in the editor
        /// </summary>
        public bool Selected
        {
            get
            {
                return m_Selected;
            }
            set
            {
                m_Selected = value;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructs object
        /// </summary>
        /// <param name="linkedObject">
        /// Associated object
        /// </param>
        internal GraphicBaseElement(BaseElement linkedObject)
        {
            if (linkedObject == null)
            {
                throw new ArgumentNullException("linkedObject");
            }
            Initialize();
            m_LinkedObject = linkedObject;
            //reverse link
            linkedObject.LinkedObject = this;
        }

        protected GraphicBaseElement()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_BackgroundColor = GraphicConstants.BackgroundColor;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Adds a child to this Element
        /// </summary>
        /// <param name="child">the child</param>
        internal virtual void AddChild(GraphicBaseElement child)
        {
            if (m_Children == null)
            {
                m_Children = new List<GraphicBaseElement>(1);
            }
            if (m_Children.Contains(child) == false && child.Parent == null)
            {
                m_Children.Add(child);
                child.Parent = this;
            }
        }

        /// <summary>
        /// Removes a child from this element
        /// </summary>
        /// <param name="child">the child</param>
        internal virtual void RemoveChild(GraphicBaseElement child)
        {
            if (m_Children.Contains(child))
            {
                m_Children.Remove(child);
                child.Parent = null;
            }
        }

        /// <summary>
        /// Trigger for the UpdateDrawing event
        /// </summary>
        protected void RaiseUpdateDrawingEvent(bool completeDrawing)
        {
            if (OnUpdateDrawing != null)
            {
                OnUpdateDrawing(this, completeDrawing);
            }
        }

        #endregion

        #region Prototypes

        /// <summary>
        /// Paints this Element
        /// </summary>
        /// <param name="graphics">the graphics to paint on</param>        
        public virtual void Paint(Graphics graphics)
        {
            if (m_Body != null)
            {
                m_Body.BackgroundColor = m_BackgroundColor;
                m_Body.Paint(graphics);
            }
            if (m_Children != null && m_Children.Count > 0)
            {
                foreach (GraphicBaseElement gbe in m_Children)
                {
                    gbe.Paint(graphics);
                }
            }
        }

        /// <summary>
        /// Paints the highlight background of this element
        /// </summary>
        /// <param name="graphics">the graphics to paint on</param>
        public virtual void PaintBackground(Graphics graphics)
        {
            if (m_Highlighted)
            {
                RectangleF widened = new RectangleF(Bounds.X - 4, Bounds.Y - 4, Bounds.Width + 8, Bounds.Height + 8);
                graphics.FillRectangle(GraphicConstants.HighlightBrush, widened);
                if (m_Children != null && m_Children.Count > 0)
                {
                    foreach (GraphicBaseElement gbe in m_Children)
                    {
                        gbe.PaintBackground(graphics);
                    }
                }
            }
        }

        /// <summary>
        /// Update this elements logic
        /// </summary>
        public virtual void Update()
        {
            //dummy solution
        }

        #endregion
    }
}

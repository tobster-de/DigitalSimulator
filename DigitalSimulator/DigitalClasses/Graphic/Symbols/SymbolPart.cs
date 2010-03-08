using System;
using System.Collections.Generic;
using System.Drawing;
using ToolBox;
using System.ComponentModel;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic.Symbols
{
    public abstract class SymbolPart
    {
        #region Events

        public virtual event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        protected bool m_IsSelected;

        public abstract PointF Location
        {
            get;
            set;
        }

        [Browsable(false)]
        public bool IsSelected
        {
            get
            {
                return m_IsSelected;
            }
            set
            {
                m_IsSelected = value;
            }
        }

        public abstract bool IsNear(PointF location);

        public abstract void DrawPart(Graphics g, PointF offset);

        public abstract GrabHandle GetGrabHandle(PointF location);

        public abstract void DrawSelection(Graphics g, PointF offset);

        protected PointF DoOffset(PointF location, PointF offset)
        {
            return new PointF(location.X + offset.X, location.Y + offset.Y);
        }

        protected double DistanceLinePoint(PointF LinePointA, PointF LinePointB, PointF point)
        {
            Vector ap = new Vector(LinePointA.X, LinePointA.Y);
            Vector ep = new Vector(LinePointB.X, LinePointB.Y);
            Vector rv = ep - ap;
            Vector p = new Vector(point.X, point.Y);

            return Vector.DistanceLinePoint(ap, rv, p);
        }
    }
}

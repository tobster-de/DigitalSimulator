using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;

namespace DigitalClasses.Graphic.Symbols
{
    internal class LinePart : SymbolPart
    {
        #region Fields

        private PointF m_Location;
        private PointF[] m_Points;
        private List<GrabHandle> m_GrabHandles;

        #endregion

        #region Properties

        public override PointF Location
        {
            get
            {
                CalcLocation();
                return m_Location;
            }
            set
            {
                float dx = value.X - m_Location.X;
                float dy = value.Y - m_Location.Y;
                for (int i = 0; i < m_Points.Length; i++)
                {
                    m_Points[i].X += dx;
                    m_Points[i].Y += dy;
                }
                MoveHandles();
            }
        }

        #endregion

        #region Construction

        public LinePart(PointF begin, PointF end)
        {
            m_Points = new PointF[2];
            m_Points[0] = begin;
            m_Points[1] = end;
            CreateHandles();
        }

        public LinePart(float x1, float y1, float x2, float y2)
        {
            m_Points = new PointF[2];
            m_Points[0] = new PointF(x1, y1);
            m_Points[1] = new PointF(x2, y2);
            CreateHandles();
        }

        #endregion

        #region Public Implementation

        public PointF GetPoint(int index)
        {
            if (index < 0 || index >= m_Points.Length)
            {
                return PointF.Empty;
            }
            return new PointF(m_Points[index].X, m_Points[index].Y);
        }

        public override bool IsNear(PointF location)
        {
            return DistanceLinePoint(m_Points[0], m_Points[1], location) < 4;
        }

        public override GrabHandle GetGrabHandle(PointF location)
        {
            foreach (GrabHandle handle in m_GrabHandles)
            {
                if (handle.IsNear(location))
                {
                    return handle;
                }
            }
            return null;
        }

        public override void DrawPart(Graphics g, PointF offset)
        {
            g.DrawLine(new Pen(Color.Black), DoOffset(m_Points[0], offset), DoOffset(m_Points[1], offset));
        }

        public override void DrawSelection(Graphics g, PointF offset)
        {
            g.DrawLine(new Pen(GraphicConstants.HighlightColor, 5), DoOffset(m_Points[0], offset), DoOffset(m_Points[1], offset));
            foreach (GrabHandle handle in m_GrabHandles)
            {
                handle.DrawHandle(g, offset);
            }
        }
        #endregion

        #region Private Implementation

        private void CalcLocation()
        {
            m_Location = m_Points[0];
            foreach (PointF p in m_Points)
            {
                if (p.X < m_Location.X)
                    m_Location.X = p.X;
                if (p.Y < m_Location.Y)
                    m_Location.Y = p.Y;
            }
        }

        private void CreateHandles()
        {
            m_GrabHandles = new List<GrabHandle>(2);
            for (int i = 0; i < 2; i++)
            {
                m_GrabHandles.Add(new GrabHandle(m_Points[i]));
                m_GrabHandles[i].Index = i;
                m_GrabHandles[i].OnHandleMoved += new GrabHandle.HandleMoved(OnHandleMoved);
            }
        }

        private void MoveHandles()
        {
            for (int i = 0; i < 2; i++)
            {
                m_GrabHandles[i].OnHandleMoved -= OnHandleMoved;
                m_GrabHandles[i].Location = m_Points[i];
                m_GrabHandles[i].OnHandleMoved += new GrabHandle.HandleMoved(OnHandleMoved);
            }
        }

        #endregion

        void OnHandleMoved(GrabHandle sender, PointF oldLocation, PointF newLocation)
        {
            m_Points[sender.Index] = newLocation;
        }

    }
}

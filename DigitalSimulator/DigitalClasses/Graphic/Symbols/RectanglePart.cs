using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;

namespace DigitalClasses.Graphic.Symbols
{
    public class RectanglePart : SymbolPart
    {
        private PointF[] m_Points;
        private List<GrabHandle> m_GrabHandles;
        //private RectangleF m_Rect;

        public override PointF Location
        {
            get
            {
                return m_Points[0];
            }
            set
            {
                float dx = value.X - m_Points[0].X;
                float dy = value.Y - m_Points[0].Y;
                for (int i = 0; i < m_Points.Length; i++)
                {
                    m_Points[i].X += dx;
                    m_Points[i].Y += dy;
                }
                MoveHandles();
            }
        }

        public float Width
        {
            get
            {
                return m_Points[3].X - m_Points[0].X;
            }
        }

        public float Height
        {
            get
            {
                return m_Points[3].Y - m_Points[0].Y;
            }
        }

        public RectanglePart(PointF location, SizeF size)
        {
            //m_Rect = new RectangleF(ocation, size);
            CreatePoints(location.X, location.Y, location.X + size.Width, location.Y + size.Height);
            CreateHandles();
        }

        public RectanglePart(float x1, float y1, float x2, float y2)
        {
            //m_Rect = new RectangleF(x1 < x2 ? x1 : x2, y1 < y2 ? y1 : y2, Math.Abs(x2 - x1), Math.Abs(y2 - y1));
            CreatePoints(x1 < x2 ? x1 : x2, y1 < y2 ? y1 : y2, x1 > x2 ? x1 : x2, y1 > y2 ? y1 : y2);
            CreateHandles();
        }

        private void CreatePoints(float x1, float y1, float x2, float y2)
        {
            m_Points = new PointF[4];
            m_Points[0] = new PointF(x1, y1);
            m_Points[1] = new PointF(x2, y1);
            m_Points[2] = new PointF(x1, y2);
            m_Points[3] = new PointF(x2, y2);
        }

        private void CreateHandles()
        {
            m_GrabHandles = new List<GrabHandle>(4);
            for (int i = 0; i < 4; i++)
            {
                m_GrabHandles.Add(new GrabHandle(m_Points[i]));
                m_GrabHandles[i].Index = i;
                m_GrabHandles[i].OnHandleMoved += new GrabHandle.HandleMoved(OnHandleMoved);
            }
        }

        private void MoveHandles()
        {
            for (int i = 0; i < 4; i++)
            {
                m_GrabHandles[i].OnHandleMoved -= OnHandleMoved;
                m_GrabHandles[i].Location = m_Points[i];
                m_GrabHandles[i].OnHandleMoved += new GrabHandle.HandleMoved(OnHandleMoved);
            }
        }

        void OnHandleMoved(GrabHandle sender, PointF oldLocation, PointF newLocation)
        {
            m_Points[sender.Index] = newLocation;
            switch (sender.Index)
            {
                case 0: //TL
                    m_Points[2].X = m_Points[0].X;
                    m_Points[1].Y = m_Points[0].Y;
                    break;
                case 1: //TR
                    m_Points[3].X = m_Points[1].X;
                    m_Points[0].Y = m_Points[1].Y;
                    break;
                case 2: //BL
                    m_Points[0].X = m_Points[2].X;
                    m_Points[3].Y = m_Points[2].Y;
                    break;
                case 3: //BR
                    m_Points[1].X = m_Points[3].X;
                    m_Points[2].Y = m_Points[3].Y;
                    break;
            }
            MoveHandles();
        }

        public override bool IsNear(PointF location)
        {
            if (DistanceLinePoint(m_Points[0], m_Points[1], location) < 4)
                return true;
            if (DistanceLinePoint(m_Points[0], m_Points[2], location) < 4)
                return true;
            if (DistanceLinePoint(m_Points[1], m_Points[3], location) < 4)
                return true;
            if (DistanceLinePoint(m_Points[2], m_Points[3], location) < 4)
                return true;
            return false;
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
            g.DrawRectangle(new Pen(Color.Black), 
                m_Points[0].X + offset.X, m_Points[0].Y + offset.Y,
                m_Points[3].X - m_Points[0].X, m_Points[3].Y - m_Points[0].Y);
        }

        public override void DrawSelection(Graphics g, PointF offset)
        {
            g.DrawRectangle(new Pen(GraphicConstants.HighlightColor, 5),
                m_Points[0].X + offset.X, m_Points[0].Y + offset.Y,
                m_Points[3].X - m_Points[0].X, m_Points[3].Y - m_Points[0].Y);
            foreach (GrabHandle handle in m_GrabHandles)
            {
                handle.DrawHandle(g, offset);
            }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;
using System.Drawing;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    class GraphicClock : GraphicInputOutputElement
    {
        #region Events

        public override event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        #region Construction

        public GraphicClock(BaseElement linkedObject)
            : base(linkedObject)
        {
            Clock input = (linkedObject as Clock);
            if (input != null)
            {
                input.OnStateChanged += new DigitalClasses.Events.NotifyEvent(Clock_OnStateChanged);
            }
        }

        #endregion

        #region Overrides

        public override void Paint(Graphics graphics)
        {
            if ((m_LinkedObject as Clock).State.Equals(State.Low))
            {
                m_BackgroundColor = GraphicConstants.StateLowColor;
            }
            else
            {
                m_BackgroundColor = GraphicConstants.StateHighColor;
            }
            base.Paint(graphics);
        }

        #endregion

        #region Private Implementation

        protected override void BuildBody()
        {
            GraphicsPath bodypath = new GraphicsPath();

            int halfheight = GraphicConstants.GraphicSignalHeight / 2;
            List<Point> points = new List<Point>(5);
            points.Add(new Point(0, 0));
            points.Add(new Point(GraphicConstants.GraphicSignalWidth - halfheight, 0));
            points.Add(new Point(GraphicConstants.GraphicSignalWidth, halfheight));
            points.Add(new Point(GraphicConstants.GraphicSignalWidth - halfheight, GraphicConstants.GraphicSignalHeight));
            points.Add(new Point(0, GraphicConstants.GraphicSignalHeight));
            bodypath.AddPolygon(points.ToArray());
            points.Clear();
            points.Add(new Point(2, 8));
            points.Add(new Point(6, 8));
            points.Add(new Point(6, 2));
            points.Add(new Point(10, 2));
            points.Add(new Point(10, 8));
            points.Add(new Point(14, 8));
            points.Add(new Point(14, 2));
            points.Add(new Point(18, 2));
            points.Add(new Point(18, 8));
            points.Add(new Point(22, 8));
            bodypath.AddLines(points.ToArray());
            bodypath.AddLines(points.ToArray().Reverse().ToArray());

            //add terminals
            AddTerminals();

            m_Body = new GraphicShape(bodypath);
        }

        protected override void AddTerminals()
        {
            InputOutputElement ioElement = LinkedObject as InputOutputElement;
            //add the Output
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Terminal), ioElement.Outputs[0]);
            element.Location = new PointF(GraphicConstants.GraphicSignalWidth, GraphicConstants.GraphicSignalHeight / 2);
            AddTerminal(element as GraphicTerminal);
        }

        private void Clock_OnStateChanged(object sender)
        {
            if (OnUpdateDrawing != null)
            {
                OnUpdateDrawing(this, false);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    class GraphicOutput : GraphicInputOutputElement
    {
        #region Events

        public override event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        #region Construction

        public GraphicOutput(BaseElement linkedObject)
            : base(linkedObject)
        {
            SignalOutput output = linkedObject as SignalOutput;
            if (output != null)
            {
                output.OnStateChanged += new NotifyEvent(StateChangedEventHandler);
                output.OnSignalNameChanged += new NotifyEvent(SignalNameChangedEventHandler);
            }
        }

        #endregion

        #region Overrides

        public override void Paint(Graphics graphics)
        {
            if ((m_LinkedObject as SignalOutput).State.Equals(State.Low))
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
            //add terminals
            AddTerminals();

            //Font arial = new Font("Arial", 12, System.Drawing.GraphicsUnit.Point);
            //FontFamily family = new FontFamily("Arial");
            //bodypath.AddString(Text, family, 0, 12, new Point(4, 18), StringFormat.GenericDefault);

            m_Body = new GraphicShape(bodypath);
        }

        protected override void AddTerminals()
        {
            InputOutputElement ioElement = LinkedObject as InputOutputElement;
            //add the Input
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Terminal), ioElement.Inputs[0]);
            element.Angle = 180;
            element.Location = new PointF(-GraphicConstants.GraphicTerminalWidth, GraphicConstants.GraphicSignalHeight / 2);
            AddTerminal(element as GraphicTerminal);
        }

        private void StateChangedEventHandler(object sender)
        {
            if (OnUpdateDrawing != null)
            {
                OnUpdateDrawing(this, false);
            }
        }

        private void SignalNameChangedEventHandler(object sender)
        {
            SignalOutput output = LinkedObject as SignalOutput;
            foreach (GraphicBaseElement gbe in Children)
            {
                if (gbe is TextElement)
                {
                    TextElement text = (gbe as TextElement);
                    if (text.Text == null || text.Text.Equals(output.SignalName) == false)
                    {
                        text.Text = output.SignalName;
                        if (OnUpdateDrawing != null)
                        {
                            OnUpdateDrawing(this, false);
                        }
                    }
                    break;
                }
            }
        }

        #endregion
    }
}

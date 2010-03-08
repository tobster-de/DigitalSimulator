using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;
using System.Drawing;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    class GraphicInput : GraphicInputOutputElement
    {
        #region Events

        public override event UpdateDrawingEvent OnUpdateDrawing;

        #endregion

        #region Properties

        public string SignalName
        {
            get
            {
                object linked = LinkedObject;
                if (linked is SignalInput)
                {
                    return (linked as SignalInput).Signal.Name;
                }
                else if (linked is ConstantInput)
                {
                    return (linked as ConstantInput).State.ToString().Substring(0,2);
                }
                return String.Empty;
            }
            set
            {
                SignalInput signalInput = LinkedObject as SignalInput;
                if (signalInput != null && value != signalInput.Signal.Name 
                    /*&& DigitalClasses.Controls.UniqueName.SignalNameIsUnique(value) */ )
                {
                    signalInput.Signal.Name = value;
                }
            }
        }

        #endregion

        #region Construction

        public GraphicInput(BaseElement linkedObject)
            : base(linkedObject)
        {
            ConstantInput input = (linkedObject as ConstantInput);
            if (input != null)
            {
                input.OnStateChanged += new DigitalClasses.Events.NotifyEvent(input_OnStateChanged);
                if (linkedObject is SignalInput)
                {
                    (input as SignalInput).Signal.OnSignalChanged += new SignalChanged(Signal_OnSignalChanged);
                }
            }
        }

        #endregion

        #region Overrides

        public override void Paint(Graphics graphics)
        {
            if ((m_LinkedObject as ConstantInput).State.Equals(State.Low))
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

        private void input_OnStateChanged(object sender)
        {
            if (LinkedObject is SignalInput == false)
            {
                ChangeSignalText((LinkedObject as ConstantInput).State.ToString().Substring(0, 2));
            }
            if (OnUpdateDrawing != null)
            {
                OnUpdateDrawing(this, false);
            }
        }

        private void Signal_OnSignalChanged(object sender, SignalChangeType change)
        {
            // handle change in the name of the signal
            if (change != SignalChangeType.SignalName)
            {
                return;
            }
            Signal signal = sender as Signal;
            ChangeSignalText(signal.Name);
        }

        private void ChangeSignalText(string signalName)
        {
            foreach (GraphicBaseElement gbe in Children)
            {
                if (gbe is TextElement)
                {
                    TextElement text = (gbe as TextElement);
                    if (text.Text == null || text.Text.Equals(signalName) == false)
                    {
                        text.Text = signalName;
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

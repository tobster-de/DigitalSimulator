using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace DigitalClasses.Graphic
{
    internal class GraphicInputOutputElement : GraphicBaseElement
    {
        #region Fields

        protected List<GraphicTerminal> m_Terminals;

        #endregion

        #region Properties

        /// <summary>
        /// Gets this object's natural position.
        /// </summary>
        public override PointF Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                if (m_Terminals != null && m_Terminals.Count > 0)
                {
                    float offsetX = m_Body.Location.X - value.X;
                    float offsetY = m_Body.Location.Y - value.Y;
                    foreach (GraphicBaseElement child in m_Terminals)
                    {
                        child.Location = new PointF(child.Location.X - offsetX, child.Location.Y - offsetY);
                    }
                }
                base.Location = value;
            }
        }

        /// <summary>
        /// Gets the bounding rectangle incl. the Terminals
        /// </summary>
        public override RectangleF Bounds
        {
            get
            {
                RectangleF result = new RectangleF(Location, Size);
                foreach (GraphicTerminal terminal in m_Terminals)
                {
                    RectangleF bounds = terminal.Bounds;
                    result = RectangleF.Union(result, bounds);
                }
                return result;
            }
        }

        /// <summary>
        /// Returns the list of the contained child objects. Readonly
        /// </summary>
        public List<GraphicTerminal> Terminals
        {
            get
            {
                if (m_Terminals != null)
                {
                    List<GraphicTerminal> result = new List<GraphicTerminal>(m_Terminals);
                    return result;
                }
                return null;
            }
        }

        #endregion

        #region Construction

        public GraphicInputOutputElement(BaseElement linkedObject)
            : base(linkedObject)
        {
            BuildBody();
            (linkedObject as InputOutputElement).OnTerminalCountChanged += new TerminalCountChanged(TerminalCountChangedEventHandler);
        }

        #endregion

        #region Overrides

        public override void Paint(Graphics graphics)
        {
            base.Paint(graphics);
            if (m_Terminals != null && m_Terminals.Count > 0)
            {
                foreach (GraphicBaseElement gbe in m_Terminals)
                {
                    gbe.Paint(graphics);
                }
            }
        }

        public override void PaintBackground(Graphics graphics)
        {
            base.PaintBackground(graphics);
            if (m_Terminals != null && m_Terminals.Count > 0)
            {
                foreach (GraphicBaseElement gbe in m_Terminals)
                {
                    gbe.PaintBackground(graphics);
                }
            }
        }

        public override void Update()
        {
            (LinkedObject as InputOutputElement).Logic();
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Returns the Terminal at the given location or null respectively
        /// </summary>
        /// <param name="location">the location of interest</param>
        /// <returns>Terminal at the given location or null respectively</returns>
        public GraphicTerminal GetTerminalAt(PointF location)
        {
            foreach (GraphicTerminal terminal in m_Terminals)
            {
                RectangleF bounds = terminal.Bounds;
                if (bounds.Contains(location))
                {
                    return terminal;
                }
            }
            return null;
        }

        #endregion

        #region Private Implementation

        protected virtual void BuildBody()
        {
            GraphicsPath bodypath = new GraphicsPath();

            int termCount = 0;
            InputOutputElement ioElement = LinkedObject as InputOutputElement;
            if (ioElement != null)
            {
                termCount = ioElement.InputCount > ioElement.OutputCount ? ioElement.InputCount : ioElement.OutputCount;
                termCount--;
            }
            bodypath.AddRectangle(new Rectangle(0, 0, GraphicConstants.GraphicElementWidth,
                GraphicConstants.GraphicElementUpperSpace + GraphicConstants.GraphicElementLowerSpace + termCount * GraphicConstants.GraphicElementTerminalSpace));
            //add terminals
            AddTerminals();
            m_Body = new GraphicShape(bodypath);
        }

        /// <summary>
        /// Counts current Terminals of a specific direction
        /// </summary>
        /// <param name="direction">The direction of interest</param>
        /// <returns>The current count of Terminals of a specific direction</returns>
        private int CountTerminals(DirectionType direction)
        {
            if (m_Terminals == null)
            {
                return 0;
            }
            int count = 0;
            foreach (GraphicBaseElement gbe in m_Terminals)
            {
                Terminal terminal = gbe.LinkedObject as Terminal;
                if (terminal != null && terminal.Direction == direction)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Adds terminals when needed
        /// </summary>
        protected virtual void AddTerminals()
        {
            InputOutputElement ioElement = LinkedObject as InputOutputElement;
            //add Outputs
            int count = CountTerminals(DirectionType.Output);
            while (count < ioElement.OutputCount)
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Terminal), ioElement.Outputs[count]);
                if (ioElement.Outputs[count].Negated)
                {
                    element.Location = new PointF(GraphicConstants.GraphicElementWidth,
                        GraphicConstants.GraphicElementUpperSpace + count * GraphicConstants.GraphicElementTerminalSpace - 3);
                }
                else
                {
                    element.Location = new PointF(GraphicConstants.GraphicElementWidth,
                        GraphicConstants.GraphicElementUpperSpace + count * GraphicConstants.GraphicElementTerminalSpace);
                }
                AddTerminal(element as GraphicTerminal);
                count++;
            }
            //add Inputs
            count = CountTerminals(DirectionType.Input);
            while (count < ioElement.InputCount)
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Terminal), ioElement.Inputs[count]);
                if (ioElement.Inputs[count].Negated)
                {
                    element.Location = new PointF(-GraphicConstants.GraphicTerminalWidth,
                        GraphicConstants.GraphicElementUpperSpace + count * GraphicConstants.GraphicElementTerminalSpace - 3);
                }
                else
                {
                    element.Location = new PointF(-GraphicConstants.GraphicTerminalWidth,
                        GraphicConstants.GraphicElementUpperSpace + count * GraphicConstants.GraphicElementTerminalSpace);
                }
                element.Angle = 180;
                AddTerminal(element as GraphicTerminal);
                count++;
            }
        }

        /// <summary>
        /// Adds a terminal to this Element
        /// </summary>
        /// <param name="terminal">the terminal</param>
        protected void AddTerminal(GraphicTerminal terminal)
        {
            if (m_Terminals == null)
            {
                m_Terminals = new List<GraphicTerminal>(1);
            }
            if (m_Terminals.Contains(terminal) == false)
            {
                m_Terminals.Add(terminal);
            }
        }

        /// <summary>
        /// Removes Terminals when needed
        /// </summary>
        protected void RemoveTerminals()
        {
            InputOutputElement ioElement = LinkedObject as InputOutputElement;
            //remove Outputs
            int count = CountTerminals(DirectionType.Output);
            while (count > ioElement.OutputCount)
            {
                GraphicTerminal terminal = m_Terminals.FindLast(delegate(GraphicTerminal element)
                {
                    return (element.LinkedObject as Terminal).Direction == DirectionType.Output;
                });
                RemoveTerminal(terminal);
                count--;
            }
            //remove Inputs
            count = CountTerminals(DirectionType.Input);
            while (count > ioElement.InputCount)
            {
                GraphicTerminal terminal = m_Terminals.FindLast(delegate(GraphicTerminal element)
                {
                    return (element.LinkedObject as Terminal).Direction == DirectionType.Input;
                });
                RemoveTerminal(terminal);
                count--;
            }
        }

        /// <summary>
        /// Removes a terminal from this element
        /// </summary>
        /// <param name="graphicTerminal">the terminal</param>
        protected void RemoveTerminal(GraphicTerminal graphicTerminal)
        {
            if (m_Terminals.Contains(graphicTerminal))
            {
                //remove crossing references (in case garbage collector is dumb)
                Terminal terminal = (graphicTerminal.LinkedObject as Terminal);
                if (terminal != null)
                {
                    terminal.LinkedObject = null;
                }
                graphicTerminal.LinkedObject = null;
                ////disconnect if connected
                //if (terminal.IsConnected)
                //{
                //    terminal.Connection.DisconnectTerminal(terminal);
                //}
                m_Terminals.Remove(graphicTerminal);
            }
        }

        /// <summary>
        /// Event handler when the count of either input or output terminals has changed
        /// </summary>
        private void TerminalCountChangedEventHandler(object sender, TerminalCountChangedEventArgs e)
        {
            if (e.NewCount < e.OldCount)
            {
                RemoveTerminals();
            }
            PointF location = Location;
            Location = new PointF(0, 0);
            BuildBody();
            Location = location;
            RaiseUpdateDrawingEvent((e.NewCount < e.OldCount));
        }

        #endregion
    }
}

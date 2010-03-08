using System;
using System.Collections.Generic;
using DigitalClasses.Logic;
using System.Drawing;
using DigitalClasses.Serialization;

namespace DigitalClasses.Graphic
{
    public static class GraphicObjectFactory
    {
        /// <summary>
        /// Returns a graphic element corresponding to the given logic Element
        /// </summary>
        /// <param name="logicType">type of the logic element to create graphic element for</param>
        /// <param name="logic">logic object that will be linked to the graphic element</param>
        /// <returns>new graphic element</returns>
        public static GraphicBaseElement CreateInstance(Type logicType, BaseElement logic)
        {
            try
            {
                if (logicType == null)
                {
                    throw new ArgumentNullException("logicType");
                }
                // get graphic corresponding to given element from cache
                GraphicBaseElement element = CreateElement(logicType.Name, logic);
                return element;
            }
            catch (KeyNotFoundException exception)
            {
                throw new ArgumentException("Element Not Found", exception);
            }
        }

        private static GraphicBaseElement CreateElement(string name, BaseElement logic)
        {
            //Terminals
            if (name.Equals(typeof(Terminal).Name))
            {
                GraphicBaseElement element = new GraphicTerminal(logic);
                element.Name = logic.Name;
                return element;
            }
            //Connections
            if (name.Equals(typeof(Connection).Name))
            {
                GraphicBaseElement element = new GraphicConnection(logic);
                element.Name = logic.Name;
                return element;
            }
            //Macros
            if (name.Equals(typeof(Macro).Name))
            {
                Macro macro = logic as Macro;
                GraphicMacro graphicMacro = new GraphicMacro(logic, MacroCache.Instance.GetSymbol(macro.TypeName));

                return graphicMacro;
            }
            //Gates
            if (name.Equals(typeof(AndGate).Name) || name.Equals(typeof(NandGate).Name))
            {
                GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
                TextElement text = new TextElement(@"&", new PointF(4, 4));
                io.AddChild(text);
                return io;
            }
            if (name.Equals(typeof(OrGate).Name) || name.Equals(typeof(NorGate).Name))
            {
                GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
                TextElement text = new TextElement(@"≥1", new PointF(4, 4));
                io.AddChild(text);
                return io;
            }
            if (name.Equals(typeof(BufferGate).Name) || name.Equals(typeof(NotGate).Name))
            {
                GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
                TextElement text = new TextElement(@"1", new PointF(4, 0));
                io.AddChild(text);
                return io;
            }
            if (name.Equals(typeof(XorGate).Name))
            {
                GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
                TextElement text = new TextElement(@"=m", new PointF(4, 4));
                io.AddChild(text);
                return io;
            }
            if (name.Equals(typeof(XnorGate).Name))
            {
                GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
                TextElement text = new TextElement(@"=", new PointF(4, 4));
                io.AddChild(text);
                return io;
            }
            //if (name.Equals(typeof(Delay).Name))
            //{
            //    GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
            //    TextElement text = new TextElement(@"|-|", new PointF(4, 4));
            //    io.AddChild(text);
            //    return io;
            //}
            //Signals
            if (name.Equals(typeof(Clock).Name))
            {
                Clock clock = (Clock)logic;
                GraphicInputOutputElement graphicClock = new GraphicClock(logic);
                return graphicClock;
            }
            if (name.Equals(typeof(ConstantInput).Name) || name.Equals(typeof(SignalInput).Name))
            {
                GraphicInputOutputElement sig = new GraphicInput(logic);
                TextElement text = null;
                if (logic is SignalInput)
                {
                    text = new TextElement((logic as SignalInput).SignalName, new PointF(2, 0));
                }
                else
                {
                    text = new TextElement((logic as ConstantInput).State.ToString().Substring(0, 2), new PointF(2, 0));
                }
                if (text != null)
                {
                    sig.AddChild(text);
                }
                return sig;
            }
            if (name.Equals(typeof(SignalOutput).Name))
            {
                SignalOutput output = (SignalOutput)logic;
                GraphicInputOutputElement sig = new GraphicOutput(logic);
                TextElement text = new TextElement(output.SignalName, new PointF(2, 0));
                sig.AddChild(text);
                return sig;
            }
            return null;
        }

        /// <summary>
        /// Creates a clone of th given element
        /// Currently only clones of GraphicInputOutputElement are supported
        /// </summary>
        /// <param name="element">element to clone</param>
        /// <returns>cloned element</returns>
        public static GraphicBaseElement CreateClone(GraphicBaseElement element)
        {
            if (element.GetType().Equals(typeof(GraphicMacro)))
            {
                BaseElement macro = ((Macro)element.LinkedObject).Clone();
                GraphicMacro graphicMacro = new GraphicMacro(macro, (element as GraphicMacro).m_Symbol);
                if (element.Children != null)
                {
                    foreach (GraphicBaseElement child in element.Children)
                    {
                        GraphicBaseElement childClone = CreateClone(child);
                        graphicMacro.AddChild(childClone);
                    }
                }
                return graphicMacro;
            }
            if (element.GetType().Equals(typeof(GraphicInputOutputElement)))
            {
                BaseElement logic = ((InputOutputElement)element.LinkedObject).Clone();
                GraphicInputOutputElement io = new GraphicInputOutputElement(logic);
                if (element.Children != null)
                {
                    foreach (GraphicBaseElement child in element.Children)
                    {
                        GraphicBaseElement childClone = CreateClone(child);
                        io.AddChild(childClone);
                    }
                }
                return io;
            }

            if (element.GetType().Equals(typeof(GraphicClock)))
            {
                BaseElement logic = ((Clock)element.LinkedObject).Clone();
                GraphicInputOutputElement grClock = new GraphicClock(logic);
                if (element.Children != null)
                {
                    foreach (GraphicBaseElement child in element.Children)
                    {
                        GraphicBaseElement childClone = CreateClone(child);
                        grClock.AddChild(childClone);
                    }
                }
                return grClock;
            }
            if (element.GetType().Equals(typeof(GraphicInput)))
            {
                BaseElement logic = ((ConstantInput)element.LinkedObject).Clone();
                GraphicInputOutputElement sig = new GraphicInput(logic);
                if (element.Children != null)
                {
                    foreach (GraphicBaseElement child in element.Children)
                    {
                        GraphicBaseElement childClone = CreateClone(child);
                        sig.AddChild(childClone);
                    }
                }
                return sig;
            }
            if (element.GetType().Equals(typeof(GraphicOutput)))
            {
                BaseElement logic = ((SignalOutput)element.LinkedObject).Clone();
                GraphicInputOutputElement sig = new GraphicOutput(logic);
                if (element.Children != null)
                {
                    foreach (GraphicBaseElement child in element.Children)
                    {
                        GraphicBaseElement childClone = CreateClone(child);
                        sig.AddChild(childClone);
                    }
                }
                return sig;
            }
            if (element.GetType().Equals(typeof(TextElement)))
            {
                TextElement orig = (element as TextElement);
                TextElement text = new TextElement(orig.Text, orig.Location);
                text.FontName = orig.FontName;
                text.FontSize = orig.FontSize;
                return text;
            }
            return null;
        }


    }
}

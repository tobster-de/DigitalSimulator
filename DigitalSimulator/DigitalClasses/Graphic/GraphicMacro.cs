using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DigitalClasses.Logic;
using DigitalClasses.Events;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Graphic
{
    internal class GraphicMacro : GraphicInputOutputElement
    {
        #region Fields

        internal Symbol m_Symbol;

        #endregion

        #region Construction

        public GraphicMacro(BaseElement linkedObject, Symbol symbol)
            : base(linkedObject)
        {
            m_Symbol = symbol;
            BuildBody();
        }

        #endregion

        #region Private Implementation

        protected override void BuildBody()
        {
            if (m_Symbol == null)
            {
                return;
            }
            //add terminals
            AddTerminals();
            AddTexts();
            m_Body = m_Symbol.GenerateShape();
        }

        /// <summary>
        /// Adds terminals
        /// </summary>
        protected override void AddTerminals()
        {
            Macro macro = LinkedObject as Macro;

            foreach (SymbolPart part in m_Symbol)
            {
                if (part is PortPart)
                {
                    PortPart port = (PortPart)part;
                    Terminal term = macro.GetTerminalByName(port.Name);
                    if (term != null)
                    {
                        GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Terminal), term);
                        element.Name = port.Name;
                        element.Location = port.Location;
                        element.Angle = port.Angle;
                        AddTerminal(element as GraphicTerminal);
                    }
                }
            }
        }

        /// <summary>
        /// Adds texts
        /// </summary>
        protected void AddTexts()
        {
            Macro macro = LinkedObject as Macro;

            foreach (SymbolPart part in m_Symbol)
            {
                TextPart text = part as TextPart;
                if (text != null)
                {
                    TextElement textElem = new TextElement(text.Text, text.Location);
                    AddChild(textElem);
                }
            }
        }

        #endregion
    }
}

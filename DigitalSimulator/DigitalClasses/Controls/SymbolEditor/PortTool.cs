using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DigitalClasses.Events;
using DigitalClasses.Graphic.Symbols;
using DigitalClasses.Logic;

namespace DigitalClasses.Controls
{
    public class PortTool : SymbolTool
    {
        private DirectionType m_Direction;

        internal override event NewSymbolPart OnNewSymbolPart;

        public PortTool(DirectionType direction)
        {
            m_Direction = direction;
        }

        internal override void MouseClick(PointF location)
        {
            SymbolPart part = new PortPart(location, m_Direction, "");
            OnNewSymbolPart(this, new NewSymbolPartEventArgs(part));
        }

        internal override void Reset()
        {
        }

    }
}

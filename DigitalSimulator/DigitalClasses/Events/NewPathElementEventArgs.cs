using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using DigitalClasses.Controls;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Events
{
    public class NewSymbolPartEventArgs
    {
        private SymbolPart m_SymbolPart;

        public NewSymbolPartEventArgs(SymbolPart symbolPart)
        {
            m_SymbolPart = symbolPart;
        }

        public SymbolPart SymbolPart
        {
            get
            {
                return m_SymbolPart;
            }
        }
    }
}

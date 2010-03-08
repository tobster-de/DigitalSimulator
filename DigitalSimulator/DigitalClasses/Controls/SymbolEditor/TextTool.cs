using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DigitalClasses.Events;
using System.Windows.Forms;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Controls
{
    public class TextTool : SymbolTool
    {
        private static SymbolTool m_Instance;

        internal override event NewSymbolPart OnNewSymbolPart;

        /// <summary>
        /// Returns the Singleton Instance of this tool
        /// </summary>
        public static SymbolTool Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new TextTool();
                }
                return m_Instance;
            }
        }

        internal override void MouseClick(PointF location)
        {
            TextInputForm input = new TextInputForm();
            if (input.ShowDialog() == DialogResult.OK)
            {
                SymbolPart part = new TextPart(location, input.TextInput);
                OnNewSymbolPart(this, new NewSymbolPartEventArgs(part));
            }
        }

        internal override void Reset()
        {
        }

    }
}

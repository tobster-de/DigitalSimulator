using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DigitalClasses.Events;

namespace DigitalClasses.Controls
{
    public class UserTool : SymbolTool
    {
        internal override event NewSymbolPart OnNewSymbolPart;

        public event MouseEventHandler OnMouseClick;

        internal override void MouseClick(PointF location)
        {
            if (OnMouseClick != null)
            {
                OnMouseClick(this, new MouseEventArgs(MouseButtons.Left, 1, (int)location.X, (int)location.Y, 0));
            }
        }

        internal override void Reset()
        {
        }

    }
}

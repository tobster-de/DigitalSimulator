using System;
using System.Collections.Generic;
using System.Drawing;

namespace DigitalClasses.Graphic
{
    internal static class GraphicConstants
    {
        //General
        public static readonly Color HighlightColor = Color.FromArgb(190, 220, 250);
        public static readonly Brush HighlightBrush = new SolidBrush(HighlightColor);
        public static readonly Color GrabHandleColor = Color.FromArgb(30, 150, 30);
        public static readonly Brush GrabHandleBrush = new SolidBrush(GrabHandleColor);
        public static readonly Color BackgroundColor = Color.White;        
        public static readonly Brush BackgroundBrush = new SolidBrush(BackgroundColor);
        public static readonly Color StateHighColor = Color.Red;
        public static readonly Color StateLowColor = Color.Blue;        
        
        //GraphicShape
        public const float DefaultPenWidth = 1.0f;
        public const float DefaultAngle = 0.0f;
        public static readonly Color DefaultColor = Color.Black;

        //GraphicElement
        public const int GraphicElementWidth = 32;
        public const int GraphicElementTerminalSpace = 8;
        public const int GraphicElementUpperSpace = 8;
        public const int GraphicElementLowerSpace = 8;
        public const int GraphicTerminalWidth = 8;

        //GraphicSignalInput
        public const int GraphicSignalWidth = 32;
        public const int GraphicSignalHeight = 16;

        
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalClasses.Graphic;
using DigitalClasses.Graphic.Symbols;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class converts symbol classes to their corresponding serializable data classes and vice versa
    /// </summary>
    public class SymbolConverter
    {
        #region Fields

        private delegate object ConvertMethod(object Input);

        private static SymbolConverter m_Instance = new SymbolConverter();
        private List<Type> m_PartTypes;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the singleton Instance for this class
        /// </summary>
        public static SymbolConverter Instance
        {
            get
            {
                return m_Instance;
            }
        }

        #endregion

        #region Construction

        private SymbolConverter()
        {
            #region build type dictionary
            m_PartTypes = new List<Type>();
            m_PartTypes.Add(typeof(PortPart));
            m_PartTypes.Add(typeof(LinePart));
            m_PartTypes.Add(typeof(RectanglePart));
            m_PartTypes.Add(typeof(TextPart));
            #endregion
        }

        #endregion

        #region Public Implementation

        public SymbolData ConvertFromSymbol(Symbol symbol)
        {
            SymbolData symbolData = new SymbolData(symbol.Name);
            CreatePartData(symbolData, symbol);
            return symbolData;
        }

        public Symbol ConvertToSymbol(SymbolData symbolData)
        {
            Symbol symbol = new Symbol();
            CreateParts(symbol, symbolData);
            return symbol;
        }

        #endregion

        #region Private Implementation

        private void CreatePartData(SymbolData symbolData, Symbol symbol)
        {
            List<SymbolPartData> parts = new List<SymbolPartData>();
            foreach (SymbolPart sp in symbol)
            {
                SymbolPartData partData = ConvertSymbolPart(sp);
                if (sp != null)
                {
                    parts.Add(partData);
                }
            }
            symbolData.SymbolParts = parts.ToArray();
        }

        private void CreateParts(Symbol symbol, SymbolData symbolData)
        {
            foreach (SymbolPartData partData in symbolData.SymbolParts)
            {
                SymbolPart part = ConvertPartData(partData);
                if (part != null)
                {
                    symbol.AddPart(part);
                }
            }
        }

        #endregion

        #region ConvertMethods

        private SymbolPartData ConvertSymbolPart(SymbolPart symbolPart)
        {
            if (symbolPart is LinePart)
            {
                LinePart linePart = (LinePart)symbolPart;
                PointF p1 = linePart.GetPoint(0);
                PointF p2 = linePart.GetPoint(1);
                return new SymbolLineData(p1.X, p1.Y, p2.X, p2.Y);
            }
            if (symbolPart is RectanglePart)
            {
                RectanglePart rectPart = (RectanglePart)symbolPart;
                return new SymbolRectData(rectPart.Location.X, rectPart.Location.Y, rectPart.Width, rectPart.Height);
            }
            if (symbolPart is TextPart)
            {
                TextPart textPart = (TextPart)symbolPart;
                return new SymbolTextData(textPart.Location.X, textPart.Location.Y, textPart.Text);
            }
            if (symbolPart is PortPart)
            {
                PortPart portPart = (PortPart)symbolPart;
                return new SymbolPortData(portPart.Location.X, portPart.Location.Y, portPart.Angle, portPart.Direction, portPart.Name);
            }
            return null;
        }

        private SymbolPart ConvertPartData(SymbolPartData partData)
        {
            if (partData is SymbolLineData)
            {
                SymbolLineData lineData = (SymbolLineData)partData;
                return new LinePart(lineData.X, lineData.Y, lineData.X2, lineData.Y2);
            }
            if (partData is SymbolRectData)
            {
                SymbolRectData rectData = (SymbolRectData)partData;
                return new RectanglePart(new PointF(rectData.X, rectData.Y), new SizeF(rectData.Width, rectData.Height));
            }
            if (partData is SymbolTextData)
            {
                SymbolTextData textData = (SymbolTextData)partData;
                return new TextPart(new PointF(textData.X, textData.Y), textData.Text);
            }
            if (partData is SymbolPortData)
            {
                SymbolPortData portData = (SymbolPortData)partData;
                return new PortPart(new PointF(portData.X, portData.Y), portData.Angle, portData.Direction, portData.Name);
            }
            return null;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DigitalClasses.Controls;
using DigitalClasses.Serialization;
using System.IO;
using System.Drawing.Drawing2D;

namespace SymbolEditor
{
    public partial class SymbolEditorForm : Form
    {
        private UserTool m_OffsetTool;
        private SymbolCollection m_SymbolCollection;

        public SymbolEditorForm()
        {
            InitializeComponent();

            m_OffsetTool = new UserTool();
            m_OffsetTool.OnMouseClick += new MouseEventHandler(OffsetTool_OnMouseClick);

            LoadSymbols();
        }

        private void LoadSymbols()
        {
            //m_SymbolCollection = SymbolSerializer.Deserialize("symbols.xml");
            //string last = combo_SymbolName.Text;
            //combo_SymbolName.Items.Clear();
            //foreach (SymbolData sd in m_SymbolCollection.Symbols)
            //{
            //    combo_SymbolName.Items.Add(sd.Name);
            //    if (sd.Name.Equals(last))
            //    {
            //        combo_SymbolName.SelectedItem = sd.Name;
            //    }
            //}
        }

        private void btn_Selection_Click(object sender, EventArgs e)
        {
            symbolEditor.CurrentTool = PartSelectionTool.Instance;
        }

        private void btn_LineTool_Click(object sender, EventArgs e)
        {
            symbolEditor.CurrentTool = LineTool.Instance;
        }

        private void btn_RectangleTool_Click(object sender, EventArgs e)
        {
            symbolEditor.CurrentTool = RectangleTool.Instance;
        }

        private void btn_Offset_Click(object sender, EventArgs e)
        {
            symbolEditor.CurrentTool = m_OffsetTool;
        }

        private void btn_TextTool_Click(object sender, EventArgs e)
        {
            symbolEditor.CurrentTool = TextTool.Instance;
        }

        private void btn_DeleteAll_Click(object sender, EventArgs e)
        {
            symbolEditor.CurrentTool = PartDeletionTool.Instance;
        }

        private void btn_TerminalRight_Click(object sender, EventArgs e)
        {
            btn_TerminalRight.Checked = true;
            btn_TerminalLeft.Checked = false;
            btn_Terminal.Image = btn_TerminalRight.Image;
            symbolEditor.CurrentTool = new PortTool(DigitalClasses.DirectionType.Output);
        }

        private void btn_TerminalLeft_Click(object sender, EventArgs e)
        {
            btn_TerminalRight.Checked = false;
            btn_TerminalLeft.Checked = true;
            btn_Terminal.Image = btn_TerminalLeft.Image;
            symbolEditor.CurrentTool = new PortTool(DigitalClasses.DirectionType.Input);
        }

        private void btn_Terminal_ButtonClick(object sender, EventArgs e)
        {
            if (btn_TerminalRight.Checked)
            {
                symbolEditor.CurrentTool = new PortTool(DigitalClasses.DirectionType.Output);
                return;
            }
            if (btn_TerminalLeft.Checked)
            {
                symbolEditor.CurrentTool = new PortTool(DigitalClasses.DirectionType.Input);
                return;
            }
        }

        void OffsetTool_OnMouseClick(object sender, MouseEventArgs e)
        {
            symbolEditor.Offset = symbolEditor.ReverseTranslate(e.Location, false);
        }

        private void symbolEditor_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = symbolEditor.Translate(e.Location, true);
            statusStrip1.Items[0].Text = String.Format("X: {0} Y: {1}", point.X, point.Y);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            ////SymbolCollection collection = SymbolSerializer.Deserialize("symbols.xml");
            //SymbolData symboldata = new SymbolData(combo_SymbolName.Text, symbolEditor.Size.Height, symbolEditor.Size.Width);
            //symboldata.Body = new BodyData(symbolEditor.Path.PathData);
            //m_SymbolCollection.Add(symboldata);
            //SymbolSerializer.SerializeSymbols("symbols.xml", m_SymbolCollection);

            //LoadSymbols();
        }

        private void btn_NewSymbol_Click(object sender, EventArgs e)
        {
            //symbolEditor.Path = new GraphicsPath();
            symbolEditor.Invalidate();
        }

        private void combo_SymbolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (SymbolData sd in m_SymbolCollection.Symbols)
            {
                if (sd.Name.Equals(combo_SymbolName.SelectedItem))
                {
                    //PathData pd = sd.Body.GetPathData();
                    //symbolEditor.Path = new GraphicsPath(pd.Points, pd.Types);
                    symbolEditor.Update();
                    symbolEditor.Refresh();
                    return;
                }
            }

        }

    }
}

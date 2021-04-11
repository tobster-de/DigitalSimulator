using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalClasses.Graphic;
using DigitalClasses.Logic;

namespace SymbolViewer
{
    public partial class SymbolViewerForm : Form
    {
        public SymbolViewerForm()
        {
            InitializeComponent();
            combo_SymbolName.Items.Add("AndGate");
            combo_SymbolName.Items.Add("OrGate");
            combo_SymbolName.Items.Add("NandGate");
            combo_SymbolName.Items.Add("NorGate");
            combo_SymbolName.Items.Add("NotGate");
        }

        private void combo_SymbolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((String)combo_SymbolName.SelectedItem == "AndGate")
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(AndGate), new AndGate(5));
                element.Location = new PointF(8, 0);
                symbolView1.Element = element;
            }
            if ((String)combo_SymbolName.SelectedItem == "OrGate")
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(OrGate), new OrGate(5));
                element.Location = new PointF(8, 0);
                symbolView1.Element = element;
            }
            if ((String)combo_SymbolName.SelectedItem == "NandGate")
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NandGate), new NandGate(5));
                element.Location = new PointF(8, 0);
                symbolView1.Element = element;
            }
            if ((String)combo_SymbolName.SelectedItem == "NorGate")
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NorGate), new NorGate(5));
                element.Location = new PointF(8, 0);
                symbolView1.Element = element;
            }
            if ((String)combo_SymbolName.SelectedItem == "NotGate")
            {
                GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NotGate), new NotGate());
                element.Location = new PointF(8, 0);
                symbolView1.Element = element;
            }
        }
    }
}

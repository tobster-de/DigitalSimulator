using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalClasses.Controls;
using DigitalClasses.Graphic;
using DigitalClasses.Logic;
using DigitalClasses.Events;

namespace CircuitEditor
{
    public partial class CircuitEditorForm : Form
    {
        #region Fields

        ConnectionTool connectionTool;
        SelectionTool selectionTool;
        PropertyForm propertyForm;

        #endregion

        public CircuitEditorForm()
        {
            InitializeComponent();

            connectionTool = new ConnectionTool();
            selectionTool = new SelectionTool();
            selectionTool.OnElementSelected += new ElementSelected(selectionTool_OnElementSelected);
        }

        private void menuItem_AND_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(AndGate), new AndGate(2));
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void menuItem_NAND_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NandGate), new NandGate(2));
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void menuItem_OR_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(OrGate), new OrGate(2));
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void menuItem_NOR_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NorGate), new NorGate(2));
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void menuItem_NOT_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NotGate), new NotGate());
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void btn_Connection_Click(object sender, EventArgs e)
        {
            circuitEditor.CurrentTool = connectionTool;
        }

        private void menuItem_Input_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(SignalInput), new SignalInput());
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void menuItem_Output_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(SignalOutput), new SignalOutput());
            circuitEditor.CurrentTool = new GateTool(element);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            circuitEditor.CurrentTool = selectionTool;
        }

        void selectionTool_OnElementSelected(object sender, ElementSelectedEventArgs e)
        {
            if (propertyForm == null || propertyForm.IsDisposed)
            {
                propertyForm = new PropertyForm();
            }
            propertyForm.SetElement(e.Element.LinkedObject);
            propertyForm.Show();
        }


        private void btn_Step_Click(object sender, EventArgs e)
        {
            circuitEditor.UpdateCircuit();
            circuitEditor.UpdateDrawing();
            circuitEditor.Invalidate();
        }

    }
}

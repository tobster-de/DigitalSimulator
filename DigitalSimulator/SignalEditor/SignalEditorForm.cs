using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalClasses.Logic;

namespace SignalEditor
{
    public partial class SignalEditorForm : Form
    {
        Signal signal = new Signal();
        SignalList signalList = new SignalList();

        public SignalEditorForm()
        {
            InitializeComponent();
            signalEditor1.Signal = signal;
            signalPanel1.SignalList = signalList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            signal.Name = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //signal.Add(State.High);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //signal.Add(State.Low);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            signal.Clear();
        }

        private void SignalEditorForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //add
            signalList.Add((Signal)signal.Clone());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //remove
            signalList.RemoveAt(signalList.Count - 1);
        }

    }
}

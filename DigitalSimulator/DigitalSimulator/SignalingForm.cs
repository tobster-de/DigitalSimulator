using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DigitalSimulator
{
    public partial class SignalingForm : DockContent
    {
        public SignalingForm()
        {
            InitializeComponent();
        }

        internal void SetSignalList(DigitalClasses.Logic.SignalList signalList)
        {
            signalPanel.SignalList = signalList;
        }
    }
}

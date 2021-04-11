using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalClasses.Graphic;

namespace CircuitEditor
{
    public partial class PropertyForm : Form
    {
        public PropertyForm()
        {
            InitializeComponent();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //e.ChangedItem
        }

        internal void SetElement(object element)
        {
            propertyGrid.SelectedObject = element;
        }
    }
}

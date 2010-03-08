using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DigitalClasses.Graphic;
using WeifenLuo.WinFormsUI.Docking;

namespace DigitalSimulator
{
    public partial class PropertyForm : DockContent
    {
        public PropertyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the displayed Element in the property grid
        /// </summary>
        /// <param name="element">element to set in the property grid</param>
        internal void SetElement(object element)
        {
            propertyGrid.SelectedObject = element;
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //refresh in case the change of a property also causes changes in other properties
            propertyGrid.Refresh();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DigitalClasses.Controls
{
    public partial class TextInputForm : Form
    {
        public string TextInput
        {
            get
            {
                return textBox.Text;
            }
        }

        public TextInputForm()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TextInputForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBox.Text = "";
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
        }
    }
}

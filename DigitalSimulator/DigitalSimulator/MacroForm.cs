using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using DigitalClasses.Logic;
using DigitalClasses.Graphic;

namespace DigitalSimulator
{
    /// <summary>
    /// With this Form the user can select the macro he wants to place
    /// </summary>
    public partial class MacroForm : Form
    {
        #region Fields

        private string m_SelectedMacro;
        private List<string> m_LastSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the name of the selected macro
        /// </summary>
        public string SelectedMacro
        {
            get
            {
                return m_SelectedMacro;
            }
        }

        /// <summary>
        /// The list of last selected macros
        /// </summary>
        public List<string> LastSelectedMacros
        {
            get
            {
                return m_LastSelected;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        public MacroForm()
        {
            InitializeComponent();

            m_LastSelected = new List<string>();
            GenerateItems();
        }

        #endregion

        #region Private Implementation

        private void GenerateItems()
        {
            MacroCache macroCache = MacroCache.Instance;
            List<string> macroNames = macroCache.GetMacroNames();
            macroNames.Sort(String.Compare);
            foreach (string name in macroNames)
            {
                //Symbol symbol = macroCache.GetSymbol(name);
                GraphicBaseElement element =
                    GraphicObjectFactory.CreateInstance(typeof(Macro), MacroCache.Instance.GetMacro(name));
                element.Location = new PointF(element.Bounds.X * -1, element.Bounds.Y * -1);
                int width = (int)element.Bounds.Width + 1;
                int height = (int)element.Bounds.Height + 1;
                if (width < 48)
                    width = 48;
                if (height < 48)
                    height = 48;
                if (width > 48 && height <= 48)
                {
                    element.Location = new PointF(element.Location.X, element.Location.Y + (width - height));
                    height = width;
                }
                if (height > 48 && width <= 48)
                {
                    element.Location = new PointF(element.Location.X + (height - width), element.Location.Y);
                    width = height;
                }
                Image img = new Bitmap(width, height);
                element.Paint(Graphics.FromImage(img));

                imageList_Symbols.Images.Add(name, img);

                ListViewItem item = listView_Macros.Items.Add(name, imageList_Symbols.Images.IndexOfKey(name));
            }
        }

        #endregion

        #region Form Event Handler

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (listView_Macros.SelectedItems.Count > 0)
            {
                string macroName = listView_Macros.SelectedItems[0].Text;
                m_SelectedMacro = macroName;
                if (m_LastSelected.Contains(macroName))
                {
                    m_LastSelected.Remove(macroName);
                }
                while (m_LastSelected.Count > 5)
                {
                    m_LastSelected.RemoveAt(m_LastSelected.Count - 1);
                }
                m_LastSelected.Insert(0, macroName);
                DialogResult = DialogResult.OK;
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button_OK.Enabled = e.IsSelected;
        }

        #endregion
    }
}

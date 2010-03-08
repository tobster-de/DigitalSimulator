using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using DigitalClasses.Logic;
using DigitalClasses.Graphic.Symbols;
using DigitalClasses.Serialization;

namespace DigitalSimulator
{
    public partial class MacroCreatorForm : DockContent
    {
        #region Fields

        private CircuitData m_CircuitData;
        private SymbolData m_SymbolData;
        private List<MatchingData> m_Matchings;

        #endregion

        public MacroCreatorForm()
        {
            InitializeComponent();
            m_Matchings = new List<MatchingData>();
        }

        private void OpenCircuit(object sender, EventArgs e)
        {
            FileSelector fileSelector = new FileSelector(FileSelectorFilters.Circuits);
            fileSelector.FileName = textBox_Circuit.Text;
            if (fileSelector.ExecuteOpenDialog())
            {
                textBox_Circuit.Text = fileSelector.FileName;
            }
            CheckSelection();
        }

        private void OpenSymbol(object sender, EventArgs e)
        {
            FileSelector fileSelector = new FileSelector(FileSelectorFilters.Symbols);
            fileSelector.FileName = textBox_Symbol.Text;
            if (fileSelector.ExecuteOpenDialog())
            {
                textBox_Symbol.Text = fileSelector.FileName;
            }
            CheckSelection();
        }

        private void CheckSelection()
        {
            button_ProceedToPage2.Enabled =
                String.IsNullOrEmpty(textBox_Circuit.Text) == false &&
                String.IsNullOrEmpty(textBox_Symbol.Text) == false &&
                File.Exists(textBox_Circuit.Text) &&
                File.Exists(textBox_Symbol.Text);
        }

        private void ProceedToPage2(object sender, EventArgs e)
        {
            textBox_MacroName.Text = Path.GetFileNameWithoutExtension(textBox_Circuit.Text);
            LoadFiles();
            panel_Sources.Hide();
            panel_Matching.Show();
            button_Add.Enabled = false;
            button_Remove.Enabled = false;
            button_Finish.Enabled = false;
        }

        private void BackToPage1(object sender, EventArgs e)
        {
            panel_Matching.Hide();
            panel_Sources.Show();
            CheckSelection();
            listView_CircuitIOs.Items.Clear();
            listView_SymbolPorts.Items.Clear();
            listView_Matchings.Items.Clear();
        }

        private void LoadFiles()
        {
            button_ProceedToPage2.Enabled = false;
            //at this point the chosen files really do exist
            m_CircuitData = CircuitSerializer.DeserializeCircuit(textBox_Circuit.Text);
            m_CircuitData.Signals = null;
            Circuit circuit = CircuitConverter.Instance.ConvertToCircuit(m_CircuitData);
            foreach (BaseElement be in circuit)
            {
                if (be is SignalInput)
                {
                    ListViewItem li = new ListViewItem(be.Name);
                    //li.ToolTipText = String.Format("{0} ({1}, X: {2}, Y: {3})", be.);
                    li.Group = listView_CircuitIOs.Groups[0];
                    listView_CircuitIOs.Items.Add(li);
                }
                if (be is SignalOutput)
                {
                    ListViewItem li = new ListViewItem(be.Name);
                    li.Group = listView_CircuitIOs.Groups[1];
                    listView_CircuitIOs.Items.Add(li);
                }
            }

            m_SymbolData = SymbolSerializer.DeserializeSymbol(textBox_Symbol.Text);
            Symbol symbol = SymbolConverter.Instance.ConvertToSymbol(m_SymbolData);
            foreach (SymbolPart part in symbol)
            {
                if (part is PortPart)
                {
                    PortPart port = (PortPart)part;
                    if (port.Direction == DirectionType.Input)
                    {
                        ListViewItem li = new ListViewItem(port.Name);
                        //li.ToolTipText = String.Format("{0} ({1}, X: {2}, Y: {3})", port.Name, port.Direction, port.Location.X, port.Location.Y);
                        li.Group = listView_SymbolPorts.Groups[0];
                        listView_SymbolPorts.Items.Add(li);
                    }
                    if (port.Direction == DirectionType.Output)
                    {
                        ListViewItem li = new ListViewItem(port.Name);
                        //li.ToolTipText = String.Format("{0} ({1}, X: {2}, Y: {3})", port.Name, port.Direction, port.Location.X, port.Location.Y);
                        li.Group = listView_SymbolPorts.Groups[1];
                        listView_SymbolPorts.Items.Add(li);
                    }
                }
            }
        }

        private void PortSelected(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button_Add.Enabled =
                listView_SymbolPorts.SelectedItems.Count == 1 &&
                listView_CircuitIOs.SelectedItems.Count == 1 &&
                listView_CircuitIOs.Groups.IndexOf(listView_CircuitIOs.SelectedItems[0].Group) ==
                listView_SymbolPorts.Groups.IndexOf(listView_SymbolPorts.SelectedItems[0].Group);
        }

        private void MatchingSelected(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button_Remove.Enabled =
                (listView_Matchings.SelectedItems.Count == 1);
        }

        private void AddMatching(object sender, EventArgs e)
        {
            if (listView_SymbolPorts.SelectedItems.Count != 1 ||
                listView_CircuitIOs.SelectedItems.Count != 1)
            {
                return;
            }
            ListViewItem ioItem = listView_CircuitIOs.SelectedItems[0];
            ListViewItem portItem = listView_SymbolPorts.SelectedItems[0];

            int index = listView_CircuitIOs.Groups.IndexOf(ioItem.Group);
            DirectionType direction = DirectionType.Input;
            if (index == 1)
                direction = DirectionType.Output;
            m_Matchings.Add(new MatchingData(portItem.Text, ioItem.Text, direction));

            ListViewItem li = new ListViewItem(ioItem.Text);
            li.Group = listView_Matchings.Groups[index];
            li.SubItems.Add(portItem.Text);
            listView_Matchings.Items.Add(li);

            listView_CircuitIOs.Items.Remove(ioItem);
            listView_SymbolPorts.Items.Remove(portItem);

            EnableFinishButton();
        }

        private void RemoveMatching(object sender, EventArgs e)
        {
            if (listView_Matchings.SelectedItems.Count != 1)
            {
                return;
            }
            ListViewItem matchItem = listView_Matchings.SelectedItems[0];
            int index = listView_Matchings.Groups.IndexOf(matchItem.Group);

            ListViewItem ioItem = new ListViewItem(matchItem.Text);
            ioItem.Group = listView_CircuitIOs.Groups[index];
            listView_CircuitIOs.Items.Add(ioItem);

            ListViewItem portItem = new ListViewItem(matchItem.SubItems[1].Text);
            portItem.Group = listView_SymbolPorts.Groups[index];
            listView_SymbolPorts.Items.Add(portItem);

            m_Matchings.Remove(m_Matchings.Find(
                delegate(MatchingData matching)
                {
                    return matching.PortName.Equals(matchItem.SubItems[1].Text) &&
                        matching.IOElementName.Equals(matchItem.Text);
                }
            ));
            listView_Matchings.Items.Remove(matchItem);

            if (listView_Matchings.Items.Count == 0)
                button_Finish.Enabled = false;
        }

        private void EnableFinishButton()
        {
            button_Finish.Enabled =
                listView_Matchings.Items.Count > 0 &&
                String.IsNullOrEmpty(textBox_MacroName.Text) == false;
        }

        private void textBox_MacroName_TextChanged(object sender, EventArgs e)
        {
            EnableFinishButton();
        }

        private void SaveMacro(object sender, EventArgs e)
        {
            /*
            FileSelector fileSelector = new FileSelector(FileSelectorFilters.Macros);
            fileSelector.FileName = textBox_MacroName.Text;
            if (fileSelector.ExecuteSaveDialog())
            {
                MacroData macroData = new MacroData(textBox_MacroName.Text);
                macroData.Circuit = m_CircuitData;
                macroData.Symbol = m_SymbolData;
                macroData.Matching = m_Matchings.ToArray();
                MacroSerializer.SerializeMacro(fileSelector.FileName, macroData);
            }
            /**/
            string macroName = textBox_MacroName.Text;
            char[] invalid = Path.GetInvalidFileNameChars();
            foreach (char inv in invalid)
            {
                int index = macroName.IndexOf(inv);
                while (index > -1)
                {
                    macroName.Remove(index, 1);
                }
            }
            macroName.Replace(" ", "_");
            string filename = Application.StartupPath + @"\Macros\" + macroName + @".xmac";
            if (File.Exists(filename) &&
                MessageBox.Show("Es existiert bereits ein Makro mit dem gewählten Namen. Soll dieses überschrieben werden?",
                    "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            MacroData macroData = new MacroData(textBox_MacroName.Text);
            macroData.Circuit = m_CircuitData;
            macroData.Symbol = m_SymbolData;
            macroData.Matching = m_Matchings.ToArray();
            MacroSerializer.SerializeMacro(filename, macroData);
            MacroCache.Instance.LoadMacro(filename);

            Close();
        }

    }
}

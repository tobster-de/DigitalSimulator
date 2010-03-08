using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using DigitalClasses.Controls;
using DigitalClasses.Graphic;
using DigitalClasses.Logic;
using DigitalClasses.Events;
using DigitalClasses.Serialization;

namespace DigitalSimulator
{
    public partial class MainForm : Form
    {
        #region Fields

        string m_HelpFileName;

        private int m_CircuitNumber = 0;
        private int m_SymbolNumber = 0;
        private int m_MacroNumber = 0;
        private ConnectionTool m_ConnectionTool;
        private ElementDeletionTool m_ElementDeletionTool;
        private DisconnectionTool m_DisconnectionTool;
        private ElementSelectionTool m_SelectionTool;
        private CircuitTool m_CircuitTool;
        private SymbolTool m_SymbolTool;
        private UserTool m_OffsetTool;

        private PropertyForm propertyForm = new PropertyForm();
        private SignalingForm signalingForm = new SignalingForm();
        private SimulationControlForm simulationForm = new SimulationControlForm();
        private MacroForm macroForm;

        private List<string> m_RecentFiles;
        private Dictionary<string, FileSelectorFilters> m_RecentFileTypes;

        #endregion

        #region Properties

        /// <summary>
        /// File name of the help file
        /// </summary>
        public string HelpFileName
        {
            get
            {
                return m_HelpFileName;
            }
        }

        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();

            m_ConnectionTool = new ConnectionTool();
            m_SelectionTool = new ElementSelectionTool();
            m_ElementDeletionTool = new ElementDeletionTool();
            m_DisconnectionTool = new DisconnectionTool();
            m_SelectionTool.OnElementSelected += new ElementSelected(selectionTool_OnElementSelected);

            m_OffsetTool = new UserTool();
            m_OffsetTool.OnMouseClick += new MouseEventHandler(OffsetTool_OnMouseClick);

            (PartSelectionTool.Instance as PartSelectionTool).OnPartSelected += new PartSelected(partSelectionTool_OnPartSelected);

            //signalingForm.Show(dockPanel, DockState.DockRight);
            signalingForm.Height = (int)(Height * 0.5);
            signalingForm.Show(dockPanel, DockState.DockBottom);
            menuItem_SignalControl.Checked = signalingForm.Visible;
            signalingForm.VisibleChanged += new EventHandler(toolForm_VisibleChanged);

            //propertyForm.Show(signalingForm.Pane, DockAlignment.Bottom, 0.5);
            propertyForm.Show(dockPanel, DockState.DockRight);
            propertyForm.VisibleChanged += new EventHandler(toolForm_VisibleChanged);
            menuItem_SignalControl.Checked = propertyForm.Visible;

            simulationForm.Show(propertyForm.Pane, DockAlignment.Top, 0.4);
            menuItem_SimControl.Checked = simulationForm.Visible;
            simulationForm.VisibleChanged += new EventHandler(toolForm_VisibleChanged);

            m_HelpFileName = Path.Combine(Application.StartupPath, "help.chm");

            //dockPanel.
        }

        #endregion

        #region General Event Handler

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Load previus settings
        }

        /// <summary>
        /// Event handler for changing child windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveMdiChild is ISaveableDocument)
            {
                btn_Save.Enabled = true;
                menuItem_Save.Enabled = true;
                menuItem_SaveAs.Enabled = true;
            }
            else
            {
                btn_Save.Enabled = false;
                menuItem_Save.Enabled = false;
                menuItem_SaveAs.Enabled = false;
            }
            if (ActiveMdiChild is SymbolEditorForm)
            {
                toolStrip_SymbolElements.Enabled = true;
            }
            else
            {
                toolStrip_SymbolElements.Enabled = false;
            }
            if (ActiveMdiChild is CircuitEditorForm)
            {
                CircuitEditorForm child = ActiveMdiChild as CircuitEditorForm;
                CircuitEditor circuitEditor = child.CircuitEditor;
                signalingForm.SetSignalList(circuitEditor.SignalList);
                propertyForm.SetElement(null);
                simulationForm.SetSimulation(child.Simulation);
                circuitEditor.CurrentTool = m_CircuitTool;
                toolStrip_DigitalElements.Enabled = true;
                //toolStrip_Simulation.Enabled = circuitEditor.SignalList.Count > 0;
            }
            else
            {
                propertyForm.SetElement(null);
                simulationForm.SetSimulation(null);
                toolStrip_DigitalElements.Enabled = false;
                //toolStrip_Simulation.Enabled = false;
            }
        }

        /// <summary>
        /// Event handler for closing child windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void childForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is CircuitEditorForm)
            {
                signalingForm.SetSignalList(null);
                propertyForm.SetElement(null);
            }
        }

        void toolForm_VisibleChanged(object sender, EventArgs e)
        {
            if (sender.Equals(propertyForm))
            {
                menuItem_Properties.Checked = propertyForm.Visible;
            }
            if (sender.Equals(signalingForm))
            {
                menuItem_SignalControl.Checked = signalingForm.Visible;
            }
            if (sender.Equals(simulationForm))
            {
                menuItem_SimControl.Checked = simulationForm.Visible;
            }
        }

        void SimulationStateChangeHandler(object sender, SimulationState newState)
        {
            if (newState == SimulationState.Running || newState == SimulationState.Paused)
            {
                signalingForm.SetSignalList((sender as Simulation).Signals);
            }
            else if (ActiveMdiChild is CircuitEditorForm)
            {
                signalingForm.SetSignalList((ActiveMdiChild as CircuitEditorForm).CircuitEditor.SignalList);
            }
        }

        #endregion

        #region General Methods

        private void ShowChildForm(DockContent childForm)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                childForm.MdiParent = this;
                childForm.Show();
            }
            else
                childForm.Show(dockPanel);
        }

        public DockContent ExecuteOpenFileDialog()
        {
            try
            {
                FileSelector fileSelector = new FileSelector(FileSelectorFilters.AnyFile | FileSelectorFilters.Circuits | FileSelectorFilters.Symbols);
                if (fileSelector.ExecuteOpenDialog())
                {
                    ISaveableDocument editor = null;
                    switch (fileSelector.UsedFilter)
                    {
                        case FileSelectorFilters.Circuits:
                            editor = CreateNewEditorForm();
                            break;
                        case FileSelectorFilters.Symbols:
                            editor = CreateNewSymbolForm();
                            break;
                    }
                    if (editor.LoadDocument(fileSelector.FileName))
                    {
                        HandleRecentFiles(fileSelector.FileName, fileSelector.UsedFilter);

                        return (editor as DockContent);
                    }
                    else
                    {
                        (editor as Form).Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Exception Handling
                MessageBox.Show(ex.Message + "\n-----\n" + ex.StackTrace, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return null;
        }

        private void HandleRecentFiles(string fileName, FileSelectorFilters filter)
        {
            if (m_RecentFiles == null)
            {
                m_RecentFiles = new List<string>();
                m_RecentFileTypes = new Dictionary<string, FileSelectorFilters>();
            }
            if (m_RecentFiles.Contains(fileName))
            {
                m_RecentFiles.Remove(fileName);
                m_RecentFileTypes.Remove(fileName);
            }
            m_RecentFiles.Insert(0, fileName);
            m_RecentFileTypes.Add(fileName, filter);
            while (m_RecentFiles.Count > 10)
            {
                m_RecentFiles.RemoveAt(m_RecentFiles.Count - 1);
            }

            int count = 0;
            foreach (ToolStripItem tsitem in menuItem_RecentFiles.DropDownItems)
            {
                tsitem.Text = Path.GetFileName(m_RecentFiles[count]);
                count++;
            }
            while (count < m_RecentFiles.Count)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(Path.GetFileName(m_RecentFiles[count]));
                menuItem.ToolTipText = m_RecentFiles[count];
                menuItem.Click += new EventHandler(LoadRecentFile);
                menuItem.Tag = count;
                menuItem_RecentFiles.DropDownItems.Add(menuItem);
                count++;
            }

        }

        void LoadRecentFile(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem == false)
            {
                return;
            }
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi == null || tsmi.Tag is int == false || (int)tsmi.Tag < 0 || (int)tsmi.Tag > m_RecentFiles.Count - 1)
            {
                return;
            }
            string fileName = m_RecentFiles[(int)tsmi.Tag];
            ISaveableDocument editor = null;
            switch (m_RecentFileTypes[fileName])
            {
                case FileSelectorFilters.Circuits:
                    editor = CreateNewEditorForm();
                    break;
                case FileSelectorFilters.Symbols:
                    editor = CreateNewSymbolForm();
                    break;
            }

            if (editor.LoadDocument(fileName))
            {
                ShowChildForm((DockContent)editor);
                HandleRecentFiles(fileName, m_RecentFileTypes[fileName]);
            }
        }

        #endregion

        #region Menu File

        private void ShowNewForm(object sender, EventArgs e)
        {
            ShowChildForm(CreateNewEditorForm());
        }

        private void OpenFile(object sender, EventArgs e)
        {
            DockContent childForm = ExecuteOpenFileDialog();
            if (childForm != null)
            {
                ShowChildForm(childForm);
            }
        }

        private CircuitEditorForm CreateNewEditorForm()
        {
            CircuitEditorForm childForm = new CircuitEditorForm();
            childForm.Text = "Schaltung " + ++m_CircuitNumber;
            childForm.Simulation.OnSimulationStateChanged += new SimulationStateChanged(SimulationStateChangeHandler);

            childForm.FormClosed += new FormClosedEventHandler(childForm_FormClosed);
            return childForm;
        }

        private void SaveFile(object sender, EventArgs e)
        {
            if (ActiveMdiChild is ISaveableDocument)
            {
                (ActiveMdiChild as ISaveableDocument).SaveDocument();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is ISaveableDocument)
            {
                (ActiveMdiChild as ISaveableDocument).SaveDocumentAs();
            }
        }

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            this.Close();
        }

        #endregion

        #region Menu Edit

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Menu View

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip_Standard.Visible = menuItem_StandardToolbar.Checked;
        }

        private void menuItem_DigitalElements_Click(object sender, EventArgs e)
        {
            toolStrip_DigitalElements.Visible = menuItem_DigitalElements.Checked;
        }

        private void menuItem_SymbolElements_Click(object sender, EventArgs e)
        {
            toolStrip_SymbolElements.Visible = menuItem_SymbolElements.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void MenuItem_SignalControl_Click(object sender, EventArgs e)
        {
            if (signalingForm.Visible)
                signalingForm.Hide();
            else
                signalingForm.Show();
            menuItem_SignalControl.Checked = signalingForm.Visible;
        }

        private void MenuItem_Properties_Click(object sender, EventArgs e)
        {
            if (propertyForm.Visible)
                propertyForm.Hide();
            else
                propertyForm.Show();
            menuItem_Properties.Checked = propertyForm.Visible;
        }

        private void MenuItem_SimControl_Click(object sender, EventArgs e)
        {
            if (simulationForm.Visible)
                simulationForm.Hide();
            else
                simulationForm.Show();
            menuItem_SimControl.Checked = simulationForm.Visible;
        }

        #endregion

        #region Menu Extras

        private void menuItem_NewSymbol_Click(object sender, EventArgs e)
        {
            ShowChildForm(CreateNewSymbolForm());
        }

        private SymbolEditorForm CreateNewSymbolForm()
        {
            SymbolEditorForm childForm = new SymbolEditorForm();
            childForm.Text = "Symbol " + ++m_SymbolNumber;
            childForm.FormClosed += new FormClosedEventHandler(childForm_FormClosed);
            return childForm;
        }

        private void menuItem_NewMacro_Click(object sender, EventArgs e)
        {
            ShowChildForm(CreateNewMacroForm());
        }

        private MacroCreatorForm CreateNewMacroForm()
        {
            MacroCreatorForm childForm = new MacroCreatorForm();
            childForm.Text = "Makro " + ++m_MacroNumber;
            childForm.FormClosed += new FormClosedEventHandler(childForm_FormClosed);
            return childForm;
        }

        #endregion

        #region Menu Window

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        #region Menu Help

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, m_HelpFileName, HelpNavigator.TableOfContents);
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, m_HelpFileName, HelpNavigator.Index);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, m_HelpFileName, HelpNavigator.Find);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutBox = new AboutBox(DigitalSimulator.Properties.Resources.Icon);
            aboutBox.ShowDialog();
        }

        #endregion

        #region Toolbar Digital Elements

        private void SetGateTool(GraphicBaseElement element)
        {
            m_CircuitTool = new GateTool(element);
            if (ActiveMdiChild is CircuitEditorForm)
            {
                (ActiveMdiChild as CircuitEditorForm).CircuitEditor.CurrentTool = m_CircuitTool;
            }
        }

        private void menuItem_AND_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(AndGate), new AndGate(2));
            SetGateTool(element);
        }

        private void menuItem_NAND_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NandGate), new NandGate(2));
            SetGateTool(element);
        }

        private void menuItem_OR_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(OrGate), new OrGate(2));
            SetGateTool(element);
        }

        private void menuItem_NOR_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NorGate), new NorGate(2));
            SetGateTool(element);
        }

        private void menuItem_XOR_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(XorGate), new XorGate(2));
            SetGateTool(element);
        }

        private void menuItem_XNOR_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(XnorGate), new XnorGate(2));
            SetGateTool(element);
        }

        private void menuItem_NOT_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(NotGate), new NotGate());
            SetGateTool(element);
        }

        private void menuItem_Buffer_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(BufferGate), new BufferGate());
            SetGateTool(element);
        }

        //private void menuItem_Delay_Click(object sender, EventArgs e)
        //{
        //    GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Delay), new Delay());
        //    SetGateTool(element);
        //}

        private void menuItem_Constant_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(ConstantInput), new ConstantInput());
            SetGateTool(element);
        }

        private void menuItem_Input_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(SignalInput), new SignalInput());
            SetGateTool(element);
        }

        private void menuItem_Output_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(SignalOutput), new SignalOutput());
            SetGateTool(element);
        }

        private void menuItem_Clock_Click(object sender, EventArgs e)
        {
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Clock), new Clock());
            SetGateTool(element);
        }

        private void btn_Connection_Click(object sender, EventArgs e)
        {
            m_CircuitTool = m_ConnectionTool;
            if (ActiveMdiChild is CircuitEditorForm)
            {
                (ActiveMdiChild as CircuitEditorForm).CircuitEditor.CurrentTool = m_CircuitTool;
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            m_CircuitTool = m_SelectionTool;
            if (ActiveMdiChild is CircuitEditorForm)
            {
                (ActiveMdiChild as CircuitEditorForm).CircuitEditor.CurrentTool = m_CircuitTool;
            }
        }

        private void btn_DeleteGate_Click(object sender, EventArgs e)
        {
            m_CircuitTool = m_ElementDeletionTool;
            if (ActiveMdiChild is CircuitEditorForm)
            {
                (ActiveMdiChild as CircuitEditorForm).CircuitEditor.CurrentTool = m_CircuitTool;
            }
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_CircuitTool = m_DisconnectionTool;
            if (ActiveMdiChild is CircuitEditorForm)
            {
                (ActiveMdiChild as CircuitEditorForm).CircuitEditor.CurrentTool = m_CircuitTool;
            }
        }

        private void selectionTool_OnElementSelected(object sender, ElementSelectedEventArgs e)
        {
            if (propertyForm == null || propertyForm.IsDisposed)
            {
                propertyForm = new PropertyForm();
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                    propertyForm.MdiParent = this;
            }
            propertyForm.SetElement(e.Element.LinkedObject);
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                propertyForm.Show();
            else
                propertyForm.Show(dockPanel);
        }

        private void menuItem_OpenMacro_Click(object sender, EventArgs e)
        {
            if (macroForm == null)
            {
                macroForm = new MacroForm();
            }
            if (macroForm.ShowDialog() == DialogResult.OK)
            {
                GraphicBaseElement element =
                    GraphicObjectFactory.CreateInstance(typeof(Macro), MacroCache.Instance.GetMacro(macroForm.SelectedMacro));
                SetGateTool(element);

                int count = 0;
                foreach (ToolStripItem tsitem in btn_Macro.DropDownItems)
                {
                    if (tsitem.Tag != null && tsitem.Tag is int && (int)tsitem.Tag == -10)
                    {
                        tsitem.Text = macroForm.LastSelectedMacros[count];
                        count++;
                    }
                }
                while (count < macroForm.LastSelectedMacros.Count)
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(macroForm.LastSelectedMacros[count]);
                    menuItem.Click += new EventHandler(SelectRecentMacro);
                    menuItem.Tag = -10;
                    btn_Macro.DropDownItems.Add(menuItem);
                    count++;
                }
            }
        }

        void SelectRecentMacro(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
            {
                return;
            }
            Macro macro = MacroCache.Instance.GetMacro(menuItem.Text);
            if (macro == null)
            {
                return;
            }
            GraphicBaseElement element = GraphicObjectFactory.CreateInstance(typeof(Macro), macro);
            SetGateTool(element);
        }

        #endregion

        #region Toolbar Simulation Control

        private void btn_Step_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is CircuitEditorForm)
            {
                CircuitEditor circuitEditor = (ActiveMdiChild as CircuitEditorForm).CircuitEditor;
                circuitEditor.UpdateCircuit();
                circuitEditor.UpdateDrawing();
                circuitEditor.Invalidate();
            }
        }

        #endregion

        #region Toolbar Symbol Elements

        private void SetSymbolTool(SymbolTool symbolTool)
        {
            m_SymbolTool = symbolTool;
            if (ActiveMdiChild is SymbolEditorForm)
            {
                (ActiveMdiChild as SymbolEditorForm).SymbolEditor.CurrentTool = symbolTool;
            }
        }

        private void btn_Selection_Click(object sender, EventArgs e)
        {
            SetSymbolTool(PartSelectionTool.Instance);
            UnselectButtons(toolStrip_SymbolElements.Items);
        }

        private void UnselectButtons(ToolStripItemCollection toolStripItems)
        {
            foreach (ToolStripItem item in toolStripItems)
            {
                if (item is ToolStripButton)
                {
                    (item as ToolStripButton).Checked = false;
                }
            }
        }

        private void btn_LineTool_Click(object sender, EventArgs e)
        {
            SetSymbolTool(LineTool.Instance);
        }

        private void btn_RectangleTool_Click(object sender, EventArgs e)
        {
            SetSymbolTool(RectangleTool.Instance);
        }

        private void btn_Offset_Click(object sender, EventArgs e)
        {
            SetSymbolTool(m_OffsetTool);
        }

        void OffsetTool_OnMouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveMdiChild is SymbolEditorForm)
            {
                SymbolEditorForm sef = (ActiveMdiChild as SymbolEditorForm);
                sef.SymbolEditor.Offset = sef.SymbolEditor.ReverseTranslate(e.Location, false);
            }
        }

        private void btn_TextTool_Click(object sender, EventArgs e)
        {
            SetSymbolTool(TextTool.Instance);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            SetSymbolTool(PartDeletionTool.Instance);
        }

        private void btn_TerminalRight_Click(object sender, EventArgs e)
        {
            btn_TerminalRight.Checked = true;
            btn_TerminalLeft.Checked = false;
            btn_Terminal.Image = btn_TerminalRight.Image;
            SetSymbolTool(new PortTool(DirectionType.Output));
        }

        private void btn_TerminalLeft_Click(object sender, EventArgs e)
        {
            btn_TerminalRight.Checked = false;
            btn_TerminalLeft.Checked = true;
            btn_Terminal.Image = btn_TerminalLeft.Image;
            SetSymbolTool(new PortTool(DirectionType.Input));
        }

        private void btn_Terminal_ButtonClick(object sender, EventArgs e)
        {
            if (btn_TerminalRight.Checked)
            {
                SetSymbolTool(new PortTool(DirectionType.Output));
                return;
            }
            if (btn_TerminalLeft.Checked)
            {
                SetSymbolTool(new PortTool(DirectionType.Input));
                return;
            }
        }

        void partSelectionTool_OnPartSelected(object sender, PartSelectedEventArgs e)
        {
            if (propertyForm == null || propertyForm.IsDisposed)
            {
                propertyForm = new PropertyForm();
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                    propertyForm.MdiParent = this;
            }
            propertyForm.SetElement(e.Part);
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                propertyForm.Show();
            else
                propertyForm.Show(dockPanel);
        }

        #endregion

        private void dockPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("blubb");
        }


    }
}

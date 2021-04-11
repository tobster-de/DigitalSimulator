namespace SymbolEditor
{
    partial class SymbolEditorForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolEditorForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_NewSymbol = new System.Windows.Forms.ToolStripButton();
            this.btn_Save = new System.Windows.Forms.ToolStripButton();
            this.combo_SymbolName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Selection = new System.Windows.Forms.ToolStripButton();
            this.btn_Offset = new System.Windows.Forms.ToolStripButton();
            this.btn_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_LineTool = new System.Windows.Forms.ToolStripButton();
            this.btn_RectangleTool = new System.Windows.Forms.ToolStripButton();
            this.btn_Terminal = new System.Windows.Forms.ToolStripSplitButton();
            this.btn_TerminalRight = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_TerminalLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_TextTool = new System.Windows.Forms.ToolStripButton();
            this.symbolEditor = new DigitalClasses.Controls.SymbolEditor();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 305);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(428, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(413, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "StatusLabel";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_NewSymbol,
            this.btn_Save,
            this.combo_SymbolName,
            this.toolStripSeparator2,
            this.btn_Selection,
            this.btn_Offset,
            this.btn_Delete,
            this.toolStripSeparator1,
            this.btn_LineTool,
            this.btn_RectangleTool,
            this.btn_Terminal,
            this.btn_TextTool});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(428, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip";
            // 
            // btn_NewSymbol
            // 
            this.btn_NewSymbol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_NewSymbol.Image = ((System.Drawing.Image)(resources.GetObject("btn_NewSymbol.Image")));
            this.btn_NewSymbol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_NewSymbol.Name = "btn_NewSymbol";
            this.btn_NewSymbol.Size = new System.Drawing.Size(23, 22);
            this.btn_NewSymbol.Text = "&Neu";
            this.btn_NewSymbol.Click += new System.EventHandler(this.btn_NewSymbol_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(23, 22);
            this.btn_Save.Text = "&Speichern";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // combo_SymbolName
            // 
            this.combo_SymbolName.Name = "combo_SymbolName";
            this.combo_SymbolName.Size = new System.Drawing.Size(121, 25);
            this.combo_SymbolName.ToolTipText = "Name des Symbols";
            this.combo_SymbolName.SelectedIndexChanged += new System.EventHandler(this.combo_SymbolName_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_Selection
            // 
            this.btn_Selection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Selection.Image = global::SymbolEditor.Properties.Resources.Pointer;
            this.btn_Selection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Selection.Name = "btn_Selection";
            this.btn_Selection.Size = new System.Drawing.Size(23, 22);
            this.btn_Selection.Text = "Auswahl";
            this.btn_Selection.Click += new System.EventHandler(this.btn_Selection_Click);
            // 
            // btn_Offset
            // 
            this.btn_Offset.CheckOnClick = true;
            this.btn_Offset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Offset.Image = global::SymbolEditor.Properties.Resources.Offset;
            this.btn_Offset.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_Offset.Name = "btn_Offset";
            this.btn_Offset.Size = new System.Drawing.Size(23, 22);
            this.btn_Offset.Text = "&Offset versetzen";
            this.btn_Offset.ToolTipText = "Offset";
            this.btn_Offset.Click += new System.EventHandler(this.btn_Offset_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Delete.Image = global::SymbolEditor.Properties.Resources.DeleteAll;
            this.btn_Delete.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(23, 22);
            this.btn_Delete.Text = "Löschen";
            this.btn_Delete.Click += new System.EventHandler(this.btn_DeleteAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_LineTool
            // 
            this.btn_LineTool.CheckOnClick = true;
            this.btn_LineTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_LineTool.Image = global::SymbolEditor.Properties.Resources.LineTool;
            this.btn_LineTool.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_LineTool.MergeIndex = 0;
            this.btn_LineTool.Name = "btn_LineTool";
            this.btn_LineTool.Size = new System.Drawing.Size(23, 22);
            this.btn_LineTool.Text = "&Linie";
            this.btn_LineTool.ToolTipText = "Linie";
            this.btn_LineTool.Click += new System.EventHandler(this.btn_LineTool_Click);
            // 
            // btn_RectangleTool
            // 
            this.btn_RectangleTool.CheckOnClick = true;
            this.btn_RectangleTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_RectangleTool.Image = global::SymbolEditor.Properties.Resources.RectangleTool;
            this.btn_RectangleTool.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_RectangleTool.MergeIndex = 0;
            this.btn_RectangleTool.Name = "btn_RectangleTool";
            this.btn_RectangleTool.Size = new System.Drawing.Size(23, 22);
            this.btn_RectangleTool.Text = "&Rechteck";
            this.btn_RectangleTool.ToolTipText = "Rechteck";
            this.btn_RectangleTool.Click += new System.EventHandler(this.btn_RectangleTool_Click);
            // 
            // btn_Terminal
            // 
            this.btn_Terminal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Terminal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_TerminalRight,
            this.btn_TerminalLeft});
            this.btn_Terminal.Image = global::SymbolEditor.Properties.Resources.Terminal;
            this.btn_Terminal.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_Terminal.Name = "btn_Terminal";
            this.btn_Terminal.Size = new System.Drawing.Size(32, 22);
            this.btn_Terminal.Text = "Anschluss";
            this.btn_Terminal.ButtonClick += new System.EventHandler(this.btn_Terminal_ButtonClick);
            // 
            // btn_TerminalRight
            // 
            this.btn_TerminalRight.Checked = true;
            this.btn_TerminalRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btn_TerminalRight.Image = global::SymbolEditor.Properties.Resources.Terminal;
            this.btn_TerminalRight.Name = "btn_TerminalRight";
            this.btn_TerminalRight.Size = new System.Drawing.Size(165, 22);
            this.btn_TerminalRight.Text = "Anschluss Rechts";
            this.btn_TerminalRight.Click += new System.EventHandler(this.btn_TerminalRight_Click);
            // 
            // btn_TerminalLeft
            // 
            this.btn_TerminalLeft.Image = global::SymbolEditor.Properties.Resources.Terminal2;
            this.btn_TerminalLeft.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_TerminalLeft.Name = "btn_TerminalLeft";
            this.btn_TerminalLeft.Size = new System.Drawing.Size(165, 22);
            this.btn_TerminalLeft.Text = "Anschluss Links";
            this.btn_TerminalLeft.Click += new System.EventHandler(this.btn_TerminalLeft_Click);
            // 
            // btn_TextTool
            // 
            this.btn_TextTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_TextTool.Image = global::SymbolEditor.Properties.Resources.Text;
            this.btn_TextTool.ImageTransparentColor = System.Drawing.Color.White;
            this.btn_TextTool.Name = "btn_TextTool";
            this.btn_TextTool.Size = new System.Drawing.Size(23, 22);
            this.btn_TextTool.Text = "Text";
            this.btn_TextTool.Click += new System.EventHandler(this.btn_TextTool_Click);
            // 
            // symbolEditor
            // 
            this.symbolEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolEditor.BackColor = System.Drawing.Color.White;
            this.symbolEditor.CurrentTool = null;
            this.symbolEditor.GridColor = System.Drawing.Color.DarkGray;
            this.symbolEditor.GridSize = new System.Drawing.Point(4, 4);
            this.symbolEditor.Location = new System.Drawing.Point(12, 28);
            this.symbolEditor.Name = "symbolEditor";
            this.symbolEditor.Offset = new System.Drawing.Point(20, 20);
            this.symbolEditor.Size = new System.Drawing.Size(404, 274);
            this.symbolEditor.TabIndex = 0;
            this.symbolEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.symbolEditor_MouseMove);
            // 
            // SymbolEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 327);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.symbolEditor);
            this.Name = "SymbolEditorForm";
            this.Text = "SymbolEditor";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DigitalClasses.Controls.SymbolEditor symbolEditor;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_LineTool;
        private System.Windows.Forms.ToolStripButton btn_RectangleTool;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton btn_Offset;
        private System.Windows.Forms.ToolStripComboBox combo_SymbolName;
        private System.Windows.Forms.ToolStripButton btn_Delete;
        private System.Windows.Forms.ToolStripButton btn_NewSymbol;
        private System.Windows.Forms.ToolStripButton btn_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btn_TextTool;
        private System.Windows.Forms.ToolStripSplitButton btn_Terminal;
        private System.Windows.Forms.ToolStripMenuItem btn_TerminalRight;
        private System.Windows.Forms.ToolStripMenuItem btn_TerminalLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_Selection;
    }
}


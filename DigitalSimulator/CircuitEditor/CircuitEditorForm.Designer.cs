namespace CircuitEditor
{
    partial class CircuitEditorForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.panel_Scoll = new System.Windows.Forms.Panel();
            this.circuitEditor = new DigitalClasses.Controls.CircuitEditor();
            this.toolStrip2_Digital = new System.Windows.Forms.ToolStrip();
            this.btn_Select = new System.Windows.Forms.ToolStripButton();
            this.btn_Gates = new System.Windows.Forms.ToolStripSplitButton();
            this.menuItem_AND = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_NAND = new System.Windows.Forms.ToolStripMenuItem();
            this.seperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_OR = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_NOR = new System.Windows.Forms.ToolStripMenuItem();
            this.seperator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_NOT = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Signals = new System.Windows.Forms.ToolStripSplitButton();
            this.menuItem_Input = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Output = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Connection = new System.Windows.Forms.ToolStripButton();
            this.btn_Macro = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Step = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.panel_Scoll.SuspendLayout();
            this.toolStrip2_Digital.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 487);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(567, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.panel_Scoll);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(567, 462);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(567, 487);
            this.toolStripContainer.TabIndex = 4;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip2_Digital);
            // 
            // panel_Scoll
            // 
            this.panel_Scoll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Scoll.AutoScroll = true;
            this.panel_Scoll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Scoll.Controls.Add(this.circuitEditor);
            this.panel_Scoll.Location = new System.Drawing.Point(0, 0);
            this.panel_Scoll.Name = "panel_Scoll";
            this.panel_Scoll.Size = new System.Drawing.Size(567, 459);
            this.panel_Scoll.TabIndex = 1;
            // 
            // circuitEditor
            // 
            this.circuitEditor.BackColor = System.Drawing.Color.White;
            this.circuitEditor.CurrentTool = null;
            this.circuitEditor.GridColor = System.Drawing.Color.LightGray;
            this.circuitEditor.GridSize = new System.Drawing.Point(32, 32);
            this.circuitEditor.Location = new System.Drawing.Point(0, 0);
            this.circuitEditor.Name = "circuitEditor";
            this.circuitEditor.Size = new System.Drawing.Size(567, 459);
            this.circuitEditor.TabIndex = 0;
            // 
            // toolStrip2_Digital
            // 
            this.toolStrip2_Digital.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2_Digital.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Select,
            this.btn_Gates,
            this.btn_Signals,
            this.btn_Connection,
            this.btn_Macro});
            this.toolStrip2_Digital.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2_Digital.Name = "toolStrip2_Digital";
            this.toolStrip2_Digital.Size = new System.Drawing.Size(122, 25);
            this.toolStrip2_Digital.TabIndex = 3;
            // 
            // btn_Select
            // 
            this.btn_Select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Select.Image = global::CircuitEditor.Properties.Resources.Pointer;
            this.btn_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(23, 22);
            this.btn_Select.Text = "Auswahl";
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_Gates
            // 
            this.btn_Gates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Gates.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_AND,
            this.menuItem_NAND,
            this.seperator2,
            this.menuItem_OR,
            this.menuItem_NOR,
            this.seperator3,
            this.menuItem_NOT});
            this.btn_Gates.Image = global::CircuitEditor.Properties.Resources.Gate;
            this.btn_Gates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Gates.MergeIndex = 1;
            this.btn_Gates.Name = "btn_Gates";
            this.btn_Gates.Size = new System.Drawing.Size(32, 22);
            this.btn_Gates.Text = "Gatter";
            // 
            // menuItem_AND
            // 
            this.menuItem_AND.CheckOnClick = true;
            this.menuItem_AND.Image = global::CircuitEditor.Properties.Resources.And;
            this.menuItem_AND.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItem_AND.MergeIndex = 1;
            this.menuItem_AND.Name = "menuItem_AND";
            this.menuItem_AND.Size = new System.Drawing.Size(108, 22);
            this.menuItem_AND.Text = "AND";
            this.menuItem_AND.Click += new System.EventHandler(this.menuItem_AND_Click);
            // 
            // menuItem_NAND
            // 
            this.menuItem_NAND.CheckOnClick = true;
            this.menuItem_NAND.Image = global::CircuitEditor.Properties.Resources.Nand;
            this.menuItem_NAND.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.menuItem_NAND.MergeIndex = 1;
            this.menuItem_NAND.Name = "menuItem_NAND";
            this.menuItem_NAND.Size = new System.Drawing.Size(108, 22);
            this.menuItem_NAND.Text = "NAND";
            this.menuItem_NAND.Click += new System.EventHandler(this.menuItem_NAND_Click);
            // 
            // seperator2
            // 
            this.seperator2.Name = "seperator2";
            this.seperator2.Size = new System.Drawing.Size(105, 6);
            // 
            // menuItem_OR
            // 
            this.menuItem_OR.CheckOnClick = true;
            this.menuItem_OR.Image = global::CircuitEditor.Properties.Resources.Or;
            this.menuItem_OR.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.menuItem_OR.MergeIndex = 1;
            this.menuItem_OR.Name = "menuItem_OR";
            this.menuItem_OR.Size = new System.Drawing.Size(108, 22);
            this.menuItem_OR.Text = "OR";
            this.menuItem_OR.Click += new System.EventHandler(this.menuItem_OR_Click);
            // 
            // menuItem_NOR
            // 
            this.menuItem_NOR.CheckOnClick = true;
            this.menuItem_NOR.Image = global::CircuitEditor.Properties.Resources.Nor;
            this.menuItem_NOR.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.menuItem_NOR.MergeIndex = 1;
            this.menuItem_NOR.Name = "menuItem_NOR";
            this.menuItem_NOR.Size = new System.Drawing.Size(108, 22);
            this.menuItem_NOR.Text = "NOR";
            this.menuItem_NOR.Click += new System.EventHandler(this.menuItem_NOR_Click);
            // 
            // seperator3
            // 
            this.seperator3.Name = "seperator3";
            this.seperator3.Size = new System.Drawing.Size(105, 6);
            // 
            // menuItem_NOT
            // 
            this.menuItem_NOT.CheckOnClick = true;
            this.menuItem_NOT.Image = global::CircuitEditor.Properties.Resources.Not;
            this.menuItem_NOT.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.menuItem_NOT.MergeIndex = 1;
            this.menuItem_NOT.Name = "menuItem_NOT";
            this.menuItem_NOT.Size = new System.Drawing.Size(108, 22);
            this.menuItem_NOT.Text = "NOT";
            this.menuItem_NOT.Click += new System.EventHandler(this.menuItem_NOT_Click);
            // 
            // btn_Signals
            // 
            this.btn_Signals.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Signals.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Input,
            this.menuItem_Output});
            this.btn_Signals.Image = global::CircuitEditor.Properties.Resources.SignalIn;
            this.btn_Signals.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Signals.Name = "btn_Signals";
            this.btn_Signals.Size = new System.Drawing.Size(32, 22);
            this.btn_Signals.Text = "Signale";
            // 
            // menuItem_Input
            // 
            this.menuItem_Input.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuItem_Input.Image = global::CircuitEditor.Properties.Resources.SignalIn;
            this.menuItem_Input.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItem_Input.Name = "menuItem_Input";
            this.menuItem_Input.Size = new System.Drawing.Size(121, 22);
            this.menuItem_Input.Text = "Eingang";
            this.menuItem_Input.Click += new System.EventHandler(this.menuItem_Input_Click);
            // 
            // menuItem_Output
            // 
            this.menuItem_Output.Image = global::CircuitEditor.Properties.Resources.SignalOut;
            this.menuItem_Output.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItem_Output.Name = "menuItem_Output";
            this.menuItem_Output.Size = new System.Drawing.Size(121, 22);
            this.menuItem_Output.Text = "Ausgang";
            this.menuItem_Output.Click += new System.EventHandler(this.menuItem_Output_Click);
            // 
            // btn_Connection
            // 
            this.btn_Connection.CheckOnClick = true;
            this.btn_Connection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Connection.Image = global::CircuitEditor.Properties.Resources.Connection;
            this.btn_Connection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Connection.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.btn_Connection.MergeIndex = 2;
            this.btn_Connection.Name = "btn_Connection";
            this.btn_Connection.Size = new System.Drawing.Size(23, 22);
            this.btn_Connection.Text = "Verbindung";
            this.btn_Connection.Click += new System.EventHandler(this.btn_Connection_Click);
            // 
            // btn_Macro
            // 
            this.btn_Macro.CheckOnClick = true;
            this.btn_Macro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Macro.Image = global::CircuitEditor.Properties.Resources.Circuit;
            this.btn_Macro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Macro.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.btn_Macro.MergeIndex = 2;
            this.btn_Macro.Name = "btn_Macro";
            this.btn_Macro.Size = new System.Drawing.Size(23, 22);
            this.btn_Macro.Text = "Schaltungen";
            this.btn_Macro.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Step});
            this.toolStrip1.Location = new System.Drawing.Point(125, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 4;
            // 
            // btn_Step
            // 
            this.btn_Step.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Step.Image = global::CircuitEditor.Properties.Resources.Step;
            this.btn_Step.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Step.Name = "btn_Step";
            this.btn_Step.Size = new System.Drawing.Size(23, 22);
            this.btn_Step.Text = "Einzelschritt";
            this.btn_Step.Click += new System.EventHandler(this.btn_Step_Click);
            // 
            // CircuitEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 509);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.statusStrip);
            this.Name = "CircuitEditorForm";
            this.Text = "Schaltung";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.panel_Scoll.ResumeLayout(false);
            this.toolStrip2_Digital.ResumeLayout(false);
            this.toolStrip2_Digital.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DigitalClasses.Controls.CircuitEditor circuitEditor;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.Panel panel_Scoll;
        private System.Windows.Forms.ToolStrip toolStrip2_Digital;
        private System.Windows.Forms.ToolStripButton btn_Macro;
        private System.Windows.Forms.ToolStripButton btn_Connection;
        private System.Windows.Forms.ToolStripSplitButton btn_Gates;
        private System.Windows.Forms.ToolStripMenuItem menuItem_AND;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NAND;
        private System.Windows.Forms.ToolStripSeparator seperator2;
        private System.Windows.Forms.ToolStripMenuItem menuItem_OR;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NOR;
        private System.Windows.Forms.ToolStripSeparator seperator3;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NOT;
        private System.Windows.Forms.ToolStripSplitButton btn_Signals;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Input;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Output;
        private System.Windows.Forms.ToolStripButton btn_Select;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_Step;


    }
}


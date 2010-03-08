namespace DigitalSimulator
{
    partial class MacroCreatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroCreatorForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Eingänge", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Ausgänge", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Eingänge", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Ausgänge", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Eingänge", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Ausgänge", System.Windows.Forms.HorizontalAlignment.Left);
            this.panel_Sources = new System.Windows.Forms.Panel();
            this.button_ProceedToPage2 = new System.Windows.Forms.Button();
            this.button_OpenSymbol = new System.Windows.Forms.Button();
            this.textBox_Symbol = new System.Windows.Forms.TextBox();
            this.label_Symbol1 = new System.Windows.Forms.Label();
            this.button_OpenCircuit = new System.Windows.Forms.Button();
            this.textBox_Circuit = new System.Windows.Forms.TextBox();
            this.label_Circuit1 = new System.Windows.Forms.Label();
            this.label_Page1 = new System.Windows.Forms.Label();
            this.panel_Matching = new System.Windows.Forms.Panel();
            this.label_MacroName = new System.Windows.Forms.Label();
            this.textBox_MacroName = new System.Windows.Forms.TextBox();
            this.label_Macro = new System.Windows.Forms.Label();
            this.button_Remove = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.listView_Matchings = new System.Windows.Forms.ListView();
            this.header_Circuit = new System.Windows.Forms.ColumnHeader();
            this.header_Symbol = new System.Windows.Forms.ColumnHeader();
            this.listView_SymbolPorts = new System.Windows.Forms.ListView();
            this.header_PortName = new System.Windows.Forms.ColumnHeader();
            this.listView_CircuitIOs = new System.Windows.Forms.ListView();
            this.header_IOName = new System.Windows.Forms.ColumnHeader();
            this.button_BackToPage1 = new System.Windows.Forms.Button();
            this.button_Finish = new System.Windows.Forms.Button();
            this.label_Symbol2 = new System.Windows.Forms.Label();
            this.label_Circuit2 = new System.Windows.Forms.Label();
            this.label_Page2 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel_Sources.SuspendLayout();
            this.panel_Matching.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Sources
            // 
            this.panel_Sources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Sources.Controls.Add(this.button_ProceedToPage2);
            this.panel_Sources.Controls.Add(this.button_OpenSymbol);
            this.panel_Sources.Controls.Add(this.textBox_Symbol);
            this.panel_Sources.Controls.Add(this.label_Symbol1);
            this.panel_Sources.Controls.Add(this.button_OpenCircuit);
            this.panel_Sources.Controls.Add(this.textBox_Circuit);
            this.panel_Sources.Controls.Add(this.label_Circuit1);
            this.panel_Sources.Controls.Add(this.label_Page1);
            this.panel_Sources.Location = new System.Drawing.Point(7, 8);
            this.panel_Sources.Name = "panel_Sources";
            this.panel_Sources.Size = new System.Drawing.Size(580, 367);
            this.panel_Sources.TabIndex = 0;
            // 
            // button_ProceedToPage2
            // 
            this.button_ProceedToPage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ProceedToPage2.Enabled = false;
            this.button_ProceedToPage2.Location = new System.Drawing.Point(484, 339);
            this.button_ProceedToPage2.Margin = new System.Windows.Forms.Padding(3, 3, 8, 5);
            this.button_ProceedToPage2.Name = "button_ProceedToPage2";
            this.button_ProceedToPage2.Size = new System.Drawing.Size(88, 23);
            this.button_ProceedToPage2.TabIndex = 8;
            this.button_ProceedToPage2.Text = "Weiter";
            this.button_ProceedToPage2.UseVisualStyleBackColor = true;
            this.button_ProceedToPage2.Click += new System.EventHandler(this.ProceedToPage2);
            // 
            // button_OpenSymbol
            // 
            this.button_OpenSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OpenSymbol.Location = new System.Drawing.Point(484, 122);
            this.button_OpenSymbol.Margin = new System.Windows.Forms.Padding(5, 3, 8, 3);
            this.button_OpenSymbol.Name = "button_OpenSymbol";
            this.button_OpenSymbol.Size = new System.Drawing.Size(88, 20);
            this.button_OpenSymbol.TabIndex = 7;
            this.button_OpenSymbol.Text = "Wählen";
            this.button_OpenSymbol.UseVisualStyleBackColor = true;
            this.button_OpenSymbol.Click += new System.EventHandler(this.OpenSymbol);
            // 
            // textBox_Symbol
            // 
            this.textBox_Symbol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Symbol.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Symbol.Location = new System.Drawing.Point(8, 123);
            this.textBox_Symbol.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.textBox_Symbol.Name = "textBox_Symbol";
            this.textBox_Symbol.ReadOnly = true;
            this.textBox_Symbol.Size = new System.Drawing.Size(468, 20);
            this.textBox_Symbol.TabIndex = 6;
            // 
            // label_Symbol1
            // 
            this.label_Symbol1.AutoSize = true;
            this.label_Symbol1.Location = new System.Drawing.Point(5, 107);
            this.label_Symbol1.Name = "label_Symbol1";
            this.label_Symbol1.Size = new System.Drawing.Size(44, 13);
            this.label_Symbol1.TabIndex = 5;
            this.label_Symbol1.Text = "Symbol:";
            // 
            // button_OpenCircuit
            // 
            this.button_OpenCircuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OpenCircuit.Location = new System.Drawing.Point(484, 74);
            this.button_OpenCircuit.Margin = new System.Windows.Forms.Padding(5, 3, 8, 3);
            this.button_OpenCircuit.Name = "button_OpenCircuit";
            this.button_OpenCircuit.Size = new System.Drawing.Size(88, 20);
            this.button_OpenCircuit.TabIndex = 4;
            this.button_OpenCircuit.Text = "Wählen";
            this.button_OpenCircuit.UseVisualStyleBackColor = true;
            this.button_OpenCircuit.Click += new System.EventHandler(this.OpenCircuit);
            // 
            // textBox_Circuit
            // 
            this.textBox_Circuit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Circuit.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Circuit.Location = new System.Drawing.Point(8, 74);
            this.textBox_Circuit.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.textBox_Circuit.Name = "textBox_Circuit";
            this.textBox_Circuit.ReadOnly = true;
            this.textBox_Circuit.Size = new System.Drawing.Size(468, 20);
            this.textBox_Circuit.TabIndex = 3;
            // 
            // label_Circuit1
            // 
            this.label_Circuit1.AutoSize = true;
            this.label_Circuit1.Location = new System.Drawing.Point(5, 58);
            this.label_Circuit1.Name = "label_Circuit1";
            this.label_Circuit1.Size = new System.Drawing.Size(58, 13);
            this.label_Circuit1.TabIndex = 2;
            this.label_Circuit1.Text = "Schaltung:";
            // 
            // label_Page1
            // 
            this.label_Page1.AutoSize = true;
            this.label_Page1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Page1.Location = new System.Drawing.Point(3, 10);
            this.label_Page1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label_Page1.Name = "label_Page1";
            this.label_Page1.Size = new System.Drawing.Size(201, 26);
            this.label_Page1.TabIndex = 1;
            this.label_Page1.Text = "Quellen bestimmen";
            // 
            // panel_Matching
            // 
            this.panel_Matching.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Matching.Controls.Add(this.label_MacroName);
            this.panel_Matching.Controls.Add(this.textBox_MacroName);
            this.panel_Matching.Controls.Add(this.label_Macro);
            this.panel_Matching.Controls.Add(this.button_Remove);
            this.panel_Matching.Controls.Add(this.button_Add);
            this.panel_Matching.Controls.Add(this.listView_Matchings);
            this.panel_Matching.Controls.Add(this.listView_SymbolPorts);
            this.panel_Matching.Controls.Add(this.listView_CircuitIOs);
            this.panel_Matching.Controls.Add(this.button_BackToPage1);
            this.panel_Matching.Controls.Add(this.button_Finish);
            this.panel_Matching.Controls.Add(this.label_Symbol2);
            this.panel_Matching.Controls.Add(this.label_Circuit2);
            this.panel_Matching.Controls.Add(this.label_Page2);
            this.panel_Matching.Location = new System.Drawing.Point(7, 8);
            this.panel_Matching.MinimumSize = new System.Drawing.Size(580, 300);
            this.panel_Matching.Name = "panel_Matching";
            this.panel_Matching.Size = new System.Drawing.Size(580, 367);
            this.panel_Matching.TabIndex = 1;
            this.panel_Matching.Visible = false;
            // 
            // label_MacroName
            // 
            this.label_MacroName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_MacroName.AutoSize = true;
            this.label_MacroName.Location = new System.Drawing.Point(5, 299);
            this.label_MacroName.Name = "label_MacroName";
            this.label_MacroName.Size = new System.Drawing.Size(96, 13);
            this.label_MacroName.TabIndex = 18;
            this.label_MacroName.Text = "Name des Makros:";
            // 
            // textBox_MacroName
            // 
            this.textBox_MacroName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_MacroName.Location = new System.Drawing.Point(8, 315);
            this.textBox_MacroName.Name = "textBox_MacroName";
            this.textBox_MacroName.Size = new System.Drawing.Size(320, 20);
            this.textBox_MacroName.TabIndex = 17;
            this.textBox_MacroName.TextChanged += new System.EventHandler(this.textBox_MacroName_TextChanged);
            // 
            // label_Macro
            // 
            this.label_Macro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Macro.AutoSize = true;
            this.label_Macro.Location = new System.Drawing.Point(375, 56);
            this.label_Macro.Name = "label_Macro";
            this.label_Macro.Size = new System.Drawing.Size(40, 13);
            this.label_Macro.TabIndex = 15;
            this.label_Macro.Text = "Makro:";
            // 
            // button_Remove
            // 
            this.button_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Remove.Image = ((System.Drawing.Image)(resources.GetObject("button_Remove.Image")));
            this.button_Remove.Location = new System.Drawing.Point(336, 189);
            this.button_Remove.Margin = new System.Windows.Forms.Padding(5);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(30, 30);
            this.button_Remove.TabIndex = 13;
            this.toolTip.SetToolTip(this.button_Remove, "Entfernen");
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.RemoveMatching);
            // 
            // button_Add
            // 
            this.button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Add.Image = ((System.Drawing.Image)(resources.GetObject("button_Add.Image")));
            this.button_Add.Location = new System.Drawing.Point(336, 149);
            this.button_Add.Margin = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(30, 30);
            this.button_Add.TabIndex = 12;
            this.toolTip.SetToolTip(this.button_Add, "Hinzufügen");
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.AddMatching);
            // 
            // listView_Matchings
            // 
            this.listView_Matchings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Matchings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header_Circuit,
            this.header_Symbol});
            this.listView_Matchings.FullRowSelect = true;
            listViewGroup1.Header = "Eingänge";
            listViewGroup1.Name = "group_Inputs";
            listViewGroup2.Header = "Ausgänge";
            listViewGroup2.Name = "group_Outputs";
            this.listView_Matchings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listView_Matchings.HideSelection = false;
            this.listView_Matchings.Location = new System.Drawing.Point(376, 74);
            this.listView_Matchings.Margin = new System.Windows.Forms.Padding(5, 3, 8, 20);
            this.listView_Matchings.MultiSelect = false;
            this.listView_Matchings.Name = "listView_Matchings";
            this.listView_Matchings.Size = new System.Drawing.Size(196, 222);
            this.listView_Matchings.TabIndex = 14;
            this.toolTip.SetToolTip(this.listView_Matchings, "Zuordnungen von Ein- und Ausgängen zu Anschlüssen");
            this.listView_Matchings.UseCompatibleStateImageBehavior = false;
            this.listView_Matchings.View = System.Windows.Forms.View.Details;
            this.listView_Matchings.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.MatchingSelected);
            // 
            // header_Circuit
            // 
            this.header_Circuit.Text = "Schaltung";
            this.header_Circuit.Width = 90;
            // 
            // header_Symbol
            // 
            this.header_Symbol.Text = "Symbol";
            this.header_Symbol.Width = 90;
            // 
            // listView_SymbolPorts
            // 
            this.listView_SymbolPorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_SymbolPorts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header_PortName});
            this.listView_SymbolPorts.FullRowSelect = true;
            listViewGroup3.Header = "Eingänge";
            listViewGroup3.Name = "group_Inputs";
            listViewGroup4.Header = "Ausgänge";
            listViewGroup4.Name = "group_Outputs";
            this.listView_SymbolPorts.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.listView_SymbolPorts.HideSelection = false;
            this.listView_SymbolPorts.Location = new System.Drawing.Point(173, 74);
            this.listView_SymbolPorts.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.listView_SymbolPorts.MultiSelect = false;
            this.listView_SymbolPorts.Name = "listView_SymbolPorts";
            this.listView_SymbolPorts.Size = new System.Drawing.Size(155, 222);
            this.listView_SymbolPorts.TabIndex = 11;
            this.toolTip.SetToolTip(this.listView_SymbolPorts, "Anschlüsse im Symbol");
            this.listView_SymbolPorts.UseCompatibleStateImageBehavior = false;
            this.listView_SymbolPorts.View = System.Windows.Forms.View.Details;
            this.listView_SymbolPorts.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.PortSelected);
            // 
            // header_PortName
            // 
            this.header_PortName.Text = "Name";
            this.header_PortName.Width = 130;
            // 
            // listView_CircuitIOs
            // 
            this.listView_CircuitIOs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_CircuitIOs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header_IOName});
            this.listView_CircuitIOs.FullRowSelect = true;
            listViewGroup5.Header = "Eingänge";
            listViewGroup5.Name = "group_Inputs";
            listViewGroup6.Header = "Ausgänge";
            listViewGroup6.Name = "group_Outputs";
            this.listView_CircuitIOs.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6});
            this.listView_CircuitIOs.HideSelection = false;
            this.listView_CircuitIOs.Location = new System.Drawing.Point(8, 74);
            this.listView_CircuitIOs.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.listView_CircuitIOs.MultiSelect = false;
            this.listView_CircuitIOs.Name = "listView_CircuitIOs";
            this.listView_CircuitIOs.Size = new System.Drawing.Size(155, 222);
            this.listView_CircuitIOs.TabIndex = 10;
            this.toolTip.SetToolTip(this.listView_CircuitIOs, "Ein- und Ausgänge innerhalb der Schaltung");
            this.listView_CircuitIOs.UseCompatibleStateImageBehavior = false;
            this.listView_CircuitIOs.View = System.Windows.Forms.View.Details;
            this.listView_CircuitIOs.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.PortSelected);
            // 
            // header_IOName
            // 
            this.header_IOName.Text = "Name";
            this.header_IOName.Width = 130;
            // 
            // button_BackToPage1
            // 
            this.button_BackToPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_BackToPage1.Location = new System.Drawing.Point(388, 339);
            this.button_BackToPage1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.button_BackToPage1.Name = "button_BackToPage1";
            this.button_BackToPage1.Size = new System.Drawing.Size(88, 23);
            this.button_BackToPage1.TabIndex = 15;
            this.button_BackToPage1.Text = "Zurück";
            this.toolTip.SetToolTip(this.button_BackToPage1, "Zurück zur Auswahl");
            this.button_BackToPage1.UseVisualStyleBackColor = true;
            this.button_BackToPage1.Click += new System.EventHandler(this.BackToPage1);
            // 
            // button_Finish
            // 
            this.button_Finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Finish.Location = new System.Drawing.Point(484, 339);
            this.button_Finish.Margin = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.button_Finish.Name = "button_Finish";
            this.button_Finish.Size = new System.Drawing.Size(88, 23);
            this.button_Finish.TabIndex = 16;
            this.button_Finish.Text = "Fertigstellen";
            this.toolTip.SetToolTip(this.button_Finish, "Makro fertigstellen und speichern");
            this.button_Finish.UseVisualStyleBackColor = true;
            this.button_Finish.Click += new System.EventHandler(this.SaveMacro);
            // 
            // label_Symbol2
            // 
            this.label_Symbol2.AutoSize = true;
            this.label_Symbol2.Location = new System.Drawing.Point(170, 56);
            this.label_Symbol2.Name = "label_Symbol2";
            this.label_Symbol2.Size = new System.Drawing.Size(44, 13);
            this.label_Symbol2.TabIndex = 5;
            this.label_Symbol2.Text = "Symbol:";
            // 
            // label_Circuit2
            // 
            this.label_Circuit2.AutoSize = true;
            this.label_Circuit2.Location = new System.Drawing.Point(5, 56);
            this.label_Circuit2.Name = "label_Circuit2";
            this.label_Circuit2.Size = new System.Drawing.Size(58, 13);
            this.label_Circuit2.TabIndex = 2;
            this.label_Circuit2.Text = "Schaltung:";
            // 
            // label_Page2
            // 
            this.label_Page2.AutoSize = true;
            this.label_Page2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Page2.Location = new System.Drawing.Point(3, 10);
            this.label_Page2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 20);
            this.label_Page2.Name = "label_Page2";
            this.label_Page2.Size = new System.Drawing.Size(365, 26);
            this.label_Page2.TabIndex = 1;
            this.label_Page2.Text = "Zuordnung der Anschlüsse festlegen";
            // 
            // MacroCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(590, 320);
            this.ClientSize = new System.Drawing.Size(595, 383);
            this.Controls.Add(this.panel_Sources);
            this.Controls.Add(this.panel_Matching);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MacroCreatorForm";
            this.Text = "MakroForm";
            this.panel_Sources.ResumeLayout(false);
            this.panel_Sources.PerformLayout();
            this.panel_Matching.ResumeLayout(false);
            this.panel_Matching.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Sources;
        private System.Windows.Forms.Label label_Circuit1;
        private System.Windows.Forms.Label label_Page1;
        private System.Windows.Forms.Button button_OpenSymbol;
        private System.Windows.Forms.TextBox textBox_Symbol;
        private System.Windows.Forms.Label label_Symbol1;
        private System.Windows.Forms.Button button_OpenCircuit;
        private System.Windows.Forms.TextBox textBox_Circuit;
        private System.Windows.Forms.Button button_ProceedToPage2;
        private System.Windows.Forms.Panel panel_Matching;
        private System.Windows.Forms.Button button_Finish;
        private System.Windows.Forms.Label label_Page2;
        private System.Windows.Forms.ListView listView_Matchings;
        private System.Windows.Forms.ListView listView_SymbolPorts;
        private System.Windows.Forms.ListView listView_CircuitIOs;
        private System.Windows.Forms.ColumnHeader header_IOName;
        private System.Windows.Forms.Button button_BackToPage1;
        private System.Windows.Forms.Label label_Symbol2;
        private System.Windows.Forms.Label label_Circuit2;
        private System.Windows.Forms.ColumnHeader header_Circuit;
        private System.Windows.Forms.ColumnHeader header_Symbol;
        private System.Windows.Forms.ColumnHeader header_PortName;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label_Macro;
        private System.Windows.Forms.Label label_MacroName;
        private System.Windows.Forms.TextBox textBox_MacroName;
    }
}
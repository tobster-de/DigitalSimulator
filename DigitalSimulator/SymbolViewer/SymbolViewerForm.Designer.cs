namespace SymbolViewer
{
    partial class SymbolViewerForm
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
            System.Drawing.Drawing2D.GraphicsPath graphicsPath1 = new System.Drawing.Drawing2D.GraphicsPath();
            DigitalClasses.Graphic.GraphicShape graphicShape1 = new DigitalClasses.Graphic.GraphicShape();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolViewerForm));
            this.symbolView1 = new DigitalClasses.Controls.SymbolView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.combo_SymbolName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // symbolView1
            // 
            this.symbolView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolView1.BackColor = System.Drawing.Color.White;
            this.symbolView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.symbolView1.Location = new System.Drawing.Point(12, 28);
            this.symbolView1.Name = "symbolView1";
            graphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.symbolView1.Path = graphicsPath1;
            graphicShape1.Angle = 0F;
            graphicShape1.Location = ((System.Drawing.PointF)(resources.GetObject("graphicShape1.Location")));
            this.symbolView1.Shape = graphicShape1;
            this.symbolView1.Size = new System.Drawing.Size(397, 246);
            this.symbolView1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combo_SymbolName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(421, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip";
            // 
            // combo_SymbolName
            // 
            this.combo_SymbolName.Name = "combo_SymbolName";
            this.combo_SymbolName.Size = new System.Drawing.Size(121, 25);
            this.combo_SymbolName.ToolTipText = "Name des Symbols";
            this.combo_SymbolName.SelectedIndexChanged += new System.EventHandler(this.combo_SymbolName_SelectedIndexChanged);
            // 
            // SymbolViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 286);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.symbolView1);
            this.Name = "SymbolViewerForm";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DigitalClasses.Controls.SymbolView symbolView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox combo_SymbolName;
    }
}


namespace DigitalClasses.Controls
{
    partial class SignalPanel
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignalPanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Add = new System.Windows.Forms.ToolStripButton();
            this.btn_Remove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_ScaleDown = new System.Windows.Forms.ToolStripButton();
            this.btn_ScaleUp = new System.Windows.Forms.ToolStripButton();
            this.panel = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Add,
            this.btn_Remove,
            this.toolStripSeparator1,
            this.btn_ScaleDown,
            this.btn_ScaleUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(214, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_Add
            // 
            this.btn_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(23, 22);
            this.btn_Add.Text = "Impuls hinzufügen";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Remove.Image = ((System.Drawing.Image)(resources.GetObject("btn_Remove.Image")));
            this.btn_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(23, 22);
            this.btn_Remove.Text = "Impuls entfernen";
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_ScaleDown
            // 
            this.btn_ScaleDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ScaleDown.Image = ((System.Drawing.Image)(resources.GetObject("btn_ScaleDown.Image")));
            this.btn_ScaleDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ScaleDown.Name = "btn_ScaleDown";
            this.btn_ScaleDown.Size = new System.Drawing.Size(23, 22);
            this.btn_ScaleDown.Text = "Skala verkleinern";
            this.btn_ScaleDown.Click += new System.EventHandler(this.btn_ScaleDown_Click);
            // 
            // btn_ScaleUp
            // 
            this.btn_ScaleUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ScaleUp.Image = ((System.Drawing.Image)(resources.GetObject("btn_ScaleUp.Image")));
            this.btn_ScaleUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ScaleUp.Name = "btn_ScaleUp";
            this.btn_ScaleUp.Size = new System.Drawing.Size(23, 22);
            this.btn_ScaleUp.Text = "Skala vergößern";
            this.btn_ScaleUp.Click += new System.EventHandler(this.btn_ScaleUp_Click);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(214, 255);
            this.panel.TabIndex = 1;
            // 
            // SignalPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SignalPanel";
            this.Size = new System.Drawing.Size(214, 280);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_Add;
        private System.Windows.Forms.ToolStripButton btn_Remove;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_ScaleDown;
        private System.Windows.Forms.ToolStripButton btn_ScaleUp;
    }
}

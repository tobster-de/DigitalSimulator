namespace DigitalClasses.Controls
{
    partial class TextInputForm
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.upDown_FontSize = new System.Windows.Forms.NumericUpDown();
            this.label_FontSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.upDown_FontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(4, 4);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(191, 20);
            this.textBox.TabIndex = 0;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(120, 30);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // upDown_FontSize
            // 
            this.upDown_FontSize.Enabled = false;
            this.upDown_FontSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.upDown_FontSize.Location = new System.Drawing.Point(46, 30);
            this.upDown_FontSize.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.upDown_FontSize.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.upDown_FontSize.Name = "upDown_FontSize";
            this.upDown_FontSize.Size = new System.Drawing.Size(53, 20);
            this.upDown_FontSize.TabIndex = 2;
            this.upDown_FontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label_FontSize
            // 
            this.label_FontSize.AutoSize = true;
            this.label_FontSize.Location = new System.Drawing.Point(1, 32);
            this.label_FontSize.Name = "label_FontSize";
            this.label_FontSize.Size = new System.Drawing.Size(39, 13);
            this.label_FontSize.TabIndex = 3;
            this.label_FontSize.Text = "Größe:";
            // 
            // TextInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(198, 57);
            this.Controls.Add(this.label_FontSize);
            this.Controls.Add(this.upDown_FontSize);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TextInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Text";
            this.VisibleChanged += new System.EventHandler(this.TextInputForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.upDown_FontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.NumericUpDown upDown_FontSize;
        private System.Windows.Forms.Label label_FontSize;
    }
}
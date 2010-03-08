namespace DigitalSimulator
{
    partial class MacroForm
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
            this.listView_Macros = new System.Windows.Forms.ListView();
            this.imageList_Symbols = new System.Windows.Forms.ImageList(this.components);
            this.button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_Macros
            // 
            this.listView_Macros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Macros.LargeImageList = this.imageList_Symbols;
            this.listView_Macros.Location = new System.Drawing.Point(9, 9);
            this.listView_Macros.MultiSelect = false;
            this.listView_Macros.Name = "listView_Macros";
            this.listView_Macros.Size = new System.Drawing.Size(447, 299);
            this.listView_Macros.SmallImageList = this.imageList_Symbols;
            this.listView_Macros.TabIndex = 0;
            this.listView_Macros.UseCompatibleStateImageBehavior = false;
            this.listView_Macros.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // imageList_Symbols
            // 
            this.imageList_Symbols.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList_Symbols.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList_Symbols.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(9, 314);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(447, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // MacroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 346);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.listView_Macros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MacroForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Makro wählen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_Macros;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ImageList imageList_Symbols;
    }
}
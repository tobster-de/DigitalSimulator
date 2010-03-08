namespace DigitalSimulator
{
    partial class SymbolEditorForm
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
            DigitalClasses.Graphic.Symbols.Symbol symbol2 = new DigitalClasses.Graphic.Symbols.Symbol();
            this.symbolEditor = new DigitalClasses.Controls.SymbolEditor();
            this.SuspendLayout();
            // 
            // symbolEditor
            // 
            this.symbolEditor.BackColor = System.Drawing.Color.White;
            this.symbolEditor.CurrentTool = null;
            this.symbolEditor.GridColor = System.Drawing.Color.DarkGray;
            this.symbolEditor.GridSize = new System.Drawing.Point(4, 4);
            this.symbolEditor.Location = new System.Drawing.Point(0, 0);
            this.symbolEditor.Name = "symbolEditor";
            this.symbolEditor.Offset = new System.Drawing.Point(20, 20);
            this.symbolEditor.Size = new System.Drawing.Size(573, 380);
            symbol2.Name = null;
            this.symbolEditor.Symbol = symbol2;
            this.symbolEditor.TabIndex = 3;
            this.symbolEditor.SymbolChanged += new DigitalClasses.Events.NotifyEvent(this.symbolEditor_SymbolChanged);
            // 
            // SymbolEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(573, 380);
            this.Controls.Add(this.symbolEditor);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SymbolEditorForm";
            this.Text = "Symbol";
            this.SizeChanged += new System.EventHandler(this.HandleSizeChanged);
            this.Shown += new System.EventHandler(this.HandleSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SymbolEditorForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private DigitalClasses.Controls.SymbolEditor symbolEditor;

    }
}
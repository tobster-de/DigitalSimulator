namespace DigitalSimulator
{
    partial class SimulationControlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationControlForm));
            this.toolStrip_Simulation = new System.Windows.Forms.ToolStrip();
            this.btn_Start = new System.Windows.Forms.ToolStripButton();
            this.btn_Stop = new System.Windows.Forms.ToolStripButton();
            this.btn_Pause = new System.Windows.Forms.ToolStripButton();
            this.btn_Step = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_ProcessRate = new System.Windows.Forms.ToolStripLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.checkBox_LoopSim = new System.Windows.Forms.CheckBox();
            this.label_Interval = new System.Windows.Forms.Label();
            this.label_IntervalValue = new System.Windows.Forms.Label();
            this.trackBar_Interval = new System.Windows.Forms.TrackBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip_Simulation.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Interval)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip_Simulation
            // 
            this.toolStrip_Simulation.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip_Simulation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Start,
            this.btn_Stop,
            this.btn_Pause,
            this.btn_Step,
            this.toolStripSeparator1,
            this.toolStripLabel_ProcessRate});
            this.toolStrip_Simulation.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip_Simulation.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_Simulation.Name = "toolStrip_Simulation";
            this.toolStrip_Simulation.Size = new System.Drawing.Size(203, 23);
            this.toolStrip_Simulation.Stretch = true;
            this.toolStrip_Simulation.TabIndex = 6;
            this.toolStrip_Simulation.Text = "Simulation";
            // 
            // btn_Start
            // 
            this.btn_Start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Start.Enabled = false;
            this.btn_Start.Image = ((System.Drawing.Image)(resources.GetObject("btn_Start.Image")));
            this.btn_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(23, 20);
            this.btn_Start.Text = "Start";
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Stop.Enabled = false;
            this.btn_Stop.Image = global::DigitalSimulator.Properties.Resources.Stop;
            this.btn_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(23, 20);
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Pause
            // 
            this.btn_Pause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Pause.Enabled = false;
            this.btn_Pause.Image = ((System.Drawing.Image)(resources.GetObject("btn_Pause.Image")));
            this.btn_Pause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(23, 20);
            this.btn_Pause.Text = "Pause";
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // btn_Step
            // 
            this.btn_Step.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Step.Enabled = false;
            this.btn_Step.Image = global::DigitalSimulator.Properties.Resources.Step;
            this.btn_Step.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Step.Name = "btn_Step";
            this.btn_Step.Size = new System.Drawing.Size(23, 20);
            this.btn_Step.Text = "Einzelschritt";
            this.btn_Step.Click += new System.EventHandler(this.btn_Step_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel_ProcessRate
            // 
            this.toolStripLabel_ProcessRate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel_ProcessRate.Name = "toolStripLabel_ProcessRate";
            this.toolStripLabel_ProcessRate.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.toolStripLabel_ProcessRate.Size = new System.Drawing.Size(48, 20);
            this.toolStripLabel_ProcessRate.Text = "Ø -- ms";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.checkBox_LoopSim);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label_Interval);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label_IntervalValue);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.trackBar_Interval);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(203, 96);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(203, 119);
            this.toolStripContainer1.TabIndex = 7;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip_Simulation);
            // 
            // checkBox_LoopSim
            // 
            this.checkBox_LoopSim.AutoSize = true;
            this.checkBox_LoopSim.Enabled = false;
            this.checkBox_LoopSim.Location = new System.Drawing.Point(6, 73);
            this.checkBox_LoopSim.Name = "checkBox_LoopSim";
            this.checkBox_LoopSim.Size = new System.Drawing.Size(126, 17);
            this.checkBox_LoopSim.TabIndex = 3;
            this.checkBox_LoopSim.Text = "Simulation in Schleife";
            this.toolTip.SetToolTip(this.checkBox_LoopSim, "Bestimmt, ob die Simulation am Anfang neu startet, wenn die Stimuli enden./nAnder" +
                    "enfalls wird die Simulation endlos fortgeführt.");
            this.checkBox_LoopSim.UseVisualStyleBackColor = true;
            this.checkBox_LoopSim.CheckedChanged += new System.EventHandler(this.checkBox_LoopSim_CheckedChanged);
            // 
            // label_Interval
            // 
            this.label_Interval.AutoSize = true;
            this.label_Interval.Location = new System.Drawing.Point(3, 6);
            this.label_Interval.Name = "label_Interval";
            this.label_Interval.Size = new System.Drawing.Size(73, 13);
            this.label_Interval.TabIndex = 1;
            this.label_Interval.Text = "Schritt-Pause:";
            // 
            // label_IntervalValue
            // 
            this.label_IntervalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_IntervalValue.Location = new System.Drawing.Point(102, 6);
            this.label_IntervalValue.Name = "label_IntervalValue";
            this.label_IntervalValue.Size = new System.Drawing.Size(98, 13);
            this.label_IntervalValue.TabIndex = 2;
            this.label_IntervalValue.Text = "50 ms (200 Hz)";
            this.label_IntervalValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBar_Interval
            // 
            this.trackBar_Interval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_Interval.Enabled = false;
            this.trackBar_Interval.LargeChange = 50;
            this.trackBar_Interval.Location = new System.Drawing.Point(3, 22);
            this.trackBar_Interval.Maximum = 1000;
            this.trackBar_Interval.Minimum = 5;
            this.trackBar_Interval.Name = "trackBar_Interval";
            this.trackBar_Interval.Size = new System.Drawing.Size(197, 45);
            this.trackBar_Interval.TabIndex = 0;
            this.trackBar_Interval.TickFrequency = 50;
            this.trackBar_Interval.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_Interval.Value = 50;
            this.trackBar_Interval.Scroll += new System.EventHandler(this.trackBar_Interval_Scroll);
            // 
            // SimulationControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(135, 115);
            this.ClientSize = new System.Drawing.Size(203, 119);
            this.Controls.Add(this.toolStripContainer1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HideOnClose = true;
            this.Name = "SimulationControlForm";
            this.RightToLeftLayout = true;
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
            this.TabText = "Simulationssteuerung";
            this.Text = "Simulationssteuerung";
            this.toolStrip_Simulation.ResumeLayout(false);
            this.toolStrip_Simulation.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Interval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_Simulation;
        private System.Windows.Forms.ToolStripButton btn_Start;
        private System.Windows.Forms.ToolStripButton btn_Stop;
        private System.Windows.Forms.ToolStripButton btn_Pause;
        private System.Windows.Forms.ToolStripButton btn_Step;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TrackBar trackBar_Interval;
        private System.Windows.Forms.Label label_Interval;
        private System.Windows.Forms.Label label_IntervalValue;
        private System.Windows.Forms.CheckBox checkBox_LoopSim;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_ProcessRate;

    }
}
namespace Nullstack.ClearCanvas.DevTools.View.WinForms {
    partial class DevToolsConfigComponentPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.grpLogFile = new System.Windows.Forms.GroupBox();
			this.lyoLogFile = new System.Windows.Forms.FlowLayoutPanel();
			this.radLogShellAssoc = new System.Windows.Forms.RadioButton();
			this.radLogOtherApp = new System.Windows.Forms.RadioButton();
			this.pnlLogOtherApp = new System.Windows.Forms.Panel();
			this.lyoLogOtherAppMacros = new System.Windows.Forms.FlowLayoutPanel();
			this.lblLogOtherAppMacroN = new System.Windows.Forms.Label();
			this.lblLogOtherAppMacroP = new System.Windows.Forms.Label();
			this.lblLogOtherAppMacroD = new System.Windows.Forms.Label();
			this.lblLogOtherAppMacroX = new System.Windows.Forms.Label();
			this.lblLogOtherAppMacroPercent = new System.Windows.Forms.Label();
			this.lblLogOtherAppMacros = new System.Windows.Forms.Label();
			this.pnlLogOtherAppPath = new System.Windows.Forms.Panel();
			this.txtLogOtherAppPath = new System.Windows.Forms.TextBox();
			this.lblLogOtherAppPath = new System.Windows.Forms.Label();
			this.grpOther = new System.Windows.Forms.GroupBox();
			this.lblAdvanceLogUnits = new System.Windows.Forms.Label();
			this.spinAdvanceLog = new System.Windows.Forms.NumericUpDown();
			this.lblAdvanceLog = new System.Windows.Forms.Label();
			this.grpLogFile.SuspendLayout();
			this.lyoLogFile.SuspendLayout();
			this.pnlLogOtherApp.SuspendLayout();
			this.lyoLogOtherAppMacros.SuspendLayout();
			this.pnlLogOtherAppPath.SuspendLayout();
			this.grpOther.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spinAdvanceLog)).BeginInit();
			this.SuspendLayout();
			// 
			// grpLogFile
			// 
			this.grpLogFile.Controls.Add(this.lyoLogFile);
			this.grpLogFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpLogFile.Location = new System.Drawing.Point(0, 0);
			this.grpLogFile.Name = "grpLogFile";
			this.grpLogFile.Size = new System.Drawing.Size(533, 155);
			this.grpLogFile.TabIndex = 0;
			this.grpLogFile.TabStop = false;
			this.grpLogFile.Text = "Log File Viewer";
			// 
			// lyoLogFile
			// 
			this.lyoLogFile.Controls.Add(this.radLogShellAssoc);
			this.lyoLogFile.Controls.Add(this.radLogOtherApp);
			this.lyoLogFile.Controls.Add(this.pnlLogOtherApp);
			this.lyoLogFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lyoLogFile.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.lyoLogFile.Location = new System.Drawing.Point(3, 16);
			this.lyoLogFile.Name = "lyoLogFile";
			this.lyoLogFile.Size = new System.Drawing.Size(527, 136);
			this.lyoLogFile.TabIndex = 0;
			// 
			// radLogShellAssoc
			// 
			this.radLogShellAssoc.AutoSize = true;
			this.radLogShellAssoc.Location = new System.Drawing.Point(3, 3);
			this.radLogShellAssoc.Name = "radLogShellAssoc";
			this.radLogShellAssoc.Size = new System.Drawing.Size(192, 17);
			this.radLogShellAssoc.TabIndex = 0;
			this.radLogShellAssoc.TabStop = true;
			this.radLogShellAssoc.Text = "Open with shell &associated program";
			this.radLogShellAssoc.UseVisualStyleBackColor = true;
			// 
			// radLogOtherApp
			// 
			this.radLogOtherApp.AutoSize = true;
			this.radLogOtherApp.Location = new System.Drawing.Point(3, 26);
			this.radLogOtherApp.Name = "radLogOtherApp";
			this.radLogOtherApp.Size = new System.Drawing.Size(167, 17);
			this.radLogOtherApp.TabIndex = 1;
			this.radLogOtherApp.TabStop = true;
			this.radLogOtherApp.Text = "Open with &external application";
			this.radLogOtherApp.UseVisualStyleBackColor = true;
			// 
			// pnlLogOtherApp
			// 
			this.pnlLogOtherApp.Controls.Add(this.lyoLogOtherAppMacros);
			this.pnlLogOtherApp.Controls.Add(this.lblLogOtherAppMacros);
			this.pnlLogOtherApp.Controls.Add(this.pnlLogOtherAppPath);
			this.pnlLogOtherApp.Location = new System.Drawing.Point(3, 49);
			this.pnlLogOtherApp.Name = "pnlLogOtherApp";
			this.pnlLogOtherApp.Padding = new System.Windows.Forms.Padding(19, 0, 0, 0);
			this.pnlLogOtherApp.Size = new System.Drawing.Size(362, 81);
			this.pnlLogOtherApp.TabIndex = 2;
			// 
			// lyoLogOtherAppMacros
			// 
			this.lyoLogOtherAppMacros.Controls.Add(this.lblLogOtherAppMacroN);
			this.lyoLogOtherAppMacros.Controls.Add(this.lblLogOtherAppMacroP);
			this.lyoLogOtherAppMacros.Controls.Add(this.lblLogOtherAppMacroD);
			this.lyoLogOtherAppMacros.Controls.Add(this.lblLogOtherAppMacroX);
			this.lyoLogOtherAppMacros.Controls.Add(this.lblLogOtherAppMacroPercent);
			this.lyoLogOtherAppMacros.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lyoLogOtherAppMacros.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.lyoLogOtherAppMacros.Location = new System.Drawing.Point(19, 36);
			this.lyoLogOtherAppMacros.Name = "lyoLogOtherAppMacros";
			this.lyoLogOtherAppMacros.Size = new System.Drawing.Size(343, 45);
			this.lyoLogOtherAppMacros.TabIndex = 2;
			// 
			// lblLogOtherAppMacroN
			// 
			this.lblLogOtherAppMacroN.AutoSize = true;
			this.lblLogOtherAppMacroN.Location = new System.Drawing.Point(3, 0);
			this.lblLogOtherAppMacroN.Name = "lblLogOtherAppMacroN";
			this.lblLogOtherAppMacroN.Size = new System.Drawing.Size(86, 13);
			this.lblLogOtherAppMacroN.TabIndex = 1;
			this.lblLogOtherAppMacroN.Text = "%n - log filename";
			// 
			// lblLogOtherAppMacroP
			// 
			this.lblLogOtherAppMacroP.AutoSize = true;
			this.lblLogOtherAppMacroP.Location = new System.Drawing.Point(3, 13);
			this.lblLogOtherAppMacroP.Name = "lblLogOtherAppMacroP";
			this.lblLogOtherAppMacroP.Size = new System.Drawing.Size(110, 13);
			this.lblLogOtherAppMacroP.TabIndex = 2;
			this.lblLogOtherAppMacroP.Text = "%p - log full pathname";
			// 
			// lblLogOtherAppMacroD
			// 
			this.lblLogOtherAppMacroD.AutoSize = true;
			this.lblLogOtherAppMacroD.Location = new System.Drawing.Point(3, 26);
			this.lblLogOtherAppMacroD.Name = "lblLogOtherAppMacroD";
			this.lblLogOtherAppMacroD.Size = new System.Drawing.Size(87, 13);
			this.lblLogOtherAppMacroD.TabIndex = 3;
			this.lblLogOtherAppMacroD.Text = "%d - log directory";
			// 
			// lblLogOtherAppMacroX
			// 
			this.lblLogOtherAppMacroX.AutoSize = true;
			this.lblLogOtherAppMacroX.Location = new System.Drawing.Point(119, 0);
			this.lblLogOtherAppMacroX.Name = "lblLogOtherAppMacroX";
			this.lblLogOtherAppMacroX.Size = new System.Drawing.Size(91, 13);
			this.lblLogOtherAppMacroX.TabIndex = 4;
			this.lblLogOtherAppMacroX.Text = "%x - log extension";
			// 
			// lblLogOtherAppMacroPercent
			// 
			this.lblLogOtherAppMacroPercent.AutoSize = true;
			this.lblLogOtherAppMacroPercent.Location = new System.Drawing.Point(119, 13);
			this.lblLogOtherAppMacroPercent.Name = "lblLogOtherAppMacroPercent";
			this.lblLogOtherAppMacroPercent.Size = new System.Drawing.Size(115, 13);
			this.lblLogOtherAppMacroPercent.TabIndex = 5;
			this.lblLogOtherAppMacroPercent.Text = "%% - literal % character";
			// 
			// lblLogOtherAppMacros
			// 
			this.lblLogOtherAppMacros.AutoSize = true;
			this.lblLogOtherAppMacros.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblLogOtherAppMacros.Location = new System.Drawing.Point(19, 23);
			this.lblLogOtherAppMacros.Name = "lblLogOtherAppMacros";
			this.lblLogOtherAppMacros.Size = new System.Drawing.Size(45, 13);
			this.lblLogOtherAppMacros.TabIndex = 0;
			this.lblLogOtherAppMacros.Text = "Macros:";
			// 
			// pnlLogOtherAppPath
			// 
			this.pnlLogOtherAppPath.Controls.Add(this.txtLogOtherAppPath);
			this.pnlLogOtherAppPath.Controls.Add(this.lblLogOtherAppPath);
			this.pnlLogOtherAppPath.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLogOtherAppPath.Location = new System.Drawing.Point(19, 0);
			this.pnlLogOtherAppPath.Name = "pnlLogOtherAppPath";
			this.pnlLogOtherAppPath.Size = new System.Drawing.Size(343, 23);
			this.pnlLogOtherAppPath.TabIndex = 1;
			// 
			// txtLogOtherAppPath
			// 
			this.txtLogOtherAppPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLogOtherAppPath.Location = new System.Drawing.Point(74, 0);
			this.txtLogOtherAppPath.Name = "txtLogOtherAppPath";
			this.txtLogOtherAppPath.Size = new System.Drawing.Size(269, 20);
			this.txtLogOtherAppPath.TabIndex = 0;
			// 
			// lblLogOtherAppPath
			// 
			this.lblLogOtherAppPath.AutoSize = true;
			this.lblLogOtherAppPath.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblLogOtherAppPath.Location = new System.Drawing.Point(0, 0);
			this.lblLogOtherAppPath.Name = "lblLogOtherAppPath";
			this.lblLogOtherAppPath.Size = new System.Drawing.Size(74, 13);
			this.lblLogOtherAppPath.TabIndex = 1;
			this.lblLogOtherAppPath.Text = "Program Path:";
			// 
			// grpOther
			// 
			this.grpOther.Controls.Add(this.lblAdvanceLogUnits);
			this.grpOther.Controls.Add(this.spinAdvanceLog);
			this.grpOther.Controls.Add(this.lblAdvanceLog);
			this.grpOther.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpOther.Location = new System.Drawing.Point(0, 155);
			this.grpOther.Name = "grpOther";
			this.grpOther.Size = new System.Drawing.Size(533, 63);
			this.grpOther.TabIndex = 1;
			this.grpOther.TabStop = false;
			this.grpOther.Text = "Other";
			// 
			// lblAdvanceLogUnits
			// 
			this.lblAdvanceLogUnits.AutoSize = true;
			this.lblAdvanceLogUnits.Location = new System.Drawing.Point(153, 39);
			this.lblAdvanceLogUnits.Name = "lblAdvanceLogUnits";
			this.lblAdvanceLogUnits.Size = new System.Drawing.Size(28, 13);
			this.lblAdvanceLogUnits.TabIndex = 5;
			this.lblAdvanceLogUnits.Text = "lines";
			// 
			// spinAdvanceLog
			// 
			this.spinAdvanceLog.Location = new System.Drawing.Point(100, 37);
			this.spinAdvanceLog.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.spinAdvanceLog.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spinAdvanceLog.Name = "spinAdvanceLog";
			this.spinAdvanceLog.Size = new System.Drawing.Size(47, 20);
			this.spinAdvanceLog.TabIndex = 4;
			this.spinAdvanceLog.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblAdvanceLog
			// 
			this.lblAdvanceLog.AutoSize = true;
			this.lblAdvanceLog.Location = new System.Drawing.Point(6, 39);
			this.lblAdvanceLog.Name = "lblAdvanceLog";
			this.lblAdvanceLog.Size = new System.Drawing.Size(88, 13);
			this.lblAdvanceLog.TabIndex = 3;
			this.lblAdvanceLog.Text = "Advance Log by:";
			// 
			// DevToolsConfigComponentPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpOther);
			this.Controls.Add(this.grpLogFile);
			this.Name = "DevToolsConfigComponentPanel";
			this.Size = new System.Drawing.Size(533, 252);
			this.grpLogFile.ResumeLayout(false);
			this.lyoLogFile.ResumeLayout(false);
			this.lyoLogFile.PerformLayout();
			this.pnlLogOtherApp.ResumeLayout(false);
			this.pnlLogOtherApp.PerformLayout();
			this.lyoLogOtherAppMacros.ResumeLayout(false);
			this.lyoLogOtherAppMacros.PerformLayout();
			this.pnlLogOtherAppPath.ResumeLayout(false);
			this.pnlLogOtherAppPath.PerformLayout();
			this.grpOther.ResumeLayout(false);
			this.grpOther.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spinAdvanceLog)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLogFile;
        private System.Windows.Forms.FlowLayoutPanel lyoLogFile;
        private System.Windows.Forms.RadioButton radLogShellAssoc;
        private System.Windows.Forms.RadioButton radLogOtherApp;
        private System.Windows.Forms.Panel pnlLogOtherApp;
        private System.Windows.Forms.Panel pnlLogOtherAppPath;
        private System.Windows.Forms.TextBox txtLogOtherAppPath;
        private System.Windows.Forms.Label lblLogOtherAppPath;
        private System.Windows.Forms.FlowLayoutPanel lyoLogOtherAppMacros;
        private System.Windows.Forms.Label lblLogOtherAppMacros;
        private System.Windows.Forms.Label lblLogOtherAppMacroN;
        private System.Windows.Forms.Label lblLogOtherAppMacroP;
        private System.Windows.Forms.Label lblLogOtherAppMacroD;
        private System.Windows.Forms.Label lblLogOtherAppMacroX;
        private System.Windows.Forms.Label lblLogOtherAppMacroPercent;
		private System.Windows.Forms.GroupBox grpOther;
		private System.Windows.Forms.NumericUpDown spinAdvanceLog;
		private System.Windows.Forms.Label lblAdvanceLog;
		private System.Windows.Forms.Label lblAdvanceLogUnits;
    }
}

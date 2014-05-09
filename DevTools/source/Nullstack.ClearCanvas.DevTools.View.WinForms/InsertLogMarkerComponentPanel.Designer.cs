namespace Nullstack.ClearCanvas.DevTools.View.WinForms {
	partial class InsertLogMarkerComponentPanel {
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
			this.lblLevel = new System.Windows.Forms.Label();
			this.cboLevel = new System.Windows.Forms.ComboBox();
			this.lblMultiplier = new System.Windows.Forms.Label();
			this.spinMultiplier = new System.Windows.Forms.NumericUpDown();
			this.lblMessage = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.pnlMessage = new System.Windows.Forms.Panel();
			this.lyoOptions = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.spinMultiplier)).BeginInit();
			this.pnlMessage.SuspendLayout();
			this.lyoOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblLevel
			// 
			this.lblLevel.Location = new System.Drawing.Point(3, 0);
			this.lblLevel.Name = "lblLevel";
			this.lblLevel.Size = new System.Drawing.Size(70, 23);
			this.lblLevel.TabIndex = 0;
			this.lblLevel.Text = "Level:";
			this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboLevel
			// 
			this.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLevel.FormattingEnabled = true;
			this.cboLevel.Location = new System.Drawing.Point(79, 3);
			this.cboLevel.Name = "cboLevel";
			this.cboLevel.Size = new System.Drawing.Size(86, 21);
			this.cboLevel.TabIndex = 1;
			// 
			// lblMultiplier
			// 
			this.lblMultiplier.Location = new System.Drawing.Point(171, 0);
			this.lblMultiplier.Name = "lblMultiplier";
			this.lblMultiplier.Size = new System.Drawing.Size(70, 23);
			this.lblMultiplier.TabIndex = 2;
			this.lblMultiplier.Text = "Multiplier:";
			this.lblMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spinMultiplier
			// 
			this.spinMultiplier.Location = new System.Drawing.Point(247, 3);
			this.spinMultiplier.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.spinMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spinMultiplier.Name = "spinMultiplier";
			this.spinMultiplier.Size = new System.Drawing.Size(48, 20);
			this.spinMultiplier.TabIndex = 3;
			this.spinMultiplier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblMessage
			// 
			this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblMessage.Location = new System.Drawing.Point(5, 5);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(312, 23);
			this.lblMessage.TabIndex = 4;
			this.lblMessage.Text = "Message:";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMessage
			// 
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMessage.Location = new System.Drawing.Point(5, 28);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMessage.Size = new System.Drawing.Size(312, 61);
			this.txtMessage.TabIndex = 5;
			// 
			// pnlMessage
			// 
			this.pnlMessage.Controls.Add(this.txtMessage);
			this.pnlMessage.Controls.Add(this.lblMessage);
			this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMessage.Location = new System.Drawing.Point(0, 31);
			this.pnlMessage.Name = "pnlMessage";
			this.pnlMessage.Padding = new System.Windows.Forms.Padding(5);
			this.pnlMessage.Size = new System.Drawing.Size(322, 94);
			this.pnlMessage.TabIndex = 7;
			// 
			// lyoOptions
			// 
			this.lyoOptions.Controls.Add(this.lblLevel);
			this.lyoOptions.Controls.Add(this.cboLevel);
			this.lyoOptions.Controls.Add(this.lblMultiplier);
			this.lyoOptions.Controls.Add(this.spinMultiplier);
			this.lyoOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.lyoOptions.Location = new System.Drawing.Point(0, 0);
			this.lyoOptions.Name = "lyoOptions";
			this.lyoOptions.Size = new System.Drawing.Size(322, 31);
			this.lyoOptions.TabIndex = 8;
			// 
			// InsertLogMarkerComponentPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlMessage);
			this.Controls.Add(this.lyoOptions);
			this.Name = "InsertLogMarkerComponentPanel";
			this.Size = new System.Drawing.Size(322, 125);
			((System.ComponentModel.ISupportInitialize)(this.spinMultiplier)).EndInit();
			this.pnlMessage.ResumeLayout(false);
			this.pnlMessage.PerformLayout();
			this.lyoOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblLevel;
		private System.Windows.Forms.ComboBox cboLevel;
		private System.Windows.Forms.Label lblMultiplier;
		private System.Windows.Forms.NumericUpDown spinMultiplier;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Panel pnlMessage;
		private System.Windows.Forms.FlowLayoutPanel lyoOptions;
	}
}

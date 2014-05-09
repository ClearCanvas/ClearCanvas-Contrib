namespace Nullstack.ClearCanvas.DevTools.View.WinForms {
	partial class DesktopLogComponentPanel {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			PerformAdditionalDisposal();
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
			this.rtfLog = new System.Windows.Forms.RichTextBox();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.chkAutoScrollToEnd = new System.Windows.Forms.CheckBox();
			this.lblSpacer = new System.Windows.Forms.Label();
			this.txtWindowSize = new System.Windows.Forms.TextBox();
			this.lblWindowSize = new System.Windows.Forms.Label();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.lblSearch = new System.Windows.Forms.Label();
			this.pnlBottom.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtfLog
			// 
			this.rtfLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtfLog.Location = new System.Drawing.Point(0, 27);
			this.rtfLog.Name = "rtfLog";
			this.rtfLog.ReadOnly = true;
			this.rtfLog.Size = new System.Drawing.Size(352, 552);
			this.rtfLog.TabIndex = 0;
			this.rtfLog.Text = "";
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.chkAutoScrollToEnd);
			this.pnlBottom.Controls.Add(this.lblSpacer);
			this.pnlBottom.Controls.Add(this.txtWindowSize);
			this.pnlBottom.Controls.Add(this.lblWindowSize);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 579);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Padding = new System.Windows.Forms.Padding(3);
			this.pnlBottom.Size = new System.Drawing.Size(352, 27);
			this.pnlBottom.TabIndex = 1;
			// 
			// chkAutoScrollToEnd
			// 
			this.chkAutoScrollToEnd.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkAutoScrollToEnd.Location = new System.Drawing.Point(194, 3);
			this.chkAutoScrollToEnd.Name = "chkAutoScrollToEnd";
			this.chkAutoScrollToEnd.Size = new System.Drawing.Size(131, 21);
			this.chkAutoScrollToEnd.TabIndex = 2;
			this.chkAutoScrollToEnd.Text = "Auto Scroll to End";
			this.chkAutoScrollToEnd.UseVisualStyleBackColor = true;
			// 
			// lblSpacer
			// 
			this.lblSpacer.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblSpacer.Location = new System.Drawing.Point(165, 3);
			this.lblSpacer.Name = "lblSpacer";
			this.lblSpacer.Size = new System.Drawing.Size(29, 21);
			this.lblSpacer.TabIndex = 3;
			// 
			// txtWindowSize
			// 
			this.txtWindowSize.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtWindowSize.Location = new System.Drawing.Point(100, 3);
			this.txtWindowSize.MaxLength = 6;
			this.txtWindowSize.Name = "txtWindowSize";
			this.txtWindowSize.Size = new System.Drawing.Size(65, 20);
			this.txtWindowSize.TabIndex = 1;
			// 
			// lblWindowSize
			// 
			this.lblWindowSize.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblWindowSize.Location = new System.Drawing.Point(3, 3);
			this.lblWindowSize.Name = "lblWindowSize";
			this.lblWindowSize.Size = new System.Drawing.Size(97, 21);
			this.lblWindowSize.TabIndex = 0;
			this.lblWindowSize.Text = "Window Size (KB)";
			this.lblWindowSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.txtSearch);
			this.pnlTop.Controls.Add(this.btnPrev);
			this.pnlTop.Controls.Add(this.btnNext);
			this.pnlTop.Controls.Add(this.lblSearch);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Padding = new System.Windows.Forms.Padding(3);
			this.pnlTop.Size = new System.Drawing.Size(352, 27);
			this.pnlTop.TabIndex = 2;
			// 
			// txtSearch
			// 
			this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSearch.Location = new System.Drawing.Point(53, 3);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(238, 20);
			this.txtSearch.TabIndex = 0;
			// 
			// btnPrev
			// 
			this.btnPrev.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnPrev.FlatAppearance.BorderSize = 0;
			this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnPrev.Image = global::Nullstack.ClearCanvas.DevTools.View.WinForms.Properties.Resources.PreviousToolSmall;
			this.btnPrev.Location = new System.Drawing.Point(291, 3);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(29, 21);
			this.btnPrev.TabIndex = 3;
			this.btnPrev.UseVisualStyleBackColor = true;
			this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
			// 
			// btnNext
			// 
			this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnNext.FlatAppearance.BorderSize = 0;
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnNext.Image = global::Nullstack.ClearCanvas.DevTools.View.WinForms.Properties.Resources.NextToolSmall;
			this.btnNext.Location = new System.Drawing.Point(320, 3);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(29, 21);
			this.btnNext.TabIndex = 2;
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// lblSearch
			// 
			this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblSearch.Location = new System.Drawing.Point(3, 3);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(50, 21);
			this.lblSearch.TabIndex = 1;
			this.lblSearch.Text = "Search:";
			this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DesktopLogComponentPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.rtfLog);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.pnlBottom);
			this.Name = "DesktopLogComponentPanel";
			this.Size = new System.Drawing.Size(352, 606);
			this.pnlBottom.ResumeLayout(false);
			this.pnlBottom.PerformLayout();
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox rtfLog;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.TextBox txtWindowSize;
		private System.Windows.Forms.Label lblWindowSize;
		private System.Windows.Forms.CheckBox chkAutoScrollToEnd;
		private System.Windows.Forms.Label lblSpacer;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnNext;
	}
}

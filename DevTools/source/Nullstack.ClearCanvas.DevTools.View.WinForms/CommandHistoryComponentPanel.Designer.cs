namespace Nullstack.ClearCanvas.DevTools.View.WinForms {
	partial class CommandHistoryComponentPanel {
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
			this.DisposeCommandHistoryComponentPanel();
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.SplitContainer splitContainer1;
			this._commandList = new System.Windows.Forms.ListBox();
			this._propertyGrid = new System.Windows.Forms.PropertyGrid();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(this._commandList);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(this._propertyGrid);
			splitContainer1.Size = new System.Drawing.Size(288, 460);
			splitContainer1.SplitterDistance = 269;
			splitContainer1.TabIndex = 1;
			// 
			// _commandList
			// 
			this._commandList.Dock = System.Windows.Forms.DockStyle.Fill;
			this._commandList.FormattingEnabled = true;
			this._commandList.Location = new System.Drawing.Point(0, 0);
			this._commandList.Name = "_commandList";
			this._commandList.Size = new System.Drawing.Size(288, 264);
			this._commandList.TabIndex = 0;
			this._commandList.SelectedIndexChanged += new System.EventHandler(this.commandList_SelectedIndexChanged);
			// 
			// _propertyGrid
			// 
			this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this._propertyGrid.Location = new System.Drawing.Point(0, 0);
			this._propertyGrid.Name = "_propertyGrid";
			this._propertyGrid.Size = new System.Drawing.Size(288, 187);
			this._propertyGrid.TabIndex = 2;
			// 
			// CommandHistoryComponentPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(splitContainer1);
			this.Name = "CommandHistoryComponentPanel";
			this.Size = new System.Drawing.Size(288, 460);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox _commandList;
		private System.Windows.Forms.PropertyGrid _propertyGrid;
	}
}

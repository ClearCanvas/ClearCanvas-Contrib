namespace SeriesQuery
{
    partial class ImageBrowserComponentControl
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


        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			ClearCanvas.Desktop.Selection selection1 = new ClearCanvas.Desktop.Selection();
//			this._resultsTitleBar = new Crownwood.DotNetMagic.Controls.TitleBar();
			this._seriesTableView = new ClearCanvas.Desktop.View.WinForms.TableView();
			this.SuspendLayout();
			// 
			// _resultsTitleBar
			// 
            //this._resultsTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            //this._resultsTitleBar.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this._resultsTitleBar.GradientColoring = Crownwood.DotNetMagic.Controls.GradientColoring.LightBackToDarkBack;
            //this._resultsTitleBar.Location = new System.Drawing.Point(0, 0);
            //this._resultsTitleBar.MouseOverColor = System.Drawing.Color.Empty;
            //this._resultsTitleBar.Name = "_resultsTitleBar";
            //this._resultsTitleBar.Size = new System.Drawing.Size(623, 30);
            //this._resultsTitleBar.Style = Crownwood.DotNetMagic.Common.VisualStyle.IDE2005;
            //this._resultsTitleBar.TabIndex = 3;
            //this._resultsTitleBar.Text = "10 results found on server";
			// 
			// _seriesTableView
			// 
			this._seriesTableView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._seriesTableView.Location = new System.Drawing.Point(0, 30);
			this._seriesTableView.MenuModel = null;
			this._seriesTableView.Name = "_seriesTableView";
			this._seriesTableView.ReadOnly = false;
			this._seriesTableView.Selection = selection1;
			//this._seriesTableView.Size = new System.Drawing.Size(623, 325);
            this._seriesTableView.Size = new System.Drawing.Size(623, 150);
			this._seriesTableView.TabIndex = 0;
			this._seriesTableView.Table = null;
			this._seriesTableView.ToolbarModel = null;
			// 
			// StudyBrowserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this._seriesTableView);
//			this.Controls.Add(this._resultsTitleBar);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "StudyBrowserControl";
			//this.Size = new System.Drawing.Size(623, 355);
            this.Size = new System.Drawing.Size(623, 155);
			this.ResumeLayout(false);

		}



		private ClearCanvas.Desktop.View.WinForms.TableView _seriesTableView;
		//private Crownwood.DotNetMagic.Controls.TitleBar _resultsTitleBar;


    }
}

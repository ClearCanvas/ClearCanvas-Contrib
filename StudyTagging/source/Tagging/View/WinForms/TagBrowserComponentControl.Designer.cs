#region License

// Copyright (c) 2006-2007, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

namespace Tagging.View.WinForms
{
    partial class TagBrowserComponentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tags = new System.Windows.Forms.TextBox();
            this._updateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._similarStudiesTableView = new ClearCanvas.Desktop.View.WinForms.TableView();
            this.label2 = new System.Windows.Forms.Label();
            this._radioSimilar = new System.Windows.Forms.RadioButton();
            this._radioSearch = new System.Windows.Forms.RadioButton();
            this._searchTags = new System.Windows.Forms.TextBox();
            this._searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _tags
            // 
            this._tags.Location = new System.Drawing.Point(17, 27);
            this._tags.Name = "_tags";
            this._tags.Size = new System.Drawing.Size(296, 20);
            this._tags.TabIndex = 0;
            // 
            // _updateButton
            // 
            this._updateButton.Location = new System.Drawing.Point(238, 53);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(75, 23);
            this._updateButton.TabIndex = 1;
            this._updateButton.Text = "Save Tags";
            this._updateButton.UseVisualStyleBackColor = true;
            this._updateButton.Click += new System.EventHandler(this._updateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Study Tags";
            // 
            // _similarStudiesTableView
            // 
            this._similarStudiesTableView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._similarStudiesTableView.Location = new System.Drawing.Point(17, 154);
            this._similarStudiesTableView.Name = "_similarStudiesTableView";
            this._similarStudiesTableView.ReadOnly = false;
            this._similarStudiesTableView.ShowToolbar = false;
            this._similarStudiesTableView.Size = new System.Drawing.Size(297, 373);
            this._similarStudiesTableView.TabIndex = 3;
            this._similarStudiesTableView.ToolStripItemDisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._similarStudiesTableView.ItemDoubleClicked += new System.EventHandler(this._similarStudiesTableView_ItemDoubleClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Results";
            // 
            // _radioSimilar
            // 
            this._radioSimilar.AutoSize = true;
            this._radioSimilar.Checked = true;
            this._radioSimilar.Location = new System.Drawing.Point(19, 85);
            this._radioSimilar.Name = "_radioSimilar";
            this._radioSimilar.Size = new System.Drawing.Size(112, 17);
            this._radioSimilar.TabIndex = 5;
            this._radioSimilar.TabStop = true;
            this._radioSimilar.Text = "Find similar studies";
            this._radioSimilar.UseVisualStyleBackColor = true;
            this._radioSimilar.CheckedChanged += new System.EventHandler(this._radioSimilar_CheckedChanged);
            // 
            // _radioSearch
            // 
            this._radioSearch.AutoSize = true;
            this._radioSearch.Location = new System.Drawing.Point(19, 108);
            this._radioSearch.Name = "_radioSearch";
            this._radioSearch.Size = new System.Drawing.Size(59, 17);
            this._radioSearch.TabIndex = 6;
            this._radioSearch.TabStop = true;
            this._radioSearch.Text = "Search";
            this._radioSearch.UseVisualStyleBackColor = true;
            // 
            // _searchTags
            // 
            this._searchTags.Location = new System.Drawing.Point(84, 105);
            this._searchTags.Name = "_searchTags";
            this._searchTags.Size = new System.Drawing.Size(176, 20);
            this._searchTags.TabIndex = 7;
            // 
            // _searchButton
            // 
            this._searchButton.Location = new System.Drawing.Point(267, 103);
            this._searchButton.Name = "_searchButton";
            this._searchButton.Size = new System.Drawing.Size(47, 23);
            this._searchButton.TabIndex = 8;
            this._searchButton.Text = "Find";
            this._searchButton.UseVisualStyleBackColor = true;
            this._searchButton.Click += new System.EventHandler(this._searchButton_Click);
            // 
            // TagBrowserComponentControl
            // 
            this.AcceptButton = this._updateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._searchButton);
            this.Controls.Add(this._searchTags);
            this.Controls.Add(this._radioSearch);
            this.Controls.Add(this._radioSimilar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._similarStudiesTableView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._updateButton);
            this.Controls.Add(this._tags);
            this.Name = "TagBrowserComponentControl";
            this.Size = new System.Drawing.Size(335, 541);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tags;
        private System.Windows.Forms.Button _updateButton;
        private System.Windows.Forms.Label label1;
        private ClearCanvas.Desktop.View.WinForms.TableView _similarStudiesTableView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton _radioSimilar;
        private System.Windows.Forms.RadioButton _radioSearch;
        private System.Windows.Forms.TextBox _searchTags;
        private System.Windows.Forms.Button _searchButton;
    }
}

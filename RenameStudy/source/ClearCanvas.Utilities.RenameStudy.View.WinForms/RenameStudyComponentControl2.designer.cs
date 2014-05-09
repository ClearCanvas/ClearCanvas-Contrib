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

namespace ClearCanvas.Utilities.RenameStudy.View.WinForms
{
    partial class RenameStudyComponentControl
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
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._studyDate = new ClearCanvas.Desktop.View.WinForms.DateTimeField();
            this._dateOfBirth = new ClearCanvas.Desktop.View.WinForms.DateTimeField();
            this._patientsName = new ClearCanvas.Desktop.View.WinForms.TextField();
            this._accessionNumber = new ClearCanvas.Desktop.View.WinForms.TextField();
            this._patientId = new ClearCanvas.Desktop.View.WinForms.TextField();
            this._studyDescription = new ClearCanvas.Desktop.View.WinForms.TextField();
            this._preserveSeriesData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(93, 322);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 6;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.OnOkButtonClicked);
            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(174, 322);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 7;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.OnCancelButtonClicked);
            // 
            // _studyDate
            // 
            this._studyDate.LabelText = "Study Date";
            this._studyDate.Location = new System.Drawing.Point(20, 242);
            this._studyDate.Margin = new System.Windows.Forms.Padding(2);
            this._studyDate.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this._studyDate.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this._studyDate.Name = "_studyDate";
            this._studyDate.Nullable = true;
            this._studyDate.Size = new System.Drawing.Size(150, 41);
            this._studyDate.TabIndex = 5;
            this._studyDate.Value = new System.DateTime(2008, 4, 21, 9, 14, 8, 984);
            // 
            // _dateOfBirth
            // 
            this._dateOfBirth.LabelText = "Date of Birth";
            this._dateOfBirth.Location = new System.Drawing.Point(20, 107);
            this._dateOfBirth.Margin = new System.Windows.Forms.Padding(2);
            this._dateOfBirth.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this._dateOfBirth.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this._dateOfBirth.Name = "_dateOfBirth";
            this._dateOfBirth.Nullable = true;
            this._dateOfBirth.Size = new System.Drawing.Size(150, 41);
            this._dateOfBirth.TabIndex = 2;
            this._dateOfBirth.Value = new System.DateTime(2008, 4, 21, 9, 11, 13, 140);
            // 
            // _patientsName
            // 
            this._patientsName.LabelText = "Patient\'s Name";
            this._patientsName.Location = new System.Drawing.Point(20, 62);
            this._patientsName.Margin = new System.Windows.Forms.Padding(2);
            this._patientsName.Mask = "";
            this._patientsName.Name = "_patientsName";
            this._patientsName.PasswordChar = '\0';
            this._patientsName.Size = new System.Drawing.Size(229, 41);
            this._patientsName.TabIndex = 1;
            this._patientsName.ToolTip = null;
            this._patientsName.Value = null;
            // 
            // _accessionNumber
            // 
            this._accessionNumber.LabelText = "Accession Number";
            this._accessionNumber.Location = new System.Drawing.Point(20, 197);
            this._accessionNumber.Margin = new System.Windows.Forms.Padding(2);
            this._accessionNumber.Mask = "";
            this._accessionNumber.Name = "_accessionNumber";
            this._accessionNumber.PasswordChar = '\0';
            this._accessionNumber.Size = new System.Drawing.Size(150, 41);
            this._accessionNumber.TabIndex = 4;
            this._accessionNumber.ToolTip = null;
            this._accessionNumber.Value = null;
            // 
            // _patientId
            // 
            this._patientId.LabelText = "Patient Id";
            this._patientId.Location = new System.Drawing.Point(20, 17);
            this._patientId.Margin = new System.Windows.Forms.Padding(2);
            this._patientId.Mask = "";
            this._patientId.Name = "_patientId";
            this._patientId.PasswordChar = '\0';
            this._patientId.Size = new System.Drawing.Size(229, 41);
            this._patientId.TabIndex = 0;
            this._patientId.ToolTip = null;
            this._patientId.Value = null;
            // 
            // _studyDescription
            // 
            this._studyDescription.LabelText = "Study Description";
            this._studyDescription.Location = new System.Drawing.Point(20, 152);
            this._studyDescription.Margin = new System.Windows.Forms.Padding(2);
            this._studyDescription.Mask = "";
            this._studyDescription.Name = "_studyDescription";
            this._studyDescription.PasswordChar = '\0';
            this._studyDescription.Size = new System.Drawing.Size(229, 41);
            this._studyDescription.TabIndex = 3;
            this._studyDescription.ToolTip = null;
            this._studyDescription.Value = null;
            // 
            // _preserveSeriesData
            // 
            this._preserveSeriesData.AutoSize = true;
            this._preserveSeriesData.Location = new System.Drawing.Point(26, 288);
            this._preserveSeriesData.Name = "_preserveSeriesData";
            this._preserveSeriesData.Size = new System.Drawing.Size(126, 17);
            this._preserveSeriesData.TabIndex = 8;
            this._preserveSeriesData.Text = "Preserve Series Data";
            this._preserveSeriesData.UseVisualStyleBackColor = true;
            this._preserveSeriesData.Visible = false;
            // 
            // RenameStudyComponentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._preserveSeriesData);
            this.Controls.Add(this._studyDescription);
            this.Controls.Add(this._patientId);
            this.Controls.Add(this._accessionNumber);
            this.Controls.Add(this._patientsName);
            this.Controls.Add(this._dateOfBirth);
            this.Controls.Add(this._studyDate);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Name = "RenameStudyComponentControl";
            this.Size = new System.Drawing.Size(266, 361);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private ClearCanvas.Desktop.View.WinForms.DateTimeField _studyDate;
		private ClearCanvas.Desktop.View.WinForms.DateTimeField _dateOfBirth;
		private ClearCanvas.Desktop.View.WinForms.TextField _patientsName;
		private ClearCanvas.Desktop.View.WinForms.TextField _accessionNumber;
		private ClearCanvas.Desktop.View.WinForms.TextField _patientId;
		private ClearCanvas.Desktop.View.WinForms.TextField _studyDescription;
		private System.Windows.Forms.CheckBox _preserveSeriesData;
    }
}

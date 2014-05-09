#region License

// Copyright (c) 2012, Stewart Bright
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

namespace ImportFromBitmap.View.WinForms
{
    partial class ImportFromBitmapComponentControl
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
			this._patientId = new ClearCanvas.Desktop.View.WinForms.TextField();
			this._middleName = new ClearCanvas.Desktop.View.WinForms.TextField();
			this._firstName = new ClearCanvas.Desktop.View.WinForms.TextField();
			this._lastName = new ClearCanvas.Desktop.View.WinForms.TextField();
			this._accessionNumber = new ClearCanvas.Desktop.View.WinForms.TextField();
			this._studyDescription = new ClearCanvas.Desktop.View.WinForms.TextField();
			this._studyDate = new ClearCanvas.Desktop.View.WinForms.DateTimeField();
			this._dob = new ClearCanvas.Desktop.View.WinForms.DateTimeField();
			this._cancel = new System.Windows.Forms.Button();
			this._ok = new System.Windows.Forms.Button();
			this._sexGroup = new System.Windows.Forms.GroupBox();
			this._other = new System.Windows.Forms.RadioButton();
			this._female = new System.Windows.Forms.RadioButton();
			this._male = new System.Windows.Forms.RadioButton();
			this._studyTime = new ClearCanvas.Desktop.View.WinForms.DateTimeField();
			this._piGroup = new System.Windows.Forms.GroupBox();
			this._piRgb = new System.Windows.Forms.RadioButton();
			this._piMonochrome2 = new System.Windows.Forms.RadioButton();
			this._sexGroup.SuspendLayout();
			this._piGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// _patientId
			// 
			this._patientId.LabelText = "Patient Id";
			this._patientId.Location = new System.Drawing.Point(14, 16);
			this._patientId.Margin = new System.Windows.Forms.Padding(2);
			this._patientId.Mask = "";
			this._patientId.Name = "_patientId";
			this._patientId.PasswordChar = '\0';
			this._patientId.Size = new System.Drawing.Size(150, 41);
			this._patientId.TabIndex = 0;
			this._patientId.ToolTip = null;
			this._patientId.Value = null;
			// 
			// _middleName
			// 
			this._middleName.LabelText = "Middle Name";
			this._middleName.Location = new System.Drawing.Point(350, 60);
			this._middleName.Margin = new System.Windows.Forms.Padding(2);
			this._middleName.Mask = "";
			this._middleName.Name = "_middleName";
			this._middleName.PasswordChar = '\0';
			this._middleName.Size = new System.Drawing.Size(148, 41);
			this._middleName.TabIndex = 5;
			this._middleName.ToolTip = null;
			this._middleName.Value = null;
			// 
			// _firstName
			// 
			this._firstName.LabelText = "First Name";
			this._firstName.Location = new System.Drawing.Point(182, 60);
			this._firstName.Margin = new System.Windows.Forms.Padding(2);
			this._firstName.Mask = "";
			this._firstName.Name = "_firstName";
			this._firstName.PasswordChar = '\0';
			this._firstName.Size = new System.Drawing.Size(150, 41);
			this._firstName.TabIndex = 4;
			this._firstName.ToolTip = null;
			this._firstName.Value = null;
			// 
			// _lastName
			// 
			this._lastName.LabelText = "Last Name";
			this._lastName.Location = new System.Drawing.Point(14, 60);
			this._lastName.Margin = new System.Windows.Forms.Padding(2);
			this._lastName.Mask = "";
			this._lastName.Name = "_lastName";
			this._lastName.PasswordChar = '\0';
			this._lastName.Size = new System.Drawing.Size(150, 41);
			this._lastName.TabIndex = 3;
			this._lastName.ToolTip = null;
			this._lastName.Value = null;
			// 
			// _accessionNumber
			// 
			this._accessionNumber.LabelText = "Accession #";
			this._accessionNumber.Location = new System.Drawing.Point(14, 104);
			this._accessionNumber.Margin = new System.Windows.Forms.Padding(2);
			this._accessionNumber.Mask = "";
			this._accessionNumber.Name = "_accessionNumber";
			this._accessionNumber.PasswordChar = '\0';
			this._accessionNumber.Size = new System.Drawing.Size(150, 41);
			this._accessionNumber.TabIndex = 6;
			this._accessionNumber.ToolTip = null;
			this._accessionNumber.Value = null;
			// 
			// _studyDescription
			// 
			this._studyDescription.LabelText = "Study Description";
			this._studyDescription.Location = new System.Drawing.Point(182, 104);
			this._studyDescription.Margin = new System.Windows.Forms.Padding(2);
			this._studyDescription.Mask = "";
			this._studyDescription.Name = "_studyDescription";
			this._studyDescription.PasswordChar = '\0';
			this._studyDescription.Size = new System.Drawing.Size(317, 41);
			this._studyDescription.TabIndex = 7;
			this._studyDescription.ToolTip = null;
			this._studyDescription.Value = null;
			// 
			// _studyDate
			// 
			this._studyDate.LabelText = "Study Date";
			this._studyDate.Location = new System.Drawing.Point(14, 148);
			this._studyDate.Margin = new System.Windows.Forms.Padding(2);
			this._studyDate.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
			this._studyDate.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this._studyDate.Name = "_studyDate";
			this._studyDate.Nullable = true;
			this._studyDate.Size = new System.Drawing.Size(150, 41);
			this._studyDate.TabIndex = 8;
			this._studyDate.Value = new System.DateTime(2008, 9, 4, 16, 54, 17, 515);
			// 
			// _dob
			// 
			this._dob.LabelText = "Birth Date";
			this._dob.Location = new System.Drawing.Point(183, 16);
			this._dob.Margin = new System.Windows.Forms.Padding(2);
			this._dob.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
			this._dob.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this._dob.Name = "_dob";
			this._dob.Nullable = true;
			this._dob.Size = new System.Drawing.Size(150, 41);
			this._dob.TabIndex = 1;
			this._dob.Value = new System.DateTime(2008, 9, 4, 16, 54, 17, 515);
			// 
			// _cancel
			// 
			this._cancel.Location = new System.Drawing.Point(431, 227);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(68, 23);
			this._cancel.TabIndex = 11;
			this._cancel.Text = "Cancel";
			this._cancel.UseVisualStyleBackColor = true;
			// 
			// _ok
			// 
			this._ok.Location = new System.Drawing.Point(351, 227);
			this._ok.Name = "_ok";
			this._ok.Size = new System.Drawing.Size(68, 23);
			this._ok.TabIndex = 10;
			this._ok.Text = "Ok";
			this._ok.UseVisualStyleBackColor = true;
			// 
			// _sexGroup
			// 
			this._sexGroup.Controls.Add(this._other);
			this._sexGroup.Controls.Add(this._female);
			this._sexGroup.Controls.Add(this._male);
			this._sexGroup.Location = new System.Drawing.Point(350, 16);
			this._sexGroup.Name = "_sexGroup";
			this._sexGroup.Size = new System.Drawing.Size(148, 41);
			this._sexGroup.TabIndex = 2;
			this._sexGroup.TabStop = false;
			this._sexGroup.Text = "Sex";
			// 
			// _other
			// 
			this._other.AutoSize = true;
			this._other.Location = new System.Drawing.Point(108, 18);
			this._other.Name = "_other";
			this._other.Size = new System.Drawing.Size(33, 17);
			this._other.TabIndex = 2;
			this._other.Text = "O";
			this._other.UseVisualStyleBackColor = true;
			// 
			// _female
			// 
			this._female.AutoSize = true;
			this._female.Location = new System.Drawing.Point(59, 18);
			this._female.Name = "_female";
			this._female.Size = new System.Drawing.Size(31, 17);
			this._female.TabIndex = 1;
			this._female.Text = "F";
			this._female.UseVisualStyleBackColor = true;
			// 
			// _male
			// 
			this._male.AutoSize = true;
			this._male.Location = new System.Drawing.Point(7, 18);
			this._male.Name = "_male";
			this._male.Size = new System.Drawing.Size(34, 17);
			this._male.TabIndex = 0;
			this._male.Text = "M";
			this._male.UseVisualStyleBackColor = true;
			// 
			// _studyTime
			// 
			this._studyTime.LabelText = "Study Time";
			this._studyTime.Location = new System.Drawing.Point(182, 148);
			this._studyTime.Margin = new System.Windows.Forms.Padding(2);
			this._studyTime.Maximum = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
			this._studyTime.Minimum = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this._studyTime.Name = "_studyTime";
			this._studyTime.Nullable = true;
			this._studyTime.ShowDate = false;
			this._studyTime.ShowTime = true;
			this._studyTime.Size = new System.Drawing.Size(150, 41);
			this._studyTime.TabIndex = 9;
			this._studyTime.Value = new System.DateTime(2008, 9, 4, 0, 0, 0, 0);
			// 
			// _piGroup
			// 
			this._piGroup.Controls.Add(this._piRgb);
			this._piGroup.Controls.Add(this._piMonochrome2);
			this._piGroup.Location = new System.Drawing.Point(14, 204);
			this._piGroup.Name = "_piGroup";
			this._piGroup.Size = new System.Drawing.Size(319, 46);
			this._piGroup.TabIndex = 12;
			this._piGroup.TabStop = false;
			this._piGroup.Text = "Photometric Interpretation";
			// 
			// _piRgb
			// 
			this._piRgb.AutoSize = true;
			this._piRgb.Location = new System.Drawing.Point(132, 19);
			this._piRgb.Name = "_piRgb";
			this._piRgb.Size = new System.Drawing.Size(48, 17);
			this._piRgb.TabIndex = 1;
			this._piRgb.TabStop = true;
			this._piRgb.Text = "RGB";
			this._piRgb.UseVisualStyleBackColor = true;
			// 
			// _piMonochrome2
			// 
			this._piMonochrome2.AutoSize = true;
			this._piMonochrome2.Location = new System.Drawing.Point(6, 19);
			this._piMonochrome2.Name = "_piMonochrome2";
			this._piMonochrome2.Size = new System.Drawing.Size(111, 17);
			this._piMonochrome2.TabIndex = 0;
			this._piMonochrome2.TabStop = true;
			this._piMonochrome2.Text = "MONOCHROME2";
			this._piMonochrome2.UseVisualStyleBackColor = true;
			// 
			// ImportFromBitmapComponentControl
			// 
			this.AcceptButton = this._ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancel;
			this.Controls.Add(this._piGroup);
			this.Controls.Add(this._studyTime);
			this.Controls.Add(this._sexGroup);
			this.Controls.Add(this._ok);
			this.Controls.Add(this._cancel);
			this.Controls.Add(this._dob);
			this.Controls.Add(this._studyDate);
			this.Controls.Add(this._studyDescription);
			this.Controls.Add(this._accessionNumber);
			this.Controls.Add(this._lastName);
			this.Controls.Add(this._firstName);
			this.Controls.Add(this._middleName);
			this.Controls.Add(this._patientId);
			this.Name = "ImportFromBitmapComponentControl";
			this.Size = new System.Drawing.Size(518, 262);
			this._sexGroup.ResumeLayout(false);
			this._sexGroup.PerformLayout();
			this._piGroup.ResumeLayout(false);
			this._piGroup.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private ClearCanvas.Desktop.View.WinForms.TextField _patientId;
		private ClearCanvas.Desktop.View.WinForms.TextField _middleName;
		private ClearCanvas.Desktop.View.WinForms.TextField _firstName;
		private ClearCanvas.Desktop.View.WinForms.TextField _lastName;
		private ClearCanvas.Desktop.View.WinForms.TextField _accessionNumber;
		private ClearCanvas.Desktop.View.WinForms.TextField _studyDescription;
		private ClearCanvas.Desktop.View.WinForms.DateTimeField _studyDate;
		private ClearCanvas.Desktop.View.WinForms.DateTimeField _dob;
		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.Button _ok;
		private System.Windows.Forms.GroupBox _sexGroup;
		private System.Windows.Forms.RadioButton _other;
		private System.Windows.Forms.RadioButton _female;
		private System.Windows.Forms.RadioButton _male;
		private ClearCanvas.Desktop.View.WinForms.DateTimeField _studyTime;
		private System.Windows.Forms.GroupBox _piGroup;
		private System.Windows.Forms.RadioButton _piRgb;
		private System.Windows.Forms.RadioButton _piMonochrome2;
    }
}

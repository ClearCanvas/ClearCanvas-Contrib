#region License

// Copyright © 2008-2010 Northwestern University

// Authors:
// Vladimir Kleper
// Northwestern University
// Feinberg School of Medicine
// Department of Radiology
// Imaging Informatics Section
// Chicago, IL

// The Academic Free License v. 2.1

// This Academic Free License (the "License") applies to any original work of
// authorship (the "Original Work") whose owner (the "Licensor") has placed the
// following notice immediately following the copyright notice for the Original
// Work:

// Licensed under the Academic Free License version 2.1

// 1) Grant of Copyright License. Licensor hereby grants You a world-wide, royalty-
// free, non-exclusive, perpetual, sublicenseable license to do the following:

// a) to reproduce the Original Work in copies;

// b) to prepare derivative works ("Derivative Works") based upon the Original Work;

// c) to distribute copies of the Original Work and Derivative Works to the public;

// d) to perform the Original Work publicly; and

// e) to display the Original Work publicly.

// 2) Grant of Patent License. Licensor hereby grants You a world-wide, royalty-
// free, non-exclusive, perpetual, sublicenseable license, under patent claims
// owned or controlled by the Licensor that are embodied in the Original Work as
// furnished by the Licensor, to make, use, sell and offer for sale the Original
// Work and Derivative Works.

// 3) Grant of Source Code License. The term "Source Code" means the preferred form
// of the Original Work for making modifications to it and all available
// documentation describing how to modify the Original Work. Licensor hereby agrees
// to provide a machine-readable copy of the Source Code of the Original Work along
// with each copy of the Original Work that Licensor distributes. Licensor reserves
// the right to satisfy this obligation by placing a machine-readable copy of the
// Source Code in an information repository reasonably calculated to permit
// inexpensive and convenient access by You for as long as Licensor continues to
// distribute the Original Work, and by publishing the address of that information
// repository in a notice immediately following the copyright notice that applies
// to the Original Work.

// 4) Exclusions From License Grant. Neither the names of Licensor, nor the names
// of any contributors to the Original Work, nor any of their trademarks or service
// marks, may be used to endorse or promote products derived from this Original
// Work without express prior written permission of the Licensor. Nothing in this
// License shall be deemed to grant any rights to trademarks, copyrights, patents,
// trade secrets or any other intellectual property of Licensor except as expressly
// stated herein. No patent license is granted to make, use, sell or offer to sell
// embodiments of any patent claims other than the licensed claims defined in
// Section 2. No right is granted to the trademarks of Licensor even if such marks
// are included in the Original Work. Nothing in this License shall be interpreted
// to prohibit Licensor from licensing under different terms from this License any
// Original Work that Licensor otherwise would have a right to license.

// 5) This section intentionally omitted.

// 6) Attribution Rights. You must retain, in the Source Code of any Derivative
// Works that You create, all copyright, patent or trademark notices from the
// Source Code of the Original Work, as well as any notices of licensing and any
// descriptive text identified therein as an "Attribution Notice." You must cause
// the Source Code for any Derivative Works that You create to carry a prominent
// Attribution Notice reasonably calculated to inform recipients that You have
// modified the Original Work.

// 7) Warranty of Provenance and Disclaimer of Warranty. Licensor warrants that the
// copyright in and to the Original Work and the patent rights granted herein by
// Licensor are owned by the Licensor or are sublicensed to You under the terms of
// this License with the permission of the contributor(s) of those copyrights and
// patent rights. Except as expressly stated in the immediately proceeding
// sentence, the Original Work is provided under this License on an "AS IS" BASIS
// and WITHOUT WARRANTY, either express or implied, including, without limitation,
// the warranties of NON-INFRINGEMENT, MERCHANTABILITY or FITNESS FOR A PARTICULAR
// PURPOSE. THE ENTIRE RISK AS TO THE QUALITY OF THE ORIGINAL WORK IS WITH YOU.
// This DISCLAIMER OF WARRANTY constitutes an essential part of this License. No
// license to Original Work is granted hereunder except under this disclaimer.

// 8) Limitation of Liability. Under no circumstances and under no legal theory,
// whether in tort (including negligence), contract, or otherwise, shall the
// Licensor be liable to any person for any direct, indirect, special, incidental,
// or consequential damages of any character arising as a result of this License or
// the use of the Original Work including, without limitation, damages for loss of
// goodwill, work stoppage, computer failure or malfunction, or any and all other
// commercial damages or losses. This limitation of liability shall not apply to
// liability for death or personal injury resulting from Licensor's negligence to
// the extent applicable law prohibits such limitation. Some jurisdictions do not
// allow the exclusion or limitation of incidental or consequential damages, so
// this exclusion and limitation may not apply to You.

// 9) Acceptance and Termination. If You distribute copies of the Original Work or
// a Derivative Work, You must make a reasonable effort under the circumstances to
// obtain the express assent of recipients to the terms of this License. Nothing
// else but this License (or another written agreement between Licensor and You)
// grants You permission to create Derivative Works based upon the Original Work or
// to exercise any of the rights granted in Section 1 herein, and any attempt to do
// so except under the terms of this License (or another written agreement between
// Licensor and You) is expressly prohibited by U.S. copyright law, the equivalent
// laws of other countries, and by international treaty. Therefore, by exercising
// any of the rights granted to You in Section 1 herein, You indicate Your
// acceptance of this License and all of its terms and conditions.

// 10) Termination for Patent Action. This License shall terminate automatically
// and You may no longer exercise any of the rights granted to You by this License
// as of the date You commence an action, including a cross-claim or counterclaim,
// against Licensor or any licensee alleging that the Original Work infringes a
// patent. This termination provision shall not apply for an action alleging patent
// infringement by combinations of the Original Work with other software or 
// hardware.

// 11) Jurisdiction, Venue and Governing Law. Any action or suit relating to this
// License may be brought only in the courts of a jurisdiction wherein the Licensor
// resides or in which Licensor conducts its primary business, and under the laws
// of that jurisdiction excluding its conflict-of-law provisions. The application
// of the United Nations Convention on Contracts for the International Sale of
// Goods is expressly excluded. Any use of the Original Work outside the scope of
// this License or after its termination shall be subject to the requirements and
// penalties of the U.S. Copyright Act, 17 U.S.C. § 101 et seq., the equivalent
// laws of other countries, and international treaty. This section shall survive
// the termination of this License.

// 12) Attorneys Fees. In any action to enforce the terms of this License or
// seeking damages relating thereto, the prevailing party shall be entitled to
// recover its costs and expenses, including, without limitation, reasonable
// attorneys' fees and costs incurred in connection with such action, including any
// appeal of such action. This section shall survive the termination of this
// License.

// 13) Miscellaneous. This License represents the complete agreement concerning the
// subject matter hereof. If any provision of this License is held to be
// unenforceable, such provision shall be reformed only to the extent necessary to
// make it enforceable.

// 14) Definition of "You" in This License. "You" throughout this License, whether
// in upper or lower case, means an individual or a legal entity exercising rights
// under, and complying with all of the terms of, this License. For legal entities,
// "You" includes any entity that controls, is controlled by, or is under common
// control with you. For purposes of this definition, "control" means (i) the
// power, direct or indirect, to cause the direction or management of such entity,
// whether by contract or otherwise, or (ii) ownership of fifty percent (50%) or
// more of the outstanding shares, or (iii) beneficial ownership of such entity.

// 15) Right to Use. You may use the Original Work in all ways not otherwise
// restricted or conditioned by this License or by law, and Licensor promises not
// to interfere with or be responsible for such uses by You.

// This license is Copyright (C) 2003-2004 Lawrence E. Rosen. All rights reserved.
// Permission is hereby granted to copy and distribute this license without
// modification. This license may not be modified without the express written
// permission of its copyright owner.

#endregion

namespace AIM.Annotation.View.WinForms
{
	partial class AimDetailsControl
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
			this.components = new System.ComponentModel.Container();
			this._groupBox = new System.Windows.Forms.GroupBox();
			this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this._flowPanelIo = new System.Windows.Forms.FlowLayoutPanel();
			this._lblIo = new System.Windows.Forms.Label();
			this._lblAe = new System.Windows.Forms.Label();
			this._flowPanelAe = new System.Windows.Forms.FlowLayoutPanel();
			this._linkOpenImageAnnotation = new System.Windows.Forms.LinkLabel();
			this._lblAimName = new System.Windows.Forms.Label();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this._groupBox.SuspendLayout();
			this._tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _groupBox
			// 
			this._groupBox.AutoSize = true;
			this._groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._groupBox.BackColor = System.Drawing.Color.Transparent;
			this._groupBox.Controls.Add(this._tableLayoutPanel);
			this._groupBox.Controls.Add(this._linkOpenImageAnnotation);
			this._groupBox.Controls.Add(this._lblAimName);
			this._groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._groupBox.Location = new System.Drawing.Point(0, 0);
			this._groupBox.Name = "_groupBox";
			this._groupBox.Size = new System.Drawing.Size(134, 97);
			this._groupBox.TabIndex = 0;
			this._groupBox.TabStop = false;
			// 
			// _tableLayoutPanel
			// 
			this._tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._tableLayoutPanel.AutoSize = true;
			this._tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this._tableLayoutPanel.ColumnCount = 2;
			this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tableLayoutPanel.Controls.Add(this._flowPanelIo, 1, 1);
			this._tableLayoutPanel.Controls.Add(this._lblIo, 0, 1);
			this._tableLayoutPanel.Controls.Add(this._lblAe, 0, 0);
			this._tableLayoutPanel.Controls.Add(this._flowPanelAe, 1, 0);
			this._tableLayoutPanel.Location = new System.Drawing.Point(3, 35);
			this._tableLayoutPanel.Name = "_tableLayoutPanel";
			this._tableLayoutPanel.RowCount = 2;
			this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayoutPanel.Size = new System.Drawing.Size(125, 37);
			this._tableLayoutPanel.TabIndex = 2;
			this._tableLayoutPanel.SizeChanged += new System.EventHandler(this.OnTableLayoutPanelSizeChanged);
			// 
			// _flowPanelIo
			// 
			this._flowPanelIo.AutoSize = true;
			this._flowPanelIo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._flowPanelIo.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flowPanelIo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this._flowPanelIo.Location = new System.Drawing.Point(41, 22);
			this._flowPanelIo.Name = "_flowPanelIo";
			this._flowPanelIo.Size = new System.Drawing.Size(80, 11);
			this._flowPanelIo.TabIndex = 3;
			this._flowPanelIo.WrapContents = false;
			this._flowPanelIo.ClientSizeChanged += new System.EventHandler(this.OnFlowPanelIoClientSizeChanged);
			// 
			// _lblIo
			// 
			this._lblIo.AutoSize = true;
			this._lblIo.Location = new System.Drawing.Point(4, 19);
			this._lblIo.Name = "_lblIo";
			this._lblIo.Size = new System.Drawing.Size(26, 17);
			this._lblIo.TabIndex = 2;
			this._lblIo.Text = "IO:";
			this._lblIo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._toolTip.SetToolTip(this._lblIo, "Imaging Observations");
			// 
			// _lblAe
			// 
			this._lblAe.AutoSize = true;
			this._lblAe.Location = new System.Drawing.Point(4, 1);
			this._lblAe.Name = "_lblAe";
			this._lblAe.Size = new System.Drawing.Size(30, 17);
			this._lblAe.TabIndex = 0;
			this._lblAe.Text = "AE:";
			this._lblAe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._toolTip.SetToolTip(this._lblAe, "Anatomic Enities");
			// 
			// _flowPanelAe
			// 
			this._flowPanelAe.AutoSize = true;
			this._flowPanelAe.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flowPanelAe.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this._flowPanelAe.Location = new System.Drawing.Point(41, 4);
			this._flowPanelAe.Name = "_flowPanelAe";
			this._flowPanelAe.Size = new System.Drawing.Size(80, 11);
			this._flowPanelAe.TabIndex = 1;
			this._flowPanelAe.WrapContents = false;
			this._flowPanelAe.ClientSizeChanged += new System.EventHandler(this.OnFlowPanelAeClientSizeChanged);
			// 
			// _linkOpenImageAnnotation
			// 
			this._linkOpenImageAnnotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._linkOpenImageAnnotation.AutoSize = true;
			this._linkOpenImageAnnotation.Location = new System.Drawing.Point(46, 75);
			this._linkOpenImageAnnotation.Name = "_linkOpenImageAnnotation";
			this._linkOpenImageAnnotation.Size = new System.Drawing.Size(82, 17);
			this._linkOpenImageAnnotation.TabIndex = 3;
			this._linkOpenImageAnnotation.TabStop = true;
			this._linkOpenImageAnnotation.Text = "Show Study";
			this._linkOpenImageAnnotation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnOpenAnnotationLinkClicked);
			// 
			// _lblAimName
			// 
			this._lblAimName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._lblAimName.AutoEllipsis = true;
			this._lblAimName.Location = new System.Drawing.Point(6, 15);
			this._lblAimName.Name = "_lblAimName";
			this._lblAimName.Size = new System.Drawing.Size(122, 17);
			this._lblAimName.TabIndex = 0;
			this._lblAimName.Text = "AIM Name:";
			// 
			// _toolTip
			// 
			this._toolTip.ShowAlways = true;
			// 
			// AimDetailsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this._groupBox);
			this.DoubleBuffered = true;
			this.Name = "AimDetailsControl";
			this.Size = new System.Drawing.Size(134, 97);
			this._groupBox.ResumeLayout(false);
			this._groupBox.PerformLayout();
			this._tableLayoutPanel.ResumeLayout(false);
			this._tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox _groupBox;
		private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
		private System.Windows.Forms.FlowLayoutPanel _flowPanelIo;
		private System.Windows.Forms.Label _lblIo;
		private System.Windows.Forms.ToolTip _toolTip;
		private System.Windows.Forms.Label _lblAe;
		private System.Windows.Forms.FlowLayoutPanel _flowPanelAe;
		private System.Windows.Forms.LinkLabel _linkOpenImageAnnotation;
		private System.Windows.Forms.Label _lblAimName;

	}
}
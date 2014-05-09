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
    partial class ImagingObservationCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagingObservationCtrl));
            this.panelImagingObservation = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnDeleteImagingObservation = new System.Windows.Forms.ToolStripButton();
            this.btnAddImagingObservation = new System.Windows.Forms.ToolStripButton();
            this.mainTree = new AIM.Annotation.View.WinForms.DropDownTreeView();
            this.menuImagingObservation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemAddImagingObs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeleteImagingObs = new System.Windows.Forms.ToolStripMenuItem();
            this.lblImagingObservation = new System.Windows.Forms.Label();
            this.treeImagingObservation = new System.Windows.Forms.TreeView();
            this.menuImagingObsCharacteristic = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblImagingObsCharacteristic = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelImagingObservation.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.menuImagingObservation.SuspendLayout();
            this.menuImagingObsCharacteristic.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelImagingObservation
            // 
            this.panelImagingObservation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelImagingObservation.BackColor = System.Drawing.Color.Transparent;
            this.panelImagingObservation.ColumnCount = 3;
            this.panelImagingObservation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.06278F));
            this.panelImagingObservation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.02242F));
            this.panelImagingObservation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.9148F));
            this.panelImagingObservation.Controls.Add(this.toolStrip, 1, 0);
            this.panelImagingObservation.Controls.Add(this.mainTree, 0, 1);
            this.panelImagingObservation.Controls.Add(this.lblImagingObservation, 0, 0);
            this.panelImagingObservation.Controls.Add(this.treeImagingObservation, 2, 1);
            this.panelImagingObservation.Controls.Add(this.lblImagingObsCharacteristic, 2, 0);
            this.panelImagingObservation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImagingObservation.Location = new System.Drawing.Point(0, 0);
            this.panelImagingObservation.Margin = new System.Windows.Forms.Padding(4);
            this.panelImagingObservation.Name = "panelImagingObservation";
            this.panelImagingObservation.RowCount = 2;
            this.panelImagingObservation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.panelImagingObservation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelImagingObservation.Size = new System.Drawing.Size(613, 120);
            this.panelImagingObservation.TabIndex = 3;
            // 
            // toolStrip
            // 
            this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip.CanOverflow = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDeleteImagingObservation,
            this.btnAddImagingObservation});
            this.toolStrip.Location = new System.Drawing.Point(232, 2);
            this.toolStrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.toolStrip.MinimumSize = new System.Drawing.Size(54, 47);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip.Size = new System.Drawing.Size(54, 47);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStrip_Paint);
            // 
            // btnDeleteImagingObservation
            // 
            this.btnDeleteImagingObservation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteImagingObservation.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteImagingObservation.Image")));
            this.btnDeleteImagingObservation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteImagingObservation.Name = "btnDeleteImagingObservation";
            this.btnDeleteImagingObservation.Size = new System.Drawing.Size(23, 44);
            this.btnDeleteImagingObservation.Text = "Delete Selected Imaging Observation";
            this.btnDeleteImagingObservation.Click += new System.EventHandler(this.btnDeleteImagingObservation_Click);
            // 
            // btnAddImagingObservation
            // 
            this.btnAddImagingObservation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddImagingObservation.Image = ((System.Drawing.Image)(resources.GetObject("btnAddImagingObservation.Image")));
            this.btnAddImagingObservation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddImagingObservation.Name = "btnAddImagingObservation";
            this.btnAddImagingObservation.Size = new System.Drawing.Size(23, 40);
            this.btnAddImagingObservation.Text = "Add Imaging Observation";
            this.btnAddImagingObservation.Click += new System.EventHandler(this.btnAddImagingObservation_Click);
            // 
            // mainTree
            // 
            this.mainTree.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelImagingObservation.SetColumnSpan(this.mainTree, 2);
            this.mainTree.ContextMenuStrip = this.menuImagingObservation;
            this.mainTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTree.HideSelection = false;
            this.mainTree.HotTracking = true;
            this.mainTree.Location = new System.Drawing.Point(0, 49);
            this.mainTree.Margin = new System.Windows.Forms.Padding(0);
            this.mainTree.Name = "mainTree";
            this.mainTree.ShowNodeToolTips = true;
            this.mainTree.ShowPlusMinus = false;
            this.mainTree.ShowRootLines = false;
            this.mainTree.Size = new System.Drawing.Size(288, 71);
            this.mainTree.TabIndex = 2;
            this.mainTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mainTree_AfterSelect);
            this.mainTree.NodeValueChanged += new System.EventHandler(this.mainTree_NodeValueChanged);
            // 
            // menuImagingObservation
            // 
            this.menuImagingObservation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddImagingObs,
            this.menuItemDeleteImagingObs});
            this.menuImagingObservation.Name = "menuImagingObservation";
            this.menuImagingObservation.Size = new System.Drawing.Size(333, 48);
            // 
            // menuItemAddImagingObs
            // 
            this.menuItemAddImagingObs.Name = "menuItemAddImagingObs";
            this.menuItemAddImagingObs.Size = new System.Drawing.Size(332, 22);
            this.menuItemAddImagingObs.Text = "A&dd Imaging Observation";
            this.menuItemAddImagingObs.Click += new System.EventHandler(this.menuItemAddImagingObs_Click);
            // 
            // menuItemDeleteImagingObs
            // 
            this.menuItemDeleteImagingObs.Name = "menuItemDeleteImagingObs";
            this.menuItemDeleteImagingObs.Size = new System.Drawing.Size(332, 22);
            this.menuItemDeleteImagingObs.Text = "&Delete Selected Imaging Observation";
            this.menuItemDeleteImagingObs.Click += new System.EventHandler(this.menuItemDeleteImagingObs_Click);
            // 
            // lblImagingObservation
            // 
            this.lblImagingObservation.AutoSize = true;
            this.lblImagingObservation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImagingObservation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImagingObservation.Location = new System.Drawing.Point(2, 2);
            this.lblImagingObservation.Margin = new System.Windows.Forms.Padding(2);
            this.lblImagingObservation.Name = "lblImagingObservation";
            this.lblImagingObservation.Size = new System.Drawing.Size(192, 45);
            this.lblImagingObservation.TabIndex = 3;
            this.lblImagingObservation.Text = "Imaging Observation:";
            this.lblImagingObservation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeImagingObservation
            // 
            this.treeImagingObservation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.treeImagingObservation.CheckBoxes = true;
            this.treeImagingObservation.ContextMenuStrip = this.menuImagingObsCharacteristic;
            this.treeImagingObservation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeImagingObservation.Location = new System.Drawing.Point(288, 49);
            this.treeImagingObservation.Margin = new System.Windows.Forms.Padding(0);
            this.treeImagingObservation.Name = "treeImagingObservation";
            this.treeImagingObservation.ShowLines = false;
            this.treeImagingObservation.ShowPlusMinus = false;
            this.treeImagingObservation.ShowRootLines = false;
            this.treeImagingObservation.Size = new System.Drawing.Size(325, 71);
            this.treeImagingObservation.TabIndex = 4;
            this.treeImagingObservation.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeImagingObservation_AfterCheck);
            // 
            // menuImagingObsCharacteristic
            // 
            this.menuImagingObsCharacteristic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem});
            this.menuImagingObsCharacteristic.Name = "menuImagingObsCharacteristic";
            this.menuImagingObsCharacteristic.ShowImageMargin = false;
            this.menuImagingObsCharacteristic.Size = new System.Drawing.Size(140, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.selectAllToolStripMenuItem_Paint);
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.unselectAllToolStripMenuItem.Text = "&Unselect All";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // lblImagingObsCharacteristic
            // 
            this.lblImagingObsCharacteristic.AutoSize = true;
            this.lblImagingObsCharacteristic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImagingObsCharacteristic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImagingObsCharacteristic.Location = new System.Drawing.Point(290, 2);
            this.lblImagingObsCharacteristic.Margin = new System.Windows.Forms.Padding(2);
            this.lblImagingObsCharacteristic.Name = "lblImagingObsCharacteristic";
            this.lblImagingObsCharacteristic.Size = new System.Drawing.Size(321, 45);
            this.lblImagingObsCharacteristic.TabIndex = 5;
            this.lblImagingObsCharacteristic.Text = "Imaging Observation Characteristic:";
            this.lblImagingObsCharacteristic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblImagingObsCharacteristic.MouseLeave += new System.EventHandler(this.lblImagingObsCharacteristic_MouseLeave);
            this.lblImagingObsCharacteristic.MouseEnter += new System.EventHandler(this.lblImagingObsCharacteristic_MouseEnter);
            // 
            // toolTip
            // 
            this.toolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip_Popup);
            // 
            // ImagingObservationCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panelImagingObservation);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(430, 70);
            this.Name = "ImagingObservationCtrl";
            this.Size = new System.Drawing.Size(613, 120);
            this.panelImagingObservation.ResumeLayout(false);
            this.panelImagingObservation.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuImagingObservation.ResumeLayout(false);
            this.menuImagingObsCharacteristic.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AIM.Annotation.View.WinForms.DropDownTreeView mainTree;
        private System.Windows.Forms.TableLayoutPanel panelImagingObservation;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnDeleteImagingObservation;
        private System.Windows.Forms.ToolStripButton btnAddImagingObservation;
        private System.Windows.Forms.Label lblImagingObservation;
        private System.Windows.Forms.TreeView treeImagingObservation;
        private System.Windows.Forms.Label lblImagingObsCharacteristic;
        private System.Windows.Forms.ContextMenuStrip menuImagingObsCharacteristic;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip menuImagingObservation;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddImagingObs;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteImagingObs;
    }
}

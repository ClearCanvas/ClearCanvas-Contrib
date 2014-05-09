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

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using AIMOntologyXML.CS.aim;
using ClearCanvas.Common.Utilities;

namespace AIM.Annotation.View.WinForms
{
    public partial class ImagingObservationCtrl : UserControl
    {
        // This struct stores current selection in both trees
        struct ObservationSelection
        {
            public ObservationSelection(ImagingObservationXML imagingObservation)
            {
                this.xmlImagingObservation = imagingObservation;
                this.selectionIndecies = new List<int>();
            }
            public ImagingObservationXML xmlImagingObservation;
            public List<int> selectionIndecies;
        }

        private string _xmlDataSource;
        private AIMOntology _xmlDoc;

        // Temporary need these two while Imaging Observation Characteristis are disconnected from Imaging Observations
        private string _xmlCharacteristicsDataSource; // Imaging Observation Characteristics data source
        private AIMOntology _xmlCharacteristicsDoc;

        // Items are added/removed to/from this tree as ImagingObservations are added/removed in the mainTree
        private List<ObservationSelection> _selectedImagingObservations = new List<ObservationSelection>();

        private const string _select_text = "<Select from list>"; // default text for all dropdowns

        [Description("Occurs when the value of Imaging Observation or its Imaging Observation Characteristic is changed")]
        public event EventHandler SelectedImagingObservationChanged;

        public ImagingObservationCtrl()
        {
            InitializeComponent();
        }

        // Control is populated with data by setting this property
        public string DataSource
        {
            get { return _xmlDataSource; }
            set
            {
                if (_xmlDataSource != value)
                {
                    _xmlDataSource = value;
                    this.ResetTree();
                }
            }
        }

        public string ImagingObsCharacteristicDataSource
        {
            get { return _xmlCharacteristicsDataSource; }
            set
            {
                if (_xmlCharacteristicsDataSource != value)
                {
                    _xmlCharacteristicsDataSource = value;

                    this.ResetTree();
                    //if (treeImagingObservation.IsHandleCreated)
                    //    treeImagingObservation.Nodes.Clear();
                    //_selectedImagingObservations.Clear();

                    //if (string.IsNullOrEmpty(_xmlCharacteristicsDataSource))
                    //{
                    //    _xmlCharacteristicsDoc = null;
                    //}
                    //else
                    //{
                    //    _xmlCharacteristicsDoc = AIMOntology.LoadFromString(_xmlCharacteristicsDataSource);
                    //}
                }
            }
        }

        public void Reset()
        {
            this.ResetTree();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<aim_dotnet.ImagingObservation> SelectedImagingObservations
        {
            get
            {
                List<aim_dotnet.ImagingObservation> imagingObservations = new List<aim_dotnet.ImagingObservation>();

                // Imaging Observation Collection
                foreach (ObservationSelection selectedValues in _selectedImagingObservations)
                {
                    if (selectedValues.xmlImagingObservation.Node != null)
                    {
                        aim_dotnet.ImagingObservation imagingObservation = new aim_dotnet.ImagingObservation();

                        imagingObservation.CodeValue = selectedValues.xmlImagingObservation.codeValue.Exists ? selectedValues.xmlImagingObservation.codeValue.First.Value : string.Empty;
                        imagingObservation.CodeMeaning = selectedValues.xmlImagingObservation.codeMeaning.Exists ? selectedValues.xmlImagingObservation.codeMeaning.First.Value : string.Empty;
                        imagingObservation.CodingSchemeDesignator = selectedValues.xmlImagingObservation.codingSchemeDesignator.Exists ? selectedValues.xmlImagingObservation.codingSchemeDesignator.First.Value : string.Empty;
                        imagingObservation.Comment = string.Empty; // no value for now

                        // Imaging Observation Characteristic Collection
                        if (selectedValues.xmlImagingObservation.imagingObservationCharacteristicCollectionXML.Exists)
                        {
                            imagingObservation.ImagingObservationCharacteristicCollection = new List<aim_dotnet.ImagingObservationCharacteristic>();

                            imagingObservationCharacteristicCollectionXMLType oldXmlImagingObsCharCollection = selectedValues.xmlImagingObservation.imagingObservationCharacteristicCollectionXML.First;
                            foreach (int selectedImgObsCharIndex in selectedValues.selectionIndecies)
                            {
                                // Check whether the selected imaging observation existed in the old (source) collection
                                if (selectedImgObsCharIndex > -1 && selectedImgObsCharIndex < oldXmlImagingObsCharCollection.ImagingObservationCharacteristicXML2.Count)
                                {
                                    aim_dotnet.ImagingObservationCharacteristic imagingObsCharacteristic = new aim_dotnet.ImagingObservationCharacteristic();

                                    // Imaging Observation Characteristic
                                    ImagingObservationCharacteristicXML oldXmlImagingObsChar = oldXmlImagingObsCharCollection.ImagingObservationCharacteristicXML2[selectedImgObsCharIndex];
                                    imagingObsCharacteristic.CodeValue = oldXmlImagingObsChar.codeValue.Exists ? oldXmlImagingObsChar.codeValue.First.Value : string.Empty;
                                    imagingObsCharacteristic.CodeMeaning = oldXmlImagingObsChar.codeMeaning.Exists ? oldXmlImagingObsChar.codeMeaning.First.Value : string.Empty;
                                    imagingObsCharacteristic.CodingSchemeDesignator = oldXmlImagingObsChar.codingSchemeDesignator.Exists ? oldXmlImagingObsChar.codingSchemeDesignator.First.Value : string.Empty;
                                    imagingObsCharacteristic.Comment = string.Empty; // no value for now

                                    imagingObservation.ImagingObservationCharacteristicCollection.Add(imagingObsCharacteristic);
                                }
                            }
                        }
                        imagingObservations.Add(imagingObservation);
                    }
                }

                return imagingObservations;
            }

            set
            {
                // Not implemented

				// Quick and dirty - do control reset only
				if (value == null || value.Count == 0)
				{
					this.ResetTree();
				}
			}
        }

        public void GetSelectedValues(AIMOntology outputDoc)
        {
            AnnotationDescriptionXML xmlAnnotationDescription = outputDoc.AnnotationDescriptionXML2.Exists ?
                outputDoc.AnnotationDescriptionXML2.First : outputDoc.AnnotationDescriptionXML2.Append();

            // Imaging Observation Collection
            imagingObservationCollectionXMLType xmlImagingObsCollection = xmlAnnotationDescription.imagingObservationCollectionXML.Exists ?
                xmlAnnotationDescription.imagingObservationCollectionXML.First : xmlAnnotationDescription.imagingObservationCollectionXML.Append();

            foreach (ObservationSelection selectedValues in _selectedImagingObservations)
            {
                if (selectedValues.xmlImagingObservation.Node != null)
                {
                    // Imaging Observation
                    ImagingObservationXML xmlImagingObservation = xmlImagingObsCollection.ImagingObservationXML2.Append();
                    xmlImagingObservation.codeValue.Append().Value = selectedValues.xmlImagingObservation.codeValue.Exists ? selectedValues.xmlImagingObservation.codeValue.First.Value : string.Empty;
                    xmlImagingObservation.codeMeaning.Append().Value = selectedValues.xmlImagingObservation.codeMeaning.Exists ? selectedValues.xmlImagingObservation.codeMeaning.First.Value : string.Empty;
                    xmlImagingObservation.codingSchemeDesignator.Append().Value = selectedValues.xmlImagingObservation.codingSchemeDesignator.Exists ? selectedValues.xmlImagingObservation.codingSchemeDesignator.First.Value : string.Empty;
                    xmlImagingObservation.comment.Append().Value = string.Empty; // no value for now

                    // Imaging Observation Characteristic Collection
                    imagingObservationCharacteristicCollectionXMLType xmlImgObsCharCollection = xmlImagingObservation.imagingObservationCharacteristicCollectionXML.Append();
                    if (selectedValues.xmlImagingObservation.imagingObservationCharacteristicCollectionXML.Exists)
                    {
                        imagingObservationCharacteristicCollectionXMLType oldXmlImagingObsCharCollection = selectedValues.xmlImagingObservation.imagingObservationCharacteristicCollectionXML.First;
                        foreach (int selectedImgObsCharIndex in selectedValues.selectionIndecies)
                        {
                            // Check whether the selected imaging observation existed in the old (source) collection
                            if (selectedImgObsCharIndex > -1 && selectedImgObsCharIndex < oldXmlImagingObsCharCollection.ImagingObservationCharacteristicXML2.Count)
                            {
                                // Imaging Observation Characteristic
                                ImagingObservationCharacteristicXML oldXmlImagingObsChar = oldXmlImagingObsCharCollection.ImagingObservationCharacteristicXML2[selectedImgObsCharIndex];
                                ImagingObservationCharacteristicXML xmlImagingObsChar = xmlImgObsCharCollection.ImagingObservationCharacteristicXML2.Append();
                                xmlImagingObsChar.codeValue.Append().Value = oldXmlImagingObsChar.codeValue.Exists ? oldXmlImagingObsChar.codeValue.First.Value : string.Empty;
                                xmlImagingObsChar.codeMeaning.Append().Value = oldXmlImagingObsChar.codeMeaning.Exists ? oldXmlImagingObsChar.codeMeaning.First.Value : string.Empty;
                                xmlImagingObsChar.codingSchemeDesignator.Append().Value = oldXmlImagingObsChar.codingSchemeDesignator.Exists ? oldXmlImagingObsChar.codingSchemeDesignator.First.Value : string.Empty;
                                xmlImagingObsChar.comment.Append().Value = string.Empty; // no value for now
                            }
                        }
                    }
                }
            }
        }

        protected void ResetTree()
        {
            this.SuspendLayout();

            // Clear the control
            mainTree.Nodes.Clear();
            _selectedImagingObservations.Clear();
            _xmlDoc = null;

            // Temp Imaging Characteristis
            _xmlCharacteristicsDoc = null;
            if (!string.IsNullOrEmpty(_xmlCharacteristicsDataSource))
                _xmlCharacteristicsDoc = AIMOntology.LoadFromString(_xmlCharacteristicsDataSource);


            if (string.IsNullOrEmpty(_xmlDataSource))
            {
                //this.AddNoDataNode(mainTree.Nodes);
            }
            else
            {
                //_xmlDoc = AIMOntology.LoadFromFile(_xmlDataSource);  // load xml from the given file
                _xmlDoc = AIMOntology.LoadFromString(_xmlDataSource); // load xml from the given string

                //this.AddRootImagingObservationNode();
            }

            this.AddRootImagingObservationNode();

            this.ResumeLayout();
            this.Update();
        }

        protected void UpdateEnabledStates()
        {
            this.menuItemDeleteImagingObs.Enabled = mainTree.SelectedNode != null;
            this.btnDeleteImagingObservation.Enabled = mainTree.SelectedNode != null;
        }

        protected void AddRootImagingObservationNode()
        {
            if (_xmlDoc != null && _xmlDoc.AnnotationDescriptionXML2.Exists && _xmlDoc.AnnotationDescriptionXML2.First.imagingObservationCollectionXML.Exists)
            {
                DropDownTreeNode node = new DropDownTreeNode(_select_text);
                int i = node.ComboBox.Items.Add(new ComboItem(_select_text));
                node.ComboBox.SelectedIndex = i;
                foreach (ImagingObservationXML xmlImagingObservation in _xmlDoc.AnnotationDescriptionXML2.First.imagingObservationCollectionXML.First.ImagingObservationXML2)
                {
                    if (xmlImagingObservation.codeMeaning.Exists)
                    {
                        ComboItem comboItem = new ComboItem(xmlImagingObservation.codeMeaning.First.Value, xmlImagingObservation);
                        node.ComboBox.Items.Add(comboItem);
                    }
                }
                int nodeIdx = mainTree.Nodes.Add(node);
                _selectedImagingObservations.Add( new ObservationSelection(new ImagingObservationXML(null)));
                System.Diagnostics.Debug.Assert(_selectedImagingObservations.Count == nodeIdx + 1);

                // Set initial selection
                if (mainTree.SelectedNode == null)
                    mainTree.SelectedNode = mainTree.Nodes[nodeIdx];
            }
            else if (mainTree.Nodes.Count == 0)
            {
                this.AddNoDataNode(mainTree.Nodes);
                _selectedImagingObservations.Clear();
            }

            this.UpdateEnabledStates();
        }

        protected void AddNoDataNode(TreeNodeCollection parentNodeCollection)
        {
            TreeNode node = new TreeNode("<No data available>");
            node.ForeColor = Color.LightGray;
            parentNodeCollection.Add(node);
        }

        private void btnAddImagingObservation_Click(object sender, EventArgs e)
        {
            this.AddRootImagingObservationNode();
        }

        private void btnDeleteImagingObservation_Click(object sender, EventArgs e)
        {
            this.DeleteImagingObservation();
        }

        private void DeleteImagingObservation()
        {
            if (mainTree.SelectedNode != null)
            {
                TreeNode current = mainTree.SelectedNode;
                while (current.Parent != null)
                    current = current.Parent;

                int nodeIdx = GetTreeNodeIndex(mainTree, current);
                mainTree.Nodes.Remove(current);
                if (nodeIdx > -1)
                {
                    System.Diagnostics.Debug.Assert(nodeIdx < _selectedImagingObservations.Count);
                    _selectedImagingObservations.RemoveAt(nodeIdx);
                }

                this.UpdateTreeImagingObservation();

                this.UpdateEnabledStates();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(TreeNode node in treeImagingObservation.Nodes)
                node.Checked = true;
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(TreeNode node in treeImagingObservation.Nodes)
                node.Checked = false;
        }

        private void mainTree_NodeValueChanged(object sender, EventArgs e)
        {
            DropDownTreeNode node = mainTree.SelectedNode as DropDownTreeNode;

            if (node != null)
            {
                int nodeIdx = this.GetTreeNodeIndex(mainTree, node);
                if (nodeIdx > -1)
                {
                    ComboItem comboItem = node.ComboBox.SelectedItem as ComboItem; // Get saved node from the ComboItem
                    if (null == comboItem)
                    {
                        _selectedImagingObservations[nodeIdx] = new ObservationSelection(new ImagingObservationXML(null));
                    }
                    else
                    {
                        ImagingObservationXML selectedImagingObservationXML = comboItem.TagValue as ImagingObservationXML;
                        if (selectedImagingObservationXML != null)
                        {
                            if (_selectedImagingObservations[nodeIdx].xmlImagingObservation.Node == null || 
                               !_selectedImagingObservations[nodeIdx].xmlImagingObservation.codeValue.Exists ||
                               !_selectedImagingObservations[nodeIdx].xmlImagingObservation.codeValue.First.Value.Equals(comboItem.TagValue))
                            {
                                _selectedImagingObservations[nodeIdx] = new ObservationSelection(new ImagingObservationXML(selectedImagingObservationXML.Node));
                            }
                        }
                        else
                        {
                            _selectedImagingObservations[nodeIdx] = new ObservationSelection(new ImagingObservationXML(null));
                        }
                    }
                }
            }

            this.UpdateTreeImagingObservation();
        }

        private void mainTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.UpdateTreeImagingObservation();
        }

        private void treeImagingObservation_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int nodeIdx = GetTreeNodeIndex(treeImagingObservation, e.Node);
            if (nodeIdx > -1)
            {
                int mainNodeIdx = GetTreeNodeIndex(mainTree, mainTree.SelectedNode);
                if (mainNodeIdx > -1)
                {
                    if (e.Node.Checked)
                        _selectedImagingObservations[mainNodeIdx].selectionIndecies.Add(nodeIdx);
                    else
                        _selectedImagingObservations[mainNodeIdx].selectionIndecies.Remove(nodeIdx);
                }
            }
        }

        private ImagingObservationXML GetSelectedImagingObservation()
        {
            ImagingObservationXML xmlImagingObservation = new ImagingObservationXML(null);

            int mainNodeIdx = GetTreeNodeIndex(mainTree, mainTree.SelectedNode);
            if (mainNodeIdx > -1)
            {
                DropDownTreeNode ddNode = mainTree.SelectedNode as DropDownTreeNode;
                if (ddNode != null)
                {
                    ComboItem comboItem = ddNode.ComboBox.SelectedItem as ComboItem;
                    if (comboItem != null)
                    {
                        ImagingObservationXML selectedImagingObservationXML = comboItem.TagValue as ImagingObservationXML;
                        if (selectedImagingObservationXML != null)
                            xmlImagingObservation = new ImagingObservationXML(selectedImagingObservationXML.Node);
                    }
                }
            }

            return xmlImagingObservation;
        }

        private void UpdateTreeImagingObservation()
        {
 
            treeImagingObservation.SuspendLayout();
            treeImagingObservation.Nodes.Clear();

            ImagingObservationXML xmlImagingObservation  = this.GetSelectedImagingObservation();
            if (xmlImagingObservation.Node != null && xmlImagingObservation.imagingObservationCharacteristicCollectionXML.Exists &&
                xmlImagingObservation.imagingObservationCharacteristicCollectionXML.First.ImagingObservationCharacteristicXML2.Exists)
            {
                int mainNodeIdx = GetTreeNodeIndex(mainTree, mainTree.SelectedNode); // if we're here, a node must be selected
                for(int i=0; i < xmlImagingObservation.imagingObservationCharacteristicCollectionXML.First.ImagingObservationCharacteristicXML2.Count; i++)
                {
                    ImagingObservationCharacteristicXML xmlImagingObsChar = xmlImagingObservation.imagingObservationCharacteristicCollectionXML.First.ImagingObservationCharacteristicXML2[i];
                    if (xmlImagingObsChar.codeMeaning.Exists)
                    {
                        TreeNode node = treeImagingObservation.Nodes.Add(xmlImagingObsChar.codeMeaning.First.Value);
                        int nodeIdx = GetTreeNodeIndex(treeImagingObservation, node);
                        if (this._selectedImagingObservations[mainNodeIdx].selectionIndecies.Contains(nodeIdx))
                            node.Checked = true;
                    }
                }
            }
                
            treeImagingObservation.ResumeLayout();
            treeImagingObservation.Update();

            EventsHelper.Fire(SelectedImagingObservationChanged, this, EventArgs.Empty);
        }

        // Returns index of the given node in the given tree.
        // Returns -1 if the node was not found
        private int GetTreeNodeIndex(TreeView tree, TreeNode node)
        {
            if (node == null)
                return -1;

            for (int i = 0; i < tree.Nodes.Count; i++)
                if (tree.Nodes[i] == node)
                    return i;

            return -1;
        }

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {
            toolStrip.Text = string.Empty;

            if (e.AssociatedControl == this.lblImagingObsCharacteristic)
            {
                ImagingObservationXML xmlImagingObservation = this.GetSelectedImagingObservation();
                if (xmlImagingObservation.Node != null && xmlImagingObservation.codeMeaning.Exists)
                    toolStrip.Text = string.Format("Imaging Observation: {0}", xmlImagingObservation.codeMeaning.First.Value);
            }
        }

        private void lblImagingObsCharacteristic_MouseEnter(object sender, EventArgs e)
        {
            ImagingObservationXML xmlImagingObservation = this.GetSelectedImagingObservation();
            if (xmlImagingObservation.Node != null && xmlImagingObservation.codeMeaning.Exists)
            {
                this.toolTip.SetToolTip(this.lblImagingObsCharacteristic, string.Format("Imaging Observation: {0}", xmlImagingObservation.codeMeaning.First.Value));
            }
        }

        private void lblImagingObsCharacteristic_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip.RemoveAll();
        }

        private void selectAllToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            // See if need to enable "Select All"
            bool enabled = false;
            for(int i = 0; !enabled && i < treeImagingObservation.Nodes.Count; i++)
                enabled |= !treeImagingObservation.Nodes[i].Checked;
            selectAllToolStripMenuItem.Enabled = enabled;

            // See if need to enable "Unselect All"
            enabled = false;
            for(int i = 0; !enabled && i < treeImagingObservation.Nodes.Count; i++)
                enabled |= treeImagingObservation.Nodes[i].Checked;
            unselectAllToolStripMenuItem.Enabled = enabled;
        }

        private void toolStrip_Paint(object sender, PaintEventArgs e)
        {
            this.UpdateEnabledStates();
        }

        private void menuItemAddImagingObs_Click(object sender, EventArgs e)
        {
            this.AddRootImagingObservationNode();
        }

        private void menuItemDeleteImagingObs_Click(object sender, EventArgs e)
        {
            this.DeleteImagingObservation();
        }

    }
}

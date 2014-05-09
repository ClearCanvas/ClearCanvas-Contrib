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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ClearCanvas.Common.Utilities;

namespace AIM.Annotation.View.WinForms
{
    public partial class VasariQuestionControl : UserControl
    {
		public event EventHandler SelectedAnswerChanged;

    	private readonly Control _questionControl;

		public VasariQuestionControl(string label, List<StandardCodeSequence> codeList, string helpText, bool addFirstDefault, bool allowMutipleResponses)
        {
            InitializeComponent();

            _lblEntity.Text = label;
        	_lblEntity.ToolTipText = this.SplitIntoLines(helpText);
			_lblEntity.ShowToolTipChanged += OnLabelShowToolTipChanged;
        	_lblEntity.TabStop = false;

			_questionControl = this.InitializeResponseControl(allowMutipleResponses, codeList, addFirstDefault);

			this.SuspendLayout();
			this.Controls.Add(_questionControl);
			this.Height += _questionControl.Height;
			this.ResumeLayout(false);
			this.PerformLayout();
        }

		private Control InitializeResponseControl(bool allowMultipleResponses, List<StandardCodeSequence> codeList, bool addFirstDefault)
		{
			int ptX = _lblEntity.Location.X + 3;  // where is that 3 coming from?
			int ptY = _lblEntity.Location.Y + _lblEntity.Height + _lblEntity.Margin.Bottom;

			if (allowMultipleResponses)
			{
				CheckedListBox chklistEntity = new CheckedListBox();
				chklistEntity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
				chklistEntity.FormattingEnabled = true;
				chklistEntity.Margin = new Padding(4);
				chklistEntity.Location = new System.Drawing.Point(ptX, ptY);
				chklistEntity.Name = "chklistEntity";
				int ctrlHeight = (codeList == null ? 1 : Math.Max(1, codeList.Count)) * chklistEntity.ItemHeight + chklistEntity.Margin.Vertical;
				chklistEntity.Size = new System.Drawing.Size(this.Width - this.Margin.Horizontal, ctrlHeight);
				chklistEntity.TabIndex = 1;
				chklistEntity.CheckOnClick = true;

				foreach (StandardCodeSequence codeSequence in codeList)
				{
					chklistEntity.Items.Add(new ResponseListItem(codeSequence));
				}

				chklistEntity.ItemCheck += OnItemCheck;

				return chklistEntity;
			}
			else
			{

				ComboBox ddlEntity = new ComboBox();
				ddlEntity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
				ddlEntity.DropDownStyle = ComboBoxStyle.DropDownList;
				ddlEntity.FormattingEnabled = true;
				ddlEntity.Margin = new Padding(4);
				ddlEntity.Location = new System.Drawing.Point(ptX, ptY);
				ddlEntity.Name = "ddlEntity";
				//ddlEntity.Size = new System.Drawing.Size(137, 24);
				ddlEntity.Size = new System.Drawing.Size(this.Width - this.Margin.Horizontal, ddlEntity.Height);
				ddlEntity.TabIndex = 1;

				if (addFirstDefault)
					ddlEntity.Items.Add(new ResponseListItem(null));
				foreach (StandardCodeSequence codeSequence in codeList)
				{
					ddlEntity.Items.Add(new ResponseListItem(codeSequence));
				}

				if (ddlEntity.Items.Count > 0)
					ddlEntity.SelectedIndex = 0;

				ddlEntity.DropDown += OnDropDown;
				ddlEntity.SelectedIndexChanged += OnSelectedIndexChanged;

				return ddlEntity;
			}
		}

    	public List<StandardCodeSequence> SelectedAnswers
        {
            get
            {
				if (_questionControl is ComboBox)
				{
					ResponseListItem item = ((ComboBox)_questionControl).SelectedItem as ResponseListItem;
					return item == null || item.Value == null ? null : new List<StandardCodeSequence> {item.Value};
				}
				else if (_questionControl is CheckedListBox)
				{
					List<StandardCodeSequence> selectedList = new List<StandardCodeSequence>();
					foreach (ResponseListItem item in ((CheckedListBox)_questionControl).CheckedItems)
					{
						if (item != null && item.Value != null)
							selectedList.Add(item.Value);
					}
					return selectedList.Count > 0 ? selectedList : null;
				}
            	Debug.Assert(false, "Unknown control type");
            	return null;
            }

            set
            {
				if (value == null || value.Count == 0) // initial value
				{
					if (_questionControl is ComboBox)
					{
						if (((ComboBox)_questionControl).Items.Count > 0)
							((ComboBox)_questionControl).SelectedIndex = 0;
					}
					else if (_questionControl is CheckedListBox)
					{
						CheckedListBox chklistQuestions = (CheckedListBox)_questionControl;
						if (chklistQuestions.CheckedIndices.Count > 0)
						{
							chklistQuestions.ItemCheck -= OnItemCheck;
							foreach (int checkedIndex in chklistQuestions.CheckedIndices)
								chklistQuestions.SetItemChecked(checkedIndex, false);
							chklistQuestions.ItemCheck += OnItemCheck;

							// Fire the event once manually
							EventsHelper.Fire(SelectedAnswerChanged, this, EventArgs.Empty);
						}
					}
					else
						Debug.Assert(false, "Unknown control type");
				}
				else
				{
					if (_questionControl is ComboBox)
					{
						Debug.Assert(value.Count == 1, "Cannot select more than one value in a ComboBox");
						ComboBox cmbBox = (ComboBox) _questionControl;
						foreach (ResponseListItem item in cmbBox.Items)
						{
							//if (Equals(item.Value, value))
							foreach (StandardCodeSequence codeSequence in value)
							{
								if (Equals(item.Value, codeSequence))
								{
									cmbBox.SelectedItem = item;
									return;
								}
							}
						}
					}
					else if (_questionControl is CheckedListBox)
					{
						CheckedListBox chklistQuestions = (CheckedListBox) _questionControl;
						chklistQuestions.ItemCheck -= OnItemCheck;
						for (int i = 0; i < chklistQuestions.Items.Count; i++)
						{
							ResponseListItem item = chklistQuestions.Items[i] as ResponseListItem;
							if (item != null)
							{
								//if (Equals(item.Value, value))
								foreach (StandardCodeSequence codeSequence in value)
								{
									if (Equals(item.Value, codeSequence))
										chklistQuestions.SetItemChecked(i, true);
								}
							}
						}
						chklistQuestions.ItemCheck += OnItemCheck;

						// Fire the event once manually
						EventsHelper.Fire(SelectedAnswerChanged, this, EventArgs.Empty);

						return;
					}
					else
						Debug.Assert(false, "Unknown control type");

					Debug.Assert(false, "Trying to set unknown VasariQuestionControl value. Some data will be lost");
				}
            }
        }

        public bool HasCode(String codeValue)
        {
			if (_questionControl is ComboBox)
			{
				foreach (ResponseListItem item in ((ComboBox)_questionControl).Items)
				{
					if (item.Value.CodeValue == codeValue)
						return true;
				}
			}
			else if(_questionControl is CheckedListBox)
			{
				foreach (ResponseListItem item in ((CheckedListBox)_questionControl).Items)
				{
					if (item.Value.CodeValue == codeValue)
						return true;
				}
			}

        	return false;
        }

    	public string Label
    	{
			get { return _lblEntity.Text; }
    	}

		private void OnDropDown(object sender, EventArgs e)
		{
			// Adjust dropdown width to be as large as the longest item's width
			ComboBox cmbBox = sender as ComboBox;
			if (cmbBox != null)
				cmbBox.DropDownWidth = AimWinFormsUtil.CalculateComboBoxDropdownWidth(cmbBox);
		}

    	private void OnSelectedIndexChanged(object sender, EventArgs e)
    	{
			EventsHelper.Fire(SelectedAnswerChanged, this, EventArgs.Empty);
    	}

		private void OnItemCheck(object sender, ItemCheckEventArgs e)
		{
			// Hack. WTF!
			// This event occurs before CheckedItems on the CheckedListBox is updated with the latest selection.
			// Delay our update event after ItemChecked is finished
			SynchronizationContext.Current.Post(
				delegate
					{ EventsHelper.Fire(SelectedAnswerChanged, this, EventArgs.Empty); }
				, null);
		}

    	private void OnLabelShowToolTipChanged(object sender, EventArgs e)
		{
			_toolTip.SetToolTip(_lblEntity, _lblEntity.ShowToolTip ? _lblEntity.ToolTipText : null);
		}

		// Takes a string and insertes NewLines to make each line less than maxLineLength long
		private string SplitIntoLines(string inLine)
		{
			if (string.IsNullOrEmpty(inLine))
				return null;

			const int maxLineLength = 90;
			string[] words = inLine.Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder sb = new StringBuilder();
			int len = 0;
			foreach (string word in words)
			{
				if (len + word.Length > maxLineLength)
				{
					sb.Append(Environment.NewLine);
					len = word.Length;
				}
				else
				{
					len += word.Length + 1;
					sb.Append(" ");
				}
				sb.Append(word);
			}

			return sb.ToString().Trim();
		}
	}

    internal class ResponseListItem
    {
        private StandardCodeSequence _value;

		public ResponseListItem(StandardCodeSequence newValue)
        {
            _value = newValue;
        }

    	public StandardCodeSequence Value
    	{
			get { return _value; }
    	}

        public override string ToString()
        {
            return _value == null ? "------" : _value.CodeMeaning;
        }
    }
}

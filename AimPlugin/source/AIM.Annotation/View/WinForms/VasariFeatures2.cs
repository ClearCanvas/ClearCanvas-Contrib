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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using ClearCanvas.Common.Utilities;
using aim_dotnet;

//using DropDownEnityControlType = AIM.Annotation.View.WinForms.DropDownEntity;
using DropDownEntityControlType = AIM.Annotation.View.WinForms.VasariQuestionControl;


namespace AIM.Annotation.View.WinForms
{
	public partial class VasariFeatures2 : UserControl, INotifyPropertyChanged
	{
		public event EventHandler IsValidChanged;

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		private const int C_VerticalScrollbarWidth = 0; // SystemInformation.VerticalScrollBarWidth;

		private bool _ignoreOnSelectedFeatureChanged; // flag to prevet reentrance in OnSelectedFeatureChanged

		private enum AimDropDownEntityType
		{
			[Description("Anatomic Entity")]
			AnatomicEntity,
			[Description("Imaging Observation")]
			ImagingObservation,
			[Description("Inference")]
			Inference,
			[Description("Imaging Observation Characteristic")]
			ImagingObservationCharacteristic
		}

		public VasariFeatures2()
		{
			InitializeComponent();

			this.SuspendLayout();

			int startPtY = 3;
			int cnt = 0;
			string currentHeading = null;

			this.AddNewDropDownEntityControls(VasariTemplateData2.AnatomicEntityData, ref cnt, ref startPtY, ref currentHeading, AimDropDownEntityType.AnatomicEntity, false);
			this.AddNewDropDownEntityControls(VasariTemplateData2.InferenceData, ref cnt, ref startPtY, ref currentHeading, AimDropDownEntityType.Inference, false);
			this.AddNewDropDownEntityControls(VasariTemplateData2.ImagingObservationData, ref cnt, ref startPtY, ref currentHeading, AimDropDownEntityType.ImagingObservation, false);
			this.AddNewDropDownEntityControls(VasariTemplateData2.ImagingObservationCharData, ref cnt, ref startPtY, ref currentHeading, AimDropDownEntityType.ImagingObservationCharacteristic, true);

			this.ResumeLayout(false);
		}

		private void AddNewDropDownEntityControls(List<TemplateQuestion> dataList, ref int controlCnt, ref int startPtY, ref string currentHeading, AimDropDownEntityType ddeType, bool isIndented)
		{
			int startPtX = isIndented ? 25 : 3; // move X to the right if indentation is desired
			string codeTypeDescription = StringValueOf(ddeType);
			string controlTag = GetTagName(ddeType);
			foreach (TemplateQuestion question in dataList)
			{
				// Add Heading
				string questionHeading = GetQuestionHeading(question.Name);
				if (questionHeading != currentHeading)
				{
					Label lblHeading = new Label();
					lblHeading.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			//		lblHeading.AutoSize = true;
					lblHeading.AutoEllipsis = true;
					lblHeading.Location = new System.Drawing.Point(startPtX, startPtY);
					lblHeading.TextAlign = ContentAlignment.MiddleLeft;
					lblHeading.Font = new Font(lblHeading.Font.FontFamily, lblHeading.Font.Size + 3F, FontStyle.Bold);
					lblHeading.Text = questionHeading ?? "Empty Heading";
					//lblHeading.BackColor = SystemColors.MenuHighlight;
					//lblHeading.ForeColor = SystemColors.MenuText;
					lblHeading.Size = new Size(this.Width - this.Margin.Horizontal - C_VerticalScrollbarWidth - (isIndented ? 20 : 0), lblHeading.Height);
					startPtY += lblHeading.Height + 3;
					controlCnt++;
					this.Controls.Add(lblHeading);

					currentHeading = questionHeading;
				}

				// Add Question Drop Down
				DropDownEntityControlType ddEnt = new DropDownEntityControlType(GetLabelForQuestion(question.Name, codeTypeDescription), question.Codes, question.Description, question.Codes.Count > 1, question.AllowMultipleAnswers);
				ddEnt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
				ddEnt.Location = new System.Drawing.Point(startPtX, startPtY);
				ddEnt.Name = "dddEnt" + controlCnt;
				ddEnt.Size = new Size(this.Width - this.Margin.Horizontal - C_VerticalScrollbarWidth - (isIndented ? 20 : 0), ddEnt.Height);
				ddEnt.TabIndex = controlCnt;
				ddEnt.Tag = controlTag; // "ImagingObservationCharacteristic";
				ddEnt.SelectedAnswerChanged += OnSelectedFeatureChanged;
				// Do not display controls with a single answer, that will be selected by default
				if (question.Codes.Count > 1)
					startPtY += ddEnt.Height + 3;
				else
					ddEnt.Visible = false;
				controlCnt++;
				this.Controls.Add(ddEnt);
			}
		}

		private void OnSelectedFeatureChanged(object sender, EventArgs e)
		{
			// Do not process the event while we're processing a previous one
			if (_ignoreOnSelectedFeatureChanged)
				return;

			_ignoreOnSelectedFeatureChanged = true;

			// Notify component that new properties are set
			DropDownEntityControlType ddEnt = sender as DropDownEntityControlType;
			if (ddEnt != null)
			{
				if (string.Equals((string)ddEnt.Tag, GetTagName(AimDropDownEntityType.AnatomicEntity)))
				{
					EventsHelper.Fire(PropertyChanged, this, new PropertyChangedEventArgs("SelectedAnatomicEntities"));
				}
				else if (string.Equals((string)ddEnt.Tag, GetTagName(AimDropDownEntityType.ImagingObservation)) ||
					string.Equals((string)ddEnt.Tag, GetTagName(AimDropDownEntityType.ImagingObservationCharacteristic)))
				{
					// Special logic for Enhancement Quality. 
					string codeTypeDescription = StringValueOf(AimDropDownEntityType.ImagingObservationCharacteristic);
					if (ddEnt.Label == GetLabelForQuestion("Enhancement Quality", codeTypeDescription))
					{
						// If None - use defaults (N/A) and disable dependents, otherwise - reset to blank.
						this.SuspendLayout();

						bool enableDependents = ddEnt.SelectedAnswers == null || !CollectionUtils.Contains(ddEnt.SelectedAnswers, codeSequence => codeSequence.CodeValue == VasariTemplateData2.EnhancementQualityNoneCode);
						List<TemplateQuestion> dependentQuestions = VasariTemplateData2.DefaultImagingObservationCharData; // default answeres to dependent questions
						foreach (DropDownEntityControlType dependentDdEnt in GetDropDownEntityControls(AimDropDownEntityType.ImagingObservationCharacteristic, dependentQuestions))
						{
							dependentDdEnt.SelectedAnswerChanged -= OnSelectedFeatureChanged;

							// 1. If re-enabling, set values to empty
							if (enableDependents && !dependentDdEnt.Enabled)
								dependentDdEnt.SelectedAnswers = null;
							// 2. If disabling, set values to defaults
							if (!enableDependents && dependentDdEnt.Enabled)
							{
								foreach (TemplateQuestion dependentQiestion in dependentQuestions)
								{
									if (dependentDdEnt.Label == GetLabelForQuestion(dependentQiestion.Name, codeTypeDescription))
									{
										Debug.Assert(dependentQiestion.Codes != null && dependentQiestion.Codes.Count > 0);
										dependentDdEnt.SelectedAnswers = new List<StandardCodeSequence> {dependentQiestion.Codes[0]};
										break;
									}
								}
							}
							dependentDdEnt.Enabled = enableDependents;

							dependentDdEnt.SelectedAnswerChanged += OnSelectedFeatureChanged;
						}

						this.ResumeLayout(true);
					}
					EventsHelper.Fire(PropertyChanged, this, new PropertyChangedEventArgs("SelectedImagingObservations"));
				}
				else if (string.Equals((string)ddEnt.Tag, GetTagName(AimDropDownEntityType.Inference)))
				{
					EventsHelper.Fire(PropertyChanged, this, new PropertyChangedEventArgs("SelectedInferences"));
				}
			}

			EventsHelper.Fire(IsValidChanged, this, EventArgs.Empty);

			_ignoreOnSelectedFeatureChanged = false;
		}

		private static string StringValueOf(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		private static string GetTagName(AimDropDownEntityType ddeType)
		{
			return StringValueOf(ddeType).Replace(" ", "");
		}

		private static string GetLabelForQuestion(string labelPrefix, string labeSuffix)
		{
			//return string.Format("{0} ({1})", labelPrefix, labeSuffix);
			return labelPrefix;
		}

		// Get a list of question controls filtered by the specific question type
		private IEnumerable<DropDownEntityControlType> GetDropDownEntityControls(AimDropDownEntityType dropDownType)
		{
			string tagName = GetTagName(dropDownType);
			foreach (Control control in this.Controls)
			{
				if (control != null && control is DropDownEntityControlType && string.Equals((string)control.Tag, tagName))
					yield return (DropDownEntityControlType)control;
			}
		}

		// Get a list of question controls filtered by the specific question type & set of questions
		private IEnumerable<DropDownEntityControlType> GetDropDownEntityControls(AimDropDownEntityType dropDownType, List<TemplateQuestion> questionList)
		{
			string codeTypeDescription = StringValueOf(dropDownType);
			foreach (DropDownEntityControlType control in this.GetDropDownEntityControls(dropDownType))
			{
				foreach (TemplateQuestion templateQuestion in questionList)
				{
					if (GetLabelForQuestion(templateQuestion.Name, codeTypeDescription) == control.Label)
					{
						yield return control;
						break;
					}
				}
			}
		}

		private string GetQuestionHeading(string question)
		{
			foreach (KeyValuePair<string, List<string>> templateHeading in VasariTemplateData2.VasariTemplateHeadings)
			{
				if (templateHeading.Value.Contains(question))
				{
					return templateHeading.Key;
				}
			}
			return null;
		}

		public List<AnatomicEntity> SelectedAnatomicEntities
		{
			get
			{
				List<AnatomicEntity> anatomicEntities = new List<AnatomicEntity>();
				foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.AnatomicEntity))
				{
					if (ddEnt.SelectedAnswers != null)
					{
						foreach (StandardCodeSequence entity in ddEnt.SelectedAnswers)
							anatomicEntities.Add(CodeUtils.ToAnatomicEntity(entity));
					}
				}

				return anatomicEntities.Count > 0 ? anatomicEntities : null;
			}

			set
			{
				List<AnatomicEntity> previouslyProcessed = new List<AnatomicEntity>();
				foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.AnatomicEntity))
				{
					List<StandardCodeSequence> newSelections = null;
					if (value != null)
					{
						newSelections = new List<StandardCodeSequence>();
						foreach (AnatomicEntity anatomicEntity in CollectionUtils.Select(value, ent => !previouslyProcessed.Contains(ent)))
						{
							if (anatomicEntity != null && ddEnt.HasCode(anatomicEntity.CodeValue))
							{
								newSelections.Add(CodeUtils.ToStandardCodeSequence(anatomicEntity));
								previouslyProcessed.Add(anatomicEntity);
							}
						}
					}
					ddEnt.SelectedAnswers = newSelections;
				}
				previouslyProcessed.Clear();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<ImagingObservation> SelectedImagingObservations
		{
			get
			{
				List<ImagingObservation> imagingObservations = new List<ImagingObservation>();
				foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.ImagingObservation))
				{
					if (ddEnt.SelectedAnswers != null)
					{
						foreach (StandardCodeSequence selectedImgObs in ddEnt.SelectedAnswers)
						{
							ImagingObservation imagingObservation = CodeUtils.ToImagingObservation(selectedImgObs);

							// Get Imaging Observation Characteristics
							// NOTE: This will work since there is only one Imaging Observation.
							// NOTE: When we have more than one Imaging Observations, we need to maintain the proper hierarchy of items.
							List<ImagingObservationCharacteristic> imgChars = new List<ImagingObservationCharacteristic>();
							foreach (DropDownEntityControlType ddEntIOC in this.GetDropDownEntityControls(AimDropDownEntityType.ImagingObservationCharacteristic))
							{
								if (ddEntIOC.SelectedAnswers != null)
								{
									foreach (StandardCodeSequence selectedIOC in ddEntIOC.SelectedAnswers)
										imgChars.Add(CodeUtils.ToImagingObservationCharacteristic(selectedIOC));
								}
							}
							if (imgChars.Count > 0)
								imagingObservation.ImagingObservationCharacteristicCollection = imgChars;

							imagingObservations.Add(imagingObservation);
						}
					}
				}

				return imagingObservations.Count > 0 ? imagingObservations : null;
			}

			set
			{
				if (value == null)
				{
					foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.ImagingObservation))
						ddEnt.SelectedAnswers = null;
					foreach (DropDownEntityControlType ddEntIoc in GetDropDownEntityControls(AimDropDownEntityType.ImagingObservationCharacteristic))
						ddEntIoc.SelectedAnswers = null;
					return;
				}

				List<ImagingObservation> prevProcessed = new List<ImagingObservation>();
				foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.ImagingObservation))
				{
					List<StandardCodeSequence> newSelections = new List<StandardCodeSequence>();
					foreach (ImagingObservation imagingObservation in CollectionUtils.Select(value, ent => !prevProcessed.Contains(ent)))
					{
						if (imagingObservation != null && ddEnt.HasCode(imagingObservation.CodeValue))
						{
							newSelections.Add(CodeUtils.ToStandardCodeSequence(imagingObservation));
							prevProcessed.Add(imagingObservation);

							// Imaging Observation Characteristics
							if (imagingObservation.ImagingObservationCharacteristicCollection != null &&
								imagingObservation.ImagingObservationCharacteristicCollection.Count > 0)
							{
								List<ImagingObservationCharacteristic> prevProcessedIoc = new List<ImagingObservationCharacteristic>();
								foreach (DropDownEntityControlType ddEntIoc in GetDropDownEntityControls(AimDropDownEntityType.ImagingObservationCharacteristic))
								{
									List<StandardCodeSequence> newSelectionsIoc = new List<StandardCodeSequence>();
									// Find correct Imaging Observation Characteristic
									foreach (ImagingObservationCharacteristic imgObsChar in
										CollectionUtils.Select(imagingObservation.ImagingObservationCharacteristicCollection, ent => !prevProcessedIoc.Contains(ent)))
									{
										if (ddEntIoc.HasCode(imgObsChar.CodeValue))
										{
											newSelectionsIoc.Add(CodeUtils.ToStandardCodeSequence(imgObsChar));
											prevProcessedIoc.Add(imgObsChar);
										}
									}
									ddEntIoc.SelectedAnswers = newSelectionsIoc;
								}
								prevProcessedIoc.Clear();
							}
						}
					}
					ddEnt.SelectedAnswers = newSelections;
				}
				prevProcessed.Clear();
			}
		}

		public List<Inference> SelectedInferences
		{
			get
			{
				List<Inference> inferences = new List<Inference>();
				foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.Inference))
				{
					if (ddEnt.SelectedAnswers != null)
					{
						foreach (StandardCodeSequence entity in ddEnt.SelectedAnswers)
							inferences.Add(CodeUtils.ToInference(entity));
					}
				}

				return inferences.Count > 0 ? inferences : null;
			}

			set
			{

				List<Inference> previouslyProcessed = new List<Inference>();
				foreach (DropDownEntityControlType ddEnt in this.GetDropDownEntityControls(AimDropDownEntityType.Inference))
				{
					List<StandardCodeSequence> newSelections = null;
					if (value != null)
					{
						newSelections = new List<StandardCodeSequence>();
						foreach (Inference inference in CollectionUtils.Select(value, ent => !previouslyProcessed.Contains(ent)))
						{
							if (inference != null && ddEnt.HasCode(inference.CodeValue))
							{
								newSelections.Add(CodeUtils.ToStandardCodeSequence(inference));
								previouslyProcessed.Add(inference);
							}
						}
					}
					ddEnt.SelectedAnswers = newSelections;
				}
				previouslyProcessed.Clear();
			}
		}

		public bool IsValid
		{
			get
			{
				foreach (Control ctrl in this.Controls)
				{
					DropDownEntityControlType ddEnt = ctrl as DropDownEntityControlType;
					if (ddEnt != null && (ddEnt.SelectedAnswers == null || ddEnt.SelectedAnswers.Count == 0))
						return false;
				}
				return true;
			}
			set { }
		}
	}

	internal class TemplateQuestion
	{
		private readonly string _name;
		private readonly string _description;
		private readonly List<StandardCodeSequence> _codes;
		private readonly bool _allowMultipleAnswers;

		public TemplateQuestion(string name, string description, bool allowMultipleAnswers, List<StandardCodeSequence> codes)
		{
			_name = name;
			_description = description;
			_allowMultipleAnswers = allowMultipleAnswers;
			_codes = codes;
		}

		public TemplateQuestion(string name, string description, List<StandardCodeSequence> codes)
			: this(name, description, false, codes)
		{
		}

		public TemplateQuestion(string name, List<StandardCodeSequence> codes)
			: this(name, null, codes)
		{
		}

		public string Name
		{
			get { return _name; }
		}

		public string Description
		{
			get { return _description; }
		}

		public bool AllowMultipleAnswers
		{
			get { return _allowMultipleAnswers; }
		}

		public List<StandardCodeSequence> Codes
		{
			get { return _codes; }
		}
	}

	internal class VasariTemplateData2
	{
		// Default values when "Enhancement Quality" : "None" is selected
		public static List<TemplateQuestion> DefaultImagingObservationCharData
		{
			get
			{
				return
					new List<TemplateQuestion>
						{
							new TemplateQuestion(
								"Proportion Enhancing",
								new List<StandardCodeSequence>
									{
				            			new StandardCodeSequence("54", "Proportion Enhancing : None (0%)", "VASARI")
									}
								),
							new TemplateQuestion(
								"Proportion Necrosis",
								new List<StandardCodeSequence>
									{
				            			new StandardCodeSequence("45", "Proportion Necrosis : None (0%)", "VASARI")
									}
								),
							new TemplateQuestion(
								"Thickness of the Enhancing Margin",
								new List<StandardCodeSequence>
									{
				            			new StandardCodeSequence("880", "Thickness of the Enhancing Margin None", "VASARI")
									}
								),
							new TemplateQuestion(
								"Definition of the Enhancing Margin",
								new List<StandardCodeSequence>
									{
				            			new StandardCodeSequence("12", "Definition of the Enhancing Margin Not Applicable", "VASARI")
									}
								),
							new TemplateQuestion(
								"Enhancing Tumor Crosses Midline",
								new List<StandardCodeSequence>
									{
				            			new StandardCodeSequence("3", "Enhancing Tumor Crosses Midline Not Applicable (no CET)", "VASARI")
									}
								),
							new TemplateQuestion(
								"Satellites",
								new List<StandardCodeSequence>
									{
				            			new StandardCodeSequence("129", "Satellites No", "VASARI")									}
								),

						};
			}
		}

		internal const string EnhancementQualityNoneCode = "67"; // Special code which controls selected values for other questions.
		public static List<TemplateQuestion> ImagingObservationCharData
		{
			get
			{
				List<TemplateQuestion> theData = new List<TemplateQuestion>();

				// Enhancement Quality
				theData.Add(new TemplateQuestion(
								"Enhancement Quality",
								"Qualitative degree of contrast enhancement is defined as having all or portions of the tumor that demonstrate significantly higher signal on the postcontrast T1W images compared to precontrast T1W images. Mild/Minimal = when barely discernable degree of enhancement is present relative to pre-contrast images. Marked/avid = obvious tissue enhancement.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence(EnhancementQualityNoneCode, "Enhancement Quality : None", "VASARI"),
				            			new StandardCodeSequence("68", "Enhancement Quality : Minimal/Mild", "VASARI"),
				            			new StandardCodeSequence("69", "Enhancement Quality : Mark/Avid", "VASARI"),
				            			new StandardCodeSequence("699", "Enhancement Quality : Indeterminate", "VASARI")
				            		}));

				// Proportion Enhancing
				theData.Add(new TemplateQuestion(
								"Proportion Enhancing",
								"Visually, when scanning through the entire tumor volume, what proportion of the entire tumor would you estimate is enhancing. (Assuming that the entire abnormality may be comprised of: (1) an enhancing component, (2) a non-enhancing component, (3) a necrotic component and (4) a edema component.)",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("54", "Proportion Enhancing : None (0%)", "VASARI"),
				            			new StandardCodeSequence("55", "Proportion Enhancing : < 5%", "VASARI"),
				            			new StandardCodeSequence("56", "Proportion Enhancing : 6-33%", "VASARI"),
				            			new StandardCodeSequence("57", "Proportion Enhancing : 34-67%", "VASARI"),
				            			new StandardCodeSequence("58", "Proportion Enhancing : 68%- 95%", "VASARI"),
				            			new StandardCodeSequence("59", "Proportion Enhancing : > 95%", "VASARI"),
				            			new StandardCodeSequence("60", "Proportion Enhancing : All (100%)", "VASARI"),
				            			new StandardCodeSequence("53", "Proportion Enhancing : Indeterminate ", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Proportion nCET",
								"Visually, when scanning through the entire tumor volume, what proportion of the entire tumor is estimated to represent non-enhancing tumor (not edema)? Nonenhancing tumor is defined as regions of T2W hyperintensity (less than the intensity of cerebrospinal fluid, with corresponding T1W hypointensity) that are associated with mass effect and architectural distortion, including blurring of the gray-white interface.(Assuming that the the entire abnormality may be comprised of: (1) an enhancing component, (2) a non-enhancing component, (3) a necrotic component and (4) a edema component.)",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("17", "Proportion nCET Not Applicable", "VASARI"),
				            			new StandardCodeSequence("19", "Proportion nCET : None (0%)", "VASARI"),
				            			new StandardCodeSequence("20", "Proportion nCET : < 5%", "VASARI"),
				            			new StandardCodeSequence("21", "Proportion nCET : 6-33%", "VASARI"),
				            			new StandardCodeSequence("22", "Proportion nCET : 34-67%", "VASARI"),
				            			new StandardCodeSequence("23", "Proportion nCET : 68%-95%", "VASARI"),
				            			new StandardCodeSequence("24", "Proportion nCET : > 95%", "VASARI"),
				            			new StandardCodeSequence("25", "Proportion nCET : 100%", "VASARI"),
				            			new StandardCodeSequence("18", "Proportion nCET : Indeterminate ", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Proportion Necrosis",
								"Visually, when scanning through the entire tumor volume, what proportion of the tumor is estimated to represent necrosis. Necrosis is defined as a region within the tumor that does not enhance or shows markedly diminished enhancement, is high on T2W and proton density images, is low on T1W images, and has an irregular border). (Assuming that the entire abnormality may be comprised of: (1) an enhancing component, (2) a non-enhancing component, (3) a necrotic component and (4) a edema component.)",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("45", "Proportion Necrosis : None (0%)", "VASARI"),
				            			new StandardCodeSequence("46", "Proportion Necrosis : < 5%", "VASARI"),
				            			new StandardCodeSequence("47", "Proportion Necrosis : 6-33%", "VASARI"),
				            			new StandardCodeSequence("48", "Proportion Necrosis : 34-67%", "VASARI"),
				            			new StandardCodeSequence("49", "Proportion Necrosis : 68%-95%", "VASARI"),
				            			new StandardCodeSequence("50", "Proportion Necrosis : > 95%", "VASARI"),
				            			new StandardCodeSequence("51", "Proportion Necrosis : 100%", "VASARI"),
				            			new StandardCodeSequence("44", "Proportion Necrosis : Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Proportion of Edema",
								"Visually, when scanning through the entire tumor volume, what proportion of the entire abnormality is estimated to represent vasogenic edema? (Edema should be greater in signal than than nCET and somewhat lower in signal than CSF. Pseudopods are characteristic of edema). (Assuming that the the entire abnormality may be comprised of: (1) an enhancing component, (2) a non-enhancing component, (3) a necrotic component and (4) a edema component.)",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("28", "Proportion of Edema : None (0%)", "VASARI"),
				            			new StandardCodeSequence("29", "Proportion of Edema : < 5%", "VASARI"),
				            			new StandardCodeSequence("30", "Proportion of Edema : 6-33%", "VASARI"),
				            			new StandardCodeSequence("31", "Proportion of Edema : 34-67%", "VASARI"),
				            			new StandardCodeSequence("32", "Proportion of Edema : 68%-95%", "VASARI"),
				            			new StandardCodeSequence("33", "Proportion of Edema : > 95%", "VASARI"),
				            			new StandardCodeSequence("34", "Proportion of Edema : 100%", "VASARI"),
				            			new StandardCodeSequence("27", "Proportion of Edema : Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Cysts",
								"Cysts are well defined, rounded, often eccentric regions of very bright T2W signal and low T1W signal essentially matching CSF signal intensity, with very thin, regular, smooth, nonenhancing or regularly enhancing walls, possibly with thin, regular, internal septations.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("36", "Cysts No", "VASARI"),
				            			new StandardCodeSequence("35", "Cysts Yes", "VASARI"),
				            			new StandardCodeSequence("366", "Cysts Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Multifocal or Multicentric",
								"Multifocal is defined as having at least one region of tumor, either enhancing or nonenhancing, which is not contiguous with the dominant lesion and is outside the region of signal abnormality (edema) surrounding the dominant mass. This can be defined as those resulting from dissemination or growth by an established route, spread via commissural or other pathways, or via CSF channels or local metastases, whereas Multicentric are widely separated lesions in different lobes or different hemispheres that cannot be attributed to one of the previously mentioned pathways. Gliomatosis refers to generalized neoplastic transformation of the white matter of most of a hemisphere.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("13", "Focal", "VASARI"),
				            			new StandardCodeSequence("16", "Multifocal", "VASARI"),
				            			new StandardCodeSequence("15", "Multicentric", "VASARI"),
				            			new StandardCodeSequence("14", "Gliomatosis", "VASARI"),
				            			new StandardCodeSequence("144", "Multifocal or Multicentric Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"T1/FLAIR Ratio",
								"Expansive = size of pre-contrast T1 abnormality (exclusive of signal intensity) approximates size of FLAIR abnormality. Mixed = Size of T1 abnormality moderately less than FLAIR envelope; Infiltrative = Size of pre-contrast T1 abnormality much smaller than size of FLAIR abnormality. (Use T2 if FLAIR is not provided)",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("93", "T1/FLAIR Ratio Not Applicable (No Flair)", "VASARI"),
				            			new StandardCodeSequence("90", "Expansive T1~FLAIR Ratio", "VASARI"),
				            			new StandardCodeSequence("91", "Mixed T1<FLAIR Ratio", "VASARI"),
				            			new StandardCodeSequence("92", "Infiltrative T1<<FLAIR Ratio", "VASARI"),
				            			new StandardCodeSequence("922", "T1/FLAIR Ratio Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Thickness of the Enhancing Margin",
								"The scoring is not applicable if there is no contrast enhancement. If most of the enhancing rim is thin, regular, and measures &lt; 3mm in thickness and has homogenous enhancement the grade is thin. If most of the rim demonstrates nodular and/or thick enhancement, the grade is thick. If there is only solid enhancement and no rim, the grade is solid.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("880", "Thickness of the Enhancing Margin None", "VASARI"),
				            			new StandardCodeSequence("81", "Thickness of the Enhancing Margin Thin (<3mm)", "VASARI"),
				            			new StandardCodeSequence("82", "Thickness of the Enhancing Margin Thick/nodular (>3mm)", "VASARI"),
				            			new StandardCodeSequence("882", "Thickness of the Enhancing Margin Solid", "VASARI"),
				            			new StandardCodeSequence("8882", "Thickness of the Enhancing Margin Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Definition of the Enhancing Margin",
								"The scoring is not applicable (n/a) if there is no contrast enhancement. Assess if most of the outside margin of the enhancement is well defined or poorly defined. Are you able to easily trace the margin of enhancement?",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("12", "Definition of the Enhancing Margin Not Applicable", "VASARI"),
				            			new StandardCodeSequence("11", "Definition of the Enhancing Margin Well-defined", "VASARI"),
				            			new StandardCodeSequence("10", "Definition of the Enhancing Margin Poorly-defined", "VASARI"),
				            			new StandardCodeSequence("100", "Definition of the Enhancing Margin Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Definition of the Non-Enhancing Margin (e.g. Grade III)",
								"If most of the outside nonenhancing margin of the tumor is well defined and smooth (geographic), versus if the margin is ill-defined and irregular.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("71", "Definition of the Non-Enhancing Margin Well-defined", "VASARI"),
				            			new StandardCodeSequence("72", "Definition of the Non-Enhancing Margin Poorly-defined", "VASARI"),
				            			new StandardCodeSequence("722", "Definition of the Non-Enhancing Margin Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Hemorrhage",
								"Intrinsic hemorrhage in the tumor matrix. Any intrinsic foci of low signal on T2WI or high signal on T1WI. (Use Bo image if necessary for confirmation.)",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("105", "Hemorrhage No", "VASARI"),
				            			new StandardCodeSequence("106", "Hemorrhage Yes", "VASARI"),
				            			new StandardCodeSequence("1066", "Hemorrhage Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Diffusion",
								"Predominantly facilitated or restricted diffusion in the enhancing or nCET portion of the tumor. (Based on ADC map). [Rate CET alone when present, otherwise use nCET]. Indeterminate=unsure. Mixed=relatively equal proportion of facilitated and restricted. No ADC maps=use no-images. Proportion of tissue not relevant.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("110", "Diffusion : No Image - no images or ADC provided", "VASARI"),
				            			new StandardCodeSequence("111", "Diffusion : Facilitated", "VASARI"),
				            			new StandardCodeSequence("112", "Diffusion : Restricted", "VASARI"),
				            			new StandardCodeSequence("113", "Diffusion : Mixed", "VASARI"),
				            			new StandardCodeSequence("1133", "Diffusion : Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Pial Invasion",
								"Enhancement of the overlying pia in continuity with enhancing or non-enhancing tumor.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("115", "Pial Invasion No", "VASARI"),
				            			new StandardCodeSequence("116", "Pial Invasion Yes", "VASARI"),
				            			new StandardCodeSequence("1166", "Pial Invasion Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Ependymal Extension",
                                "Extension of any adjacent ependymal surface in continuity with enhancing or non-enhancing tumor matrix.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("120", "Ependymal Extension No", "VASARI"),
				            			new StandardCodeSequence("121", "Ependymal Extension Yes", "VASARI"),
				            			new StandardCodeSequence("1211", "Ependymal Extension Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Cortical Involvement",
								"Non-enhancing or enhancing tumor extending to the cortical mantle, or cortex is no longer distinguishable relative to subjacent tumor.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("125", "Cortical involvement No", "VASARI"),
				            			new StandardCodeSequence("126", "Cortical involvement Yes", "VASARI"),
				            			new StandardCodeSequence("1266", "Cortical involvement Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Deep WM Invasion",
								"Enhancing or nCET tumor extending into the internal capsule, corpus callosum or brainstem.",
								true,
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("7", "Deep WM Invasion None", "VASARI"),
				            			new StandardCodeSequence("6", "Deep WM Invasion Internal Capsule", "VASARI"),
				            			new StandardCodeSequence("6123", "Deep WM Invasion Corpus Callosum", "VASARI"),
				            			new StandardCodeSequence("6124", "Deep WM Invasion Brainstem", "VASARI"),
				            			new StandardCodeSequence("6125", "Deep WM Invasion Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"nCET tumor Crosses Midline",
								"nCET crosses into contralateral hemisphere through white matter commissures (exclusive of herniated ipsilateral tissue).",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("63", "nCET Tumor Crosses Midline Not Applicable (No nCET)", "VASARI"),
				            			new StandardCodeSequence("62", "nCET Tumor Crosses Midline No", "VASARI"),
				            			new StandardCodeSequence("61", "nCET Tumor Crosses Midline Yes", "VASARI"),
				            			new StandardCodeSequence("611", "nCET Tumor Crosses Midline Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Enhancing Tumor Crosses Midline",
								"Enhancing tissue crosses into contralateral hemisphere through white matter commisures (exclusive of herniated ipsilateral tissue).",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("3", "Enhancing Tumor Crosses Midline Not Applicable (no CET)", "VASARI"),
				            			new StandardCodeSequence("2", "Enhancing Tumor Crosses Midline No", "VASARI"),
				            			new StandardCodeSequence("1", "Enhancing Tumor Crosses Midline Yes", "VASARI"),
				            			new StandardCodeSequence("1111", "Enhancing Tumor Crosses Midline Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Satellites",
								"A satellite lesion is an area of enhancement within the region of signal abnormality surrounding the dominant lesion but not contiguous in any part with the major tumor mass.",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("129", "Satellites No", "VASARI"),
				            			new StandardCodeSequence("130", "Satellites Yes", "VASARI"),
				            			new StandardCodeSequence("1300", "Satellites Indeterminate", "VASARI")
				            		}));

				theData.Add(new TemplateQuestion(
								"Calvarial Remodeling",
								"Erosion of inner table of skull (possibly a secondary sign of slow growth).",
								new List<StandardCodeSequence>
				            		{
				            			new StandardCodeSequence("135", "Calvarial Remodeling No", "VASARI"),
				            			new StandardCodeSequence("136", "Calvarial Remodeling Yes", "VASARI"),
				            			new StandardCodeSequence("1366", "Calvarial Remodeling Indeterminate", "VASARI")
				            		}));

				return theData;
			}
		}

		public static List<TemplateQuestion> AnatomicEntityData
		{
			get
			{
				return new List<TemplateQuestion>
				       	{
				       		new TemplateQuestion(
				       			"Tumor Location",
                                "Location of lesion geographic epicenter; the largest component of the tumor (either CET or nCET)",
				       			new List<StandardCodeSequence>
				       				{
				       					new StandardCodeSequence("RID6440", "Frontal lobe", "RadLex"),
				       					new StandardCodeSequence("RID6476", "Temporal lobe", "RadLex"),
				       					new StandardCodeSequence("RID6472", "Insula", "RadLex"),
				       					new StandardCodeSequence("RID6493", "Parietal lobe", "RadLex"),
				       					new StandardCodeSequence("RID6502", "Occipital lobe", "RadLex"),
				       					new StandardCodeSequence("RID6677", "Brainstem", "RadLex"),
				       					new StandardCodeSequence("RID6815", "Cerebellum", "RadLex"),
				       					new StandardCodeSequence("RID6537", "Basal Ganglia", "RadLex"),
				       					new StandardCodeSequence("RID6578", "Thalamus", "RadLex"),
				       					new StandardCodeSequence("RID6915", "Corpus Callosum", "RadLex")
				       				}),
				       		new TemplateQuestion(
				       			"Side of Tumor Epicenter",
                                "Side of lesion epicenter",
				       			new List<StandardCodeSequence>
				       				{
				       					new StandardCodeSequence("G-A100", "Right", "SRT"),
				       					new StandardCodeSequence("G-A102", "Center/Bilateral", "SRT"),
				       					new StandardCodeSequence("G-A101", "Left", "SRT")
				       				})
				       	};
			}
		}

		public static List<TemplateQuestion> InferenceData
		{
			get
			{
				return new List<TemplateQuestion>
				       	{
				       		new TemplateQuestion(
				       			"Eloquent Brain",
                                "Does any component of the tumor (CET and nCET) involve eloquent cortex or the immediate subcortical white matter of eloquent cortex (motor, language, vision)?",
								true,
				       			new List<StandardCodeSequence>
				       				{
				       					new StandardCodeSequence("85", "No Eloquent Brain", "VASARI"),
				       					new StandardCodeSequence("86", "Eloquent Brain : Speech Motor", "VASARI"),
				       					new StandardCodeSequence("87", "Eloquent Brain : Speech Receptive", "VASARI"),
				       					new StandardCodeSequence("88", "Eloquent Brain : Motor", "VASARI"),
				       					new StandardCodeSequence("89", "Eloquent Brain : Vision", "VASARI")
				       				})
				       	};
			}
		}

		public static List<TemplateQuestion> ImagingObservationData
		{
			get
			{
				return new List<TemplateQuestion>
				       	{
				       		new TemplateQuestion(
				       			"Mass",
				       			new List<StandardCodeSequence>
				       				{
				       					new StandardCodeSequence("RID3874", "mass", "RadLex")
				       				})
				       	};
			}
		}

		public static Dictionary<string, List<string>> VasariTemplateHeadings
		{
			get
			{
				return new Dictionary<string, List<string>>
				       	{
				       		{
				       			"LESION LOCATION",
				       			new List<string>
				       				{
				       					"Tumor Location",
				       					"Side of Tumor Epicenter",
				       					"Eloquent Brain",
				       					"Mass"
				       				}
				       			},
				       		{
				       			"MORPHOLOGY OF LESION SUBSTANCE",
				       			new List<string>
				       				{
				       					"Enhancement Quality",
				       					"Proportion Enhancing",
				       					"Proportion nCET",
				       					"Proportion Necrosis",
				       					"Proportion of Edema",
				       					"Cysts",
				       					"Multifocal or Multicentric",
				       					"T1/FLAIR Ratio"
				       				}
				       			},
				       		{
				       			"MORPHOLOGY OF LESION MARGIN",
				       			new List<string>
				       				{
				       					"Thickness of the Enhancing Margin",
				       					"Definition of the Enhancing Margin",
				       					"Definition of the Non-Enhancing Margin (e.g. Grade III)"
				       				}
				       			},
				       		{
				       			"ALTERATIONS IN VICINITY OF LESION",
				       			new List<string>
				       				{
				       					"Hemorrhage",
				       					"Diffusion",
				       					"Pial Invasion",
				       					"Ependymal Extension",
				       					"Cortical Involvement",
				       					"Deep WM Invasion",
				       					"nCET tumor Crosses Midline",
				       					"Enhancing Tumor Crosses Midline",
				       					"Satellites",
				       					"Calvarial Remodeling"
				       				}
				       			}
				       	};
			}
		}
	}
}

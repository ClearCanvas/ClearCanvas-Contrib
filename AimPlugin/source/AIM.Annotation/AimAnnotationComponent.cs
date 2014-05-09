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

using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.ImageViewer;
using ClearCanvas.Desktop.Validation;
using ClearCanvas.ImageViewer.Graphics;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.Dicom;
using ClearCanvas.ImageViewer.Services.ServerTree;
using ClearCanvas.Dicom.Network.Scu;
using ClearCanvas.Dicom.Network;
using ClearCanvas.ImageViewer.RoiGraphics;
using ClearCanvas.ImageViewer.RoiGraphics.Analyzers;

using aim_dotnet;
using AIM.Annotation.Configuration;

namespace AIM.Annotation
{
    /// <summary>
    /// Extension point for views onto <see cref="AimAnnotationComponent"/>.
    /// </summary>
    [ExtensionPoint]
    public sealed class AimAnnotationComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    /// <summary>
    /// AIMAnnotationComponent class.
    /// </summary>
    [AssociateView(typeof(AimAnnotationComponentViewExtensionPoint))]
    public class AimAnnotationComponent : ImageViewerToolComponent
    {
        private string _annotationName;
        private List<AnatomicEntity> _anatomicEntities;
        private List<ImagingObservation> _imagingObservations;
    	private List<Inference> _inferences;
        private int _annotationType; // TODO: remove this
        private StandardCodeSequence _annotationTypeCode;
    	private bool _isVasariTemplateValid;

        private string _selectedOntology; // ontology xml document which corresponds to the selected annotation type

        private string _imagingObservationCharacteristicOntology; // temp - ontology of Imaging Observation Characteristics (It's not integrated in AIM Ontology for now)

        // Map between SOP Instance UID and AE Title where the instance needs to be stored
        // Used for sending annotation instances back to PACS
        private Dictionary<string, string> _sopInstanceUidToAeTitle = new Dictionary<string, string>();
        private object _mapLock = new object();

        private ILocalDataStoreEventBroker _localDataStoreEventBroker;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AimAnnotationComponent(IDesktopWindow desktopWindow)
            : base(desktopWindow)
        {
            // TODO: 1. New default (-1) when UI is present for selecting the type;
            AnnotationType = 4; // NOTE: Setting annotation type triggers ontology loading

            _annotationTypeCode = this.AvailableAnnotationTypes[0];
        }

        [ValidateNotNull(Message = "AnnotationTypeCannotBeNull")]
        public StandardCodeSequence AnnotationTypeCode
        {
            get { return _annotationTypeCode; }
            set
            {
                if (_annotationTypeCode != value)
                {
                    if (_annotationTypeCode != null && _annotationTypeCode.Equals(value))
                        return; // the same code

                    _annotationTypeCode = value;

					//NotifyPropertyChanged("AnnotationTypeCode");
                    NotifyPropertyChanged("VasariTemplateVisible");
                    NotifyPropertyChanged("StandardTemplateVisible");
                }
            }
        }

        public List<StandardCodeSequence> AvailableAnnotationTypes
        {
            get { return new List<StandardCodeSequence> (CodeList.AnnotationTypeCodeSequences); }
        }

        public bool VasariTemplateVisible
        {
            get
            {
            	return this.AnnotationTypeCode == null || CodeList.IsAnnotationTypeVasari(this.AnnotationTypeCode);
            }
        }

        public bool StandardTemplateVisible
        {
            get { return !this.VasariTemplateVisible; }
        }

        [ValidateNotNull(Message = "AnnotationNameCannotBeNull")]
        public string AnnotationName
        {
            get { return _annotationName; }
            set
            {
                if (_annotationName != value)
                {
                    _annotationName = value;

                    NotifyPropertyChanged("AnnotationName");
                    NotifyPropertyChanged("CreateAnnotationEnabled");
                }
            }
        }

        public string UserName
        {
            get { return AimSettings.Default.UserName; }
            set
            {
				if (AimSettings.Default.UserName != value)
				{
					AimSettings.Default.UserName = value;
					AimSettings.Default.Save();
				}
            }
        }

        public string LoginName
        {
            get { return AimSettings.Default.UserLoginName; }
            set
            {
				if (AimSettings.Default.UserLoginName != value)
				{
					AimSettings.Default.UserLoginName = value;
					AimSettings.Default.Save();
				}
            }
        }

        public string RoleInTrial
        {
            get { return AimSettings.Default.UserRoleInTrial; }
            set
            {
				if (AimSettings.Default.UserRoleInTrial != value)
				{
					AimSettings.Default.UserRoleInTrial = value;
					AimSettings.Default.Save();
				}
            }
        }

        public int NumberWithinRoleInTrial
        {
            get { return AimSettings.Default.UserNumberWitinRoleOfClinicalTrial; }
			set
			{
				AimSettings.Default.UserNumberWitinRoleOfClinicalTrial = value;
				AimSettings.Default.Save();
			}
        }

        public int AnnotationType
        {
            get { return _annotationType; }
            set
            {
                if (_annotationType != value)
                {
                    _annotationType = value;

                    // Load new ontology

                    // TODO
                    // Theoretically, this would call Protege here, if it were not that slow


                    // Temporary Code for Hard-Coded ontology
                    ResourceResolver resolver = new ResourceResolver(this.GetType().Assembly);
                    try
                    {
 //                       using (Stream stream = resolver.OpenResource("Ontology.AIMOntology_1_0_4.xml"))
 //                       using (Stream stream = resolver.OpenResource("Ontology.AIMOntology_1_0.xml"))
 //                       using (Stream stream = resolver.OpenResource("Ontology.AIMOntology_1_2.xml"))
 //                       using (Stream stream = resolver.OpenResource("Ontology.AIMOntology_1_2_1.xml"))
                        using (System.IO.Stream stream = resolver.OpenResource("Ontology.AIMOntology_1_3_1.xml"))
                        {
                            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                            SelectedOntology = reader.ReadToEnd();
                        }
                    }
                    catch (Exception)
                    {
                        SelectedOntology = string.Empty;
                    }

                    // Temp. Imaging Observation Characteristics ontology
                    try
                    {
                        using (System.IO.Stream stream = resolver.OpenResource("Ontology.ImagingObsCharacteristicOntology_1.xml"))
                        {
                            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                            SelectedImgObsCharacteristicOntology = reader.ReadToEnd();
                        }
                    }
                    catch (Exception)
                    {
                        SelectedImgObsCharacteristicOntology = string.Empty;
                    }

                    NotifyPropertyChanged("AnnotationType");
                }
            }
        }

        [ValidateNotNull(Message = "AnatomicEntityCannotBeNull")]
        public List<AnatomicEntity> SelectedAnatomicEntities
        {
            get { return _anatomicEntities; }
            set
            {
                if (_anatomicEntities != value)
                {
                    _anatomicEntities = (null == value || value.Count == 0) ? null : new List<AnatomicEntity>(value);

					NotifyPropertyChanged("SelectedAnatomicEntities");
                    NotifyPropertyChanged("CreateAnnotationEnabled");
                }
            }
        }

        public List<ImagingObservation> SelectedImagingObservations
        {
            get { return _imagingObservations; }
            set
            {
                if (_imagingObservations != value)
                {
                    _imagingObservations = (null == value || value.Count == 0) ? null : new List<ImagingObservation>(value);

					NotifyPropertyChanged("SelectedImagingObservations");
                }
            }
        }

    	public List<Inference> SelectedInferences
    	{
			get { return _inferences; }
			set
			{
				if(_inferences != value)
				{
					_inferences = null == value || value.Count == 0 ? null : new List<Inference>(value);

					NotifyPropertyChanged("SelectedInferences");
				}
			}
    	}

        // Returns True is the whole annotation module should be enabled, False - if not
        public bool AnnotationModuleEnabled
        {
            //get { return this.ImageViewer != null && this.ImageViewer.PhysicalWorkspace.SelectedImageBox != null; }
            get { return this.ImageViewer != null && this.ImageViewer.SelectedPresentationImage != null; }
        }

        // Return True if Create Annotation button should be enabled, False - if not
        public bool CreateAnnotationEnabled
        {
            get
            {
                return this.AnnotationModuleEnabled && this.AnnotationTypeCode != null
                && !string.IsNullOrEmpty(_selectedOntology)
				//&& !string.IsNullOrEmpty(_annotationName) && !this.IsAnatomicEntityEmpty;
                && !string.IsNullOrEmpty(_annotationName)
				&& (!this.VasariTemplateVisible || (this.VasariTemplateVisible && this.IsVasariTemplateValid))
				&& this.HasValidUserData
				&& this.HasRequiredMakup;
            }
        }

        protected bool IsAnatomicEntityEmpty
        {
			//get { return _anatomicEntities == null || !_anatomicEntities.IsValidCodeSequence; }
			get { return _anatomicEntities == null || _anatomicEntities.Count == 0; }
        }

    	protected bool HasValidUserData
    	{
			get { return AimSettings.Default.RequireUserInfo ? !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.LoginName) : true; }
    	}

    	protected bool HasRequiredMakup
    	{
			get { return AimSettings.Default.RequireMarkupInAnnotation ? AimHelpers.IsImageMarkupPresent(this.ImageViewer.SelectedPresentationImage) : true; }
    	}

		// Flag that sets validity for VASARI template data
    	public bool IsVasariTemplateValid
    	{
			get { return _isVasariTemplateValid; }
			set
			{
				if (_isVasariTemplateValid != value)
				{
					_isVasariTemplateValid = value;
					NotifyPropertyChanged("CreateAnnotationEnabled");
				}
			}
    	}

        protected override void OnActiveImageViewerChanged(ActiveImageViewerChangedEventArgs e)
        {
            // Add update event handlers
            if (e.ActivatedImageViewer != null)
            {
                //e.ActivatedImageViewer.EventBroker.PresentationImageSelected += OnPresentationImageSelected;
                e.ActivatedImageViewer.EventBroker.TileSelected += OnTileSelected;
                e.ActivatedImageViewer.EventBroker.MouseCaptureChanged += OnMouseCaptureChanged;
            }

            NotifyPropertyChanged("AnnotationModuleEnabled");
            NotifyPropertyChanged("CreateAnnotationEnabled");
            NotifyPropertyChanged("CalculationInfo");
        }

        protected override void OnActiveImageViewerChanging(ActiveImageViewerChangedEventArgs e)
        {
            // Remove update event handlers
            if (e.DeactivatedImageViewer != null)
            {
                //e.DeactivatedImageViewer.EventBroker.PresentationImageSelected -= OnPresentationImageSelected;
                e.DeactivatedImageViewer.EventBroker.TileSelected -= OnTileSelected;
                e.DeactivatedImageViewer.EventBroker.MouseCaptureChanged -= OnMouseCaptureChanged;
            }
        }

        //protected void OnPresentationImageSelected(object sender, PresentationImageSelectedEventArgs e)
        //{
        //    NotifyPropertyChanged("AnnotationModuleEnabled");
        //    NotifyPropertyChanged("CreateAnnotationEnabled");
        //}

        private void OnTileSelected(object sender, TileSelectedEventArgs e)
        {
            NotifyPropertyChanged("AnnotationModuleEnabled");
            NotifyPropertyChanged("CreateAnnotationEnabled");
            NotifyPropertyChanged("CalculationInfo");
        }

        private void OnMouseCaptureChanged(object sender, ClearCanvas.ImageViewer.InputManagement.MouseCaptureChangedEventArgs e)
        {
            NotifyPropertyChanged("CalculationInfo");
        }

        #region Start/Stop methods
        /// <summary>
        /// Called by the host to initialize the application component.
        /// </summary>
        public override void Start()
        {
            base.Start();
            
            _localDataStoreEventBroker = LocalDataStoreActivityMonitor.CreatEventBroker();
            _localDataStoreEventBroker.SopInstanceImported += OnSopInstanceImported;
			AimSettings.Default.PropertyChanged += OnAimSettingsChanged;
        }

        /// <summary>
        /// Called by the host when the application component is being terminated.
        /// </summary>
        public override void Stop()
        {
			AimSettings.Default.PropertyChanged -= OnAimSettingsChanged;
            _localDataStoreEventBroker.SopInstanceImported -= OnSopInstanceImported;
            _localDataStoreEventBroker.Dispose();

            base.Stop();
        }
        #endregion

		private void OnAimSettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			NotifyPropertyChanged("CreateAnnotationEnabled");
		}

        public string SelectedOntology
        {
            get { return _selectedOntology; }
            set
            {
                if (_selectedOntology != value)
                {
                    _selectedOntology = value;

                    SelectedAnatomicEntities = null;
                    SelectedImagingObservations = null;

                    NotifyPropertyChanged("SelectedOntology");
                }
            }
        }

        public string SelectedImgObsCharacteristicOntology
        {
            get { return _imagingObservationCharacteristicOntology; }
            set
            {
                if (_imagingObservationCharacteristicOntology != value)
                {
                    _imagingObservationCharacteristicOntology = value;

                    NotifyPropertyChanged("SelectedImgObsCharacteristicOntology");
                }
            }
        }

		//public List<string> CalculationInfo
        public string[] CalculationInfo
        {
            get
            {
                if (this.ImageViewer != null)
                {
                    IOverlayGraphicsProvider overlayGraphicProvider = this.ImageViewer.SelectedPresentationImage as IOverlayGraphicsProvider;
                    if (overlayGraphicProvider != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        string lineFeed = Environment.NewLine + "    ";

                        int roiCnt = 0;
                        foreach (IGraphic graphic in overlayGraphicProvider.OverlayGraphics)
                        {
                            RoiGraphic currentRoiGraphic = graphic as RoiGraphic;
                            if (currentRoiGraphic != null)
                            {
                                if (sb.Length > 0)
                                    sb.Append(Environment.NewLine);
								if (string.IsNullOrEmpty(currentRoiGraphic.Name))
									sb.AppendFormat("ROI #{0}:", ++roiCnt);
								else
									sb.AppendFormat("ROI #{0} ({1}):", ++roiCnt, currentRoiGraphic.Name);
                                Roi roi = currentRoiGraphic.Roi;
                                foreach (IRoiAnalyzer analyzer in currentRoiGraphic.Callout.RoiAnalyzers)
                                {
                                    if (analyzer.SupportsRoi(roi))
                                    {
                                        string text = analyzer.Analyze(roi, RoiAnalysisMode.Normal).Trim().Replace("\r\n", "{0}").Replace("\n", "{0}");
                                        sb.Append(lineFeed);
                                        sb.AppendFormat(text, lineFeed);
                                    }
                                }
                            }
                        }

                        string[] strRetVal = sb.ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
						sb.Length = 0;
                    	return strRetVal;

						//List<string> retVal = new List<string>(strRetVal.Length);
						//retVal.AddRange(strRetVal);
						//return retVal;

                        //return sb.ToString().Split(new string[]{"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
                    }
                }

				//return new List<string>();
                return new string[0];
            }

            set {  }
        }

		private void ResetAnnotationData()
		{
			this.SelectedImagingObservations = null;
			this.SelectedAnatomicEntities = null;
			this.SelectedInferences = null;
			this.AnnotationName = null;
		}

		// Creates AIM Annotation(s) based on user template/image selection and our internal preferences
		// Returns a list of the created annotations and a list of IGraphic elements used to create the annotations.
		// The list of graphic elements is used later to replace these elements with AimGraphics
		private List<aim_dotnet.Annotation> CreateAnnotationsFromUserInput(out List<IGraphic> annotationsGraphic)
		{
			AimAnnotationCreationContext aimContext = new AimAnnotationCreationContext(AnnotationKind.AK_ImageAnnotation, AnnotationTypeCode, AnnotationName);
			aimContext.SelectedAnatomicEntities = this.SelectedAnatomicEntities;
			aimContext.SelectedImagingObservations = this.SelectedImagingObservations;
			aimContext.SelectedInferences = this.SelectedInferences;
			aimContext.AnnotationUser = (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.LoginName))
			                            	?
			                            		new User
			                            			{
			                            				Name = this.UserName,
			                            				LoginName = this.LoginName,
			                            				RoleInTrial = this.RoleInTrial,
			                            				NumberWithinRoleOfClinicalTrial = this.NumberWithinRoleInTrial >= 0 ? this.NumberWithinRoleInTrial : -1
			                            			}
			                            	: null;
			aimContext.includeCalculations = true; // !this.VasariTemplateVisible; // no calculations for VASARI
			// !!! Create annotation for the current image only !!!
			aimContext.SOPImageUIDs = new List<string> { ((IImageSopProvider)this.ImageViewer.SelectedPresentationImage).ImageSop.SopInstanceUid };
			//            aim_dotnet.Annotation annotation = AIMHelpers.CreateAimAnnotation(this.ImageViewer.SelectedPresentationImage, aimContext);
			return AimHelpers.CreateAimAnnotations(this.ImageViewer.SelectedPresentationImage.ParentDisplaySet.PresentationImages, aimContext, out annotationsGraphic);
		}

		// Main method called by templates to create annotaion(s)
		public void CreateAnnotation()
		{
			if (!this.CreateAnnotationEnabled)
				return;

			if (base.HasValidationErrors)
			{
				this.ShowValidation(true);
				return;
			}

			// We should bail out here even we only want to store it locally and/or to the grid.
			// There will be no way for us to display the new annotation back to the user after we remove the original markup.
			if (!LocalDataStoreActivityMonitor.IsConnected)
			{
				this.DesktopWindow.ShowMessageBox("Failed to save annotation. Not connected to the local data store. Is workstation service running?", MessageBoxActions.Ok);
				return;
			}

			// Get new annotations
			List<IGraphic> annotationsGraphic;
			List<aim_dotnet.Annotation> annotations = this.CreateAnnotationsFromUserInput(out annotationsGraphic);
			if (annotations.Count == 0)
			{
				Platform.Log(LogLevel.Warn, "CreateAnnotation resulted in no annotations being created");
				return;
			}

			bool isAnyOperationPerformed = false;

			// Save AIM XML to the Desktop
			if (AimSettings.Default.SaveXmlToDesktop)
			{
				int annCnt = AimHelpers.WriteXmlAnnotationsToFolder(annotations, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				if (annotations.Count != annCnt)
					Platform.Log(LogLevel.Error, "Only {0} annotations(s) out of {1} written to the Desktop", annCnt, annotations.Count);

				isAnyOperationPerformed = annCnt > 0 || isAnyOperationPerformed;
			}

			// Save AIM XML Annotations locally
			if (AimSettings.Default.StoreXmlAnnotationsLocally)
			{
				string destinationFolder
					= AimSettings.Default.StoreXmlInMyDocuments
					  	?
					  		System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AIM Annotations")
					  	:
					  		AimSettings.Default.LocalAnnotationsFolder;
				try
				{
					if (!System.IO.Directory.Exists(destinationFolder))
						System.IO.Directory.CreateDirectory(destinationFolder);

					int annCnt = AimHelpers.WriteXmlAnnotationsToFolder(annotations, destinationFolder);
					if (annotations.Count != annCnt)
						Platform.Log(LogLevel.Error, "Only {0} annotations(s) out of {1} written to \"{0}\"", annCnt, annotations.Count, destinationFolder);

					isAnyOperationPerformed = annCnt > 0 || isAnyOperationPerformed;
				}
				catch (Exception ex)
				{
					Platform.Log(LogLevel.Error, ex, "Failed to store annotations to local folder \"{0}\"", destinationFolder);
				}
			}

			// RSNA code. Real one is at the start of the method
			if (LocalDataStoreActivityMonitor.IsConnected)
				isAnyOperationPerformed = this.SendAnnotationsToLocalStorageAndPacs(annotations) || isAnyOperationPerformed;
			else
				this.DesktopWindow.ShowMessageBox("Failed to save annotation. Not connected to the local data store. Is workstation service running?", MessageBoxActions.Ok);

			if (isAnyOperationPerformed)
			{
				// Remove all IGraphic used to create annotations and add them back as AimGraphic
				this.UpdateImagesWithProperAimGraphic(annotations, annotationsGraphic);

				// Reset all controls on the template
				this.ResetAnnotationData();
			}
		}

		// Helper. Removes given IGraphic from their images and adds given annotations to the images as AimGraphic
		private void UpdateImagesWithProperAimGraphic(List<aim_dotnet.Annotation> annotations, List<IGraphic> annotationsGraphic)
		{
			if (annotationsGraphic != null)
			{
				foreach (IGraphic graphic in annotationsGraphic)
				{
					// For now, we should be creating annotations only for the current image
					// Code below that reads annotations back depends on this.
					// Otherwise we either need to keep track of images from which we delete graphics (could be different from the images for the annotations?)
					// or to pass in a list of images.
					Debug.Assert(graphic.ParentPresentationImage == null || graphic.ParentPresentationImage == this.ImageViewer.SelectedPresentationImage);

					IOverlayGraphicsProvider currentOverlayGraphics = graphic.ParentPresentationImage as IOverlayGraphicsProvider;
					if (currentOverlayGraphics != null && currentOverlayGraphics.OverlayGraphics != null)
						currentOverlayGraphics.OverlayGraphics.Remove(graphic);
		
				}
			}

			if(annotations != null)
			{
				foreach (aim_dotnet.Annotation annotation in annotations)
				{
					AimHelpers.ReadGraphicsFromAnnotation(annotation, this.ImageViewer.SelectedPresentationImage);
				}
			}
		}

    	private bool SendAnnotationsToLocalStorageAndPacs(List<aim_dotnet.Annotation> annotations)
		{
			// NOTE: This code assumes that all images came from the same server.

			// 0. Save each annotation to a temp DICOM file
			DcmModel model = new DcmModel();
			// Map of InstanceUID=>fileName
			Dictionary<string, string> instanceInfos = new Dictionary<string, string>();
			foreach (aim_dotnet.Annotation annotation in annotations)
			{
				// Save annotation to a temp file first
				string tempFileName = System.IO.Path.GetTempFileName();
				try
				{
					model.WriteAnnotationToFile(annotation, tempFileName);
				}
				catch (Exception ex)
				{
					Platform.Log(LogLevel.Error, "Failed to save annotation to temp file.", ex);
					try
					{
						System.IO.File.Delete(tempFileName);
					}
					catch
					{
					}
					continue;
				}

				// Get SOP Instance UID of our annotation
				DicomFile annFile = new DicomFile(tempFileName);
				annFile.Load(DicomReadOptions.Default | DicomReadOptions.DoNotStorePixelDataInDataSet);
				string annSopInstanceUid = annFile.DataSet[DicomTags.SopInstanceUid].GetString(0, string.Empty);
				annFile = null;

				instanceInfos.Add(annSopInstanceUid, tempFileName);
			}
			model = null;

			if (instanceInfos.Count < 1)
				return false; // Save failed

			// Get AE Title for remote storage. NOTE: we use AE Title of the first image for all images
			string imageAeTitle = string.Empty;
			IImageSopProvider imageSopProvider = this.ImageViewer.SelectedPresentationImage as IImageSopProvider;
			if (imageSopProvider != null)
			{
				//imageAeTitle = imageSopProvider.ImageSop[DicomTags.SourceApplicationEntityTitle].GetString(0, string.Empty);
				ILocalSopDataSource localSopDataSource = imageSopProvider.ImageSop.DataSource as ILocalSopDataSource; //NativeDicomObject as DicomFile;
				if (localSopDataSource != null)
					imageAeTitle = localSopDataSource.File.SourceApplicationEntityTitle.Trim("\n\r".ToCharArray());
			}

			// 1. Import into the local storage
			using (LocalDataStoreServiceClient localClient = new LocalDataStoreServiceClient())
			{
				try
				{
					localClient.Open();
					FileImportRequest request = new FileImportRequest();
					request.BadFileBehaviour = BadFileBehaviour.Delete;
					request.FileImportBehaviour = FileImportBehaviour.Move;
					List<string> filePaths = new List<string>();
					foreach (KeyValuePair<string, string> instanceInfo in instanceInfos)
					{
						string annSopInstanceUid = instanceInfo.Key;
						string tempFileName = instanceInfo.Value;

						filePaths.Add(tempFileName);
						// Store destination AE title for the annotaion
						if (!string.IsNullOrEmpty(imageAeTitle))
						{
							lock (_mapLock)
								_sopInstanceUidToAeTitle.Add(annSopInstanceUid, imageAeTitle);
						}
					}
					request.FilePaths = filePaths.ToArray();
					request.IsBackground = true;
					request.Recursive = false;
					localClient.Import(request);
					localClient.Close();
				}
				catch (Exception ex)
				{
					localClient.Abort();
					Platform.Log(LogLevel.Error, ex);
					this.DesktopWindow.ShowMessageBox("Failed to store your annotation(s).", "Annotation Import Error", MessageBoxActions.Ok);
					return false;
				}
			}

			// 2. Saving to PACS happens after the SOP Instance is imported (OnSopInstanceImported)

			return true;
		}

    	// Find configuration for the given AE Title
        private Server FindAETitle(ServerGroup serverGroup, string aeTitle)
        {
            foreach (Server server in serverGroup.ChildServers)
            {
                if (server.AETitle == aeTitle)
                    return server;
            }

            foreach (ServerGroup childeServerGroup in serverGroup.ChildGroups)
            {
                Server server = FindAETitle(childeServerGroup, aeTitle);
                if (server != null)
                    return server;
            }

            return null;
        }

        // Called when a SOP Instance is imported into the local datastore
        private void OnSopInstanceImported(object sender, ItemEventArgs<ImportedSopInstanceInformation> e)
        {
            // Send new annotation to PACS
            if (_sopInstanceUidToAeTitle.ContainsKey(e.Item.SopInstanceUid))
            {
                string destinationAeTitle = _sopInstanceUidToAeTitle[e.Item.SopInstanceUid];
                Server destinationServer = FindAETitle(new ServerTree().RootNode.ServerGroupNode, destinationAeTitle);
                if (destinationServer == null)
                {
                    Platform.Log(LogLevel.Error, "Study " + e.Item.SopInstanceUid + "cannot be send to server. Failed to find server infromation for AE Title " + destinationAeTitle +".");
                }
                else
                {
                    StorageScu storageScu = new StorageScu(ServerTree.GetClientAETitle(), destinationServer.AETitle, destinationServer.Host, destinationServer.Port);
                    storageScu.ImageStoreCompleted += OnStoreEachInstanceCompleted;
                    storageScu.AddFile(e.Item.SopInstanceFileName);
                    IAsyncResult asyncResult = storageScu.BeginSend(OnAnnotationSendComplete, storageScu);
                }
                lock (_mapLock)
                    _sopInstanceUidToAeTitle.Remove(e.Item.SopInstanceUid);
            }
        }

        private void OnStoreEachInstanceCompleted(object sender, StorageInstance e)
        {
            //StorageScu storageScu = (StorageScu)sender;
            if (e.SendStatus.Status != DicomState.Success)
                Platform.Log(LogLevel.Error, "Failed to send annotation to server. Status: " + e.SendStatus.Description);
        }

        private void OnAnnotationSendComplete(IAsyncResult ar)
        {
            StorageScu storageScu = (StorageScu)ar.AsyncState;
            if (storageScu.TotalSubOperations - storageScu.SuccessSubOperations > 0)
                Platform.Log(LogLevel.Error, "Not all annotations could be stored to server.");
            storageScu.Dispose();
        }

        private void OnImageViewerClosing(object sender, ClosingEventArgs e)
        {
            RunCreateAnnotation();
        }

        public void RunCreateAnnotation()
        {
            BlockingOperation.Run(
                delegate()
                {

                    this.CreateAnnotation();

                    if (_sopInstanceUidToAeTitle.Count == 0)
                        Thread.Sleep(500);
                    else
                    {
                        int totalAnnotations = _sopInstanceUidToAeTitle.Count;
                        while (_sopInstanceUidToAeTitle.Count > 0)
                        {
                            //int currentAnnotation = totalAnnotations - _sopInstanceUidToAeTitle.Count + 1;
                            //int percentComplete = (int) (((float) (currentAnnotation)/totalAnnotations)*100 - .1);
                            //string message = string.Format("Importing annotation {0} out of {1}", currentAnnotation, totalAnnotations);
                            //context.ReportProgress(new BackgroundTaskProgress(percentComplete, message));
                            Thread.Sleep(500);
                        }
                    }

                    //context.Complete(null);

                });
		}
	}
}

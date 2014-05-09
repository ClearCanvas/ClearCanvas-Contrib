/////////  WORK IN PROGRESS
// TODO:
// Write query image code for remote server  (tried but limited by the values returned by server, not that useful)
// Image code for local dbase


#region License

// Copyright (c) 2006-2008, Seth Berkowitz
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

using System;
using System.Collections.Generic;
using System.Text;

using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.ImageViewer.Explorer.Dicom;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Network.Scu;
using ClearCanvas.ImageViewer.Services.ServerTree;
using ClearCanvas.ImageViewer.Services.Tools;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.Dicom.Utilities.Anonymization;


using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.Services;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.Dicom.Utilities;
using ClearCanvas.Desktop.Tables;
using ClearCanvas.ImageViewer.Annotations.Dicom;

namespace SeriesQuery
{
    [MenuAction("apply", "dicomseriesbrowser-toolbar/Image Query", "Apply")]
    [ButtonAction("apply", "dicomseriesbrowser-contextmenu/Image Query", "Apply")]
    [Tooltip("apply", "Query Images in Series")]
    [IconSet("apply", IconScheme.Colour, "Icons.SeriesQueryToolSmall.png", "SeriesQueryToolSmall.png", "Icons.SeriesQueryToolLarge.png")]
    [EnabledStateObserver("apply", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(SeriesBrowserToolExtensionPoint))]
    public class ImageQuery : Tool<ISeriesBrowserToolContext>
    {
        private bool _enabled;
        private event EventHandler _enabledChanged;
        private IShelf _image_shelf;
        private IApplicationComponent _component;
        private static object _localStudyLoader = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public ImageQuery()
        {
            _enabled = true;
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Gets whether this tool is enabled/disabled in the UI.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            protected set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    EventsHelper.Fire(_enabledChanged, this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Notifies that the <see cref="Enabled"/> state of this tool has changed.
        /// </summary>
        public event EventHandler EnabledChanged
        {
            add { _enabledChanged += value; }
            remove { _enabledChanged -= value; }
        }

        /// <summary>
        /// Called by the framework when the user clicks the "apply" menu item or toolbar button.
        /// </summary>
        public void Apply()
        {
            List<string> seriesUIDs = new List<string>();
            List<string> studyUIDs = new List<string>();

            foreach (SeriesItem item in this.Context.SelectedMultipleSeries)
            {
                string foo = item.SeriesInstanceUID;
                Platform.Log(LogLevel.Info, foo);
                seriesUIDs.Add(item.SeriesInstanceUID);
                studyUIDs.Add(item.StudyInstanceUID);
            }

            // Code for local data store only (tool should be disabled for remote devices

            IImageViewer viewer = new ImageViewerComponent();
            StudyTree studyTree = viewer.StudyTree;

            List<SeriesItem> imageList = new List<SeriesItem>();

            // Add selected objects studies to study tree
            foreach (SeriesItem selectedseries in this.Context.SelectedMultipleSeries)
            {
                string studyUID = selectedseries.StudyInstanceUID;
                int numberOfSops = LocalStudyLoader.Start(new StudyLoaderArgs(studyUID, null));
                for (int i = 0; i < numberOfSops; ++i)
                {
                    Sop imageSop = LocalStudyLoader.LoadNextSop();
                    studyTree.AddSop(imageSop);
                }
            }

            foreach (Patient patient in studyTree.Patients)
                {
                    foreach (Study study in patient.Studies)
                    {
                        string description = "";
                        foreach (Series series in study.Series)
                        {
                            string saveseries = seriesUIDs.Find(delegate(string s) { return s.Equals(series.SeriesInstanceUid); });
                            
                            if (saveseries != null)
                            {
                                foreach (Sop sop in series.Sops)
                                {
                                    SeriesItem image = new SeriesItem();

                                    DicomFile file = ((ILocalSopDataSource)sop.DataSource).File;
                                    StudyData originalData = new StudyData();
                                    file.DataSet.LoadDicomFields(originalData);
                                    image.Time = file.DataSet[DicomTags.TriggerTime];
                                    image.SeriesDescription = file.DataSet[DicomTags.SeriesDescription];
                                    image.Modality = file.DataSet[DicomTags.Modality];
                                    image.SeriesNumber = sop.InstanceNumber.ToString();
                                    image.Date = file.DataSet[DicomTags.StudyDate];
                                    imageList.Add(image);
                                    description = image.SeriesDescription;
                                }
                            }

                       }
                       _component = new ImageBrowserComponent(imageList, null);
                       _image_shelf = ApplicationComponent.LaunchAsShelf(this.Context.DesktopWindow, _component, DicomDataFormatHelper.PersonNameFormatter(patient.PatientsName) + ":  " + description, ShelfDisplayHint.DockBottom | ShelfDisplayHint.DockAutoHide);
                       _image_shelf.Closed += Image_Shelf_Closed;   
                    }
                }
      }

        private void UpdateEnabled()
        {
            //this.Enabled = (this.Context.SelectedStudy != null &&
            //                !this.Context.SelectedServerGroup.IsLocalDatastore &&
            //                LocalDataStoreActivityMonitor.IsConnected);
            //this.Enabled = ( this.Context.SelectedServerGroup.IsLocalDatastore);
        }

        private static IStudyLoader LocalStudyLoader
        {
            get
            {
                if (_localStudyLoader == null)
                {
                    try
                    {
                        StudyLoaderExtensionPoint xp = new StudyLoaderExtensionPoint();
                        foreach (IStudyLoader loader in xp.CreateExtensions())
                        {
                            if (loader.Name == "DICOM_LOCAL")
                            {
                                _localStudyLoader = loader;
                                break;
                            }
                        }
                    }
                    catch (NotSupportedException)
                    {
                        Platform.Log(LogLevel.Info, "Batch Retrieve tool disabled; no local study loader exists.");
                    }

                    if (_localStudyLoader == null)
                        _localStudyLoader = new object(); //there is no loader.
                }

                return _localStudyLoader as IStudyLoader;
            }
        }

        private void Image_Shelf_Closed(object sender, ClosedEventArgs e)
        {
            //_image_shelf.Closed -= Image_Shelf_Closed;
            _image_shelf = null;

        }

        }  
}

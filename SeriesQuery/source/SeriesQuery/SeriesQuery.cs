#region License

// Copyright (c) 2006-2010, Seth Berkowitz
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
using System.IO;
using System.Diagnostics;


using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.ImageViewer.Explorer.Dicom;
using ClearCanvas.ImageViewer.Services;

using ClearCanvas.Dicom;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.Dicom.Utilities;
using ClearCanvas.ImageViewer.Services.ServerTree;
using ClearCanvas.Dicom.Network.Scu;
using ClearCanvas.Desktop.Tables;
using ClearCanvas.ImageViewer.Annotations.Dicom;



namespace SeriesQuery
{
    [MenuAction("SeriesQuery", "dicomstudybrowser-contextmenu/Get Series List", "SeriesQuery")]
    [ButtonAction("SeriesQuery", "dicomstudybrowser-toolbar/Export Study List", "SeriesQuery")]
    [Tooltip("SeriesQuery", "Get Series List")]
    [IconSet("SeriesQuery", IconScheme.Colour, "Icons.SeriesQueryToolSmall.png", "Icons.SeriesQueryToolMedium.png", "Icons.SeriesQueryToolLarge.png")]
    [EnabledStateObserver("SeriesQuery", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(StudyBrowserToolExtensionPoint))]
    public class SeriesQueryTool : StudyBrowserTool
    {
        private IShelf _shelf;
        private IApplicationComponent _component;
        private static object _localStudyLoader = null;
   
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public SeriesQueryTool()
        {
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            //  SetDoubleClickHandler();

            base.Initialize();
        }


        /// <summary>
        /// Called by the framework when the user clicks the "apply" menu item or toolbar button.
        /// </summary>
        public void SeriesQuery()
        {

            string callingAE = ServerTree.GetClientAETitle();
            List<SeriesItem> SeriesList = new List<SeriesItem>();

            #region remote datastore
            // If remote data store, need to query server for series level information
            if (!this.Context.SelectedServerGroup.IsLocalDatastore)
            {
                // Loop through all selected servers
                foreach (Server node in this.Context.SelectedServerGroup.Servers)
                {
                    DicomAttributeCollection dicomAttributeCollection = new DicomAttributeCollection();
                    // Query on "Series" Level
                    dicomAttributeCollection[DicomTags.QueryRetrieveLevel].SetStringValue("SERIES");

                    string studyUID = this.Context.SelectedStudies[0].StudyInstanceUid;
                    dicomAttributeCollection[DicomTags.StudyInstanceUid].SetStringValue(studyUID);
                    dicomAttributeCollection[DicomTags.SeriesDescription].SetNullValue();
                    dicomAttributeCollection[DicomTags.SeriesInstanceUid].SetNullValue();
                    dicomAttributeCollection[DicomTags.SeriesNumber].SetNullValue();
                    dicomAttributeCollection[DicomTags.Modality].SetNullValue();
                    dicomAttributeCollection[DicomTags.Date].SetNullValue();
                    dicomAttributeCollection[DicomTags.Time].SetNullValue();
                    dicomAttributeCollection[DicomTags.RepetitionTime].SetNullValue();

                    IList<DicomAttributeCollection> resultsList;
                    StudyRootFindScu findScu = new StudyRootFindScu();
                    List<string> seriesUIDs = new List<string>();

                    resultsList = findScu.Find(
                                    callingAE,
                                    node.AETitle,
                                    node.Host,
                                    node.Port,
                                    dicomAttributeCollection);

                    findScu.CloseAssociation();
                    findScu.Dispose();

                    foreach (DicomAttributeCollection msg in resultsList)
                    {
                        string text = msg[DicomTags.SeriesInstanceUid];
                        Platform.Log(LogLevel.Info, text);

                        SeriesItem series = new SeriesItem();

                        series.SeriesNumber = msg[DicomTags.SeriesNumber];
                        series.SeriesDescription = msg[DicomTags.SeriesDescription];
                        series.StudyInstanceUID = msg[DicomTags.StudyInstanceUid];
                        series.SeriesInstanceUID = msg[DicomTags.SeriesInstanceUid];
                        series.Modality = msg[DicomTags.Modality];
                        series.Date = msg[DicomTags.Date];
                        series.Time = msg[DicomTags.Time];
                        //series.NumberOfSeriesRelatedInstances = int.Parse(msg[DicomTags.NumberOfSeriesRelatedInstances].ToString());
                        SeriesList.Add(series);

                    }
                    _component = new SeriesBrowserComponent(SeriesList, node);
                    _shelf = ApplicationComponent.LaunchAsShelf(this.Context.DesktopWindow, _component, "Series Browser", ShelfDisplayHint.DockBottom | ShelfDisplayHint.DockAutoHide);
                    _shelf.Closed += Shelf_Closed;
                }              
            }
            #endregion
           
            #region Local Datastore
            // If local datastore, can obtain series information by building study tree
            else
            {
                IImageViewer viewer = new ImageViewerComponent();
                StudyTree studyTree = viewer.StudyTree;

                // Add selected objects studies to study tree
                foreach (StudyItem selectedstudy in this.Context.SelectedStudies)
                {
                    string studyUID = selectedstudy.StudyInstanceUid;
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
                        foreach (Series series in study.Series)
                        {
                            SeriesItem seriesitem = new SeriesItem();

                            seriesitem.SeriesNumber = series.SeriesNumber.ToString();
                            seriesitem.SeriesDescription = series.SeriesDescription;
                            seriesitem.StudyInstanceUID = study.StudyInstanceUid;
                            seriesitem.SeriesInstanceUID = series.SeriesInstanceUid;
                            seriesitem.Modality = series.Modality;
                            seriesitem.Date = series.SeriesDate;
                            seriesitem.Time = series.SeriesTime;
                            //series.NumberOfSeriesRelatedInstances = int.Parse(msg[DicomTags.NumberOfSeriesRelatedInstances].ToString());
                            seriesitem.NumberOfSeriesRelatedInstances = series.Sops.Count.ToString();

                            SeriesList.Add(seriesitem);
                        }
                    _component = new SeriesBrowserComponent(SeriesList, null);
                    _shelf = ApplicationComponent.LaunchAsShelf(this.Context.DesktopWindow, _component, DicomDataFormatHelper.PersonNameFormatter(patient.PatientsName), ShelfDisplayHint.DockBottom | ShelfDisplayHint.DockAutoHide);
                    _shelf.Closed += Shelf_Closed;
                    }
                }
            }
            #endregion

        }

        private void Shelf_Closed(object sender, ClosedEventArgs e)
        {
            //_shelf.Closed -= Shelf_Closed;
            _shelf = null;

        }

        protected override void OnSelectedStudyChanged(object sender, EventArgs e)
        {
            if (_shelf != null && !_shelf.State.Equals(DesktopObjectState.Closed))
                //&& _shelf.Active ==false)
            {
                try
                {
                    _shelf.Close();
                }
                catch { }
            }

            UpdateEnabled();
        }

        protected override void OnSelectedServerChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
            //SetDoubleClickHandler();
        }
        private void UpdateEnabled()
        {
            this.Enabled = (this.Context.SelectedStudy != null &&
                            LocalDataStoreActivityMonitor.IsConnected);
        }

        // Create an interface for the StudyLoader
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


    }

           
}

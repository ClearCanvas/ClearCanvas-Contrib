#region License

// Copyright (c) 2006-2008, ClearCanvas Inc.
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

namespace SeriesQuery
{
    [MenuAction("apply", "dicomseriesbrowser-toolbar/ToolbarRetrieveSeries", "Apply")]
    [ButtonAction("apply", "dicomseriesbrowser-contextmenu/ToolbarRetrieveSeries", "Apply")]
    [Tooltip("apply", "Retrieve Series")]
    [IconSet("apply", IconScheme.Colour, "Icons.RetrieveSeriesToolSmall.png", "RetrieveSeriesToolSmall.png", "Icons.RetrieveSeriesToolLarge.png")]
    [EnabledStateObserver("apply", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(SeriesBrowserToolExtensionPoint))]
    public class RetrieveSeries : Tool<ISeriesBrowserToolContext>
    {
        private bool _enabled;
        private event EventHandler _enabledChanged;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public RetrieveSeries()
        {
            _enabled = true;
            
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (this.Context.SelectedServer.Name.Equals("localdatastore"))
            {
                _enabled = false;
            }
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

        // Note: you may change the name of the 'Apply' method as desired, but be sure to change the
        // corresponding parameter in the MenuAction and ButtonAction attributes

        /// <summary>
        /// Called by the framework when the user clicks the "apply" menu item or toolbar button.
        /// </summary>
        public void Apply()
        {
            // TODO: add code here to implement the functionality of the tool
            //|| this.Context.SelectedServerGroup.IsLocalDatastore 
            if (!Enabled || this.Context.SelectedSingleSeries == null)
                return;

            //Dictionary<ApplicationEntity, List<StudyInformation>> retrieveInformation = new Dictionary<ApplicationEntity, List<StudyInformation>>();

            List<string> seriesUIDs = new List<string>();
            List<string> studyUIDs = new List<string>();

            foreach (SeriesItem item in this.Context.SelectedMultipleSeries)
            {
                string foo = item.SeriesInstanceUID;
                Platform.Log(LogLevel.Info, foo);
                seriesUIDs.Add(item.SeriesInstanceUID);
                studyUIDs.Add(item.StudyInstanceUID);

            }

            
            string callingAE = ServerTree.GetClientAETitle();


            BackgroundTask task = new BackgroundTask(
                    delegate(IBackgroundTaskContext context)
                    {

                        // Send series level c-move
                        //MyMoveScu moveScu = new MyMoveScu(
                        //DicomMoveManager.MyMoveScu moveScu = new DicomMoveManager.MyMoveScu( 
                        MoveScuBase moveScu = new StudyRootMoveScu(
                                    callingAE,
                                    this.Context.SelectedServer.AETitle,
                                    this.Context.SelectedServer.Host,
                                    this.Context.SelectedServer.Port,
                                    callingAE);
                        // Need to disable this line for the 1.5 version, because the movescu
                        // code only allows one level per movescu request.  This may not work with some
                        // PACS
                        moveScu.AddStudyInstanceUid(studyUIDs[0]);
                        foreach (string seriesUID in seriesUIDs)
                            moveScu.AddSeriesInstanceUid(seriesUID);
                        moveScu.Move();
                        //moveScu.updated += delegate(object sender, EventArgs args)
                        //{
                        //    OnMoveUpdate(context, moveScu.completed, moveScu.total, moveScu.movestatus);
                        //    //context.ReportProgress(new BackgroundTaskProgress(moveScu.completed / moveScu.total, "Moving Study"));
                        //    //OnMoveUpdate(context, moveScu.completed, moveScu.total);
                        //};
                        //  += new EventHandler(OnMoveCompleted);
                        moveScu.Dispose();
                        //OnMoveCompleted();
                    }, true);
            task.Run();

            LocalDataStoreActivityMonitorComponentManager.ShowSendReceiveActivityComponent(this.Context.DesktopWindow);
           



        //        //if (!retrieveInformation.ContainsKey(item.Server))
        //        //    retrieveInformation[item.Server] = new List<StudyInformation>();

        //        //StudyInformation studyInformation = new StudyInformation();
        //        //studyInformation.PatientId = item.PatientId;
        //        //studyInformation.PatientsName = item.PatientsName;
        //        //DateTime studyDate;
        //        //DateParser.Parse(item.StudyDate, out studyDate);
        //        //studyInformation.StudyDate = studyDate;
        //        //studyInformation.StudyDescription = item.StudyDescription;
        //        //studyInformation.StudyInstanceUid = item.StudyInstanceUID;

        //        //retrieveInformation[item.Server].Add(studyInformation);
            

        }

    }
}

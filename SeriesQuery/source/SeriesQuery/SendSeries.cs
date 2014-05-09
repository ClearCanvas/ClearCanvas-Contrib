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
using ClearCanvas.ImageViewer.Services;
using ClearCanvas.ImageViewer.Services.ServerTree;
using ClearCanvas.ImageViewer.Configuration.ServerTree;
//using ClearCanvas.ImageViewer.Services.Configuration.ServerTree;
using ClearCanvas.ImageViewer.Services.Tools;
using ClearCanvas.ImageViewer.Services.DicomServer;

namespace SeriesQuery
{
    // This template provides the boiler-plate code for creating a basic tool
    // that performs a single action when its menu item or toolbar button is clicked.

    // Declares a menu action with action ID "apply"
    // TODO: Change the action path hint to your desired menu path, or
    // remove this attribute if you do not want to create a menu item for this tool
    [MenuAction("apply", "dicomseriesbrowser-toolbar/ToolbarSendSeries", "Apply")]

    // Declares a toolbar button action with action ID "apply"
    // TODO: Change the action path hint to your desired toolbar path, or
    // remove this attribute if you do not want to create a toolbar button for this tool
    [ButtonAction("apply", "dicomseriesbrowser-contextmenu/ToolbarSendSeries", "Apply")]

    // Specifies tooltip text for the "apply" action
    // TODO: Replace tooltip text
    [Tooltip("apply", "Send Series")]

    // Specifies icon resources to use for the "apply" action
    // TODO: Replace the icon resource names with your desired icon resources
    [IconSet("apply", IconScheme.Colour, "Icons.SendStudyToolSmall.png", "SendStudyToolSmall.png", "Icons.SendStudyToolLarge.png")]

    // Specifies that the enablement of the "apply" action in the user-interface
    // is controlled by observing a boolean property named "Enabled", listening to
    // an event named "EnabledChanged" for changes to this property
    [EnabledStateObserver("apply", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(SeriesBrowserToolExtensionPoint))]
    public class SendSeries : Tool<ISeriesBrowserToolContext>
    {
        private bool _enabled;
        private event EventHandler _enabledChanged;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public SendSeries()
        {
            _enabled = false;
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (this.Context.SelectedServer.Name.Equals("localdatastore"))
            {
                _enabled = true;
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
            if (!Enabled || this.Context.SelectedSingleSeries == null)
                return;

            List<string> seriesUIDs = new List<string>();
            List<string> studyUIDs = new List<string>();

            foreach (SeriesItem item in this.Context.SelectedMultipleSeries)
            {
                string foo = item.SeriesInstanceUID;
                Platform.Log(LogLevel.Info, foo);
                seriesUIDs.Add(item.SeriesInstanceUID);
                studyUIDs.Add(item.StudyInstanceUID);
            }

            AEInformation destination = get_server();
            if (destination != null)
            {

                BackgroundTask task = new BackgroundTask(
                    delegate(IBackgroundTaskContext context)
                    {
                        DicomSendServiceClient sender = new DicomSendServiceClient();
                        sender.Open();
                        SendSeriesRequest series_request = new SendSeriesRequest();
                        series_request.DestinationAEInformation = destination;
                        series_request.StudyInstanceUid = studyUIDs[0];
                        series_request.SeriesInstanceUids = seriesUIDs;
                        sender.SendSeries(series_request);
                        sender.Close();
                        //       OnMoveCompleted();

                    }, true);

                task.Run();

                LocalDataStoreActivityMonitorComponentManager.ShowSendReceiveActivityComponent(this.Context.DesktopWindow);
            }



            
        }


        private AEInformation get_server()
        {
            ServerTreeComponent serverTreeComponent = new ServerTreeComponent();
            serverTreeComponent.IsReadOnly = false;
            serverTreeComponent.ShowCheckBoxes = false;
            serverTreeComponent.ShowLocalDataStoreNode = true;
            serverTreeComponent.ShowTitlebar = true;
            serverTreeComponent.ShowTools = true;

            SimpleComponentContainer dialogContainer = new SimpleComponentContainer(serverTreeComponent);

            ApplicationComponentExitCode code =
            ApplicationComponent.LaunchAsDialog(
                this.Context.DesktopWindow,
                dialogContainer,
                "Choose Server");

            if (code == ApplicationComponentExitCode.Accepted)
            {
                if (serverTreeComponent.SelectedServers.IsLocalDatastore == true)
                {
                    this.Context.DesktopWindow.ShowMessageBox("Cannot send to 'My Studies'", MessageBoxActions.Ok);
                    return null;
                }
                if (serverTreeComponent.SelectedServers == null || serverTreeComponent.SelectedServers.Servers == null || serverTreeComponent.SelectedServers.Servers.Count == 0)
                {
                    this.Context.DesktopWindow.ShowMessageBox("Invalid selection", MessageBoxActions.Ok);
                    return null;
                }

                if (serverTreeComponent.SelectedServers.Servers.Count > 1)
                {
                    this.Context.DesktopWindow.ShowMessageBox("Cannot select multiple servers", MessageBoxActions.Ok);
                    return null;
                }

                Server selected_server = (Server)serverTreeComponent.SelectedServers.Servers[0];

                AEInformation destination = new AEInformation();
                destination.AETitle = selected_server.AETitle;
                destination.HostName = selected_server.Host;
                destination.Port = selected_server.Port;

                return destination;
            }
            else
            {
                return null;
            }
        }

    }



}

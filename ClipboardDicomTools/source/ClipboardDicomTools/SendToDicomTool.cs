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
using System.ServiceModel;
using System.Collections.Generic;

using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.Clipboard;
using ClearCanvas.ImageViewer.Services;
using ClearCanvas.ImageViewer.Services.ServerTree;
using ClearCanvas.ImageViewer.Configuration.ServerTree;
using ClearCanvas.ImageViewer.Services.DicomServer;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.ImageViewer.Services.Tools;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Codec;
using ClearCanvas.Dicom.Network.Scu;

namespace ClipboardDicomTools
{
    [MenuAction("send", "clipboard-contextmenu/MenuSendToDicom", "Send")]
    [ButtonAction("send", "clipboard-toolbar/ToolbarSendToDicom", "Send")]
    [Tooltip("send", "Send to Dicom Device")]
    [IconSet("send", IconScheme.Colour, "Icons.SendStudyToolSmall.png", "Icons.SendStudyToolSmall.png", "Icons.SendStudyToolSmall.png")]
    [EnabledStateObserver("send", "Enabled", "EnabledChanged")]
    
    [ExtensionOf(typeof(ClipboardToolExtensionPoint))]
    public class SendToDicomTool : ClipboardTool
    {
        public SendToDicomTool()
        {
        }

        public override void Initialize()
        {
            this.Enabled = this.Context.SelectedClipboardItems.Count > 0;
            base.Initialize();
        }

        public void Send()
        {
            foreach (IClipboardItem item in this.Context.SelectedClipboardItems)
            {
                string message = String.Format("{0}", item.ToString());
                Platform.Log(LogLevel.Info, message);

                List<IPresentationImage> image_queue = new List<IPresentationImage>();

                // If Display set, then can use send series, else need to make list of images
                // to send individually
                if (item.Item is IDisplaySet)
                {
                    Enqueue(image_queue, (IDisplaySet)item.Item);

                    ImageSop imageSop = ((IImageSopProvider)image_queue[0]).ImageSop;
                    string studyUid = imageSop.StudyInstanceUid;
                    string seriesUid = imageSop.SeriesInstanceUid;

                    // Check if display set contains all images in series by comparing number of 
                    // images in display set with NumberOfFrames in DICOM header
                    int num_images_in_series = imageSop.ParentSeries.Sops.Count;

                    string message2 = String.Format("{0}", num_images_in_series + " ?= " + image_queue.Count);
                    Platform.Log(LogLevel.Info, message2);

                    // Send entire series
                    if (num_images_in_series.Equals(image_queue.Count))
                    {
                        send_series(studyUid, seriesUid);
                    }
                    else
                    {
                        send_images(image_queue);
                    }

                }
                else
                {
                    if (item.Item is IPresentationImage)
                        Enqueue(image_queue, (IPresentationImage)item.Item);

                    else if (item.Item is IImageSet)
                        Enqueue(image_queue, (IImageSet)item.Item);

                    send_images(image_queue);
                }
            }
        }

        void send_series(string studyUid, string seriesUid)
        {
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
                        series_request.StudyInstanceUid = studyUid;
                        List<string> seriesUids = new List<string>();
                        seriesUids.Add(seriesUid);
                        series_request.SeriesInstanceUids = seriesUids;
                        sender.SendSeries(series_request);
                        sender.Close();
                    }, true);

                task.Run();

                LocalDataStoreActivityMonitorComponentManager.ShowSendReceiveActivityComponent(this.Context.DesktopWindow);

            }
        }

        void send_images(List<IPresentationImage> queue)
        {
            AEInformation destination = get_server();
            SendSopInstancesRequest image_request = new SendSopInstancesRequest();
            DicomSendServiceClient sender = new DicomSendServiceClient();
            if (destination != null)
            {
                image_request.DestinationAEInformation = destination;
                List<string> studyUids = new List<string>();
                List<string> seriesUids = new List<string>();
                List<string> imageSopUids = new List<string>();
                foreach (IPresentationImage image in queue)
                {
                    if (image is IImageSopProvider)
                    {
                        ImageSop imageSop = ((IImageSopProvider)image).ImageSop;
                        studyUids.Add(imageSop.StudyInstanceUid);
                        seriesUids.Add(imageSop.SeriesInstanceUid);
                        imageSopUids.Add(imageSop.SopInstanceUid);
                    }
                }
                image_request.StudyInstanceUid = studyUids[0];
                image_request.SeriesInstanceUid = seriesUids[0];
                image_request.SopInstanceUids = imageSopUids;
                sender.SendSopInstances(image_request);
                sender.Close();
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

            if (serverTreeComponent.SelectedServers == null || serverTreeComponent.SelectedServers.Servers == null || serverTreeComponent.SelectedServers.Servers.Count == 0)
            {
                this.Context.DesktopWindow.ShowMessageBox("Invalid Selection", MessageBoxActions.Ok);
                return null;
            }

            if (serverTreeComponent.SelectedServers.Servers.Count > 1)
            {
                this.Context.DesktopWindow.ShowMessageBox("Cannot select multiple servers?", MessageBoxActions.Ok);
                return null;
            }

            Server selected_server = (Server)serverTreeComponent.SelectedServers.Servers[0];

            AEInformation destination = new AEInformation();
            destination.AETitle = selected_server.AETitle;
            destination.HostName = selected_server.Host;
            destination.Port = selected_server.Port;

            return destination;
        }

        // From Jasper on CCW Forum
        private static int Enqueue(ICollection<IPresentationImage> queue, IPresentationImage pImage)
        {
            queue.Add(pImage);
            return 1;
        }

        private static int Enqueue(ICollection<IPresentationImage> queue, IDisplaySet dSet)
        {
            int count = 0;
            foreach (IPresentationImage pImage in dSet.PresentationImages)
            {
                count += Enqueue(queue, pImage);
            }
            return count;
        }

        private static int Enqueue(ICollection<IPresentationImage> queue, IImageSet iSet)
        {
            int count = 0;
            foreach (IDisplaySet dSet in iSet.DisplaySets)
            {
                count += Enqueue(queue, dSet);
            }
            return count;
        }
    }
}
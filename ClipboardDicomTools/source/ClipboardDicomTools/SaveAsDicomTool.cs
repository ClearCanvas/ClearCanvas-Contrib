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
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.ImageViewer;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageViewer.Clipboard;
using ClearCanvas.ImageViewer.Services;
using ClearCanvas.ImageViewer.Services.DicomServer;
using ClearCanvas.ImageViewer.StudyManagement;
using System.ServiceModel;
using System.Collections.Generic;
using ClearCanvas.Dicom;
using System.Windows.Forms;
using ClearCanvas.ImageViewer.BaseTools;
using System.IO;

namespace ClipboardDicomTools
{
    [MenuAction("Save", "clipboard-contextmenu/SaveAsDicom", "Save")]
    [ButtonAction("Save", "clipboard-toolbar/SaveAsDicom", "Save")]
    [Tooltip("Save", "Save as Dicom")]
    [IconSet("Save", IconScheme.Colour, "Icons.SaveAsDicomToolSmall.png", "Icons.SaveAsDicomToolMedium.png", "Icons.SaveAsDicomToolLarge.png")]
    [EnabledStateObserver("Save", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(ClipboardToolExtensionPoint))]
    public class SaveAsDicomTool : ClipboardTool
    {
        public SaveAsDicomTool()
        {
        }

        public override void Initialize()
        {
            this.Enabled = this.Context.SelectedClipboardItems.Count > 0;
            base.Initialize();
        }

        public void Save()
        {
            string path;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Choose destination folder for saving DICOM images";
                if (DialogResult.OK == dialog.ShowDialog())
                    path = dialog.SelectedPath;
                else
                    return;
            }


            foreach (IClipboardItem item in this.Context.SelectedClipboardItems)
            {
                List<IPresentationImage> queue = new List<IPresentationImage>();
                if (item.Item is IPresentationImage)
                    Enqueue(queue, (IPresentationImage)item.Item);
                else if (item.Item is IDisplaySet)
                    Enqueue(queue, (IDisplaySet)item.Item);
                else if (item.Item is IImageSet)
                    Enqueue(queue, (IImageSet)item.Item);


                foreach (IPresentationImage image in queue)
                {
                    if (image is IImageSopProvider)
                    {
                        ImageSop imageSop = ((IImageSopProvider)image).ImageSop;

                        ILocalSopDataSource localsource = (ILocalSopDataSource)imageSop.DataSource;
                        File.Copy(localsource.Filename, path + "\\" + imageSop[DicomTags.SopInstanceUid] + ".dcm");
                     }
                }
            }
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
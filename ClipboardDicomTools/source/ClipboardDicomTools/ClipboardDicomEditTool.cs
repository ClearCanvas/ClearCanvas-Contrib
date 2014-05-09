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

using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.ImageViewer.Clipboard;
using ClearCanvas.Utilities.DicomEditor;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.ImageViewer;
using ClearCanvas.Dicom;
using ClearCanvas.ImageViewer.Services.LocalDataStore;

namespace ClipboardDicomEdit
{

    [MenuAction("edit", "clipboard-contextmenu/MenuDicomEdit", "Edit")]
    [ButtonAction("edit", "clipboard-toolbar/ToolbarDicomEdit", "Edit")]
    [Tooltip("edit", "Edit Dicom Files")]
    [IconSet("edit", IconScheme.Colour, "Icons.ClipboardDicomEditToolSmall.png", "Icons.ClipboardDicomEditToolSmall.png", "Icons.ClipboardDicomEditToolSmall.png")]
    [EnabledStateObserver("edit", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(ClipboardToolExtensionPoint))]
    public class ClipboardDicomEditTool : ClipboardTool
    {
        private static DicomEditorComponent _component = null;
        private static IShelf _shelf;
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public ClipboardDicomEditTool()
        {
            base.Enabled = true;
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Called by the framework when the user clicks the "apply" menu item or toolbar button.
        /// </summary>
        public void Edit()
        {
            _component = new DicomEditorComponent();
            List<string> filePaths = new List<string>();
       
            // Loop through all items in clipboard (code from CC forum)
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
                        // Load each image path into dicom editor, and record path in array for later, re-import
                        _component.Load(localsource.SourceMessage);
                        filePaths.Add(localsource.Filename);
                    }
                }
            }
            //common to both contexts
            if (_shelf != null)
            {
                _shelf.Activate();
            }
            else
            {
                _shelf = ApplicationComponent.LaunchAsShelf(
                    this.Context.DesktopWindow,
                    _component,
                    "Dicom Editor",
                    "Dicom Editor",
                    ShelfDisplayHint.DockRight | ShelfDisplayHint.DockAutoHide);
                _shelf.Closed += OnShelfClosed;
            }

            _component.UpdateComponent();

            //trigger an import of the Renamed files.
            LocalDataStoreServiceClient client = new LocalDataStoreServiceClient();
            client.Open();
            try
            {
                FileImportRequest request = new FileImportRequest();
                request.BadFileBehaviour = BadFileBehaviour.Move;
                request.FileImportBehaviour = FileImportBehaviour.Move;
                request.FilePaths = filePaths;
                request.Recursive = false;
                client.Import(request);
                client.Close();
            }
            catch
            {
                client.Abort();
                throw;
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
        private void OnShelfClosed(object sender, ClosedEventArgs e)
        {
            // We need to cache the owner DesktopWindow (_desktopWindow) because this tool is an 
            // ImageViewer tool, disposed when the viewer component is disposed.  Shelves, however,
            // exist at the DesktopWindow level and there can only be one of each type of shelf
            // open at the same time per DesktopWindow (otherwise things look funny).  Because of 
            // this, we need to allow this event handling method to be called after this tool has
            // already been disposed (e.g. viewer workspace closed), which is why we store the 
            // _desktopWindow variable.

            _shelf.Closed -= OnShelfClosed;
            _shelf = null;
        }
    }
}

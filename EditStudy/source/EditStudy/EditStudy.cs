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
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.ImageViewer.Explorer.Dicom;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.Utilities.DicomEditor;
using ClearCanvas.Dicom.DataStore;
using ClearCanvas.Dicom.Utilities;
using ClearCanvas.ImageViewer;
using ClearCanvas.Desktop.Validation;
using ClearCanvas.Dicom.Utilities.Anonymization;


namespace EditStudy
{
    [MenuAction("apply", "dicomstudybrowser-contextmenu/EdityStudy", "Apply")]
    [ButtonAction("apply", "dicomstudybrowser-toolbar/EditStudy", "Apply")]
    [Tooltip("apply", "Edit Study")]
    [IconSet("apply", IconScheme.Colour, "Icons.EditStudyToolSmall.png", "Icons.EditStudyToolMedium.png", "Icons.EditStudyToolLarge.png")]
    [EnabledStateObserver("apply", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(StudyBrowserToolExtensionPoint))]
    public class EditStudyTool : StudyBrowserTool
    {
        private bool _enabled;
        private event EventHandler _enabledChanged;
        private static DicomEditorComponent _component = null;
        private static IShelf _shelf;
        private static object _localStudyLoader = null;
        List<string> _filePaths = new List<string>();


        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public EditStudyTool()
        {
            _enabled = true;
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: add any significant initialization code here rather than in the constructor
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
            _component = new DicomEditorComponent();
            
            // Loop through all selected studies
            foreach (StudyItem selectedstudy in this.Context.SelectedStudies)
            {
                string studyUID = selectedstudy.StudyInstanceUid;
                int numberOfSops = LocalStudyLoader.Start(new StudyLoaderArgs(studyUID, null));
                
                // Loop through all images in study
                for (int i = 0; i < numberOfSops; ++i)
                {
                    Sop imageSop = LocalStudyLoader.LoadNextSop();
                    ILocalSopDataSource localsource = (ILocalSopDataSource)imageSop.DataSource;
                    // Load images into dicom editor
                    _component.Load(localsource.SourceMessage);
                    // Keep track of file paths for later re-importation
                    _filePaths.Add(localsource.Filename);
                }
                // This code deletes the study from the database, so that when it is re-imported the changed fields 
                // will appear
                using (IDataStoreStudyRemover studyRemover = DataAccessLayer.GetIDataStoreStudyRemover())
                {
                    studyRemover.RemoveStudy(selectedstudy.StudyInstanceUid);
                }
            }
            // Launch Dicom Editor Shelf
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
        }
                 
        protected override void OnSelectedStudyChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        protected override void OnSelectedServerChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }
   
        private void UpdateEnabled()
        {
            this.Enabled = (this.Context.SelectedStudy != null &&
                            this.Context.SelectedServerGroup.IsLocalDatastore &&
                            LocalDataStoreActivityMonitor.IsConnected);
            //this.Enabled = (this.Context.SelectedStudy != null);// &&
            //                            !this.Context.SelectedServerGroup.IsLocalDatastore);
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
    
        private void OnShelfClosed(object sender, ClosedEventArgs e)
        {
            // We need to cache the owner DesktopWindow (_desktopWindow) because this tool is an 
            // ImageViewer tool, disposed when the viewer component is disposed.  Shelves, however,
            // exist at the DesktopWindow level and there can only be one of each type of shelf
            // open at the same time per DesktopWindow (otherwise things look funny).  Because of 
            // this, we need to allow this event handling method to be called after this tool has
            // already been disposed (e.g. viewer workspace closed), which is why we store the 
            // _desktopWindow variable.

            //trigger an import of the Renamed files.
            LocalDataStoreServiceClient client = new LocalDataStoreServiceClient();
            client.Open();
            try
            {
                FileImportRequest request = new FileImportRequest();
                request.BadFileBehaviour = BadFileBehaviour.Move;
                request.FileImportBehaviour = FileImportBehaviour.Move;
                request.FilePaths = _filePaths;
                request.Recursive = false;
                client.Import(request);
                client.Close();
            }
            catch
            {
                client.Abort();
                throw;
            }

            _shelf.Closed -= OnShelfClosed;
            _shelf = null;
        }
    
    }
    
}

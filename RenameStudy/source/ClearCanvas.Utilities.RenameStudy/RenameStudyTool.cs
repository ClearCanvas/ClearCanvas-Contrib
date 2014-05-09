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
using System.IO;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.DataStore;
using ClearCanvas.Dicom.Utilities.Anonymization;
using ClearCanvas.ImageViewer.Explorer.Dicom;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.ImageViewer.StudyManagement;
using Path = System.IO.Path;

using ClearCanvas.Dicom.Iod;
using ClearCanvas.ImageViewer.Services.Tools;
using ClearCanvas.ImageViewer.Services;

namespace ClearCanvas.Utilities.RenameStudy
{
    [ButtonAction("activate", "dicomstudybrowser-toolbar/RenameStudy", "RenameStudy")]
    [MenuAction("activate", "dicomstudybrowser-contextmenu/Rename", "RenameStudy")]
    [EnabledStateObserver("activate", "Enabled", "EnabledChanged")]
    [Tooltip("activate", "Rename Selected Study")]
    [IconSet("activate", IconScheme.Colour, "Icons.RenameToolSmall.png", "Icons.RenameToolSmall.png", "Icons.RenameToolSmall.png")]

    [ExtensionOf(typeof(StudyBrowserToolExtensionPoint))]
    public class RenameStudyTool : StudyBrowserTool
    {
        private volatile RenameStudyComponent _component;
        private string _tempPath;
        private static object _localStudyLoader = null;

        public RenameStudyTool()
        {
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
                        Platform.Log(LogLevel.Info, "Rename tool disabled; no local study loader exists.");
                    }

                    if (_localStudyLoader == null)
                        _localStudyLoader = new object(); //there is no loader.
                }

                return _localStudyLoader as IStudyLoader;
            }
        }

        public void RenameStudy()
        {
            _component = new RenameStudyComponent(this.Context.SelectedStudy);
            if (ApplicationComponentExitCode.Accepted ==
                ApplicationComponent.LaunchAsDialog(this.Context.DesktopWindow, _component, "Rename Study"))
            {
                BackgroundTask task = null;
                try
                {
                    task = new BackgroundTask(Rename, false,  this.Context.SelectedStudy.StudyInstanceUid);
                    ProgressDialog.Show(task, this.Context.DesktopWindow, true);
                }
                catch (Exception e)
                {
                    Platform.Log(LogLevel.Error, e);
                    string message = String.Format("Must Delete Manually", _tempPath);
                    this.Context.DesktopWindow.ShowMessageBox(message, MessageBoxActions.Ok);
                }
                finally
                {
                    _tempPath = null;

                    if (task != null)
                        task.Dispose();
                }
            }
        }

        private void Rename(IBackgroundTaskContext context)
        {
            try
            {
                _tempPath = String.Format(".\\temp\\{0}", Path.GetRandomFileName());
                Directory.CreateDirectory(_tempPath);
                _tempPath = Path.GetFullPath(_tempPath);

                context.ReportProgress(new BackgroundTaskProgress(0, "Renaming Study"));

                int numberOfSops = LocalStudyLoader.Start(new StudyLoaderArgs(this.Context.SelectedStudy.StudyInstanceUid, null));
                if (numberOfSops <= 0)
                    return;

                for (int i = 0; i < numberOfSops; ++i)
                {
                    string message = String.Format("{0} of {1}", i.ToString(), numberOfSops.ToString());
                    Platform.Log(LogLevel.Info, message);

                    Sop sop = LocalStudyLoader.LoadNextSop();
                    ILocalSopDataSource localsource = (ILocalSopDataSource)sop.DataSource;
                    string progressMessage = localsource.Filename.ToString();
                    DicomFile file = ((ILocalSopDataSource)sop.DataSource).File;

                    //renamer.Anonymize(file);

                    StudyData originalData = new StudyData();
                    file.DataSet.LoadDicomFields(originalData);

                    originalData.PatientId = _component.PatientId;
                    originalData.PatientsBirthDate = _component.PatientsBirthDate;
                    originalData.PatientsNameRaw = _component.PatientsName;
                    originalData.AccessionNumber = _component.AccessionNumber;
                    originalData.StudyDate = _component.StudyDate;
                    originalData.StudyDescription = _component.StudyDescription;
                    
                    file.DataSet.SaveDicomFields(originalData);                 

                    file.Save(String.Format("{0}\\{1}.dcm", _tempPath, i));

                    int progressPercent = (int)Math.Floor((i + 1) / (float)numberOfSops * 100);
                    //string progressMessage = String.Format(SR.MessageAnonymizingStudy, _tempPath);

                    context.ReportProgress(new BackgroundTaskProgress(progressPercent, progressMessage));

                    // This code deletes the study from the database, so that when it is re-imported the changed fields 
                    // will appear

                }

                
                using (IDataStoreStudyRemover studyRemover = DataAccessLayer.GetIDataStoreStudyRemover())
                {
                    studyRemover.RemoveStudy(this.Context.SelectedStudy.StudyInstanceUid);
                }

                //trigger an import of the Renamed files.
                LocalDataStoreServiceClient client = new LocalDataStoreServiceClient();
                client.Open();
                try
                {
                    FileImportRequest request = new FileImportRequest();
                    request.BadFileBehaviour = BadFileBehaviour.Move;
                    request.FileImportBehaviour = FileImportBehaviour.Move;
                    List<string> filePaths = new List<string>();
                    filePaths.Add(_tempPath);
                    request.FilePaths = filePaths;
                    request.Recursive = true;
                    client.Import(request);
                    client.Close();

                    //  Need to refresh study list in order for changed values to appear
                    //  This method doesn't work.  Need to manually click 'Search' again
                    //this.Context.RefreshStudyList();
                }
                catch
                {
                    client.Abort();
                    throw;
                }
                //this.Context.RefreshStudyList();
                context.Complete();
            }
            catch (Exception e)
            {
                context.Error(e);
            }
            
        }

        private void UpdateEnabled()
        {
            if (this.Context.SelectedStudy == null)
            {
                this.Enabled = false;
                return;
            }

            this.Enabled = LocalStudyLoader != null &&
                LocalDataStoreActivityMonitor.IsConnected &&
                this.Context.SelectedStudies.Count == 1 &&
                this.Context.SelectedServerGroup.IsLocalDatastore;
        }

        protected override void OnSelectedStudyChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        protected override void OnSelectedServerChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }
    }
}


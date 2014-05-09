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
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.ImageViewer.Explorer.Dicom;
using ClearCanvas.ImageViewer.Services;
using ClearCanvas.Dicom;
using ClearCanvas.ImageViewer.Services.LocalDataStore;
using ClearCanvas.Dicom.Utilities;

namespace WriteStudyList
{
    [MenuAction("WriteList", "dicomstudybrowser-contextmenu/Export Study List", "WriteList")]
    [ButtonAction("WriteList", "dicomstudybrowser-toolbar/Export Study List", "WriteList")]
    [Tooltip("WriteList", "Export list of studies to file")]
    [IconSet("WriteList", IconScheme.Colour, "Icons.WriteStudyToolSmall.png", "Icons.WriteStudyToolMedium.png", "Icons.WriteStudyToolLarge.png")]
    [EnabledStateObserver("WriteList", "Enabled", "EnabledChanged")]

    [ExtensionOf(typeof(StudyBrowserToolExtensionPoint))]
    public class WriteStudyTool : StudyBrowserTool
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// A no-args constructor is required by the framework.  Do not remove.
        /// </remarks>
        public WriteStudyTool()
        {
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
        public void WriteList()
        {
            string outfile = "studylist.txt";

            Dictionary<ApplicationEntity, List<StudyInformation>> retrieveInformation = new Dictionary<ApplicationEntity, List<StudyInformation>>();
            
            try
            {
                StreamWriter writer = new StreamWriter(ClearCanvas.Common.Platform.LogDirectory + "\\" + outfile);
                writer.WriteLine("PatientID \t PatientsName \t StudyDate \t Last Name \t First Name \t StudyUID");
                foreach (StudyItem item in this.Context.SelectedStudies)
                {
                    writer.WriteLine(item.PatientId + "\t" + item.PatientsName.FormattedName + "\t" + item.StudyDate + "\t" + item.PatientsName.LastName + "\t" + item.PatientsName.FirstName + "\t" + item.StudyInstanceUid);

                    string message = String.Format("{0}", item.PatientId + "\t" + item.PatientsName + "\t" + item.StudyDate + "\t" + item.PatientsName.FormattedName + "\t" + item.PatientsName.FirstName + "\t" + item.PatientsName.LastName + "\t" + item.StudyInstanceUid);
                    Platform.Log(LogLevel.Info, message);
                }
                writer.Close();

                try
                {
                    Process myProc;
                    // Start the process.
                    string message2 = String.Format("{0}", "excel.exe \"" + ClearCanvas.Common.Platform.LogDirectory + "\\" + outfile + "\"");
                    Platform.Log(LogLevel.Info, message2);
                    myProc = Process.Start("excel.exe", "\"" + ClearCanvas.Common.Platform.LogDirectory + "\\" + outfile + "\"");
                }
                catch
                {
                    this.Context.DesktopWindow.ShowMessageBox("Study list file created:  " + ClearCanvas.Common.Platform.LogDirectory + outfile, MessageBoxActions.Ok);
                }
            }
            catch
            {
                this.Context.DesktopWindow.ShowMessageBox("Verify that " + outfile + " is not open in another application.", MessageBoxActions.Ok);
            }
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
            if (this.Context.SelectedStudy == null)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
            }
        }
    }              
}

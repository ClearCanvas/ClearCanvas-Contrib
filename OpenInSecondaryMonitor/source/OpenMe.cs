#region License
// Copyright (c) 2008, Wael Tahoun.
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
using System.Windows.Forms;
using System.Drawing;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.View.WinForms;
using ClearCanvas.ImageViewer.Explorer.Dicom;
using ClearCanvas.ImageViewer.StudyManagement;

namespace ClearCanvas.WT
{
    [ButtonAction("activate", "dicomstudybrowser-toolbar/Open in secondary monitor", "Activate")]
    [MenuAction("activate", "dicomstudybrowser-contextmenu/Open in secondary monitor", "Activate")]
    [EnabledStateObserver("activate", "Enabled", "EnabledChanged")]
    [Tooltip("activate", "Open in secondary monitor")]
    [IconSet("activate", IconScheme.Colour, "OpenSmall.png", "OpenMedium.png", "OpenLarge.png")]

    [ExtensionOf(typeof(StudyBrowserToolExtensionPoint))]
    public class OpenInSecondaryMonitor : StudyBrowserTool
    {
        /// <summary>
        /// Activate the tool action.
        /// </summary>
        /// <remarks>
        /// This function detects secondary monitor if availible then open the study in separate window and show it in the secondary monitor
        /// </remarks>
        public void Activate()
        {
            //Get Current Selected Study
            StudyItem item = this.Context.SelectedStudy;
            
            if (item != null)
            {
                string[] studyInstanceUids = { "" + item.StudyInstanceUID + "" };
                
                //Create an OpenStudyArgs to be used later to open the study
                //Note that I used WindowBehaviour.Separate to open the study in a separate Form
                OpenStudyArgs args = new OpenStudyArgs(studyInstanceUids, item.Server, item.StudyLoaderName, WindowBehaviour.Separate);
                
                //Open the selected study 
                OpenStudyHelper.OpenStudies(args);
                
                //Move it to secondary screen if only there are more than one screen
                if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                {
                    //Get All System Screens
                    System.Windows.Forms.Screen[] Screens = System.Windows.Forms.Screen.AllScreens;

                    //Create a variable to hold the secondary screen variable and assign it to null
                    System.Windows.Forms.Screen SecScreenVar;
                    SecScreenVar = null;

                    //assign the SecScreenVar to the secondary screen
                    foreach (System.Windows.Forms.Screen SecScreen in Screens)
                    {
                        //if its the secondary assign it
                        if (SecScreen.Primary == false)
                        {
                            SecScreenVar = SecScreen;
                        }
                    }

                    //If a secondary screen found
                    if (SecScreenVar != null)
                    {
                        //Get the newly opened study window
                        Form currentForm = Form.ActiveForm;
                        
                        //Set its start position to manual
                        currentForm.StartPosition = FormStartPosition.Manual;

                        //Set its location to the secondary screen
                        currentForm.Location = SecScreenVar.WorkingArea.Location;

                        //Set its size to the secondary screen size
                        currentForm.Size = new Size(SecScreenVar.WorkingArea.Width, SecScreenVar.WorkingArea.Height);

                        //Reshow it in its new location
                        currentForm.Hide();
                        currentForm.Show();
                    }
                }
            }
        }

        /// <summary>
        /// Enable Disable the Tool.
        /// </summary>
        /// <remarks>
        /// This function set the Enabled/Disabled attribute of the tool.
        /// </remarks>
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

        /// <summary>
        /// On Selected Study Changed event handler
        /// </summary>
        /// <remarks>
        /// On Selected Study Changed event handler
        /// </remarks>
        protected override void OnSelectedStudyChanged(object sender, EventArgs e)
        {
            //Update the tool Enable/Disable status when selected study changed
            UpdateEnabled();
        }

        /// <summary>
        /// On Selected Server Changed event handler
        /// </summary>
        /// <remarks>
        /// On Selected Server Changed event handler
        /// </remarks>
        protected override void OnSelectedServerChanged(object sender, EventArgs e)
        {
            //Update the tool Enable/Disable status when selected server changed
            UpdateEnabled();
        }

    }
}

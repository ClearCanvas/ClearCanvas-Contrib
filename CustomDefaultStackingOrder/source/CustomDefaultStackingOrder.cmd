/*
::    CustomDefaultStackingOrder
::    Copyright (C) 2015 Martin Kirsche <martin.kirsche+cc@gmail.com>, econmed GmbH
::
::    This program is free software: you can redistribute it and/or modify
::    it under the terms of the GNU General Public License as published by
::    the Free Software Foundation, either version 3 of the License, or
::    (at your option) any later version.
::
::    This program is distributed in the hope that it will be useful,
::    but WITHOUT ANY WARRANTY; without even the implied warranty of
::    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
::    GNU General Public License for more details.
::
::    You should have received a copy of the GNU General Public License
::    along with this program.  If not, see <http://www.gnu.org/licenses/>.

@echo off
cls
setlocal
set installationPath=c:\Program Files\ClearCanvas\ClearCanvas DICOM Viewer
set /p installationPath=Enter the ClearCanvas Workstation or DicomViewer installation directory [%installationPath%]: 
echo.
pushd %installationPath%
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\csc.exe /nologo ^
/target:library ^
/reference:.\common\ClearCanvas.Common.dll ^
/reference:.\plugins\ClearCanvas.Dicom.dll ^
/reference:.\plugins\ClearCanvas.Desktop.dll ^
/reference:.\plugins\ClearCanvas.ImageViewer.dll ^
/out:.\plugins\CustomDefaultStackingOrder.dll ^
%0
popd
echo.
if errorlevel 1 (
  echo Installation of CustomDefaultStackingOrder plugin failed.
) else (
  echo Installation of CustomDefaultStackingOrder plugin successfully done.
)
pause>NUL
endlocal
goto:eof
*/

using System;
using System.Reflection;
using System.Collections.Generic;
using ClearCanvas.Common;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.BaseTools;
using ClearCanvas.ImageViewer.Comparers;

[assembly: ClearCanvas.Common.Plugin()]
[assembly: AssemblyTitle("CustomDefaultStackingOrder")]
[assembly: AssemblyDescription("Plugin that can change the default stacking order.")]
[assembly: AssemblyCompany("econmed GmbH")]
[assembly: AssemblyCopyright("Copyright (c) 2015")]
[ExtensionOf(typeof(ImageViewerToolExtensionPoint))]
public class CustomDefaultStackingOrder : ImageViewerTool
{
    static readonly IComparer<IPresentationImage> comparer =
    /*
     * Choose between InstanceAndFrameNumberComparer, AcquisitionTimeComparer 
     * or SliceLocationComparer and remove the ReverseComparer<IPresentationImage>
     * as needed.
     */
        new ReverseComparer<IPresentationImage>(new SliceLocationComparer());

    public override void Initialize()
    {
        base.Initialize();
        ApplyComparer();
        ImageViewer.EventBroker.LayoutCompleted += OnLayoutCompleted;
    }

    protected override void Dispose(bool disposing)
    {
        ImageViewer.EventBroker.LayoutCompleted -= OnLayoutCompleted;
        base.Dispose(disposing);
    }

    private void OnLayoutCompleted(object sender, System.EventArgs e)
    {
        ApplyComparer();
    }

    private void ApplyComparer()
    {
        if (null == ImageViewer) { return; }
        if (null != ImageViewer.LogicalWorkspace && null != ImageViewer.LogicalWorkspace.ImageSets)
        {
            foreach (var imageSets in ImageViewer.LogicalWorkspace.ImageSets)
            {
                if (null == imageSets.DisplaySets) { continue; }
                foreach (var displaySet in imageSets.DisplaySets)
                {
                    displaySet.PresentationImages.Sort(comparer);
                }
            }
        }
        if (null != ImageViewer.PhysicalWorkspace && null != ImageViewer.PhysicalWorkspace.ImageBoxes)
        {
            foreach (var imageBox in ImageViewer.PhysicalWorkspace.ImageBoxes)
            {
                if (null == imageBox.DisplaySet || null == imageBox.DisplaySet.PresentationImages) { continue; }
                imageBox.DisplaySet.PresentationImages.Sort(comparer);
            }
        }
    }
}
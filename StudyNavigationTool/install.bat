@echo off
cls
setlocal

set installationPath=%ProgramFiles%\ClearCanvas\ClearCanvas DICOM Viewer
set /p installationPath=Enter the ClearCanvas Workstation or DicomViewer installation directory [%installationPath%]: 
echo.
pushd %installationPath%
"%ProgramFiles%\Microsoft SDKs\Windows\v6.0A\Bin\ResGen.exe" "%~dp0\source\SR.resx"
"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /nologo ^
/target:library ^
/reference:.\common\ClearCanvas.Common.dll ^
/reference:.\plugins\ClearCanvas.Dicom.dll ^
/reference:.\plugins\ClearCanvas.Desktop.dll ^
/reference:.\plugins\ClearCanvas.ImageViewer.dll ^
/reference:.\plugins\ClearCanvas.ImageViewer.Layout.Basic.dll ^
/out:.\plugins\Econmed.ImageViewer.Layout.Basic.StudyNavigationTool.dll ^
/resource:%~dp0\source\SR.resources,Econmed.ImageViewer.Layout.Basic.SR.resources ^
/resource:%~dp0\source\Icons\NextLarge.png,Econmed.ImageViewer.Layout.Basic.Icons.NextLarge.png ^
/resource:%~dp0\source\Icons\NextMedium.png,Econmed.ImageViewer.Layout.Basic.Icons.NextMedium.png ^
/resource:%~dp0\source\Icons\NextSmall.png,Econmed.ImageViewer.Layout.Basic.Icons.NextSmall.png ^
/resource:%~dp0\source\Icons\PreviousLarge.png,Econmed.ImageViewer.Layout.Basic.Icons.PreviousLarge.png ^
/resource:%~dp0\source\Icons\PreviousMedium.png,Econmed.ImageViewer.Layout.Basic.Icons.PreviousMedium.png ^
/resource:%~dp0\source\Icons\PreviousSmall.png,Econmed.ImageViewer.Layout.Basic.Icons.PreviousSmall.png ^
%~dp0\source\StudyNavigationTool.cs ^
%~dp0\source\LayoutSettingsHelper.cs ^
%~dp0\source\Properties\AssemblyInfo.cs ^
%~dp0\source\SR.Designer.cs
popd
echo.
if errorlevel 1 (
  echo Installation of StudyNavigationTool plugin failed.
) else (
  echo Installation of StudyNavigationTool plugin successfully done.
)
pause>NUL
endlocal

@echo off
SETLOCAL
rem Change the following line if you did not put the make.bat and
rem Econmed.ImageServer.UpdateOriginalAttributesSequence.cs inside
rem the plugins dircetory of the Image Server 
SET ImageServerPath=.\..\
%SystemRoot%\Microsoft.NET\Framework\v3.5\csc.exe /t:library ^
/r:%ImageServerPath%\Common\ClearCanvas.Common.dll ^
/r:%ImageServerPath%\Common\ClearCanvas.Dicom.dll ^
/r:%ImageServerPath%\Plugins\ClearCanvas.ImageServer.Core.dll ^
/r:%ImageServerPath%\Plugins\ClearCanvas.ImageServer.Common.dll ^
Econmed.ImageServer.UpdateOriginalAttributesSequence.cs
ENDLOCAL
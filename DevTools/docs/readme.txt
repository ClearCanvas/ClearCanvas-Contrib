Developer Tools for ClearCanvas Desktop Executable
Version 1.2.1
by Jasper Yeh

==License==
Copyright (c) 2010, Jasper Yeh
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice,
      this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice,
      this list of conditions and the following disclaimer in the documentation
      and/or other materials provided with the distribution.
    * Neither the name of Jasper Yeh nor the names of its contributors may be
      used to endorse or promote products derived from this software without
      specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

==Installation==
Simply copy the binaries in the /bin folder to the /plugins directory of your
installation of ClearCanvas Workstation 2.0 SP1.

==Important Note==
If you're running ClearCanvas Workstation from the sources, you will probably
find that the application crashes from this plugin. No worries - just add the
included source files to your solution and include it in your build process.
The included binaries have strong name references to the official 2.0 SP1
binaries, so they do work if they are installed on an official 2.0 SP1 release.

==Tools Description==
This plugin adds a Debug menu with the following options to the desktop.
* Service Log -> Open
  Opens an external log viewer and loads the service log file.

* Service Log -> Tail
  Opens an external log viewer and loads the end of the service log file.

* Service Log -> Monitor
  Opens an internal log viewer and loads the end of the service log file.

* Workstation Log -> Open
  Opens an external log viewer and loads the workstation log file.

* Workstation Log -> Tail
  Opens an external log viewer and loads the end of the workstation log file.

* Workstation Log -> Monitor
  Opens an internal log viewer and loads the end of the workstation log file.

* Logs -> Insert Log Marker
  Inserts some text of your choosing into the log file immediately. This is
  great if you need to mark the current position of the log so you can
  identify actions versus log events.

* Logs -> Advance Log
  Advances the log by a number of blank lines. Another way to discern
  individual log events.

* Action Models -> Delete
  Deletes the action model and prompts for a restart. This is very useful if
  you've made changes to the action model and they aren't propagating.

* Folders -> Application Settings
  Launches an explorer window pointed at the settings directory for the
  currently running instance. This is very useful if you need to see and/or
  make manual changes to the user settings file that the instance is actually
  using.

* Folders -> Executable
  Launches an explorer window pointed at the executable directory for the
  currently running instance.

* Folders -> Common
  Launches an explorer window pointed at the common directory for the currently
  running instance.

* Folders -> Plugins
  Launches an explorer window pointed at the plugins directory for the currently
  running instance.
  
* Folders -> Service
  Launches an explorer window pointed at the executable directory for the
  currently running Workstation’s DICOM Service.

* Command History Inspector
  Opens a dockable shelf that allows for browsing the current desktop’s Undo/Redo
  command history.  Composite commands can even be opened to reveal the constituent
  subcommands, their mementos, and their originators.

* Synchronize Device XMLs
  When building from source using the Visual Studio IDE, there are two copies of
  the AE device list – one for the Workstation proper, and another for the service
  executable. The Workstation is unaware of the other copy, and does not update it
  for the service when a developer updates the device list using the Workstation GUI.
  This tool automatically synchronizes the service’s device list with the Workstation’s
  local copy.

==Known Issues==
There are a few known issues, usually related to known issues in the framework.
1. In an official ClearCanvas Workstation, the "Workstation Log" is usually
   unavailable.  All its contents are actually found in the Service Log, and so only
   the Service Log menu items will function for both logs.

==Change Log==
===Version 1.2.1===
* Now compiled against Workstation 2.0 SP1.

===Version 1.2===
* Added a command history inspector tool.
* Added the service executable folder shortcut.
* Added a synchronize AE device list XML tool.

===Version 1.1===
* Rearranged the debug menu into submenus to avoid clutter.
* Added log marking and insert event tools.
* Fixed service and workstation log detection.

===Version 1.0===
* Initial release.

Econmed.ImageViewer.Layout.ExplodeImageBoxesTool
================================================

The ExplodeImageBox**es**Tool is a modified version of the existing
ExplodeImageBoxTool that will enable you to explode multiple Image
Boxs accross all of your monitors.

Installation
------------

1. navigate into the ClearCanvas DICOM Viewer or Workstation path
2. to disable the existing ExplodeImageBoxTool add the following lines into the ClearCanvas.Desktop.Executable.exe.critical.config
  - at configuration/configSections/sectionGroup[name="applicationSettings"] add the following line if it doesn't already exist:

            <section name="ClearCanvas.Common.ExtensionSettings" type="System.Configuration.ClientSettingsSection, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false"/>

  - at configuration/applicationSettings add or modify:

            <ClearCanvas.Common.ExtensionSettings>
              <setting name="ExtensionConfigurationXml" serializeAs="Xml">
                <value>
                  <extensions>
                    <extension class="ClearCanvas.ImageViewer.Layout.Basic.ExplodeImageBoxTool, ClearCanvas.ImageViewer.Layout.Basic" enabled="false" />
                    <extension class="Econmed.ImageViewer.Layout.ExplodeImageBoxesTool, Econmed.ImageViewer.Layout.ExplodeImageBoxesTool" enabled="true" />
                  </extensions>
                </value>
              </setting>
            </ClearCanvas.Common.ExtensionSettings>

3. Run the install.bat to compile and install the plugin.

Usage
-----

1. span the workspace over multiple monitors
2. select one image box after another (a history of those selections will be kept internally)
3. press 'x' to expose as much image boxes as possible
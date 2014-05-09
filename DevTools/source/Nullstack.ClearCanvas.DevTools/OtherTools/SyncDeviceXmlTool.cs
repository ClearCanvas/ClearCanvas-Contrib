using System.IO;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using Nullstack.ClearCanvas.DevTools.Common;
using Path=System.IO.Path;

namespace Nullstack.ClearCanvas.DevTools
{
	[MenuAction("syncnow", "global-menus/mnDebug/mnSynchronizeDeviceXmls", "Synchronize")]
	[GroupHint("syncnow", "Nullstack.ClearCanvas.DevTools.Service")]
	//
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class SyncDeviceXmlTool : Tool<IDesktopToolContext>
	{
		private const string XML_FILENAME = "DicomAEServers.xml";

		public static readonly string DesktopXmlFile = Path.Combine(CCProcess.Desktop.WorkingDirectory, XML_FILENAME);
		public static readonly string ServiceXmlFile = Path.Combine(CCProcess.Service.WorkingDirectory, XML_FILENAME);

		public void Synchronize()
		{
			FileInfo desktopFile = new FileInfo(DesktopXmlFile);
			FileInfo serviceFile = new FileInfo(ServiceXmlFile);

			if (desktopFile.FullName.ToLowerInvariant().Equals(serviceFile.FullName.ToLowerInvariant()))
				return;

			if (desktopFile.Exists)
			{
				File.Copy(DesktopXmlFile, ServiceXmlFile, true);
				Platform.Log(LogLevel.Info, "Synchronizing service's device XML with the desktop's.");
			}
			else if (serviceFile.Exists)
			{
				File.Copy(ServiceXmlFile, DesktopXmlFile, true);
				Platform.Log(LogLevel.Info, "Synchronizing desktop's device XML with the service's.");
			}
		}
	}
}
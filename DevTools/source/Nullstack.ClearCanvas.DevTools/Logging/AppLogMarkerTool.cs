using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using Nullstack.ClearCanvas.DevTools.Common;

namespace Nullstack.ClearCanvas.DevTools.Logging
{
	/// <summary>
	/// Tools for dropping markers in the workstation log.
	/// </summary>
	[GroupHint("markLog", "Nullstack.ClearCanvas.DevTools.Logs.Workstation.Mark")]
	[MenuAction("markLog", "global-menus/mnDebug/mnLogs/mnMarkLogPosition", "MarkLog")]
	//
	[GroupHint("advanceLog", "Nullstack.ClearCanvas.DevTools.Logs.Workstation.Advance")]
	[MenuAction("advanceLog", "global-menus/mnDebug/mnLogs/mnAdvanceLog", "AdvanceLog")]
	//
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class AppLogMarkerTool : Tool<IDesktopToolContext>
	{
		public void MarkLog()
		{
			InsertLogMarkerComponent component = new InsertLogMarkerComponent();
			if (base.Context.DesktopWindow.ShowDialogBox(new SimpleComponentContainer(component), "szInsertLogMarker") == DialogBoxAction.Ok)
			{
				for (int n = 0; n < component.Multiplier; n++)
					Platform.Log(component.LogLevel, component.Message);
			}
		}

		public void AdvanceLog()
		{
			for (int n = 0; n < ConfigurationHelper.LogAdvanceLines; n++)
				Platform.Log(LogLevel.Info, "");
		}
	}
}
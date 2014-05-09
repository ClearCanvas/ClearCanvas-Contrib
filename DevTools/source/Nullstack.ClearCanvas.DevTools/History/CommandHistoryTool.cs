using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;

namespace Nullstack.ClearCanvas.DevTools.History
{
	[GroupHint("launch", "Nullstack.ClearCanvas.DevTools.Workspaces.CommandHistory")]
	[MenuAction("launch", "global-menus/mnDebug/mnCommandHistoryInspector", "Show")]
	//
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class CommandHistoryTool : Tool<IDesktopToolContext>
	{
		public void Show()
		{
			CommandHistoryComponent component = new CommandHistoryComponent();
			component.TargetWorkspace = base.Context.DesktopWindow.ActiveWorkspace;
			ApplicationComponent.LaunchAsShelf(base.Context.DesktopWindow, component, SR.szCommandHistoryInspector, ShelfDisplayHint.DockLeft);
		}
	}
}
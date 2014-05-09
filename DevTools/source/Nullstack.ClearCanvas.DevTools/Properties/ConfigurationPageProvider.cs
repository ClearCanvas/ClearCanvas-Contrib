using System.Collections.Generic;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Configuration;
using ClearCanvas.Desktop.Tools;

namespace Nullstack.ClearCanvas.DevTools.Properties
{
	/// <summary>
	/// This class provides configuration pages for the options dialog.
	/// </summary>
	[ExtensionOf(typeof (ConfigurationPageProviderExtensionPoint))]
	public class ConfigurationPageProvider : IConfigurationPageProvider
	{
		public static string DefaultPageId
		{
			get { return DevToolsConfigComponent.CONFIG_PAGE_ID; }
		}

		/// <summary>
		/// Gets an enumerable object of configuration pages.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IConfigurationPage> GetPages()
		{
			List<IConfigurationPage> list = new List<IConfigurationPage>(1);
			list.Add(new ConfigurationPage<DevToolsConfigComponent>(DevToolsConfigComponent.CONFIG_PAGE_ID));
			return list.AsReadOnly();
		}
	}

	[MenuAction("launch", "global-menus/mnDebug/mnConfigure", "Show")]
	[GroupHint("launch", "Nullstack.ClearCanvas.DevTools")]
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class ConfigTool : Tool<IDesktopToolContext>
	{
		public void Show()
		{
			ConfigurationDialog.Show(base.Context.DesktopWindow, ConfigurationPageProvider.DefaultPageId);
		}
	}
}
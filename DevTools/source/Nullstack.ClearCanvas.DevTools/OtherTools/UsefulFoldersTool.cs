using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using Nullstack.ClearCanvas.DevTools.Common;
using Path=System.IO.Path;

namespace Nullstack.ClearCanvas.DevTools
{
	/// <summary>
	/// Tools to open ClearCanvas folders that are of interest to developers, such as the exe folder, the common folder, the plugins folder, and the folder that the user application settings are stored in.
	/// </summary>
	[GroupHint("foldExecutable", "Nullstack.ClearCanvas.DevTools.Folders.Executable")]
	[MenuAction("foldExecutable", "global-menus/mnDebug/mnFolders/mnExecutable", "LaunchExeDir")]
	//
	[GroupHint("foldCommon", "Nullstack.ClearCanvas.DevTools.Folders.Common")]
	[MenuAction("foldCommon", "global-menus/mnDebug/mnFolders/mnCommon", "LaunchCommonDir")]
	//
	[GroupHint("foldPlugins", "Nullstack.ClearCanvas.DevTools.Folders.Plugins")]
	[MenuAction("foldPlugins", "global-menus/mnDebug/mnFolders/mnPlugins", "LaunchPluginsDir")]
	//
	[GroupHint("foldSettings", "Nullstack.ClearCanvas.DevTools.Folders.Settings")]
	[MenuAction("foldSettings", "global-menus/mnDebug/mnFolders/mnApplicationSettings", "LaunchSettingsDir")]
	//
	[GroupHint("foldService", "Nullstack.ClearCanvas.DevTools.Folders.Service")]
	[MenuAction("foldService", "global-menus/mnDebug/mnFolders/mnService", "LaunchServiceDir")]
	//
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class UsefulFoldersTool : Tool<IDesktopToolContext>
	{
		/// <summary>
		/// Launches the exe's directory
		/// </summary>
		public void LaunchExeDir()
		{
			LaunchCCFolder("");
		}

		/// <summary>
		/// Launches the common directory
		/// </summary>
		public void LaunchCommonDir()
		{
			LaunchCCFolder(".\\common");
		}

		/// <summary>
		/// Launches the plugins directory
		/// </summary>
		public void LaunchPluginsDir()
		{
			LaunchCCFolder(".\\plugins");
		}

		/// <summary>
		/// Launches the service directory
		/// </summary>
		public void LaunchServiceDir()
		{
			string wkdir = CCProcess.Service.WorkingDirectory;
			if (string.IsNullOrEmpty(wkdir))
				return;

			DirectoryInfo di = new DirectoryInfo(wkdir);
			if (di.Exists)
			{
				Process.Start(di.FullName);
			}
		}

		/// <summary>
		/// Launches the directory that all user application settings are stored in
		/// </summary>
		public void LaunchSettingsDir()
		{
			Configuration c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
			string verspec = Path.GetDirectoryName(c.FilePath);
			DirectoryInfo di = new DirectoryInfo(verspec);
			if (di.Exists)
			{
				Process.Start(di.FullName);
			}
		}

		/// <summary>
		/// A helper method to open a folder path relative to the working directory of the executable
		/// </summary>
		/// <param name="relpath">The relative path.</param>
		private static void LaunchCCFolder(string relpath)
		{
			FileInfo fi = new FileInfo(Environment.CommandLine.Replace("\"", ""));
			string path = Path.Combine(fi.DirectoryName, relpath);
			DirectoryInfo di = new DirectoryInfo(path);
			if (di.Exists)
			{
				Process.Start(di.FullName);
			}
		}
	}
}
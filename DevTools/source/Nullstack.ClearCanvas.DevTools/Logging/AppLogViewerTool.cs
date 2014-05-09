using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using Nullstack.ClearCanvas.DevTools.Common;
using Path=System.IO.Path;

namespace Nullstack.ClearCanvas.DevTools.Logging
{
	/// <summary>
	/// Tools for opening logs related to the viewer and the service.
	/// </summary>
	[GroupHint("openWksLog", "Nullstack.ClearCanvas.DevTools.Logs.Workstation.Open")]
	[MenuAction("openWksLog", "global-menus/mnDebug/mnWorkstationLog/mnOpen", "OpenWorkstationLog")]
	[VisibleStateObserver("openWksLog", "IsWorkstationLogAvailable", "IsWorkstationLogAvailableChanged")]
	//
	[GroupHint("tailWksLog", "Nullstack.ClearCanvas.DevTools.Logs.Workstation.Tail")]
	[MenuAction("tailWksLog", "global-menus/mnDebug/mnWorkstationLog/mnTail", "TailWorkstationLog")]
	[VisibleStateObserver("tailWksLog", "IsWorkstationLogAvailable", "IsWorkstationLogAvailableChanged")]
	//
	[GroupHint("deskWksLog", "Nullstack.ClearCanvas.DevTools.Logs.Workstation.Desk")]
	[MenuAction("deskWksLog", "global-menus/mnDebug/mnWorkstationLog/mnMonitor", "DeskWorkstationLog")]
	[VisibleStateObserver("deskWksLog", "IsWorkstationLogAvailable", "IsWorkstationLogAvailableChanged")]
	//
	[GroupHint("openSrvLog", "Nullstack.ClearCanvas.DevTools.Logs.Service.Open")]
	[MenuAction("openSrvLog", "global-menus/mnDebug/mnServiceLog/mnOpen", "OpenServiceLog")]
	[VisibleStateObserver("openSrvLog", "IsServiceLogAvailable", "IsServiceLogAvailableChanged")]
	//
	[GroupHint("tailSrvLog", "Nullstack.ClearCanvas.DevTools.Logs.Service.Tail")]
	[MenuAction("tailSrvLog", "global-menus/mnDebug/mnServiceLog/mnTail", "TailServiceLog")]
	[VisibleStateObserver("tailSrvLog", "IsServiceLogAvailable", "IsServiceLogAvailableChanged")]
	//
	[GroupHint("deskSrvLog", "Nullstack.ClearCanvas.DevTools.Logs.Service.Desk")]
	[MenuAction("deskSrvLog", "global-menus/mnDebug/mnServiceLog/mnMonitor", "DeskServiceLog")]
	[VisibleStateObserver("deskSrvLog", "IsServiceLogAvailable", "IsServiceLogAvailableChanged")]
	//
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class AppLogViewerTool : Tool<IDesktopToolContext>
	{
		private const string WORKSTATION_LOG_FILENAME = "logs\\ClearCanvas.Workstation.log";
		private const string SERVICE_LOG_FILENAME = "logs\\ClearCanvas.Server.ShredHostService.log";

		private static readonly Regex CMDLINE = new Regex("((?:\"[^\"]+\")|\\S+)(?: (.*))?", RegexOptions.Compiled | RegexOptions.Singleline);
		public static readonly string WorkstationLogFile = Path.Combine(Environment.CurrentDirectory, WORKSTATION_LOG_FILENAME);
		public static readonly string ServiceLogFile = Path.Combine(CCProcess.Service.WorkingDirectory, SERVICE_LOG_FILENAME);

		/// <summary>
		/// Fired when the service log file's existence changes.
		/// </summary>
		public event EventHandler IsServiceLogAvailableChanged;

		/// <summary>
		/// Fired when the workstation log file's existence changes.
		/// </summary>
		public event EventHandler IsWorkstationLogAvailableChanged;

		private readonly SynchronizationContext _syncContext;

		private FileSystemWatcher _watchService;
		private FileSystemWatcher _watchWorkstation;

		private Shelf _shelfWorkstation;
		private Shelf _shelfService;

		public AppLogViewerTool()
		{
			_syncContext = SynchronizationContext.Current;
		}

		/// <summary>
		/// Gets a value indicating if the service log file exists.
		/// </summary>
		public bool IsServiceLogAvailable
		{
			get { return File.Exists(ServiceLogFile); }
		}

		/// <summary>
		/// Gets a value indicating if the workstation log file exists.
		/// </summary>
		public bool IsWorkstationLogAvailable
		{
			get { return File.Exists(WorkstationLogFile); }
		}

		public override void Initialize()
		{
			base.Initialize();

			/// initialization is just subscribing to file system events on these two files
			string path;

			path = ServiceLogFile;
			_watchService = new FileSystemWatcher(Path.GetDirectoryName(path), Path.GetFileName(path));
			_watchService.EnableRaisingEvents = true;
			_watchService.Created += OnIsServiceLogAvailableChanged;
			_watchService.Deleted += OnIsServiceLogAvailableChanged;
			_watchService.Renamed += OnIsServiceLogAvailableChanged;

			path = WorkstationLogFile;
			_watchWorkstation = new FileSystemWatcher(Path.GetDirectoryName(path), Path.GetFileName(path));
			_watchWorkstation.EnableRaisingEvents = true;
			_watchWorkstation.Created += OnIsWorkstationLogAvailableChanged;
			_watchWorkstation.Deleted += OnIsWorkstationLogAvailableChanged;
			_watchWorkstation.Renamed += OnIsWorkstationLogAvailableChanged;
		}

		protected override void Dispose(bool disposing)
		{
			// and disposal is just unsubscribing to the same events
			_watchService.Created -= OnIsServiceLogAvailableChanged;
			_watchService.Deleted -= OnIsServiceLogAvailableChanged;
			_watchService.Renamed -= OnIsServiceLogAvailableChanged;
			_watchService.Dispose();
			_watchService = null;

			_watchWorkstation.Created -= OnIsWorkstationLogAvailableChanged;
			_watchWorkstation.Deleted -= OnIsWorkstationLogAvailableChanged;
			_watchWorkstation.Renamed -= OnIsWorkstationLogAvailableChanged;
			_watchWorkstation.Dispose();
			_watchWorkstation = null;

			base.Dispose(disposing);
		}

		private void OnIsServiceLogAvailableChanged(object sender, FileSystemEventArgs e)
		{
			if (_syncContext == SynchronizationContext.Current)
				EventsHelper.Fire(this.IsServiceLogAvailableChanged, this, new EventArgs());
			else
				_syncContext.Send(delegate { EventsHelper.Fire(this.IsServiceLogAvailableChanged, this, new EventArgs()); }, null);
		}

		private void OnIsWorkstationLogAvailableChanged(object sender, FileSystemEventArgs e)
		{
			if (_syncContext == SynchronizationContext.Current)
				EventsHelper.Fire(this.IsWorkstationLogAvailableChanged, this, new EventArgs());
			else
				_syncContext.Send(delegate { EventsHelper.Fire(this.IsWorkstationLogAvailableChanged, this, new EventArgs()); }, null);
		}

		/// <summary>
		/// Launches the workstation log in an external viewer
		/// </summary>
		public void OpenWorkstationLog()
		{
			LaunchFile(WorkstationLogFile);
		}

		/// <summary>
		/// Launches the last few kb of the workstation log in an external viewer
		/// </summary>
		public void TailWorkstationLog()
		{
			LaunchRecentFile(WorkstationLogFile);
		}

		/// <summary>
		/// Launches the workstation log in the viewer component
		/// </summary>
		public void DeskWorkstationLog()
		{
			// note: we use this field to ensure we don't open multiple copies of the same log viewer
			if (_shelfWorkstation == null)
			{
				DesktopLogComponent component = new DesktopLogComponent(WorkstationLogFile);
				_shelfWorkstation = base.Context.DesktopWindow.Shelves.AddNew(component, SR.szWorkstationLog, "DesktopWorkstationLog", ShelfDisplayHint.DockFloat);
				_shelfWorkstation.Closed += OnWorkstationShelfClosed;
			}
		}

		/// <summary>
		/// callback for when the viewer closes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWorkstationShelfClosed(object sender, ClosedEventArgs e)
		{
			_shelfWorkstation = null;
		}

		/// <summary>
		/// Launches the service log in the viewer component
		/// </summary>
		public void DeskServiceLog()
		{
			// note: we use this field to ensure we don't open multiple copies of the same log viewer
			if (_shelfService == null)
			{
				DesktopLogComponent component = new DesktopLogComponent(ServiceLogFile);
				_shelfService = base.Context.DesktopWindow.Shelves.AddNew(component, SR.szServiceLog, "DesktopServiceLog", ShelfDisplayHint.DockFloat);
				_shelfService.Closed += OnServiceShelfClosed;
			}
		}

		/// <summary>
		/// callback for when the viewer closes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnServiceShelfClosed(object sender, ClosedEventArgs e)
		{
			_shelfService = null;
		}

		/// <summary>
		/// Opens the service log in an external viewer.
		/// </summary>
		public void OpenServiceLog()
		{
			LaunchFile(ServiceLogFile);
		}

		/// <summary>
		/// Opens the last few KB of the service log in an external viewer.
		/// </summary>
		public void TailServiceLog()
		{
			LaunchRecentFile(ServiceLogFile);
		}

		/// <summary>
		/// Helper method to copy the last 4 KB of the specified file into a temp file and launch it using the external viewer
		/// </summary>
		/// <param name="basefile">The filename</param>
		private static void LaunchRecentFile(string basefile)
		{
			const int tailSize = 4096;
			const int bufferSize = 100;

			if (!File.Exists(basefile))
				return;

			try
			{
				string tempfile = Path.GetTempFileName();
				FileStream outfile = File.Open(tempfile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
				FileStream fs = File.Open(basefile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				byte[] buffer = new byte[bufferSize];
				if (fs.Length > tailSize)
				{
					fs.Seek(-tailSize, SeekOrigin.End);
				}

				int count;
				do
				{
					count = fs.Read(buffer, 0, bufferSize);
					if (count > 0)
					{
						outfile.Write(buffer, 0, count);
					}
				} while (count > 0);
				outfile.Close();
				fs.Close();

				string xfile = tempfile + ".txt";
				File.Move(tempfile, xfile);
				int pid = LaunchFile(xfile);
				ProcessWaiter pw = new ProcessWaiter(pid, xfile);
				Thread t = new Thread(new ThreadStart(pw.Execute));
				t.IsBackground = true;
				t.Start();
			}
			catch (IOException) {}
		}

		/// <summary>
		/// Launches the file using the configured external viewer
		/// </summary>
		/// <param name="filename">The filename</param>
		/// <returns>Returns the process id of the external viewer</returns>
		private static int LaunchFile(string filename)
		{
			string command = filename;
			string arguments = "";

			if (!File.Exists(filename))
				return 0;

			if (!ConfigurationHelper.LogViewAppPathIsEmpty)
			{
				FileInfo fi = new FileInfo(filename);
				Match m = CMDLINE.Match(ConfigurationHelper.LogViewAppPath);
				if (m.Success)
				{
					command = m.Groups[1].Value;
					command = command.Replace("%n", fi.Name);
					command = command.Replace("%p", fi.FullName);
					command = command.Replace("%d", fi.DirectoryName);
					command = command.Replace("%x", fi.Extension);
					command = command.Replace("%%", "%");

					arguments = m.Groups[2].Value;
					arguments = arguments.Replace("%n", fi.Name);
					arguments = arguments.Replace("%p", fi.FullName);
					arguments = arguments.Replace("%d", fi.DirectoryName);
					arguments = arguments.Replace("%x", fi.Extension);
					arguments = arguments.Replace("%%", "%");
				}
				else
				{
					command = ConfigurationHelper.LogViewAppPath;
				}
			}

			ProcessStartInfo nfo = new ProcessStartInfo(command, arguments);
			nfo.WorkingDirectory = Environment.CurrentDirectory;

			Process p = Process.Start(nfo);
			return p.Id;
		}

		/// <summary>
		/// A class to delete a file after a certain process ends
		/// </summary>
		private class ProcessWaiter
		{
			private readonly int _pid;
			private readonly string _filename;

			public ProcessWaiter(int pid, string filename)
			{
				_pid = pid;
				_filename = filename;
			}

			public void Execute()
			{
				Process p = Process.GetProcessById(_pid);
				p.WaitForExit();
				File.Delete(_filename);
			}
		}
	}
}
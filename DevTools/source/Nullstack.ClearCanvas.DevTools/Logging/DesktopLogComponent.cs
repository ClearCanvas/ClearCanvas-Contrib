using System;
using System.IO;
using System.Text;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using Path=System.IO.Path;

namespace Nullstack.ClearCanvas.DevTools.Logging
{
	/// <summary>
	/// An extension point which views of the <see cref="DesktopLogComponent"/> should use.
	/// </summary>
	[ExtensionPoint]
	public class DesktopLogComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView> {}

	/// <summary>
	/// A component for viewing logs in the application.
	/// </summary>
	[AssociateView(typeof (DesktopLogComponentViewExtensionPoint))]
	public class DesktopLogComponent : ApplicationComponent
	{
		public event EventHandler LogUpdated;

		private FileSystemWatcher _watchFile;
		private readonly string _filename;
		private string _log = "";
		private int _windowSize = 4096;
		private int _readBufferSize = 100;

		/// <summary>
		/// Constructs a log viewer component for the given log file
		/// </summary>
		/// <param name="filename">The filename of the log file</param>
		public DesktopLogComponent(string filename)
		{
			_filename = filename;
		}

		/// <summary>
		/// Gets the contents of the log
		/// </summary>
		public string Log
		{
			get { return _log; }
			private set
			{
				_log = value;
				EventsHelper.Fire(LogUpdated, this, new EventArgs());
			}
		}

		/// <summary>
		/// Gets or sets the window size (in bytes) of the log.
		/// </summary>
		public int WindowSize
		{
			get { return _windowSize; }
			set
			{
				if (_windowSize != value)
				{
					_windowSize = value;
					base.NotifyPropertyChanged("WindowSize");
				}
			}
		}

		/// <summary>
		/// Gets or sets the read buffer size (in bytes) for performance tweaking.
		/// </summary>
		public int ReadBufferSize
		{
			get { return _readBufferSize; }
			set
			{
				if (_readBufferSize != value)
				{
					_readBufferSize = value;
					base.NotifyPropertyChanged("ReadBufferSize");
				}
			}
		}

		public override void Start()
		{
			base.Start();

			OnLogFileContentsChanged(this, null);

			// subscribe to file change events
			_watchFile = new FileSystemWatcher(Path.GetDirectoryName(_filename), Path.GetFileName(_filename));
			_watchFile.EnableRaisingEvents = true;
			_watchFile.NotifyFilter = _watchFile.NotifyFilter | NotifyFilters.LastWrite | NotifyFilters.Size;
			_watchFile.Created += OnLogFileContentsChanged;
			_watchFile.Deleted += OnLogFileContentsChanged;
			_watchFile.Renamed += OnLogFileContentsChanged;
			_watchFile.Changed += OnLogFileContentsChanged;
		}

		public override void Stop()
		{
			// unsubscribe to file change events
			_watchFile.EnableRaisingEvents = false;
			_watchFile.Created -= OnLogFileContentsChanged;
			_watchFile.Deleted -= OnLogFileContentsChanged;
			_watchFile.Renamed -= OnLogFileContentsChanged;
			_watchFile.Changed -= OnLogFileContentsChanged;
			_watchFile.Dispose();
			_watchFile = null;

			base.Stop();
		}

		/// <summary>
		/// Event handler for file content changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLogFileContentsChanged(object sender, FileSystemEventArgs e)
		{
			int bufferSize = _readBufferSize;
			int windowSize = _windowSize;

			if (File.Exists(_filename))
			{
				// This routine reads in a windowsize full of data from the end of the file
				StringBuilder sb = new StringBuilder();
				FileStream fs = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				byte[] buffer = new byte[bufferSize];
				if (fs.Length > windowSize)
				{
					fs.Seek(-windowSize, SeekOrigin.End);
				}

				int count;
				do
				{
					count = fs.Read(buffer, 0, bufferSize);
					if (count > 0)
					{
						sb.Append(Encoding.ASCII.GetString(buffer, 0, count));
					}
				} while (count > 0);
				fs.Close();

				this.Log = sb.ToString();
			}
			else
			{
				this.Log = string.Empty;
			}
		}
	}
}
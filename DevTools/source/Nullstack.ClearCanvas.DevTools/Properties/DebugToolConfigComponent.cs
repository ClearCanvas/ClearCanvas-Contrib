using System;
using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Configuration;

namespace Nullstack.ClearCanvas.DevTools.Properties
{
	/// <summary>
	/// The extension point that views of the <see cref="DevToolsConfigComponent"/> must use.
	/// </summary>
	[ExtensionPoint]
	public class DevToolsConfigComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView> {}

	/// <summary>
	/// A component for configuring the dev tools
	/// </summary>
	[AssociateView(typeof (DevToolsConfigComponentViewExtensionPoint))]
	public class DevToolsConfigComponent : ConfigurationApplicationComponent
	{
		/// <summary>
		/// The id of the page.
		/// </summary>
		public static readonly string CONFIG_PAGE_ID = "szDevTools";

		private Settings _settings;
		private string _logViewerAppPath;
		private int _logAdvanceLines;

		/// <summary>
		/// The external application used to view logs.
		/// </summary>
		public string LogViewerAppPath
		{
			get { return _logViewerAppPath ?? ""; }
			set
			{
				if (_logViewerAppPath != value)
				{
					_logViewerAppPath = value ?? "";
					base.NotifyPropertyChanged("LogViewerAppPath");
					base.Modified = true;
				}
			}
		}

		public int LogAdvanceLines
		{
			get { return _logAdvanceLines; }
			set
			{
				if (_logAdvanceLines != value)
				{
					_logAdvanceLines = 5;
					base.NotifyPropertyChanged("LogAdvanceLines");
					base.Modified = true;
				}
			}
		}

		[Obsolete]
		public int LogTailSize
		{
			get { return 4096; }
			set { }
		}

		/// <summary>
		/// Load the settings from the settings file on startup
		/// </summary>
		public override void Start()
		{
			base.Start();

			_settings = Settings.Default;

			_logViewerAppPath = _settings.LogViewerAppPath;
			_logAdvanceLines = _settings.LogAdvanceLines;
		}

		/// <summary>
		/// Saves the settings to the settings file
		/// </summary>
		public override void Save()
		{
			_settings.LogViewerAppPath = _logViewerAppPath;
			_settings.LogAdvanceLines = _logAdvanceLines;
			_settings.Save();
		}

		/// <summary>
		/// Unload the settings file on termination
		/// </summary>
		public override void Stop()
		{
			_settings = null;

			base.Stop();
		}
	}
}
using System;
using System.Reflection;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace Nullstack.ClearCanvas.DevTools.Logging
{
	/// <summary>
	/// Tools for changing the application log level.
	/// </summary>
	[MenuAction("debug", "global-menus/mnDebug/mnLogLevel/mnDebug", "SetDebug")]
	[CheckedStateObserver("debug", "IsDebug", "LogLevelChanged")]
	[GroupHint("debug", "Nullstack.ClearCanvas.DevTools.Logs.Level.0.Debug")]
	//
	[MenuAction("info", "global-menus/mnDebug/mnLogLevel/mnInfo", "SetInfo")]
	[CheckedStateObserver("info", "IsInfo", "LogLevelChanged")]
	[GroupHint("info", "Nullstack.ClearCanvas.DevTools.Logs.Level.1.Info")]
	//
	[MenuAction("warn", "global-menus/mnDebug/mnLogLevel/mnWarn", "SetWarn")]
	[CheckedStateObserver("warn", "IsWarn", "LogLevelChanged")]
	[GroupHint("warn", "Nullstack.ClearCanvas.DevTools.Logs.Level.2.Warn")]
	//
	[MenuAction("error", "global-menus/mnDebug/mnLogLevel/mnError", "SetError")]
	[CheckedStateObserver("error", "IsError", "LogLevelChanged")]
	[GroupHint("error", "Nullstack.ClearCanvas.DevTools.Logs.Level.3.Error")]
	//
	[MenuAction("fatal", "global-menus/mnDebug/mnLogLevel/mnFatal", "SetFatal")]
	[CheckedStateObserver("fatal", "IsFatal", "LogLevelChanged")]
	[GroupHint("fatal", "Nullstack.ClearCanvas.DevTools.Logs.Level.4.Fatal")]
	//
	[ExtensionOf(typeof (DesktopToolExtensionPoint))]
	public class ChangeLogLevelTool : Tool<IDesktopToolContext>
	{
		private static readonly FieldInfo _rfPlatformLog = typeof (Platform).GetField("_log", BindingFlags.NonPublic | BindingFlags.Static);

		private event EventHandler _logLevelChanged;
		private LogLevel _logLevel;

		public LogLevel LogLevel
		{
			get { return _logLevel; }
			set
			{
				if (_logLevel != value)
				{
					_logLevel = value;
					this.OnLogLevelChanged();
				}
			}
		}

		public event EventHandler LogLevelChanged
		{
			add { _logLevelChanged += value; }
			remove { _logLevelChanged -= value; }
		}

		protected virtual void OnLogLevelChanged()
		{
			Logger.Level = Convert(this.LogLevel);
			EventsHelper.Fire(_logLevelChanged, this, new EventArgs());
		}

		private static Logger Logger
		{
			get { return GetLogger(_rfPlatformLog.GetValue(null)); }
		}

		private static Logger GetLogger(object logger)
		{
			if (logger is Logger)
				return (Logger) logger;
			else if (logger is ILog)
				return GetLogger(((ILog) logger).Logger);
			return null;
		}

		private static LogLevel Convert(Level level)
		{
			if (level == Level.Debug)
				return LogLevel.Debug;
			else if (level == Level.Info)
				return LogLevel.Info;
			else if (level == Level.Warn)
				return LogLevel.Warn;
			else if (level == Level.Error)
				return LogLevel.Error;
			else if (level == Level.Fatal)
				return LogLevel.Fatal;
			return LogLevel.Debug;
		}

		private static Level Convert(LogLevel level)
		{
			switch (level)
			{
				case LogLevel.Debug:
					return Level.Debug;
				case LogLevel.Info:
					return Level.Info;
				case LogLevel.Warn:
					return Level.Warn;
				case LogLevel.Error:
					return Level.Error;
				case LogLevel.Fatal:
					return Level.Fatal;
				default:
					return Level.Debug;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
			_logLevel = Convert(Logger.EffectiveLevel);
		}

		#region Internal Methods for Actions

		internal void SetDebug()
		{
			this.LogLevel = LogLevel.Debug;
		}

		internal void SetInfo()
		{
			this.LogLevel = LogLevel.Info;
		}

		internal void SetWarn()
		{
			this.LogLevel = LogLevel.Warn;
		}

		internal void SetError()
		{
			this.LogLevel = LogLevel.Error;
		}

		internal void SetFatal()
		{
			this.LogLevel = LogLevel.Fatal;
		}

		public bool IsDebug
		{
			get { return this.LogLevel == LogLevel.Debug; }
		}

		public bool IsInfo
		{
			get { return this.LogLevel == LogLevel.Info; }
		}

		public bool IsWarn
		{
			get { return this.LogLevel == LogLevel.Warn; }
		}

		public bool IsError
		{
			get { return this.LogLevel == LogLevel.Error; }
		}

		public bool IsFatal
		{
			get { return this.LogLevel == LogLevel.Fatal; }
		}

		#endregion
	}
}
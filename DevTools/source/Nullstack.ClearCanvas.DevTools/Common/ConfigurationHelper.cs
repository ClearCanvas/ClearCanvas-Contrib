using Nullstack.ClearCanvas.DevTools.Properties;

namespace Nullstack.ClearCanvas.DevTools.Common
{
	/// <summary>
	/// A helper class to get values from the configuration
	/// </summary>
	public static class ConfigurationHelper
	{
		public static string LogViewAppPath
		{
			get { return Settings.Default.LogViewerAppPath; }
		}

		public static bool LogViewAppPathIsEmpty
		{
			get
			{
				string s = Settings.Default.LogViewerAppPath;
				return s == null || s.Length == 0;
			}
		}

		public static int LogAdvanceLines
		{
			get
			{
				return Settings.Default.LogAdvanceLines;
			}
		}
	}
}
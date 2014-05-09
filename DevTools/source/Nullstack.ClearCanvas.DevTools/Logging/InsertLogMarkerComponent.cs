using ClearCanvas.Common;
using ClearCanvas.Desktop;

namespace Nullstack.ClearCanvas.DevTools.Logging
{
	[ExtensionPoint]
	public sealed class InsertLogMarkerComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView> {}

	[AssociateView(typeof (InsertLogMarkerComponentViewExtensionPoint))]
	public class InsertLogMarkerComponent : ApplicationComponent
	{
		private LogLevel _loglevel = LogLevel.Info;
		private string _message = "";
		private int _multiplier = 1;

		public LogLevel LogLevel
		{
			get { return _loglevel; }
			set { _loglevel = value; }
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public int Multiplier
		{
			get { return _multiplier; }
			set
			{
				Platform.CheckPositive(value, "value");
				_multiplier = value;
			}
		}

		public void Insert()
		{
			base.Exit(ApplicationComponentExitCode.Accepted);
		}

		public void Cancel()
		{
			base.Exit(ApplicationComponentExitCode.None);
		}
	}
}
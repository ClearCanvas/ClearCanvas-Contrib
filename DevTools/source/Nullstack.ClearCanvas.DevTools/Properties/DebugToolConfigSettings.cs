using System.Configuration;
using Nullstack.ClearCanvas.DevTools.Common;

namespace Nullstack.ClearCanvas.DevTools.Properties
{
	[SettingsProvider(typeof (DllSettingsProvider))]
	internal sealed partial class DebugToolConfigSettings {}
}
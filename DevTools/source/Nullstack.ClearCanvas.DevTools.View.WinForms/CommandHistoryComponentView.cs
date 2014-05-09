using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.View.WinForms;
using Nullstack.ClearCanvas.DevTools.History;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
	[ExtensionOf(typeof (CommandHistoryComponentViewExtensionPoint))]
	internal class CommandHistoryComponentView : WinFormsView, IApplicationComponentView
	{
		private CommandHistoryComponent _component;
		private CommandHistoryComponentPanel _control;

		public void SetComponent(IApplicationComponent component)
		{
			_component = (CommandHistoryComponent) component;
		}

		public override object GuiElement
		{
			get
			{
				if (_control == null)
					_control = new CommandHistoryComponentPanel(_component);
				return _control;
			}
		}
	}
}
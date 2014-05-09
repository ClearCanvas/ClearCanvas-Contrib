using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.View.WinForms;
using Nullstack.ClearCanvas.DevTools.Properties;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
    /// <summary>
	/// This class specifies a view of the <see cref="DevToolsConfigComponent"/> using Windows Forms controls.
    /// </summary>
	[ExtensionOf(typeof (DevToolsConfigComponentViewExtensionPoint))]
	public class DevToolsConfigComponentView : WinFormsView, IApplicationComponentView
	{
		private DevToolsConfigComponent _component;
		private DevToolsConfigComponentPanel _control;

        /// <summary>
        /// Called by the application framework to set the component the view is for.
        /// </summary>
        /// <param name="component"></param>
		public void SetComponent(IApplicationComponent component)
		{
			_component = (DevToolsConfigComponent) component;
		}

        /// <summary>
        /// Gets the control that will actually show the data in the component.
        /// </summary>
		public override object GuiElement
		{
			get
			{
				if (_control == null)
				{
					_control = new DevToolsConfigComponentPanel(_component);
				}
				return _control;
			}
		}
	}
}
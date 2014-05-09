using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.View.WinForms;
using Nullstack.ClearCanvas.DevTools.Logging;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
    /// <summary>
	/// This class specifies a view of the <see cref="InsertLogMarkerComponent"/> using Windows Forms controls.
    /// </summary>
	[ExtensionOf(typeof(InsertLogMarkerComponentViewExtensionPoint))]
	public class InsertLogMarkerComponentView : WinFormsView, IApplicationComponentView
	{
		private InsertLogMarkerComponent _component;
		private InsertLogMarkerComponentPanel _control;

        /// <summary>
        /// Called by the application framework to set the component the view is for.
        /// </summary>
        /// <param name="component"></param>
		public void SetComponent(IApplicationComponent component)
		{
			_component = (InsertLogMarkerComponent)component;
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
					_control = new InsertLogMarkerComponentPanel(_component);
				}
				return _control;
			}
		}
	}
}
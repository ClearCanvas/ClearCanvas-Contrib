using System;
using System.Windows.Forms;
using Nullstack.ClearCanvas.DevTools.Properties;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
    /// <summary>
	/// Lightweight view control for the <see cref="DevToolsConfigComponent"/>
    /// </summary>
	public partial class DevToolsConfigComponentPanel : UserControl
	{
		private readonly DevToolsConfigComponent _component;

		public DevToolsConfigComponentPanel()
		{
			InitializeComponent();
		}

		public DevToolsConfigComponentPanel(DevToolsConfigComponent component)
			: this()
		{
			_component = component;
            
            // bind the controls to the component
			spinAdvanceLog.DataBindings.Add("Value", component, "LogAdvanceLines");

			txtLogOtherAppPath.Text = _component.LogViewerAppPath ?? "";
			txtLogOtherAppPath.TextChanged += txtLogOtherAppPath_TextChanged;

			radLogOtherApp.Checked = _component.LogViewerAppPath != null && _component.LogViewerAppPath != "";
			radLogOtherApp.CheckedChanged += radLogOtherApp_CheckedChanged;

			radLogShellAssoc.Checked = !radLogOtherApp.Checked;
			radLogShellAssoc.CheckedChanged += radLogShellAssoc_CheckedChanged;
		}

		private void txtLogOtherAppPath_TextChanged(object sender, EventArgs e)
		{
			if (radLogOtherApp.Checked)
				_component.LogViewerAppPath = txtLogOtherAppPath.Text;
		}

		private void radLogOtherApp_CheckedChanged(object sender, EventArgs e)
		{
			txtLogOtherAppPath.Enabled = radLogOtherApp.Checked;
			_component.LogViewerAppPath = txtLogOtherAppPath.Text;
		}

		private void radLogShellAssoc_CheckedChanged(object sender, EventArgs e)
		{
			_component.LogViewerAppPath = null;
		}
	}
}
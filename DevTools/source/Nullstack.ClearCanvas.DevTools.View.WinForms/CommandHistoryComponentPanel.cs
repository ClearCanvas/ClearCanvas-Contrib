using System;
using System.Windows.Forms;
using Nullstack.ClearCanvas.DevTools.History;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
	public partial class CommandHistoryComponentPanel : UserControl
	{
		private readonly CommandHistoryComponent _component;

		internal CommandHistoryComponentPanel()
		{
			InitializeComponent();
		}

		public CommandHistoryComponentPanel(CommandHistoryComponent component) : this()
		{
			_component = component;
			_component.CurrentCommandChanged += OnComponentCurrentCommandChanged;

			_commandList.DataSource = component.CommandList;
		}

		private void DisposeCommandHistoryComponentPanel()
		{
			_component.CurrentCommandChanged -= OnComponentCurrentCommandChanged;
		}

		private void OnComponentCurrentCommandChanged(object sender, EventArgs e)
		{
			//int index = _component.CurrentCommand;
			//if (index >= 0 && index < _commandList.Items.Count)
			//{
			//    _commandList.SelectedIndex = index;
			//}
			//else
			//{
			//    _commandList.SelectedItem = null;
			//}
		}

		private void commandList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_propertyGrid.IsDisposed || _commandList.IsDisposed)
				return;

			int index = _commandList.SelectedIndex;
			if (index >= 0 && index < _commandList.Items.Count)
				_propertyGrid.SelectedObject = _commandList.SelectedItem;
			else
				_propertyGrid.SelectedObject = null;
		}
	}
}
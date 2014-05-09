using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;

namespace Nullstack.ClearCanvas.DevTools.History
{
	[ExtensionPoint]
	public sealed class CommandHistoryComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView> {}

	[AssociateView(typeof (CommandHistoryComponentViewExtensionPoint))]
	public class CommandHistoryComponent : ApplicationComponent
	{
		private static readonly FieldInfo _rfHistoryField = typeof(CommandHistory).GetField("_history", BindingFlags.NonPublic | BindingFlags.Instance);
		private event EventHandler _currentCommandChanged;
		private readonly BindingList<CommandInfo> _commandList;
		private IWorkspace _targetWorkspace;
		private int _currentCommand;

		public CommandHistoryComponent()
		{
			_commandList = new BindingList<CommandInfo>();
			_commandList.AllowEdit = false;
			_commandList.AllowNew = false;
			_commandList.AllowRemove = false;
		}

		public override void Stop()
		{
			this.TargetWorkspace = null;
			base.Stop();
		}

		public event EventHandler CurrentCommandChanged
		{
			add { _currentCommandChanged += value; }
			remove { _currentCommandChanged -= value; }
		}

		public int CurrentCommand
		{
			get { return _currentCommand; }
		}

		public IBindingList CommandList
		{
			get { return _commandList; }
		}

		public IWorkspace TargetWorkspace
		{
			get { return _targetWorkspace; }
			set
			{
				if (_targetWorkspace != value)
				{
					if (_targetWorkspace != null)
					{
						_targetWorkspace.Closed -= OnTargetWorkspaceClosed;
						if (_targetWorkspace.CommandHistory != null)
							_targetWorkspace.CommandHistory.CurrentCommandChanged -= OnCommandHistoryChanged;
					}

					_targetWorkspace = value;
					Refresh();

					if (_targetWorkspace != null)
					{
						if (_targetWorkspace.CommandHistory != null)
							_targetWorkspace.CommandHistory.CurrentCommandChanged += OnCommandHistoryChanged;
						_targetWorkspace.Closed += OnTargetWorkspaceClosed;
					}
				}
			}
		}

		public void Refresh()
		{
			_commandList.RaiseListChangedEvents = false;
			_commandList.Clear();
			_currentCommand = -1;
			try
			{
				if (_targetWorkspace != null)
				{
					CommandHistory ch = _targetWorkspace.CommandHistory;
					if (ch != null)
					{
						List<UndoableCommand> history = _rfHistoryField.GetValue(ch) as List<UndoableCommand>;
						if (history != null)
						{
							foreach (UndoableCommand command in history)
								_commandList.Add(CommandInfo.CreateCommandInfo(command));
						}
						_currentCommand = ch.CurrentCommandIndex;
						//if (_currentCommand >= 0 && _currentCommand < _commandList.Count)
						//    _commandList[_currentCommand].IsCurrent = true;
					}
				}
			}
			finally
			{
				EventsHelper.Fire(_currentCommandChanged, this, new EventArgs());
				_commandList.RaiseListChangedEvents = true;
				_commandList.ResetBindings();
			}
		}

		private void OnCommandHistoryChanged(object sender, EventArgs e)
		{
			Refresh();
		}

		private void OnTargetWorkspaceClosed(object sender, ClosedEventArgs e)
		{
			this.TargetWorkspace = null;
		}
	}
}
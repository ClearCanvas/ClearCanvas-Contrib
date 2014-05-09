using System;
using System.ComponentModel;
using System.Windows.Forms;
using ClearCanvas.Common.Utilities;
using Nullstack.ClearCanvas.DevTools.Logging;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
    /// <summary>
    /// A light-weight control for viewing the <see cref="DesktopLogComponent"/>
    /// </summary>
	public partial class DesktopLogComponentPanel : UserControl
	{
		public event EventHandler WindowSizeKBChanged;

		private delegate void VoidDelegate();

		private DesktopLogComponent _component;
		private string _searchText = "";
		private bool _autoScrollToEnd = true;

		public DesktopLogComponentPanel()
		{
			InitializeComponent();
		}

		public DesktopLogComponentPanel(DesktopLogComponent component) : this()
		{
			_component = component;
			_component.PropertyChanged += Component_PropertyChanged;
			_component.LogUpdated += Component_LogUpdated;

			try
			{
				_autoScrollToEnd = ViewSettings.Default.AutoScrollToEnd;
			}
			catch (Exception) {}

            // note: we don't bind directly to the component because the search and auto scroll features are control-specific, and the window size
            // on the component is specified in bytes (so we have to convert them ourselves)
			txtWindowSize.DataBindings.Add("Text", this, "WindowSizeKB", false, DataSourceUpdateMode.OnPropertyChanged);
			txtSearch.DataBindings.Add("Text", this, "SearchText", false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoScrollToEnd.DataBindings.Add("Checked", this, "AutoScrollToEnd", false, DataSourceUpdateMode.OnPropertyChanged);

			UpdateLogContents();
		}

		private void PerformAdditionalDisposal()
		{
			if (_component != null)
			{
				_component.PropertyChanged -= Component_PropertyChanged;
				_component.LogUpdated -= Component_LogUpdated;
				_component = null;
			}
		}

		private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "WindowSize")
			{
				EventsHelper.Fire(WindowSizeKBChanged, this, new EventArgs());
			}
		}

		private void Component_LogUpdated(object sender, EventArgs e)
		{
			UpdateLogContents();
		}

		private void UpdateLogContents()
		{
            // invocation may be required here because the log component listens to file change events on a different thread
			if (rtfLog.InvokeRequired)
			{
				rtfLog.Invoke(new VoidDelegate(UpdateLogContents));
			}
			else
			{
				rtfLog.Text = _component.Log;
				if (this.AutoScrollToEnd)
				{
					rtfLog.Select(rtfLog.TextLength, 0);
					rtfLog.ScrollToCaret();
				}
			}
		}

		private void FindFirstInLog()
		{
            // invocation may be required here because the log component listens to file change events on a different thread
			if (rtfLog.InvokeRequired)
			{
				rtfLog.Invoke(new VoidDelegate(FindFirstInLog));
			}
			else
			{
				int i = rtfLog.Find(this.SearchText, RichTextBoxFinds.None);
				if (i >= 0)
				{
					rtfLog.Select(i, 5);
					rtfLog.HideSelection = false;
					rtfLog.ScrollToCaret();
				}
			}
		}

		private void FindNextInLog()
		{
            // invocation may be required here because the log component listens to file change events on a different thread
			if (rtfLog.InvokeRequired)
			{
				rtfLog.Invoke(new VoidDelegate(FindNextInLog));
			}
			else
			{
				if (rtfLog.SelectionStart == rtfLog.TextLength)
					return;
				int i = rtfLog.Find(this.SearchText, rtfLog.SelectionStart + 1, rtfLog.TextLength, RichTextBoxFinds.None);
				if (i >= 0)
				{
					rtfLog.Select(i, 5);
					rtfLog.ScrollToCaret();
				}
			}
		}

		private void FindPrevInLog()
		{
            // invocation may be required here because the log component listens to file change events on a different thread
			if (rtfLog.InvokeRequired)
			{
				rtfLog.Invoke(new VoidDelegate(FindPrevInLog));
			}
			else
			{
				int i = rtfLog.Find(this.SearchText, 0, rtfLog.SelectionStart, RichTextBoxFinds.Reverse);
				if (i >= 0)
				{
					rtfLog.Select(i, 5);
					rtfLog.ScrollToCaret();
				}
			}
		}

		public string WindowSizeKB
		{
			get { return (_component.WindowSize/1024).ToString(); }
			set
			{
				int result;
				if (int.TryParse(value, out result))
					_component.WindowSize = result*1024;
			}
		}

		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (_searchText != value)
				{
					_searchText = value;
					FindFirstInLog();
				}
			}
		}

		public bool AutoScrollToEnd
		{
			get { return _autoScrollToEnd; }
			set { ViewSettings.Default.AutoScrollToEnd = _autoScrollToEnd = value; }
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			FindPrevInLog();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			FindNextInLog();
		}
	}
}
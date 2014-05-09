using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ClearCanvas.Common;
using Nullstack.ClearCanvas.DevTools.Logging;

namespace Nullstack.ClearCanvas.DevTools.View.WinForms
{
	public partial class InsertLogMarkerComponentPanel : UserControl
	{
		public InsertLogMarkerComponentPanel()
		{
			InitializeComponent();

			cboLevel.Items.Add(LogLevel.Debug);
			cboLevel.Items.Add(LogLevel.Info);
			cboLevel.Items.Add(LogLevel.Warn);
			cboLevel.Items.Add(LogLevel.Error);
			cboLevel.Items.Add(LogLevel.Fatal);
		}

		public InsertLogMarkerComponentPanel(InsertLogMarkerComponent component) : this()
		{
			cboLevel.DataBindings.Add("SelectedItem", component, "LogLevel");
			spinMultiplier.DataBindings.Add("Value", component, "Multiplier");
			txtMessage.DataBindings.Add("Text", component, "Message");
		}
	}
}
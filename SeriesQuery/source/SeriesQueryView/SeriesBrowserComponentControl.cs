using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ClearCanvas.Desktop.View.WinForms;
using ClearCanvas.Desktop;


namespace SeriesQuery
{
    public partial class SeriesBrowserComponentControl : UserControl
    {

        private SeriesBrowserComponent _seriesBrowserComponent;
        private BindingSource _bindingSource;

        public SeriesBrowserComponentControl(SeriesBrowserComponent component)
        {
            InitializeComponent();

            _seriesBrowserComponent = component;
 
            //_seriesBrowserComponent.SelectedServerChanged += new EventHandler(OnSelectedServerChanged);
            _seriesTableView.Table = _seriesBrowserComponent.SeriesList;
            _seriesTableView.ToolbarModel = _seriesBrowserComponent.ToolbarModel;
            _seriesTableView.MenuModel = _seriesBrowserComponent.ContextMenuModel;
            _seriesTableView.SelectionChanged += new EventHandler(OnSeriesTableViewSelectionChanged);
            //_seriesTableView.ItemDoubleClicked += new EventHandler(OnSeriesTableViewDoubleClick);

            _bindingSource = new BindingSource();
            _bindingSource.DataSource = _seriesBrowserComponent;


        }

        void OnSeriesTableViewSelectionChanged(object sender, EventArgs e)
        {
            //The table view remembers the selection order, with the most recent being first.
            //We actually want that same order, but in reverse.
            _seriesBrowserComponent.SetSelection(ReverseSelection(_seriesTableView.Selection));
        }

        private static ISelection ReverseSelection(ISelection selection)
        {
            ArrayList list = new ArrayList();

            if (selection != null && selection.Items != null)
            {
                foreach (object o in selection.Items)
                    list.Add(o);

                list.Reverse();
            }

            return new Selection(list);
        }
    }
}

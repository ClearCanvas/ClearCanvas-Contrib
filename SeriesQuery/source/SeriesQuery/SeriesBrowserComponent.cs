#region License

// Copyright (c) 2006-2008, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;


using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tables;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.ImageViewer.Services;
using ClearCanvas.ImageViewer.Services.ServerTree;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageViewer.Annotations.Dicom;
using ClearCanvas.Dicom.Utilities;

namespace SeriesQuery
{
    [ExtensionPoint()]
    public sealed class SeriesBrowserToolExtensionPoint : ExtensionPoint<ITool>
    {
    }

    /// <summary>
    /// Extension point for views onto <see cref="SeriesBrowserComponent"/>.
    /// </summary>
    [ExtensionPoint]
    public sealed class SeriesBrowserComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    public interface ISeriesBrowserToolContext : IToolContext
    {
        SeriesItem SelectedSingleSeries { get; }

        ReadOnlyCollection<SeriesItem> SelectedMultipleSeries { get; }

        //AEServerGroup SelectedServerGroup { get; }

        Server SelectedServer { get; }

        ServerGroup SelectedServerGroup { get; }

        event EventHandler SelectedSeriesChanged;

        event EventHandler SelectedServerChanged;

        ClickHandlerDelegate DefaultActionHandler { get; set; }

        IDesktopWindow DesktopWindow { get; }

        void RefreshSeriesList();
    }



    /// <summary>
    /// SeriesBrowserComponent class.
    /// </summary>
    /// 
    [AssociateView(typeof(SeriesBrowserComponentViewExtensionPoint))]
    public class SeriesBrowserComponent : ApplicationComponent
    {
        public class SeriesBrowserToolContext : ToolContext, ISeriesBrowserToolContext
		{
            SeriesBrowserComponent _component;

            public SeriesBrowserToolContext(SeriesBrowserComponent component)
			    {
				    Platform.CheckForNullReference(component, "component");
				    _component = component;
			    }

			#region IStudyBrowserToolContext Members

			public SeriesItem SelectedSingleSeries
			{
				get
				{
					return _component.SelectedSingleSeries;
				}
			}

			public ReadOnlyCollection<SeriesItem> SelectedMultipleSeries 
			{
				get 
				{
					return _component.SelectedMultipleSeries;
				} 
			}

            public Server SelectedServer
            {
                get { return _component._server; }
                
            }

            public ServerGroup SelectedServerGroup
            {
                get { return _component._serverGroup; }
            }

			public event EventHandler SelectedSeriesChanged
			{
				add { _component.SelectedSeriesChanged += value; }
				remove { _component.SelectedSeriesChanged -= value; }
			}

			public event EventHandler SelectedServerChanged
			{
				add { _component.SelectedServerChanged += value; }
				remove { _component.SelectedServerChanged -= value; }
			}

			public ClickHandlerDelegate DefaultActionHandler
			{
				get { return _component._defaultActionHandler; }
				set { _component._defaultActionHandler = value; }
			}

			public IDesktopWindow DesktopWindow
			{
				get { return _component.Host.DesktopWindow; }
			}

			public void RefreshSeriesList()
			{
				//_component.Search();
			}

			#endregion
		}

		public class SearchResult
		{
			private Table<SeriesItem> _seriesList;
			private string _resultsTitle = "";

			public SearchResult()
			{

			}

			public Table<SeriesItem> SeriesList
			{
				get 
				{
					if (_seriesList == null)
						_seriesList = new Table<SeriesItem>();

					return _seriesList; 
				}
			}

			public string ResultsTitle
			{
				get { return _resultsTitle; }
				set { _resultsTitle = value; }
			}
		}

        #region Fields

        //private SearchPanelComponent _searchPanelComponent;

        private ISelection _currentSelection;
        private event EventHandler _selectedSeriesChangedEvent;
        private ClickHandlerDelegate _defaultActionHandler;
        private ToolSet _toolSet;

        private AEServerGroup _selectedServerGroup;
        private event EventHandler _selectedServerChangedEvent;

        private ActionModelRoot _toolbarModel;
        private ActionModelRoot _contextMenuModel;

        private Server _server;
        private ServerGroup _serverGroup;

        SearchResult searchResult = new SearchResult();

       // private ILocalDataStoreEventBroker _localDataStoreEventBroker;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public SeriesBrowserComponent()
        {
        }

        public SeriesBrowserComponent(List<SeriesItem> InputSeriesList, Server node)
        {
            server = node;
            
            if(node == null)
            {
                server = new Server("localdatastore", "local", "localhost", "AE", 0, false, 0, 0);
                Platform.Log(LogLevel.Info, "null server");
            }

            //this.Host.DesktopWindow.ShowMessageBox("foo", MessageBoxActions.Ok);
            
            foreach (SeriesItem item in InputSeriesList)
            {
                searchResult.SeriesList.Items.Add(item);
               
                //_currentSeriesList.Add
            }


            TableColumn<SeriesItem, string> column;

            column = new TableColumn<SeriesItem, string>(
                      "Series Num",
                      delegate(SeriesItem item) { return item.SeriesNumber; },
                      null,
                      1.5f,
                      //delegate(SeriesItem one, SeriesItem two) { return (int)(double.Parse(one.Time.ToString()) - double.Parse(two.Time.ToString())); }
                      delegate(SeriesItem one, SeriesItem two) { return (int)(double.Parse(one.SeriesNumber.ToString()) - double.Parse(two.SeriesNumber.ToString())); }
                      );

            searchResult.SeriesList.Columns.Add(column);
            

           column = new TableColumn<SeriesItem, string>(
                    "Series Description",
                    delegate(SeriesItem item) { return item.SeriesDescription; },
                    1.5f);

            searchResult.SeriesList.Columns.Add(column);

            
            column = new TableColumn<SeriesItem, string>(
                    "Modality",
                    delegate(SeriesItem item) { return item.Modality; },
                    1.5f);

            searchResult.SeriesList.Columns.Add(column);


            column = new TableColumn<SeriesItem, string>(
                    "Number of Images",
                    delegate(SeriesItem item) { return item.NumberOfSeriesRelatedInstances; },
                    1.5f);
            searchResult.SeriesList.Columns.Add(column);

            column = new TableColumn<SeriesItem, string>(
                    "Date",
                    delegate(SeriesItem item) { return DicomDataFormatHelper.DateFormat(item.Date); },
                    1.5f);

            searchResult.SeriesList.Columns.Add(column);

            column = new TableColumn<SeriesItem, string>(
                    "Time",
                    delegate(SeriesItem item) { return timeformatHMS(item.Time); },
                    null,
                    1.5f,
                    delegate(SeriesItem one, SeriesItem two) { return (int)(double.Parse(one.Time.ToString()) - double.Parse(two.Time.ToString())); }
                    
                    );

            searchResult.SeriesList.Columns.Add(column);

        }



		public Table<SeriesItem> SeriesList
		{
            get { return searchResult.SeriesList; }
            //get { return _currentStudyList; }
            //set { _currentStudyList = value; }
		}

		public SeriesItem SelectedSingleSeries
		{
			get
			{
				if (_currentSelection == null)
					return null;

				return _currentSelection.Item as SeriesItem;
			}
		}

		public ReadOnlyCollection<SeriesItem> SelectedMultipleSeries
		{
			get
			{
				if (_currentSelection == null)
					return null;

				List<SeriesItem> selectedSeries = new List<SeriesItem>();

				foreach (SeriesItem item in _currentSelection.Items)
					selectedSeries.Add(item);

				return selectedSeries.AsReadOnly();
			}
		}

		public ActionModelRoot ToolbarModel
		{
			get { return _toolbarModel; }
		}

		public ActionModelNode ContextMenuModel
		{
			get { return _contextMenuModel; }
		}

        //public string ResultsTitle
        //{
        //    get { return _resultsTitle; }
        //    set
        //    {
        //        _resultsTitle = value;
        //        NotifyPropertyChanged("ResultsTitle");
        //    }
        //}

		private event EventHandler SelectedSeriesChanged
		{
			add { _selectedSeriesChangedEvent += value; }
			remove { _selectedSeriesChangedEvent -= value; }
		}

		public event EventHandler SelectedServerChanged
		{
			add { _selectedServerChangedEvent += value; }
			remove { _selectedServerChangedEvent -= value; }
        }
       
        public Server server
        {
            get { return _server; }
            set { _server = value; }
        }

        public ServerGroup serverGroup
        {
            get { return _serverGroup; }
            set { _serverGroup = value; }
        }


        #region IApplicationComponent overrides
        /// <summary>
        /// Called by the host to initialize the application component.
        /// </summary>
        public override void Start()
        {
            // TODO prepare the component for its live phase
            base.Start();
            _toolSet = new ToolSet(new SeriesBrowserToolExtensionPoint(), new SeriesBrowserToolContext(this));
            _toolbarModel = ActionModelRoot.CreateModel(this.GetType().FullName, "dicomseriesbrowser-toolbar", _toolSet.Actions);
            _contextMenuModel = ActionModelRoot.CreateModel(this.GetType().FullName, "dicomseriesbrowser-contextmenu", _toolSet.Actions);

        }

        /// <summary>
        /// Called by the host when the application component is being terminated.
        /// </summary>
        public override void Stop()
        {
            // TODO prepare the component to exit the live phase
            // This is a good place to do any clean up
            base.Stop();
        }

        #endregion

        public void SelectServerGroup(AEServerGroup selectedServerGroup)
        {
            _selectedServerGroup = selectedServerGroup;

            //if (!_searchResults.ContainsKey(_selectedServerGroup.GroupID))
            //{
            //    SearchResult searchResult = new SearchResult();
            //    searchResult.ResultsTitle = String.Format("{0}", _selectedServerGroup.Name);
            //    AddColumns(searchResult.StudyList);

            //    _searchResults.Add(_selectedServerGroup.GroupID, searchResult);
            //}

            //ProcessReceivedAndRemovedStudies();

            //Update both of these in the view.
            //this.ResultsTitle = _searchResults[_selectedServerGroup.GroupID].ResultsTitle;
            //this.StudyList = _searchResults[_selectedServerGroup.GroupID].StudyList;

            EventsHelper.Fire(_selectedServerChangedEvent, this, EventArgs.Empty);
        }

        //public Table<SeriesItem> SeriesList
        //{
        // //   get { return _currentSeriesList; }
        // //   set { _currentSeriesList = value; }
        //    get { return searchResult.SeriesList; }
        //}

        public void SetSelection(ISelection selection)
        {
            if (_currentSelection != selection)
            {
                _currentSelection = selection;
                EventsHelper.Fire(_selectedSeriesChangedEvent, this, EventArgs.Empty);
            }
        }

        private string timeformatHMS(string input)
        {
            DateTime time;
            if (!TimeParser.Parse(input, out time))
                return input;

            return time.ToString("HH:mm:ss.FFFFFF");
        }


    }
}

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

using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.Desktop.Tables;

namespace Tagging
{
    [MenuAction("launch", "global-menus/MenuTools/MenuStudyTagging", "Open")]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public class TagBrowserTool : Tool<IDesktopToolContext>
    {
        private Shelf _shelf;

        public void Open()
        {
            if (_shelf == null)
            {
                _shelf = ApplicationComponent.LaunchAsShelf(
                    this.Context.DesktopWindow,
                    new TagBrowserComponent(new TagDatabase()),
                    "Study Tagging",
                    ShelfDisplayHint.DockRight);
                _shelf.Closed += delegate { _shelf = null; };
            }
            else
            {
                _shelf.Activate();
            }
        }
    }



    /// <summary>
    /// Extension point for views onto <see cref="TagBrowserComponent"/>.
    /// </summary>
    [ExtensionPoint]
    public sealed class TagBrowserComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    /// <summary>
    /// TagBrowserComponent class.
    /// </summary>
    [AssociateView(typeof(TagBrowserComponentViewExtensionPoint))]
    public class TagBrowserComponent : ApplicationComponent
    {

        #region StudyTableEntry class

        /// <summary>
        /// Holds information for a study table entry.
        /// </summary>
        internal class StudyTableEntry
        {
            private StudyItem _studyItem; 
            private string _tags;
            private double _similarityScore;

            public StudyTableEntry(StudyItem studyItem, string tags, double similarityScore)
            {
                _studyItem = studyItem;
                _tags = tags;
                _similarityScore = similarityScore;
            }

            public StudyItem StudyItem
            {
                get { return _studyItem; }
            }

            public string Tags
            {
                get { return _tags; }
            }

            public double SimilarityScore
            {
                get { return _similarityScore; }
            }
        }

        #endregion


        #region SimilarStudyQueryProgress class

        /// <summary>
        /// Helper class for async study queries.
        /// </summary>
        internal class StudyQueryProgress : BackgroundTaskProgress
        {
            private StudyTableEntry _entry;

            public StudyQueryProgress(StudyTableEntry entry)
            {
                _entry = entry;
            }

            public StudyTableEntry Entry
            {
                get { return _entry; }
            }
        }

        #endregion


        private TagDatabase _database;
        private string _tags;
        private string _originalTags;
        private Study _activeStudy;
        private bool _searchEnabled;
        private string _searchTags = "";

        private Table<StudyTableEntry> _searchResults;
        private StudyTableEntry _selectedSearchResult;

        private BackgroundTask _queryStudiesTask;


        /// <summary>
        /// Constructor.
        /// </summary>
        public TagBrowserComponent(TagDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Called by the host to initialize the application component.
        /// </summary>
        public override void Start()
        {

            _searchResults = new Table<StudyTableEntry>(2);
            _searchResults.Columns.Add(new TableColumn<StudyTableEntry, string>("Study",
                delegate(StudyTableEntry item)
                {
                    return string.Format("{0} {1} A# {2} {3}: {4}",
                        item.StudyItem.PatientId,
                        item.StudyItem.PatientsName.FormattedName,
                        item.StudyItem.AccessionNumber,
                        item.StudyItem.ModalitiesInStudy,
                        item.StudyItem.StudyDescription);
                }, 1.0f));
            _searchResults.Columns.Add(new TableColumn<StudyTableEntry, string>("Relevance",
                delegate(StudyTableEntry item)
                {
                    return string.Format("{0} %", (int)(item.SimilarityScore*100));
                }, 0.2f));

            _searchResults.Columns.Add(new TableColumn<StudyTableEntry, string>("Tags",
                delegate(StudyTableEntry item)
                {
                    return item.Tags;
                }, 1));

            this.Host.DesktopWindow.Workspaces.ItemActivationChanged += Workspaces_ItemActivationChanged;

            LoadData(this.Host.DesktopWindow.ActiveWorkspace);

            base.Start();
        }

        /// <summary>
        /// Called by the host when the application component is being terminated.
        /// </summary>
        public override void Stop()
        {
            this.Host.DesktopWindow.Workspaces.ItemActivationChanged -= Workspaces_ItemActivationChanged;
            base.Stop();
        }

        #region Presentation Model

        /// <summary>
        /// Gets or sets the tags for the study in the currently active workspace.
        /// </summary>
        public string Tags
        {
            get { return _tags; }
            set
            {
                if (_tags != value)
                {
                    _tags = value;
                    NotifyPropertyChanged("Tags");
                    NotifyPropertyChanged("UpdateEnabled");
                }
            }
        }

        /// <summary>
        /// Gets the search results table.
        /// </summary>
        public ITable SearchResultsTable
        {
            get { return _searchResults; }
        }

        /// <summary>
        /// Gets or sets the selected search result.
        /// </summary>
        public ISelection SelectedSearchResult
        {
            get { return new Selection(_selectedSearchResult); }
            set
            {
                _selectedSearchResult = (StudyTableEntry)value.Item;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Update Tags button is enabled.
        /// </summary>
        public bool UpdateEnabled
        {
            get { return _activeStudy != null && _tags != _originalTags; }
        }

        /// <summary>
        /// Gets or sets whether the search mode is enabled.
        /// </summary>
        public bool SearchEnabled
        {
            get { return _searchEnabled; }
            set
            {
                if (_searchEnabled != value)
                {
                    _searchEnabled = value;
                    NotifyPropertyChanged("SearchEnabled");

                    if (_searchEnabled)
                        Search();
                    else
                        FindSimilar();
                }
            }
        }

        /// <summary>
        /// Gets or sets the tags for searching.
        /// </summary>
        public string SearchTags
        {
            get { return _searchTags; }
            set { _searchTags = value; }
        }

        /// <summary>
        /// Updates the tags for the study in the active workspace.
        /// </summary>
        public void UpdateTags()
        {
            if (_activeStudy != null)
            {
                string[] tags = ParseTagString(_tags);
                _database.SetTagsForStudy(_activeStudy.StudyInstanceUID, tags);
                _originalTags = _tags;

                NotifyPropertyChanged("UpdateEnabled");

                if (!_searchEnabled)
                {
                    FindSimilar();
                }
            }
        }

        /// <summary>
        /// Executes a search based on <see cref="SearchTags"/>.
        /// </summary>
        public void Search()
        {
            if (!string.IsNullOrEmpty(_searchTags))
            {
                QueryStudies(_searchTags,
                    delegate(string query)
                    {
                        return _database.FindStudies(ParseTagString(query), 10, 0.01);
                    });
            }
        }

        /// <summary>
        /// Opens the selected search result in a new workspace.
        /// </summary>
        public void OpenSelectedSearchResult()
        {
            if(_selectedSearchResult != null)
            {
                OpenStudyHelper.OpenStudies("DICOM_LOCAL", new string[] { _selectedSearchResult.StudyItem.StudyInstanceUID }, WindowBehaviour.Auto);
            }
        }

        #endregion


        private void Workspaces_ItemActivationChanged(object sender, ItemEventArgs<Workspace> e)
        {
            Workspace workspace = e.Item;
            LoadData(workspace.Active ? workspace : null);
        }

        /// <summary>
        /// Loads tag data for the study in the specified workspace.
        /// </summary>
        /// <param name="workspace"></param>
        private void LoadData(Workspace workspace)
        {
            IImageViewer viewer = workspace == null ? null : ImageViewerComponent.GetAsImageViewer(workspace);
            if (viewer != null)
            {
                // for simplicity, assume only one patient and study in the workspace
                Patient patient = CollectionUtils.FirstElement(viewer.StudyTree.Patients.Values);
                _activeStudy = CollectionUtils.FirstElement(patient.Studies.Values);

                // load the tags for the active study, remembering the original value so we know if any changes are made
                _originalTags = _tags = MakeTagsString(_database.GetTagsForStudy(_activeStudy.StudyInstanceUID));

                if (!_searchEnabled)
                {
                    FindSimilar();
                }
            }
            else
            {
                Clear();
            }

            NotifyPropertyChanged("Tags");
            NotifyPropertyChanged("UpdateEnabled");
        }

        /// <summary>
        /// Clears all data related to the active study.
        /// </summary>
        private void Clear()
        {
            _tags = "";
            _originalTags = "";
            _activeStudy = null;
            _searchResults.Items.Clear();
            _selectedSearchResult = null;
        }

        /// <summary>
        /// Executes query for similar studies.
        /// </summary>
        private void FindSimilar()
        {
            QueryStudies(_activeStudy.StudyInstanceUID,
                delegate(string studyUid)
                {
                    return _database.FindSimilarStudies(studyUid, 10, 0.01);
                });
        }

        /// <summary>
        /// Queries local datastore for studies based on specified search function.
        /// </summary>
        private void QueryStudies(string query, Converter<string, KeyValuePair<string, double>[]> searchFunc)
        {
            // if there is a previous task that may still be running, disconnect it so we effectively abandon it
            if (_queryStudiesTask != null)
                _queryStudiesTask.ProgressUpdated -= OnQueryStudiesProgressUpdate;

            // clear existing items
            _searchResults.Items.Clear();

            // create new task to query similar studies asynchronously so as not to block UI
            _queryStudiesTask = new BackgroundTask(
                delegate(IBackgroundTaskContext ctx)
                {
                    KeyValuePair<string, double>[] similarStudyUids = searchFunc(query);
                    foreach (KeyValuePair<string, double> kvp in similarStudyUids)
                    {
                        string studyUid = kvp.Key;
                        double score = kvp.Value;

                        QueryParameters queryParams = new QueryParameters();
                        queryParams.Add("PatientsName", "");
                        queryParams.Add("PatientId", "");
                        queryParams.Add("AccessionNumber", "");
                        queryParams.Add("StudyDescription", "");
                        queryParams.Add("ModalitiesInStudy", "");
                        queryParams.Add("StudyDate", "");
                        queryParams.Add("StudyInstanceUid", studyUid);

                        // currently only the local dicom store is supported
                        StudyItemList similarStudyItems = ImageViewerComponent.FindStudy(queryParams, null, "DICOM_LOCAL");

                        // should only ever be one result at most
                        // if zero results, it means the study exists in the tag database but not in the local dicom store
                        StudyItem studyItem = CollectionUtils.FirstElement(similarStudyItems);
                        if (studyItem != null)
                        {
                            StudyTableEntry entry = new StudyTableEntry(studyItem, MakeTagsString(_database.GetTagsForStudy(studyUid)), score);
                            ctx.ReportProgress(new StudyQueryProgress(entry));
                        }
                    }

                }, false);

            _queryStudiesTask.ProgressUpdated += OnQueryStudiesProgressUpdate;
            _queryStudiesTask.Run();
        }

        /// <summary>
        /// Called when study query posts a new result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnQueryStudiesProgressUpdate(object sender, BackgroundTaskProgressEventArgs args)
        {
            StudyQueryProgress progress = (StudyQueryProgress)args.Progress;
            _searchResults.Items.Add(progress.Entry);
        }

        /// <summary>
        /// Formats a set of tags as a single string.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private static string MakeTagsString(string[] tags)
        {
            return StringUtilities.Combine(tags, " ");
        }

        /// <summary>
        /// Parses a string of tags separated by spaces, tabs, or commas.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private static string[] ParseTagString(string tags)
        {
            return tags.Split(new string[] { " ", "\t", "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

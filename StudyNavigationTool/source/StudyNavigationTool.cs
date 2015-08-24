#region License

// Copyright(C) 2015 Martin Kirsche <martin.kirsche+cc@gmail.com>, [econmed GmbH](http://econmed.de)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.Layout.Basic;
using ClearCanvas.ImageViewer.StudyManagement;

namespace Econmed.ImageViewer.Layout.Basic
{
    [MenuAction("previous", "global-menus/MenuTools/MenuStandard/MenuPreviousDisplaySets", "PreviousDisplaySets")]
    [ButtonAction("previous", "global-toolbars/ToolbarStandard/ToolbarPreviousDisplaySets", "PreviousDisplaySets", KeyStroke = XKeys.Shift | XKeys.Tab)]
    [Tooltip("previous", "TooltipPreviousDisplaySets")]
    [IconSet("previous", "Icons.PreviousSmall.png", "Icons.PreviousMedium.png", "Icons.PreviousLarge.png")]
    [GroupHint("previous", "Tools.Navigation.DisplaySets.Previous.DisplaySets")]
    [EnabledStateObserver("previous", "PreviousEnabled", "PreviousEnabledChanged")]
    [MenuAction("next", "global-menus/MenuTools/MenuStandard/MenuNextDisplaySets", "NextDisplaySets")]
    [ButtonAction("next", "global-toolbars/ToolbarStandard/ToolbarNextDisplaySets", "NextDisplaySets", KeyStroke = XKeys.Tab)]
    [Tooltip("next", "TooltipNextDisplaySets")]
    [IconSet("next", "Icons.NextSmall.png", "Icons.NextMedium.png", "Icons.NextLarge.png")]
    [GroupHint("next", "Tools.Navigation.DisplaySets.Next.DisplaySets")]
    [EnabledStateObserver("next", "NextEnabled", "NextEnabledChanged")]
    [ExtensionOf(typeof(ImageViewerToolExtensionPoint))]
    public class StudyNavigationTool : Tool<IImageViewerToolContext>
    {
        private bool nextEnabled = true;
        public event EventHandler NextEnabledChanged;
        public bool NextEnabled
        {
            get { return nextEnabled; }
            protected set
            {
                if (nextEnabled != value)
                {
                    nextEnabled = value;
                    EventsHelper.Fire(NextEnabledChanged, this, EventArgs.Empty);
                }
            }
        }

        private bool previousEnabled = true;
        public event EventHandler PreviousEnabledChanged;
        public bool PreviousEnabled
        {
            get { return previousEnabled; }
            protected set
            {
                if (previousEnabled != value)
                {
                    previousEnabled = value;
                    EventsHelper.Fire(PreviousEnabledChanged, this, EventArgs.Empty);
                }
            }
        }

        public bool NextDisplaySetsAvailable { get; private set; }
        public bool PreviousDisplaySetsAvailable { get; private set; }
        public bool NextStudyAvailable { get; private set; }
        public bool PreviousStudyAvailable { get; private set; }

        private void OnStudyLoaded(object sender, StudyLoadedEventArgs e)
        {
            UpdateEnabled();
        }

        private void OnImageLoaded(object sender, ItemEventArgs<Sop> e)
        {
            UpdateEnabled();
        }

        private void OnDisplaySetSelected(object sender, DisplaySetSelectedEventArgs e)
        {
            UpdateEnabled();
        }

        private void OnImageBoxDrawing(object sender, ImageBoxDrawingEventArgs e)
        {
            UpdateEnabled();
        }

        private IImageBox FindPrimaryImageBox()
        {
            if (null != Context.Viewer.SelectedImageBox &&
                null != Context.Viewer.SelectedImageBox.DisplaySet &&
                null != Context.Viewer.SelectedImageBox.DisplaySet.ParentImageSet)
            {
                return Context.Viewer.SelectedImageBox;
            }
            foreach (IImageBox imageBox in Context.Viewer.PhysicalWorkspace.ImageBoxes)
            {
                if (null == imageBox.DisplaySet || null == imageBox.DisplaySet.ParentImageSet) { continue; }
                return imageBox;
            }
            return null;
        }

        private IList<IDisplaySet> GetSourceDisplaySets()
        {
            var imageBox = FindPrimaryImageBox();
            if (null == imageBox) { return null; }
            return imageBox.DisplaySet.ParentImageSet.DisplaySets;
        }

        private void GetIndex(out int displaySetIndex, out int imageSetIndex)
        {
            var imageBox = FindPrimaryImageBox();

            if (null == imageBox)
            {
                imageSetIndex = displaySetIndex = -1;
                return;
            }

            var displaySets = imageBox.DisplaySet.ParentImageSet.DisplaySets;
            int di;
            if (-1 == (di = displaySets.IndexOf(displaySets.FirstOrDefault(i => i.Uid == imageBox.DisplaySet.Uid))))
            {
                displaySetIndex = -1;
            }
            else
            {
                displaySetIndex = di / Context.Viewer.PhysicalWorkspace.ImageBoxes.Count;
            }

            var imageSets = imageBox.ParentPhysicalWorkspace.LogicalWorkspace.ImageSets;
            imageSetIndex = imageSets.IndexOf(imageSets.FirstOrDefault(i => i.Uid == imageBox.DisplaySet.ParentImageSet.Uid));
        }

        private void UpdateEnabled()
        {
            IList<IDisplaySet> sourceDisplaySets;
            if (0 == Context.Viewer.PhysicalWorkspace.ImageBoxes.Count ||
                Context.Viewer.PhysicalWorkspace.ImageBoxes.Any(imageBox => imageBox.DisplaySetLocked) ||
                null == (sourceDisplaySets = GetSourceDisplaySets()))
            {
                NextEnabled = false;
                PreviousEnabled = false;
                return;
            }

            var availableImageSets = Context.Viewer.LogicalWorkspace.ImageSets.Count;
            var availableDisplaySets = sourceDisplaySets.Count();
            var availableImageBoxes = Context.Viewer.PhysicalWorkspace.ImageBoxes.Count;

            int displaySetIndex, imageSetIndex;
            GetIndex(out displaySetIndex, out imageSetIndex);
            NextDisplaySetsAvailable = -1 != displaySetIndex && (displaySetIndex + 1) * availableImageBoxes < availableDisplaySets;
            PreviousDisplaySetsAvailable = -1 != displaySetIndex && (displaySetIndex - 1) * availableImageBoxes >= 0;

            NextStudyAvailable = -1 != imageSetIndex && imageSetIndex + 1 < availableImageSets;
            PreviousStudyAvailable = -1 != imageSetIndex && imageSetIndex - 1 >= 0;

            NextEnabled = NextDisplaySetsAvailable || NextStudyAvailable;
            PreviousEnabled = PreviousDisplaySetsAvailable || PreviousStudyAvailable;
        }

        public override void Initialize()
        {
            base.Initialize();
            UpdateEnabled();
            base.Context.Viewer.EventBroker.ImageLoaded += OnImageLoaded;
            base.Context.Viewer.EventBroker.StudyLoaded += OnStudyLoaded;
            base.Context.Viewer.EventBroker.ImageBoxDrawing += OnImageBoxDrawing;
            base.Context.Viewer.EventBroker.DisplaySetSelected += OnDisplaySetSelected;
        }

        protected override void Dispose(bool disposing)
        {
            base.Context.Viewer.EventBroker.ImageLoaded -= OnImageLoaded;
            base.Context.Viewer.EventBroker.StudyLoaded -= OnStudyLoaded;
            base.Context.Viewer.EventBroker.ImageBoxDrawing -= OnImageBoxDrawing;
            base.Context.Viewer.EventBroker.DisplaySetSelected -= OnDisplaySetSelected;

            base.Dispose(disposing);
        }

        public void NextDisplaySets()
        {
            AdvanceDisplaySets(+1);
        }

        public void PreviousDisplaySets()
        {
            AdvanceDisplaySets(-1);
        }

        public void LayoutPhysicalWorkspace(IImageSet imageSet)
        {
            StoredLayout layout = LayoutSettingsHelper.MinimumLayout;
            foreach (IDisplaySet displaySet in imageSet.DisplaySets)
            {
                if (displaySet.PresentationImages.Count <= 0) { continue; }
                StoredLayout storedLayout = LayoutSettingsHelper.GetLayout(displaySet.PresentationImages[0] as IImageSopProvider);
                layout.ImageBoxRows = Math.Max(layout.ImageBoxRows, storedLayout.ImageBoxRows);
                layout.ImageBoxColumns = Math.Max(layout.ImageBoxColumns, storedLayout.ImageBoxColumns);
                layout.TileRows = Math.Max(layout.TileRows, storedLayout.TileRows);
                layout.TileColumns = Math.Max(layout.TileColumns, storedLayout.TileColumns);
            }
            Context.Viewer.PhysicalWorkspace.SetImageBoxGrid(layout.ImageBoxRows, layout.ImageBoxColumns);
            foreach (var imageBox in Context.Viewer.PhysicalWorkspace.ImageBoxes)
            {
                imageBox.SetTileGrid(layout.TileRows, layout.TileColumns);
            }
        }

        public void AdvanceDisplaySets(int direction)
        {
            int displaySetIndex, imageSetIndex;
            GetIndex(out displaySetIndex, out imageSetIndex);
            if (-1 == displaySetIndex || -1 == imageSetIndex) { UpdateEnabled(); return; }

            IPhysicalWorkspace workspace = Context.Viewer.PhysicalWorkspace;
            MemorableUndoableCommand memorableCommand = new MemorableUndoableCommand(workspace);
            memorableCommand.BeginState = workspace.CreateMemento();

            int newDisplaySetIndex;
            IImageSet imageSet;

            if ((direction > 0 && NextDisplaySetsAvailable) ||
                (direction < 0 && PreviousDisplaySetsAvailable))
            {
                newDisplaySetIndex = displaySetIndex + direction;
                imageSet = Context.Viewer.PhysicalWorkspace.ImageBoxes.First().DisplaySet.ParentImageSet;
            }
            else if (direction > 0 && NextStudyAvailable)
            {
                imageSet = Context.Viewer.LogicalWorkspace.ImageSets[imageSetIndex + 1];
                LayoutPhysicalWorkspace(imageSet);
                newDisplaySetIndex = 0;
            }
            else if (direction < 0 && PreviousStudyAvailable)
            {
                imageSet = Context.Viewer.LogicalWorkspace.ImageSets[imageSetIndex - 1];
                LayoutPhysicalWorkspace(imageSet);
                newDisplaySetIndex = (imageSet.DisplaySets.Count - 1) / Context.Viewer.PhysicalWorkspace.ImageBoxes.Count;
            }
            else
            {
                throw new ArgumentException("Unable to advance DisplaySets.");
            }

            int imageBoxIndex = 0;
            foreach (var imageBox in workspace.ImageBoxes)
            {
                int i = ((workspace.ImageBoxes.Count) * newDisplaySetIndex) + imageBoxIndex;
                if (i < imageSet.DisplaySets.Count)
                {
                    imageBox.DisplaySet = imageSet.DisplaySets[i].CreateFreshCopy();
                }
                else
                {
                    imageBox.DisplaySet = null;
                }
                imageBoxIndex++;
            }

            workspace.Draw();
            workspace.SelectDefaultImageBox();

            memorableCommand.EndState = workspace.CreateMemento();
            DrawableUndoableCommand historyCommand = new DrawableUndoableCommand(workspace);
            historyCommand.Name = "AdvanceDisplaySet";
            historyCommand.Enqueue(memorableCommand);
            Context.Viewer.CommandHistory.AddCommand(historyCommand);
        }
    }
}

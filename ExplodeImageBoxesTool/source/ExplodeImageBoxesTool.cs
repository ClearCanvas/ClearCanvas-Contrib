#region License

// Copyright(C) 2015 Martin Kirsche<martin.kirsche+cc@gmail.com>, [econmed GmbH](http://econmed.de)
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

// Copyright (c) 2013, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This file is part of the ClearCanvas RIS/PACS open source project.
//
// The ClearCanvas RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The ClearCanvas RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the ClearCanvas RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.BaseTools;
using ClearCanvas.ImageViewer.InputManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Econmed.ImageViewer.Layout
{
    [ButtonAction("explodeImageBoxes", "global-toolbars/ToolbarStandard/ToolbarExplodeImageBoxes", "ToggleExplode", Flags = ClickActionFlags.CheckAction, KeyStroke = XKeys.X)]
	[MenuAction("explodeImageBoxes", "global-menus/MenuTools/MenuStandard/MenuExplodeImageBoxes", "ToggleExplode", Flags = ClickActionFlags.CheckAction)]
	[CheckedStateObserver("explodeImageBoxes", "Checked", "CheckedChanged")]
	[EnabledStateObserver("explodeImageBoxes", "Enabled", "EnabledChanged")]
	[Tooltip("explodeImageBoxes", "TooltipExplodeImageBox")]
	[MouseButtonIconSet("explodeImageBoxes", "Icons.ExplodeImageBoxToolSmall.png", "Icons.ExplodeImageBoxMedium.png", "Icons.ExplodeImageBoxLarge.png")]
	[GroupHint("explodeImageBoxes", "Tools.Layout.ImageBox.Explode")]

	[DefaultMouseToolButton(XMouseButtons.Left)]
	[ExtensionOf(typeof(ImageViewerToolExtensionPoint), Enabled = false)]
	public class ExplodeImageBoxesTool : MouseImageViewerTool
	{
        [ThreadStatic]
		private static Dictionary<IImageViewer, ExplodeImageBoxesTool> _tools;

		private ListObserver<IImageBox> _imageBoxesObserver;
		private object _unexplodeMemento = null;
		private Dictionary<IImageBox, IImageBox> oldImageBoxMap = new Dictionary<IImageBox, IImageBox>();

		private static Dictionary<IImageViewer, ExplodeImageBoxesTool> Tools
		{
			get
			{
				if (_tools == null)
					_tools = new Dictionary<IImageViewer, ExplodeImageBoxesTool>();
				return _tools;
			}	
		}

		public ExplodeImageBoxesTool()
		{          
            //this tool is activated on a double-click
            base.Behaviour &= ~MouseButtonHandlerBehaviour.CancelStartOnDoubleClick;
		}

		public bool Checked
		{
			get { return _unexplodeMemento != null; }
			set
			{
				if (value == Checked)
					return;

				ToggleExplode();
			}
		}

		public event EventHandler CheckedChanged;

		public override void Initialize()
		{
			base.Initialize();            

            Tools[ImageViewer] = this;

			_imageBoxesObserver = new ListObserver<IImageBox>(ImageViewer.PhysicalWorkspace.ImageBoxes, OnImageBoxesChanged);

			UpdateEnabled();
		}

		protected override void Dispose(bool disposing)
		{
			Tools.Remove(ImageViewer);

			_imageBoxesObserver.Dispose();
			
			base.Dispose(disposing);
		}

		List<IImageBox> imageBoxHistory = new List<IImageBox>();

		protected override void OnTileSelected(object sender, TileSelectedEventArgs e)
		{
			if (!Checked && null != e.SelectedTile &&
				CanExplodeImageBox(e.SelectedTile.ParentImageBox))
			{
				imageBoxHistory.Remove(e.SelectedTile.ParentImageBox);
				imageBoxHistory.Add(e.SelectedTile.ParentImageBox);
			}
			UpdateEnabled();
		}

		protected override void OnPresentationImageSelected(object sender, PresentationImageSelectedEventArgs e)
		{
			UpdateEnabled();
		}

		private void OnImageBoxesChanged()
		{
			CancelExplodeMode();
			UpdateEnabled();
		}

		private void UpdateEnabled()
		{
			IPhysicalWorkspace workspace = base.ImageViewer.PhysicalWorkspace;
			if (Checked)
			{
				base.Enabled = true;
			}
			else
			{
				base.Enabled = !workspace.Locked && workspace.ImageBoxes.Count > 1 && workspace.SelectedImageBox != null &&
							   workspace.SelectedImageBox.SelectedTile != null &&
							   workspace.SelectedImageBox.SelectedTile.PresentationImage != null;
			}
		}

		private void CancelExplodeMode()
		{
			_unexplodeMemento = null;
			oldImageBoxMap = new Dictionary<IImageBox, IImageBox>();
			OnCheckedChanged();
		}

		private void OnCheckedChanged()
		{
			EventsHelper.Fire(CheckedChanged, this, EventArgs.Empty);
		}

		public override bool Start(IMouseInformation mouseInformation)
		{
			//this is a double-click tool.
			if (mouseInformation.ClickCount < 2)
				return false;

			if (!Enabled)
				return false;

			if (Checked)
			{
				UnexplodeImageBoxes();
				return true;
			}
			else
			{
				return false;
			}	
		}

		private static bool CanExplodeImageBox(IImageBox imageBox)
		{
			if (imageBox == null)
				return false;

			if (imageBox.ParentPhysicalWorkspace == null)
				return false;

			if (imageBox.DisplaySet == null || imageBox.SelectedTile == null || imageBox.SelectedTile.PresentationImage == null)
				return false;

			return true;
		}

		private bool CanUnexplodeImageBox(IImageBox imageBox)
		{
			if (imageBox == null)
				return false;

			IPhysicalWorkspace workspace = imageBox.ParentPhysicalWorkspace;
			if (workspace == null)
				return false;

			if (imageBox.DisplaySet == null)
			{
				CancelExplodeMode();
				return false;
			}

			return true;
		}

		class ImageBoxState
		{
			public IImageBox ImageBox { get; set; }
			public object Memento { get; set; }
		}

		private void ExplodeImageBoxes()
		{
			if (0 == imageBoxHistory.Count ||
				!CanExplodeImageBox(ImageViewer.SelectedImageBox)) { return; }

			IPhysicalWorkspace workspace = ImageViewer.SelectedImageBox.ParentPhysicalWorkspace;
			MemorableUndoableCommand memorableCommand = new MemorableUndoableCommand(workspace);
			memorableCommand.BeginState = workspace.CreateMemento();

			_imageBoxesObserver.SuppressChangedEvent = true;

			//set this here so checked will be correct.
			_unexplodeMemento = memorableCommand.BeginState;
			List<IImageBox> explodeImageBoxes = new List<IImageBox>(imageBoxHistory.Skip(imageBoxHistory.Count - WorkspaceScreenCount()));
			explodeImageBoxes.Reverse();

			List<object> mementoList = new List<object>();
			foreach (IImageBox imageBox in explodeImageBoxes)
			{
				mementoList.Add(imageBox.CreateMemento());				
			}

			workspace.SetImageBoxGrid(1, explodeImageBoxes.Count);
			int i = 0;
			foreach (IImageBox imageBox in explodeImageBoxes)
			{			
				IImageBox newImageBox = workspace.ImageBoxes[i];
				oldImageBoxMap.Add(imageBox, newImageBox);
				newImageBox.SetMemento(mementoList[i]);
				i++;
			}

			_imageBoxesObserver.SuppressChangedEvent = false;

			workspace.Draw();
			workspace.SelectDefaultImageBox();

			memorableCommand.EndState = workspace.CreateMemento();
			DrawableUndoableCommand historyCommand = new DrawableUndoableCommand(workspace);
			historyCommand.Name = SR.CommandSurveyExplode;
			historyCommand.Enqueue(memorableCommand);
			base.ImageViewer.CommandHistory.AddCommand(historyCommand);

			OnCheckedChanged();
			UpdateEnabled();
		}

		private void UnexplodeImageBoxes()
		{			
			if (!CanUnexplodeImageBox(ImageViewer.SelectedImageBox))
				return;

			IPhysicalWorkspace workspace = ImageViewer.SelectedImageBox.ParentPhysicalWorkspace;

			Dictionary<IImageBox, object> imageBoxMementoDictionary = new Dictionary<IImageBox, object>();
			foreach(IImageBox imageBox in workspace.ImageBoxes)
			{
				imageBoxMementoDictionary.Add(imageBox, imageBox.CreateMemento());
			}
			Dictionary<IImageBox, IImageBox> oldMap = oldImageBoxMap;
			
			MemorableUndoableCommand memorableCommand = new MemorableUndoableCommand(workspace);
			memorableCommand.BeginState = workspace.CreateMemento();

			workspace.SetMemento(_unexplodeMemento);
			
			if(0 != oldMap.Count)
			{
				foreach (IImageBox box in workspace.ImageBoxes)
				{
					//Keep the state of the image box the same.
					if (oldMap.ContainsKey(box))
					{
						box.SetMemento(imageBoxMementoDictionary[oldMap[box]]);
					}
				}
			}

			workspace.Draw();

			memorableCommand.EndState = workspace.CreateMemento();

			DrawableUndoableCommand historyCommand = new DrawableUndoableCommand(workspace);            
            historyCommand.Name = SR.CommandSurveyExplode;
			historyCommand.Enqueue(memorableCommand);
			base.ImageViewer.CommandHistory.AddCommand(historyCommand);

			CancelExplodeMode();
			OnCheckedChanged();
			UpdateEnabled();
		}

		public void ToggleExplode()
		{
			if (!Enabled)
				return;

			if (Checked)
				UnexplodeImageBoxes();
			else
				ExplodeImageBoxes();
		}

		int WorkspaceScreenCount()
		{
			IPhysicalWorkspace workspace = ImageViewer.PhysicalWorkspace;
			int count = 0;
			foreach (Screen screen in Screen.AllScreens)
			{
				if (Rectangle.Intersect(screen.WorkingArea, workspace.ScreenRectangle).IsEmpty) { continue; }
				count++;
			}
			return count;		
		}

		internal static bool IsExploded(IImageViewer viewer)
		{
			if (Tools.ContainsKey(viewer))
				return Tools[viewer].Checked;

			return false;
		}
	}
}

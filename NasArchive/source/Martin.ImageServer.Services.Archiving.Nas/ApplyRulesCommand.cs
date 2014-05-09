#region License

// Copyright (c) 2009, ClearCanvas Inc.
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
using System.IO;
using System.Xml;
using ClearCanvas.Common;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Utilities.Xml;
using ClearCanvas.ImageServer.Common;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Rules;

namespace Martin.ImageServer.Services.Archiving.Nas
{
	/// <summary>
	/// <see cref="ServerCommand"/> for applying rules to a study after it has been restored.
	/// </summary>
	public class ApplyRulesCommand : ServerCommand
	{
		private readonly string _directory;
		private readonly string _studyInstanceUid;
		private readonly ServerActionContext _context;
		private readonly ServerRulesEngine _engine;
		public ApplyRulesCommand(string directory, string studyInstanceUid, ServerActionContext context) : base("Apply Server Rules", true)
		{
			_directory = directory;
			_studyInstanceUid = studyInstanceUid;
			_context = context;
			_engine = new ServerRulesEngine(ServerRuleApplyTimeEnum.StudyRestored, _context.ServerPartitionKey);
			_engine.Load();
		}

		/// <summary>
		/// Apply the rules.
		/// </summary>
		/// <remarks>
		/// When rules are applied, we are simply adding new <see cref="ServerDatabaseCommand"/> instances
		/// for the rules to the currently executing <see cref="ServerCommandProcessor"/>.  They will be
		/// executed after all other rules have been executed.
		/// </remarks>
		protected override void OnExecute(ServerCommandProcessor theProcessor)
		{
			string studyXmlFile = Path.Combine(_directory, String.Format("{0}.xml", _studyInstanceUid));
			StudyXml theXml = new StudyXml(_studyInstanceUid);

			if (File.Exists(studyXmlFile))
			{
				using (Stream fileStream = FileStreamOpener.OpenForRead(studyXmlFile, FileMode.Open))
				{
					XmlDocument theDoc = new XmlDocument();

					StudyXmlIo.Read(theDoc, fileStream);

					theXml.SetMemento(theDoc);

					fileStream.Close();
				}
			}
			else
			{
				string errorMsg = String.Format("Unable to load study XML file of restored study: {0}", studyXmlFile);

				Platform.Log(LogLevel.Error, errorMsg);
				throw new ApplicationException(errorMsg);
			}

			DicomFile defaultFile = null;
			bool rulesExecuted = false;
			foreach (SeriesXml seriesXml in theXml)
			{
				foreach (InstanceXml instanceXml in seriesXml)
				{
					// Skip non-image objects
					if (instanceXml.SopClass.Equals(SopClass.KeyObjectSelectionDocumentStorage)
					    || instanceXml.SopClass.Equals(SopClass.GrayscaleSoftcopyPresentationStateStorageSopClass)
					    || instanceXml.SopClass.Equals(SopClass.BlendingSoftcopyPresentationStateStorageSopClass)
					    || instanceXml.SopClass.Equals(SopClass.ColorSoftcopyPresentationStateStorageSopClass))
					{
						// Save the first one encountered, just in case the whole study is non-image objects.
						if (defaultFile == null)
							defaultFile = new DicomFile("test", new DicomAttributeCollection(), instanceXml.Collection);
						continue;
					}

					DicomFile file = new DicomFile("test", new DicomAttributeCollection(), instanceXml.Collection);
					_context.Message = file;
					_engine.Execute(_context);
					rulesExecuted = true;
					break;
				}
				if (rulesExecuted) break;
			}

			if (!rulesExecuted && defaultFile != null)
			{
				_context.Message = defaultFile;
				_engine.Execute(_context);
			}
		}

		/// <summary>
		/// Nothing to undo.  We just inserted into the command processing new DB updates, which will
		/// be rolledback on their own.
		/// </summary>
		protected override void OnUndo()
		{
			
		}
	}
}

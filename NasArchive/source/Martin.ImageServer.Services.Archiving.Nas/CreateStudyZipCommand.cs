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
using ClearCanvas.Dicom.Utilities.Xml;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using Ionic.Zip;

namespace Martin.ImageServer.Services.Archiving.Nas
{
	/// <summary>
	/// <see cref="ServerCommand"/> to create Zip file containing all the dcm files in a study
	/// </summary>
	public class CreateStudyZipCommand : ServerCommand
	{
		private readonly string _zipFile;
		private readonly StudyXml _studyXml;
		private readonly string _studyFolder;
		private readonly string _tempFolder;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="zipFile">The path of the zip file to create</param>
		/// <param name="studyXml">The <see cref="StudyXml"/> file describing the contents of the study.</param>
		/// <param name="studyFolder">The folder the study is stored in.</param>
		/// <param name="tempFolder">The folder for tempory files when creating the zip file.</param>
		public CreateStudyZipCommand(string zipFile, StudyXml studyXml, string studyFolder, string tempFolder) : base("Create study zip file",true)
		{
			_zipFile = zipFile;
			_studyXml = studyXml;
			_studyFolder = studyFolder;
			_tempFolder = tempFolder;
		}

		/// <summary>
		/// Do the work.
		/// </summary>
		protected override void OnExecute(ServerCommandProcessor theProcessor)
		{
			using (ZipFile zip = new ZipFile(_zipFile))
			{
				zip.ForceNoCompression = !NasSettings.Default.CompressZipFiles;
				zip.TempFileFolder = _tempFolder;
				zip.Comment = String.Format("Archive for study {0}", _studyXml.StudyInstanceUid);
				zip.UseZip64WhenSaving = Zip64Option.AsNecessary;

				// Add the studyXml file
				zip.AddFile(Path.Combine(_studyFolder,String.Format("{0}.xml",_studyXml.StudyInstanceUid)), String.Empty);

				// Add the studyXml.gz file
				zip.AddFile(Path.Combine(_studyFolder, String.Format("{0}.xml.gz", _studyXml.StudyInstanceUid)), String.Empty);

			    string uidMapXmlPath = Path.Combine(_studyFolder, "UidMap.xml");
                if (File.Exists(uidMapXmlPath))
                    zip.AddFile(uidMapXmlPath, String.Empty);

				// Add each sop from the StudyXmlFile
				foreach (SeriesXml seriesXml in _studyXml)
					foreach (InstanceXml instanceXml in seriesXml)
					{
						string filename = Path.Combine(_studyFolder, seriesXml.SeriesInstanceUid);
						filename = Path.Combine(filename, String.Format("{0}.dcm", instanceXml.SopInstanceUid));

						zip.AddFile(filename, seriesXml.SeriesInstanceUid);
					}

				zip.Save();
			}
		}

		/// <summary>
		/// Undo the work.
		/// </summary>
		protected override void OnUndo()
		{
			if (File.Exists(_zipFile))
				File.Delete(_zipFile);
		}
	}
}

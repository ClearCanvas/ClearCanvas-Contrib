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
using ClearCanvas.ImageServer.Common.CommandProcessor;
using Ionic.Zip;

namespace Martin.ImageServer.Services.Archiving.Nas
{
	/// <summary>
	/// Class for extracting a file from a zip over the top of another file, and preserving
	/// the old file for restoring on failure.
	/// </summary>
	public class ExtractZipFileAndReplaceCommand : ServerCommand, IDisposable
	{
		private readonly string _zipFile;
		private readonly string _destinationFolder;
		private readonly string _sourceFile;
		private bool _fileBackedup = false;
		private string _storageFile = String.Empty;
		private string _backupFile = String.Empty;

		public ExtractZipFileAndReplaceCommand(string zipFile, string sourceFile, string destinationFolder)
			: base("Extract file from Zip and replace existing file", true)
		{
			_zipFile = zipFile;
			_destinationFolder = destinationFolder;
			_sourceFile = sourceFile;
		}

		protected override void OnExecute(ServerCommandProcessor theProcessor)
		{
			_storageFile = Path.Combine(_destinationFolder, _sourceFile);
			_backupFile = Path.Combine(ExecutionContext.TempDirectory, _sourceFile);


			string baseDirectory = _backupFile.Substring(0, _backupFile.LastIndexOfAny(new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar }));
			if (!Directory.Exists(baseDirectory))
				Directory.CreateDirectory(baseDirectory);

			if (File.Exists(_storageFile))
			{
				File.Move(_storageFile, _backupFile);
				_fileBackedup = true;
			}
			using (ZipFile zip = new ZipFile(_zipFile))
			{
				zip.Extract(_sourceFile, _destinationFolder, true);
			}
		}

		protected override void OnUndo()
		{
			if (_fileBackedup)
			{
				if (File.Exists(_storageFile))
					File.Delete(_storageFile);
				File.Move(_backupFile, _storageFile);
				_fileBackedup = false;
			}
		}

		public void Dispose()
		{
			if (File.Exists(_backupFile))
				File.Delete(_backupFile);
		}
	}
}

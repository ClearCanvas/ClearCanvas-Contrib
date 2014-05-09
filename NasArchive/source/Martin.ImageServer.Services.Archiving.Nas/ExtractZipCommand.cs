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

using System.IO;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using Ionic.Zip;

namespace Martin.ImageServer.Services.Archiving.Nas
{
	/// <summary>
	/// <see cref="ServerCommand"/> for extracting a zip file containing study files to a specific directory.
	/// </summary>
	public class ExtractZipCommand : ServerCommand
	{
		private readonly string _zipFile;
		private readonly string _destinationFolder;
		private readonly bool _overwrite;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="zip">The zip file to extract.</param>
		/// <param name="destinationFolder">The destination folder.</param>
		public ExtractZipCommand(string zip, string destinationFolder): base("Extract Zip File",true)
		{
			_zipFile = zip;
			_destinationFolder = destinationFolder;
			_overwrite = false;
		}

		/// <summary>
		/// Do the unzip.
		/// </summary>
		protected override void OnExecute(ServerCommandProcessor theProcessor)
		{
			using (ZipFile zip = new ZipFile(_zipFile))
			{
				zip.ExtractAll(_destinationFolder,_overwrite);
			}
		}

		/// <summary>
		/// Undo.  Remove the destination folder.
		/// </summary>
		protected override void OnUndo()
		{
			Directory.Delete(_destinationFolder, true);
		}
	}
}

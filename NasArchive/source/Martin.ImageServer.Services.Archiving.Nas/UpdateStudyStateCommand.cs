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

using ClearCanvas.Enterprise.Core;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Model.EntityBrokers;

namespace Martin.ImageServer.Services.Archiving.Nas
{
	public class UpdateStudyStateCommand : ServerDatabaseCommand
	{
		private readonly StudyStatusEnum _newStatus;
		private readonly ServerTransferSyntax _newSyntax;
		private readonly StudyStorageLocation _location;

		public UpdateStudyStateCommand(StudyStorageLocation location, StudyStatusEnum status, ServerTransferSyntax newSyntax) : base("Update the StudyState", true)
		{
			_location = location;
			_newStatus = status;
			_newSyntax = newSyntax;
		}

		protected override void OnExecute(ServerCommandProcessor theProcessor, IUpdateContext updateContext)
		{
			// Update StudyStatusEnum in the StudyStorageTable
			IStudyStorageEntityBroker studyStorageUpdate = updateContext.GetBroker<IStudyStorageEntityBroker>();
			StudyStorageUpdateColumns studyStorageUpdateColumns = new StudyStorageUpdateColumns();
			studyStorageUpdateColumns.StudyStatusEnum = _newStatus;
			studyStorageUpdate.Update(_location.Key, studyStorageUpdateColumns);

			// Update ServerTransferSyntaxGUID in FilesystemStudyStorage
			IFilesystemStudyStorageEntityBroker filesystemUpdate = updateContext.GetBroker<IFilesystemStudyStorageEntityBroker>();
			FilesystemStudyStorageUpdateColumns filesystemUpdateColumns = new FilesystemStudyStorageUpdateColumns();
			filesystemUpdateColumns.ServerTransferSyntaxKey = _newSyntax.Key;
			filesystemUpdate.Update(_location.FilesystemStudyStorageKey, filesystemUpdateColumns);
		}
	}
}

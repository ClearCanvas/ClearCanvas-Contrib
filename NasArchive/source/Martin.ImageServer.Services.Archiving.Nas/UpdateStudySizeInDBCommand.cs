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
using ClearCanvas.Dicom.Utilities.Xml;
using ClearCanvas.Enterprise.Core;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Model.EntityBrokers;

namespace Martin.ImageServer.Services.Archiving.Nas
{
    /// <summary>
    /// Command to update the Study Size in the database.
    /// </summary>
    public class UpdateStudySizeInDBCommand : ServerDatabaseCommand
    {
        #region Private Members
        const decimal KB = 1024;
        private readonly StudyStorageLocation _location;
        private readonly decimal _studySizeInKB; 
        #endregion

        #region Constructors
        public UpdateStudySizeInDBCommand(StudyStorageLocation location)
            : base("Update Study Size In DB", true)
        {
            _location = location;

            // this may take a few ms so it's better to do it here instead in OnExecute()
            StudyXml studyXml = _location.LoadStudyXml();
            _studySizeInKB = studyXml.GetStudySize() / KB;
        } 
        #endregion

        protected override void OnExecute(ServerCommandProcessor theProcessor, IUpdateContext updateContext)
        {
            Study study = _location.Study ?? Study.Find(updateContext, _location.Key);

            if (study.StudySizeInKB != _studySizeInKB)
            {
                IStudyEntityBroker broker = updateContext.GetBroker<IStudyEntityBroker>();
                StudyUpdateColumns parameters = new StudyUpdateColumns()
                                                    {
                                                        StudySizeInKB = _studySizeInKB
                                                    };
                if (!broker.Update(study.Key, parameters))
                    throw new ApplicationException("Unable to update study size in the database");
            }
            
        }
    }
}
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
using ClearCanvas.Common;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using ClearCanvas.ImageServer.Model;

namespace Martin.ImageServer.Services.Archiving.Nas
{
    /// <summary>
    /// Represents the execution context of a <cref="RestoreQueue"/> item.
    /// </summary>
    public class RestoreProcessorContext : ExecutionContext
    {
        #region Private Fields
        private readonly RestoreQueue _item;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of <see cref="RestoreProcessorContext"/>
        /// </summary>
        /// <param name="item"></param>
        public RestoreProcessorContext(RestoreQueue item)
            :base(item.GetKey().Key.ToString())
        {
            Platform.CheckForNullReference(item, "item");
            _item = item;
        }
        #endregion

        #region Protected Methods
        protected override string GetTemporaryPath()
        {
            StudyStorageLocation storage = StudyStorageLocation.FindStorageLocations(StudyStorage.Load(_item.StudyStorageKey))[0];
            if (storage == null)
                return base.TempDirectory;
            else
            {
                String basePath = GetTempPathRoot();

                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = Path.Combine(storage.FilesystemPath, "temp");
                }
                String tempDirectory = Path.Combine(basePath, String.Format("RestoreQueue-{0}", _item.GetKey()));

                for (int i = 2; i < 1000; i++)
                {
                    if (!Directory.Exists(tempDirectory))
                    {
                        break;
                    }

                    tempDirectory = Path.Combine(basePath, String.Format("RestoreQueue-{0}({1})", _item.GetKey(), i));
                }

                if (!Directory.Exists(tempDirectory))
                    Directory.CreateDirectory(tempDirectory);

                return tempDirectory;
            }
        }

        #endregion
    }
}
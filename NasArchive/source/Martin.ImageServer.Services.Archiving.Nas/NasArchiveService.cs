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
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageServer.Common;
using ClearCanvas.ImageServer.Model;

namespace Martin.ImageServer.Services.Archiving.Nas
{
	/// <summary>
	/// Service thread for handling archivals.
	/// </summary>
	public class NasArchiveService : ThreadedService
	{
		private readonly NasArchive _nasArchive;
		private readonly ItemProcessingThreadPool<ArchiveQueue> _threadPool;

		public NasArchiveService(string name, NasArchive nasArchive) : base(name)
		{
			_nasArchive = nasArchive;
			_threadPool = new ItemProcessingThreadPool<ArchiveQueue>(NasSettings.Default.ArchiveThreadCount);
			_threadPool.ThreadPoolName = "NasArchive Pool";
		}

		protected override void Initialize()
		{
			_nasArchive.ResetFailedArchiveQueueItems();

			// Start the thread pool
			if (!_threadPool.Active)
				_threadPool.Start();
		}

		/// <summary>
		/// Execute the service.
		/// </summary>
		protected override void Run()
		{
			while (true)
			{
				if ((_threadPool.QueueCount + _threadPool.ActiveCount) < _threadPool.Concurrency)
				{
					try
					{
						ArchiveQueue queueItem = _nasArchive.GetArchiveCandidate();

						if (queueItem != null)
						{
							NasStudyArchive archiver = new NasStudyArchive(_nasArchive);
							_threadPool.Enqueue(queueItem, archiver.Run);
						}
						else if (CheckStop(NasSettings.Default.PollDelayMilliseconds))
						{
							Platform.Log(LogLevel.Info, "Shutting down {0} archiving service.", _nasArchive.PartitionArchive.Description);
							return;
						}
					}
					catch (Exception e)
					{
						Platform.Log(LogLevel.Error,e,"Unexpected exception when querying for archive candidates, rescheduling.");
						if (CheckStop(NasSettings.Default.PollDelayMilliseconds))
						{
							Platform.Log(LogLevel.Info, "Shutting down {0} archiving service.", _nasArchive.PartitionArchive.Description);
							return;
						}
					}
				}
				else
				{
					if (CheckStop(NasSettings.Default.PollDelayMilliseconds))
					{
						Platform.Log(LogLevel.Info, "Shutting down {0} archiving service.", _nasArchive.PartitionArchive.Description);
						return;
					}
				}
			}
		}

		/// <summary>
		/// Stop the service.
		/// </summary>
		protected override void Stop()
		{
			_threadPool.Stop(true);
		}
	}
}

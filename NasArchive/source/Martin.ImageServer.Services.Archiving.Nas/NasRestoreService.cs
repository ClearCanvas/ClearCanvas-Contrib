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
	/// Service thread for handling restore requests for <see cref="NasArchive"/>s.
	/// </summary>
	public class NasRestoreService : ThreadedService
	{
		private readonly NasArchive _nasArchive;
		private readonly ItemProcessingThreadPool<RestoreQueue> _threadPool;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name">The name of the service.</param>
		/// <param name="nasArchive">The <see cref="NasArchive"/> for which to do restores. </param>
		public NasRestoreService(string name, NasArchive nasArchive)
			: base(name)
		{
			_nasArchive = nasArchive;

			_threadPool = new ItemProcessingThreadPool<RestoreQueue>(NasSettings.Default.RestoreThreadCount);
			_threadPool.ThreadPoolName = "NasRestore Pool";
		}

		/// <summary>
		/// Initialize the service.
		/// </summary>
		protected override void Initialize()
		{
			_nasArchive.ResetFailedRestoreQueueItems();

			// Start the thread pool
			if (!_threadPool.Active)
				_threadPool.Start();
		}

		/// <summary>
		/// Run the service.
		/// </summary>
		protected override void Run()
		{

			while (true)
			{
				if ((_threadPool.QueueCount + _threadPool.ActiveCount) < _threadPool.Concurrency)
				{
					try
					{
						RestoreQueue queueItem = _nasArchive.GetRestoreCandidate();

						if (queueItem != null)
						{
							NasStudyRestore archiver = new NasStudyRestore(_nasArchive);
							_threadPool.Enqueue(queueItem, archiver.Run);
						}
						else if (CheckStop(NasSettings.Default.PollDelayMilliseconds))
						{
							Platform.Log(LogLevel.Info, "Shutting down {0} restore service.", _nasArchive.PartitionArchive.Description);
							return;
						}
					}
					catch (Exception e)
					{
						Platform.Log(LogLevel.Error, e, "Unexpected exception when querying for restore candidates.  Rescheduling.");
						if (CheckStop(NasSettings.Default.PollDelayMilliseconds))
						{
							Platform.Log(LogLevel.Info, "Shutting down {0} restore service.", _nasArchive.PartitionArchive.Description);
							return;
						}
					}
				}
				else
				{
					if (CheckStop(NasSettings.Default.PollDelayMilliseconds))
					{
						Platform.Log(LogLevel.Info, "Shutting down {0} restore service.", _nasArchive.PartitionArchive.Description);
						return;
					}
				}
			}
		}

		/// <summary>
		/// Stop the service thread.
		/// </summary>
		protected override void Stop()
		{
			_threadPool.Stop(true);
		}
	}
}

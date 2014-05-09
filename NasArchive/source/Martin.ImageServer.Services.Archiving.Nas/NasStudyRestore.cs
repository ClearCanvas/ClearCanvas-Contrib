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
using System.Diagnostics;
using System.IO;
using System.Xml;
using ClearCanvas.Common;
using ClearCanvas.Dicom;
using ClearCanvas.Enterprise.Core;
using ClearCanvas.ImageServer.Common;
using ClearCanvas.ImageServer.Common.CommandProcessor;
using ClearCanvas.ImageServer.Core.Process;
using ClearCanvas.ImageServer.Core.Rebuild;
using ClearCanvas.ImageServer.Core.Validation;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Model.Brokers;
using ClearCanvas.ImageServer.Model.Parameters;
using ClearCanvas.ImageServer.Rules;
using Ionic.Zip;
using ClearCanvas.ImageServer.Services.Archiving;

namespace Martin.ImageServer.Services.Archiving.Nas
{
    /// <summary>
	/// Helper class for restoring a study from an <see cref="NasArchive"/>
	/// </summary>
	public class NasStudyRestore
	{
		#region Private Members
		private readonly NasArchive _nasArchive;
		private ArchiveStudyStorage _archiveStudyStorage;
		private StudyStorageLocation _location;
		private TransferSyntax _syntax;
		private ServerTransferSyntax _serverSyntax;
		private StudyStorage _studyStorage;

        #endregion

		#region Constructors
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="nasArchive">The NasArchive to work with.</param>
		public NasStudyRestore(NasArchive nasArchive)
		{
			_nasArchive = nasArchive;
		}
		#endregion

		/// <summary>
		/// Do the restore.
		/// </summary>
		/// <param name="queueItem">The queue item to restore.</param>
		public void Run(RestoreQueue queueItem)
		{
            using (RestoreProcessorContext context = new RestoreProcessorContext(queueItem))
            {
                try
                {
                    // Load up related classes.
                    using (IReadContext readContext = _nasArchive.PersistentStore.OpenReadContext())
                    {
                        _archiveStudyStorage = ArchiveStudyStorage.Load(readContext, queueItem.ArchiveStudyStorageKey);
                        _serverSyntax = ServerTransferSyntax.Load(readContext, _archiveStudyStorage.ServerTransferSyntaxKey);
                        _syntax = TransferSyntax.GetTransferSyntax(_serverSyntax.Uid);

                        StudyStorageLocationQueryParameters parms = new StudyStorageLocationQueryParameters
                                                                    	{StudyStorageKey = queueItem.StudyStorageKey};
                    	IQueryStudyStorageLocation broker = readContext.GetBroker<IQueryStudyStorageLocation>();
                        _location = broker.FindOne(parms);
                        if (_location == null)
                        {
                            _studyStorage = StudyStorage.Load(readContext, queueItem.StudyStorageKey);
							if (_studyStorage==null)
							{
								DateTime scheduleTime = Platform.Time.AddMinutes(5);
								Platform.Log(LogLevel.Error, "Unable to find storage location, rescheduling restore request to {0}",
											 scheduleTime);
								queueItem.FailureDescription = "Unable to find storage location, rescheduling request.";
								_nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Pending, scheduleTime);
								return;
							}
                        }
                    }

					if (_location == null)
						Platform.Log(LogLevel.Info, "Starting restore of nearline study: {0}", _studyStorage.StudyInstanceUid);
					else
                        Platform.Log(LogLevel.Info, "Starting restore of online study: {0}", _location.StudyInstanceUid);

                    // If restoring a Nearline study, select a filesystem
                    string destinationFolder;
                    if (_location == null)
                    {
                        ServerFilesystemInfo fs = _nasArchive.Selector.SelectFilesystem();
                        if (fs == null)
                        {
                            DateTime scheduleTime = Platform.Time.AddMinutes(5);
                            Platform.Log(LogLevel.Error, "No writeable filesystem for restore, rescheduling restore request to {0}",
                                         scheduleTime);
                            queueItem.FailureDescription = "No writeable filesystem for restore, rescheduling request.";
                            _nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Pending, scheduleTime);
                            return;
                        }
                        destinationFolder = Path.Combine(fs.Filesystem.FilesystemPath, _nasArchive.ServerPartition.PartitionFolder);
                    }
                    else
                        destinationFolder = _location.GetStudyPath();


                    // Get the zip file path from the xml data in the ArchiveStudyStorage entry
                    // Also store the "StudyFolder" for use below
                    string studyFolder = String.Empty;
                    string filename = String.Empty;
                    string studyInstanceUid = String.Empty;
                    XmlElement element = _archiveStudyStorage.ArchiveXml.DocumentElement;
					if (element!=null)
						foreach (XmlElement node in element.ChildNodes)
							if (node.Name.Equals("StudyFolder"))
								studyFolder = node.InnerText;
							else if (node.Name.Equals("Filename"))
								filename = node.InnerText;
							else if (node.Name.Equals("Uid"))
								studyInstanceUid = node.InnerText;

                    string zipFile = Path.Combine(_nasArchive.NasPath, studyFolder);
                    zipFile = Path.Combine(zipFile, studyInstanceUid);
                    zipFile = Path.Combine(zipFile, filename);


                    // Do a test read of the zip file.  If it succeeds, the file is available, if it 
                    // fails, we just set back to pending and recheck.
                    try
                    {
                        FileStream stream = File.OpenRead(zipFile);
                        // Read a byte, just in case that makes a difference.
                        stream.ReadByte();
                        stream.Close();
                        stream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        DateTime scheduledTime = Platform.Time.AddSeconds(NasSettings.Default.ReadFailRescheduleDelaySeconds);
                        Platform.Log(LogLevel.Error, ex, "Archive {0} for Study  {1} is unreadable, rescheduling restore to {2}",
                                     zipFile, _studyStorage == null ? (_location == null ? string.Empty : _location.StudyInstanceUid) : _studyStorage.StudyInstanceUid,
                                     scheduledTime);
                        // Just reschedule in "Restoring" state, the file is unreadable.
                        _nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Restoring,
                                                       scheduledTime);
                        return;
                    }

                    if (_location == null)
                        RestoreNearlineStudy(queueItem, zipFile, destinationFolder, studyFolder);
                    else
                        RestoreOnlineStudy(queueItem, zipFile, destinationFolder);
                }
                catch (Exception e)
                {
                    Platform.Log(LogLevel.Error, e, "Unexpected exception processing restore request for {0} on archive {1}",
                        _studyStorage == null ? (_location == null ? string.Empty : _location.StudyInstanceUid) : _studyStorage.StudyInstanceUid,
                        _nasArchive.PartitionArchive.Description);
                    queueItem.FailureDescription = e.Message;
                    _nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Failed, Platform.Time);
                }
            }
			
		}

		public void RestoreNearlineStudy(RestoreQueue queueItem, string zipFile, string destinationFolder, string studyFolder)
		{
            ServerFilesystemInfo fs = _nasArchive.Selector.SelectFilesystem();
			if (fs == null)
			{
				DateTime scheduleTime = Platform.Time.AddMinutes(5);
				Platform.Log(LogLevel.Error, "No writeable filesystem for restore, rescheduling restore request to {0}", scheduleTime);
				queueItem.FailureDescription = "No writeable filesystem for restore, rescheduling restore request";
				_nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Pending, scheduleTime);
				return;
			}

		    StudyStorageLocation restoredLocation = null;
			try
			{
				using (ServerCommandProcessor processor = 
                    new ServerCommandProcessor("NAS Restore Offline Study"))
				{
					processor.AddCommand(new CreateDirectoryCommand(destinationFolder));
					destinationFolder = Path.Combine(destinationFolder, studyFolder);
					processor.AddCommand(new CreateDirectoryCommand(destinationFolder));
					destinationFolder = Path.Combine(destinationFolder, _studyStorage.StudyInstanceUid);
					processor.AddCommand(new CreateDirectoryCommand(destinationFolder));
					processor.AddCommand(new ExtractZipCommand(zipFile, destinationFolder));

					// We rebuild the StudyXml, in case any settings or issues have happened since archival
					processor.AddCommand(new RebuildStudyXmlCommand(_studyStorage.StudyInstanceUid, destinationFolder));

                    // Apply the rules engine.
					ServerActionContext context =
						new ServerActionContext(null, fs.Filesystem.GetKey(), _nasArchive.ServerPartition,
						                        queueItem.StudyStorageKey) {CommandProcessor = processor};
					processor.AddCommand(
						new ApplyRulesCommand(destinationFolder, _studyStorage.StudyInstanceUid, context));

					// Do the actual insert into the DB
					InsertFilesystemStudyStorageCommand insertStorageCommand = new InsertFilesystemStudyStorageCommand(
													_nasArchive.PartitionArchive.ServerPartitionKey,
						                            _studyStorage.StudyInstanceUid,
						                            studyFolder,
						                            fs.Filesystem.GetKey(), _syntax);
					processor.AddCommand(insertStorageCommand);

					if (!processor.Execute())
					{
						Platform.Log(LogLevel.Error, "Unexpected error processing restore request for {0} on archive {1}",
						             _studyStorage.StudyInstanceUid, _nasArchive.PartitionArchive.Description);
						queueItem.FailureDescription = processor.FailureReason;
						_nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Failed, Platform.Time);
					}
					else
					{
					    restoredLocation = insertStorageCommand.Location;

						// Unlock the Queue Entry
						using (
							IUpdateContext update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
						{
							bool retVal = _nasArchive.UpdateRestoreQueue(update, queueItem, RestoreQueueStatusEnum.Completed, Platform.Time.AddSeconds(60));
							ILockStudy studyLock = update.GetBroker<ILockStudy>();
							LockStudyParameters parms = new LockStudyParameters
							                            	{
							                            		StudyStorageKey = queueItem.StudyStorageKey,
							                            		QueueStudyStateEnum = QueueStudyStateEnum.Idle
							                            	};
							retVal = retVal && studyLock.Execute(parms);
							if (!parms.Successful || !retVal)
							{
								string message =
									String.Format("Study {0} on partition {1} failed to unlock.", _studyStorage.StudyInstanceUid,
									              _nasArchive.ServerPartition.Description);
								Platform.Log(LogLevel.Info, message);
								throw new ApplicationException(message);
							}
							update.Commit();

							Platform.Log(LogLevel.Info, "Successfully restored study: {0} on archive {1}", _studyStorage.StudyInstanceUid,
										 _nasArchive.PartitionArchive.Description);

                            OnStudyRestored(restoredLocation);
						}
					}
				}
			}
            catch(StudyIntegrityValidationFailure ex)
            {
                Debug.Assert(restoredLocation != null);
                // study has been restored but it seems corrupted. Need to reprocess it.
                ReprocessStudy(restoredLocation, ex.Message);
            }
			catch (Exception e)
			{
				Platform.Log(LogLevel.Error, e, "Unexpected exception processing restore request for {0} on archive {1}",
							 _studyStorage.StudyInstanceUid, _nasArchive.PartitionArchive.Description);
				_nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Failed, Platform.Time);
			}
		}

        private void ReprocessStudy(StudyStorageLocation restoredLocation, string reason)
        {
            StudyReprocessor reprocessor = new StudyReprocessor();
            String reprocessReason = String.Format("Restore Validation Error: {0}", reason);
            reprocessor.ReprocessStudy(reprocessReason, restoredLocation, Platform.Time);
            string message = string.Format("Study {0} has been restored but failed the validation. Reprocess Study has been triggerred. Reason for validation failure: {1}", restoredLocation.StudyInstanceUid, reason);
            Platform.Log(LogLevel.Warn, message);

            ServerPlatform.Alert(AlertCategory.Application, AlertLevel.Informational, "Restore", 0, null, TimeSpan.Zero, message);
        }

        private static void OnStudyRestored(StudyStorageLocation location)
        {
            ValidateStudy(location);

            using(ServerCommandProcessor processor = new ServerCommandProcessor("Update Study Size In DB"))
            {
                processor.AddCommand(new UpdateStudySizeInDBCommand(location));
                if (!processor.Execute())
                {
                    Platform.Log(LogLevel.Error, "Unexpected error when trying to update the study size in DB:", processor.FailureReason);
                }
            }
        }

        private static void ValidateStudy(StudyStorageLocation location)
        {
            StudyStorageValidator validator = new StudyStorageValidator();
            validator.Validate(location, ValidationLevels.Study | ValidationLevels.Series);
        }

        private void RestoreOnlineStudy(RestoreQueue queueItem, string zipFile, string destinationFolder)
		{
			try
			{
				using (ServerCommandProcessor processor = new ServerCommandProcessor("NAS Restore Online Study"))
				{
					using (ZipFile zip = new ZipFile(zipFile))
					{
						foreach (string file in zip.EntryFileNames)
						{
							processor.AddCommand(new ExtractZipFileAndReplaceCommand(zipFile, file, destinationFolder));
						}
					}

					// We rebuild the StudyXml, in case any settings or issues have happened since archival
					processor.AddCommand(new RebuildStudyXmlCommand(_location.StudyInstanceUid, destinationFolder));

					StudyStatusEnum status;

					if (_syntax.Encapsulated && _syntax.LosslessCompressed)
						status = StudyStatusEnum.OnlineLossless;
					else if (_syntax.Encapsulated && _syntax.LossyCompressed)
						status = StudyStatusEnum.OnlineLossy;
					else
						status = StudyStatusEnum.Online;

					processor.AddCommand(new UpdateStudyStateCommand(_location, status, _serverSyntax));

					// Apply the rules engine.
					ServerActionContext context =
						new ServerActionContext(null, _location.FilesystemKey, _nasArchive.ServerPartition,
												queueItem.StudyStorageKey) {CommandProcessor = processor};
					processor.AddCommand(
						new ApplyRulesCommand(destinationFolder, _location.StudyInstanceUid, context));

					if (!processor.Execute())
					{
						Platform.Log(LogLevel.Error, "Unexpected error processing restore request for {0} on archive {1}",
									 _location.StudyInstanceUid, _nasArchive.PartitionArchive.Description);
						queueItem.FailureDescription = processor.FailureReason;
						_nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Failed, Platform.Time);
					}
					else
					{
						// Unlock the Queue Entry and set to complete
						using (IUpdateContext update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
						{
							_nasArchive.UpdateRestoreQueue(update, queueItem, RestoreQueueStatusEnum.Completed, Platform.Time.AddSeconds(60));
							ILockStudy studyLock = update.GetBroker<ILockStudy>();
							LockStudyParameters parms = new LockStudyParameters
							                            	{
							                            		StudyStorageKey = queueItem.StudyStorageKey,
							                            		QueueStudyStateEnum = QueueStudyStateEnum.Idle
							                            	};
							bool retVal = studyLock.Execute(parms);
							if (!parms.Successful || !retVal)
							{
								Platform.Log(LogLevel.Info, "Study {0} on partition {1} failed to unlock.", _location.StudyInstanceUid,
											 _nasArchive.ServerPartition.Description);
							}

							update.Commit();

							Platform.Log(LogLevel.Info, "Successfully restored study: {0} on archive {1}", _location.StudyInstanceUid,
										 _nasArchive.PartitionArchive.Description);

                            OnStudyRestored(_location);
						}
					}
				}
			}
			catch (Exception e)
			{
				Platform.Log(LogLevel.Error, e, "Unexpected exception processing restore request for {0} on archive {1}",
							 _location.StudyInstanceUid, _nasArchive.PartitionArchive.Description);
				queueItem.FailureDescription = e.Message;
				_nasArchive.UpdateRestoreQueue(queueItem, RestoreQueueStatusEnum.Failed, Platform.Time);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using ClearCanvas.Common;
using ClearCanvas.Enterprise.Core;
using ClearCanvas.ImageServer.Model.EntityBrokers;
using ClearCanvas.ImageServer.Common;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Enterprise;

namespace Martin.ImageServer.Services.Archiving.Nas
{
    [ExtensionOf(typeof(ApplicationRootExtensionPoint))]
    class MoveArchiveQueueToActiveArchivePartition : IApplicationRoot
    {
        private readonly IPersistentStore _store = PersistentStoreRegistry.GetDefaultStore();

        public IPersistentStore PersistentStore
        {
            get { return _store; }
        }

        public void RunApplication(string[] args)
        {
            using (IUpdateContext updateContext = PersistentStore.OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                IPartitionArchiveEntityBroker partitionBroker = updateContext.GetBroker<IPartitionArchiveEntityBroker>();
                PartitionArchiveSelectCriteria partitionCriteria = new PartitionArchiveSelectCriteria();

                partitionCriteria.Enabled.EqualTo(true);
                partitionCriteria.ReadOnly.EqualTo(false);
                PartitionArchive activePartition = partitionBroker.FindOne(partitionCriteria);
                if (activePartition == null)
                {
                    Platform.Log(LogLevel.Error, "No active ArchivePartition were found.");
                    return;
                }

                partitionCriteria.ReadOnly.EqualTo(true);
                IList<ServerEntityKey> partitionKeys = new List<ServerEntityKey>();
                foreach (PartitionArchive partition in partitionBroker.Find(partitionCriteria))
                {
                    partitionKeys.Add(partition.Key);
                }

                IArchiveQueueEntityBroker queueBroker = updateContext.GetBroker<IArchiveQueueEntityBroker>();
                ArchiveQueueSelectCriteria queueCriteria = new ArchiveQueueSelectCriteria();
                queueCriteria.ArchiveQueueStatusEnum.In(new ArchiveQueueStatusEnum[] { ArchiveQueueStatusEnum.Failed, ArchiveQueueStatusEnum.Pending });
                queueCriteria.PartitionArchiveKey.In(partitionKeys);

                ArchiveQueueUpdateColumns queueColumns = new ArchiveQueueUpdateColumns()
                {
                    PartitionArchiveKey = activePartition.Key,
                    ArchiveQueueStatusEnum = ArchiveQueueStatusEnum.Pending,
                    ProcessorId = "",
                    ScheduledTime = Platform.Time
                };

                if (queueBroker.Update(queueCriteria, queueColumns))
                {
                    updateContext.Commit();
                }
            }
        }
    }
}

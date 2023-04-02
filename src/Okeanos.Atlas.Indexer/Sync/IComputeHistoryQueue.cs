namespace Okeanos.Atlas.Indexer.Sync;

public interface IComputeHistoryQueue
{
   bool IsQueueEmpty();
   void AddAddressToComputeHistoryQueue(string address);
   bool GetNextItemFromQueue(out string address);
}

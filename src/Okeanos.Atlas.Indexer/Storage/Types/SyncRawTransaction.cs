namespace Okeanos.Atlas.Indexer.Storage.Types
{
   public class SyncRawTransaction
   {
      public byte[] RawTransaction { get; set; }

      public string TransactionHash { get; set; }
   }
}

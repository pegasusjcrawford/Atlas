namespace Okeanos.Atlas.Indexer.Sync.SyncTasks
{
   public interface IBlockableItem
   {
      bool Blocked { get; set; }

      void Deplete();
   }
}

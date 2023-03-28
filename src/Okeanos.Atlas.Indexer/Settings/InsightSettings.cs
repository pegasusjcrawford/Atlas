using System.Collections.Generic;
using Okeanos.Atlas.Indexer.Models;

namespace Okeanos.Atlas.Indexer.Settings
{
   public class InsightSettings
   {
      public InsightSettings()
      {
         Wallets = new List<Wallet>();
      }

      public List<Wallet> Wallets { get; set; }

      public List<RewardModel> Rewards { get; set; }
   }
}

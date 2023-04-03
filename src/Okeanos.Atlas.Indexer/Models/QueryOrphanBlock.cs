using System;
using System.Collections.Generic;
using Okeanos.Atlas.Indexer.Storage.Mongo.Types;

namespace Okeanos.Atlas.Indexer.Models
{
   public class QueryOrphanBlock
   {
      public DateTime Created { get; set; }
      public uint BlockIndex { get; set; }
      public string BlockHash { get; set; }
      public BlockTable Block { get; set; }
   }
}

﻿using Newtonsoft.Json;

namespace Okeanos.Atlas.Indexer.Client.Types
{
   #region Using Directives

   #endregion

   public class SignRawTransactionInput
   {
      #region Public Properties

      [JsonProperty("vout")]
      public int Output { get; set; }

      [JsonProperty("scriptPubKey")]
      public string ScriptPubKey { get; set; }

      [JsonProperty("txid")]
      public string TransactionId { get; set; }

      #endregion
   }
}

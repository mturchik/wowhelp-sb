using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WowHelp.Shared.BattleNet.Models.Response
{
    public class CrIndexResponse
    {
        [JsonProperty("connected_realms")]
        public List<Url> ConnectedRealmUrls { get; set; } = new List<Url>();
    }
}
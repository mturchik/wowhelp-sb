using Newtonsoft.Json;
using System.Collections.Generic;

namespace WowHelp.Shared.BattleNet.Models
{
    public class ConnectedRealm
    {
        public int Id { get; set; }

        [JsonProperty("has_queue")]
        public bool HasQueue { get; set; }

        public List<Realm> Realms { get; set; } = new List<Realm>();

        [JsonProperty("auctions")]
        public Url AuctionsUrl { get; set; }
    }
}
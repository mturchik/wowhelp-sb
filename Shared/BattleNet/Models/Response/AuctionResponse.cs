using Newtonsoft.Json;
using System.Collections.Generic;

namespace WowHelp.Shared.BattleNet.Models.Response
{
    public class AuctionResponse
    {
        [JsonProperty("connected_realm")]
        public Url ConnectedRealmUrl { get; set; }

        public List<Auction> Auctions { get; set; } = new List<Auction>();
    }
}
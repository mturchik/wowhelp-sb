using Newtonsoft.Json;
using WowHelp.Shared.BattleNet.Models.Response;

namespace WowHelp.Shared.BattleNet.Models
{
    public class Realm
    {
        public int Id { get; set; }

        [JsonProperty("connected_realm")]
        public Url ConnectedRealmUrl { get; set; }

        public string Name { get; set; }

        public string Locale { get; set; }

        public string Timezone { get; set; }

        public string Slug { get; set; }
    }
}
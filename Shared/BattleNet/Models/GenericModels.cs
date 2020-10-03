using Newtonsoft.Json;

namespace WowHelp.Shared.BattleNet.Models
{
    public class TypeNamePair
    {
        public string Type { get; set; }
        public LocaleArray Name { get; set; }
    }

    public class LocaleArray
    {
        [JsonProperty("en_US")]
        public string US { get; set; }
    }

    public class Url
    {
        public string Href { get; set; }
    }

    public readonly struct StaticRequestUrl
    {
        public static string AuthUrl => 
            "https://us.battle.net/oauth/token";

        public static string CrIndexUrl => 
            "https://us.api.blizzard.com/data/wow/connected-realm/index?namespace=dynamic-us&locale=en_US";
    }
}

using Newtonsoft.Json;

namespace WowHelp.Shared.BattleNet.Models
{
    public class TypeNamePair
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Url
    {
        public string Href { get; set; }
    }

    public readonly struct StaticRequestUrl
    {
        public static string Auth => 
            "https://us.battle.net/oauth/token";

        public static string CrIndex => 
            "https://us.api.blizzard.com/data/wow/connected-realm/index?namespace=dynamic-us&locale=en_US";

        public static string RecipeType =>
            "https://us.api.blizzard.com/data/wow/item-class/9?namespace=static-us&locale=en_US";
    }
}

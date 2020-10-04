using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WowHelp.Shared.BattleNet.Models
{
    public class ItemClassResponse
    {
        [JsonProperty("class_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("item_subclasses")]
        public List<ItemSubClass> ItemSubClasses { get; set; } = new List<ItemSubClass>();
    }
}

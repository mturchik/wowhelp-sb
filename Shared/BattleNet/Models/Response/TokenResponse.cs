using Newtonsoft.Json;
using System;

namespace WowHelp.Shared.BattleNet.Models.Response
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public string Scope { get; set; }

        [JsonIgnore]
        public DateTime? ExpiresOn { get; set; }
    }
}
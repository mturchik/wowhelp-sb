﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WowHelp.Shared.BattleNet.Models;
using WowHelp.Shared.BattleNet.Models.Response;

namespace WowHelp.Shared.BattleNet
{
    public class BattleNetService : IBattleNetService
    {
        private readonly string _authKey;
        private readonly HttpClient _httpClient;

        private static TokenResponse AuthToken { get; set; }

        public BattleNetService(HttpClient httpClient, string authKey)
        {
            _authKey = authKey;
            _httpClient = httpClient;
        }

        #region Utility

        private async Task SetAuthorizeToken()
        {
            if (AuthToken?.ExpiresOn is null || AuthToken.ExpiresOn <= DateTime.Now)
            {
                var authRequest = CreateAuthRequest();
                var token = await _httpClient.Send<TokenResponse>(authRequest);
                if (token is null) return;

                // Set expires timestamp to keep an active OAuth token
                token.ExpiresOn = DateTime.Now
                    .AddSeconds(token.ExpiresIn)
                    .Subtract(TimeSpan.FromMinutes(30));

                AuthToken = token;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthToken.AccessToken);
            }
        }

        private HttpRequestMessage CreateAuthRequest()
        {
            var authRequest = new HttpRequestMessage(HttpMethod.Post, StaticRequestUrl.AuthUrl);

            var byteArray = new UTF8Encoding().GetBytes(_authKey);
            authRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(":region", "us"),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };
            authRequest.Content = new FormUrlEncodedContent(formData);

            return authRequest;
        }

        #endregion Utility

        public async Task<T> GetByUrl<T>(Url url)
        {
            var response = await _httpClient.Get<T>(url.Href);

            return response;
        }

        public async Task<List<ConnectedRealm>> GetRealms()
        {
            await SetAuthorizeToken();

            var index = await _httpClient.Get<CrIndexResponse>(StaticRequestUrl.CrIndexUrl);
            if (index is null) return new List<ConnectedRealm>();

            var realms = new List<ConnectedRealm>();

            foreach (var url in index.ConnectedRealmUrls)
            {
                var realm = await GetByUrl<ConnectedRealm>(url);

                if (realm is null) continue;

                realms.Add(realm);
            }

            return realms;
        }

    }
}
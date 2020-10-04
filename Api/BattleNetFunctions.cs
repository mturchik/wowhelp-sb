using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WowHelp.Shared.BattleNet;
using WowHelp.Shared.BattleNet.Models;

namespace WowHelp.Api
{
    public static class BattleNetFunctions
    {
        private static readonly IBattleNetService _bNetService;
        private static readonly IMemoryCache _cache;

        static BattleNetFunctions()
        {
            var authkey = $"{Environment.GetEnvironmentVariable("BattleNet_ClientId")}:{Environment.GetEnvironmentVariable("BattleNet_ClientSecret")}";
            _bNetService = new BattleNetService(new HttpClient(), authkey);
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        [FunctionName("BNRealms")]
        public static async Task<IActionResult> GetRealms(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var realms = _cache.Get<List<ConnectedRealm>>("BN_CR_INDEX");
            if (realms != null) return new OkObjectResult(realms);

            realms = await _bNetService.GetRealms();
            if (realms is null || !realms.Any())
            {
                log.LogError("Unable to look up battle net connected realms!");
                return new NotFoundResult();
            }

            _cache.Set("BN_CR_INDEX", realms, TimeSpan.FromHours(1));

            return new OkObjectResult(realms);
        }

        [FunctionName("BNRecipes")]
        public static async Task<IActionResult> GetRecipeClasses(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var recipeClasses = _cache.Get<List<ItemSubClass>>("BN_RECIPE_TYPES");
            if (recipeClasses != null) return new OkObjectResult(recipeClasses);

            var itemClass = await _bNetService.GetRecipeItemClass();
            if (itemClass is null || itemClass.ItemSubClasses is null || !itemClass.ItemSubClasses.Any())
            {
                log.LogError("Unable to look up recipe types!");
                return new NotFoundResult();
            }

            recipeClasses = itemClass.ItemSubClasses;
            _cache.Set("BN_RECIPE_TYPES", recipeClasses, TimeSpan.FromHours(1));

            return new OkObjectResult(recipeClasses);
        }
    }
}

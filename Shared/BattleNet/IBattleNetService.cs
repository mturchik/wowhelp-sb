using System.Collections.Generic;
using System.Threading.Tasks;
using WowHelp.Shared.BattleNet.Models;

namespace WowHelp.Shared.BattleNet
{
    public interface IBattleNetService
    {
        Task<T> GetByUrl<T>(string url);
        Task<List<ConnectedRealm>> GetRealms();
        Task<ItemClassResponse> GetRecipeItemClass();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WowHelp.Shared.BattleNet.Models;

namespace WowHelp.Shared.BattleNet
{
    public interface IBattleNetService
    {
        Task<T> GetByUrl<T>(Url url);
        Task<List<ConnectedRealm>> GetRealms();
    }
}
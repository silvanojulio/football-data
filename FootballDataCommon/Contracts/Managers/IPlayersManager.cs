using System.Threading.Tasks;

namespace FootballDataCommon.Contracts.Managers
{
    public interface IPlayersManager
    {
        Task<int> GetTotalPlayersByLeage(string leageCode);
    }
}
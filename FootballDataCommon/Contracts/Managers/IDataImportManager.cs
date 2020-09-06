using System;
using System.Threading.Tasks;

namespace FootballDataCommon.Contracts.Managers
{
    public interface IDataImportManager
    {
        Task ImportLeage(string leageCode);
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using FootballDataCommon.Contracts.Entities.ApiEntities;
using FootballDataCommon.Contracts.Managers;
using FootballDataCommon.Utils;
using FootballDataRepository.DbModel;

namespace FootballDataManagers
{
    public class DataImportManager : IDataImportManager
    {
        const string baseUrl = "https://api.football-data.org/v2";
        private readonly FootballDataBaseContext dbContext;

        public DataImportManager(FootballDataBaseContext context)
        {
            dbContext = context;
        }
        public async Task ImportLeage(string leageCode)
        {
            var leageAndTeamsData = await ApiClient.get<LeageTeamsApiResponse>(string.Format("{0}/competitions/{1}/teams", baseUrl, leageCode));

            var playersRequests = leageAndTeamsData.teams
                .Select(async x=>{
                    try
                    {
                        var data = await ApiClient.get<TeamDetailsApiResponse>(string.Format("{0}/teams/{1}", baseUrl, x.id));
                        return data;
                    }
                    catch (System.Exception ex)
                    {
                        return null;
                    }
                }).ToArray();

            Task.WaitAll(playersRequests);

            var playersData = playersRequests.Select(x=> x.Result);

        }
    }
}

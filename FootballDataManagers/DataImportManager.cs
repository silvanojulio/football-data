using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public DataImportManager(FootballDataBaseContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
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

            var playersData = playersRequests
                .Select(x=> x.Result)
                .Where(x=> x != null).ToList();

            var competition = mapper.Map<Competition>(leageAndTeamsData.competition);

            dbContext.Competitions.Add(competition);

            foreach (var apiTeam in leageAndTeamsData.teams)
            {
                var team =  mapper.Map<Team>(apiTeam);
                team.Competition = competition;
                dbContext.Teams.Add(team);

                var teamPlayersData = playersData.Find(x=> x.id == apiTeam.id);

                if(teamPlayersData != null){
                    dbContext.Players.AddRange(
                        teamPlayersData.squad
                        .Select( p=>{
                            var player = mapper.Map<Player>(p);
                            player.Team = team;
                            return player;
                        }).ToList()
                    );
                }
            }

            dbContext.SaveChanges();
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FootballDataCommon.Contracts.Entities.ApiEntities;
using FootballDataCommon.Contracts.Exceptions;
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
            var existingCompetition = dbContext.Competitions.FirstOrDefault(c=>c.Code == leageCode);

            if(existingCompetition!=null)
            {
                throw new AlreadyImportedLeageException(leageCode);
            }

            LeageTeamsApiResponse leageAndTeamsData = null;

            try{
                leageAndTeamsData = await ApiClient.get<LeageTeamsApiResponse>(string.Format("{0}/competitions/{1}/teams", baseUrl, leageCode));
            }
            catch(Exception ex)
            {
                throw new ItemNotFoundException(ex);
            }

            var playersRequests = leageAndTeamsData.teams
                .Select(async x=>{
                    try
                    {
                        var data = await ApiClient.get<TeamDetailsApiResponse>(string.Format("{0}/teams/{1}", baseUrl, x.id));
                        return data;
                    }
                    catch (System.Exception ex)
                    {
                        //It is because it is not possible to receive more than 10 success response in 1 minute
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

                var teamPlayersData = playersData.Find(x=> x.id == apiTeam.id);

                if(teamPlayersData != null){

                    var players =  teamPlayersData.squad
                        .Select( p=>{
                            var player = mapper.Map<Player>(p);
                            return player;
                        }).ToList();

                    foreach (var p in players)
                    {
                        if(p.Id == 46){
                            var a = 1;
                            a++;
                        }
                    }
                    team.Players = players;
                }

                dbContext.Teams.Add(team);
            }

            dbContext.SaveChanges();
        }
    }
}

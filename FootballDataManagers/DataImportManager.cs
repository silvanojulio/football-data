using System;
using System.Collections.Generic;
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
        private readonly IApiClient apiClient;
        private readonly IMapper mapper;

        public DataImportManager(FootballDataBaseContext context, IMapper mapper, IApiClient apiClient)
        {
            dbContext = context;
            this.mapper = mapper;
            this.apiClient = apiClient;
        }

        private async Task<LeageTeamsApiResponse> GetCompetition(string leagueCode){
            var existingCompetition = dbContext.Competitions.FirstOrDefault(c=>c.Code == leagueCode);

            if(existingCompetition!=null)
                throw new AlreadyImportedLeageException(leagueCode);

            LeageTeamsApiResponse leageAndTeamsData = null;

            try{
                leageAndTeamsData = await apiClient.get<LeageTeamsApiResponse>(string.Format("{0}/competitions/{1}/teams", baseUrl, leagueCode));
            }
            catch(ApiErrorException ex){
                throw new List<int>{400, 404}
                .Contains(ex.errorCode)?
                    new ItemNotFoundException(ex):
                    new Exception(ex.message);
            }
            catch(Exception)
            {
                throw;
            }

            return leageAndTeamsData;
        }

        private List<TeamDetailsApiResponse> getPlayers(LeageTeamsApiResponse leageAndTeamsData){
            var playersRequests = leageAndTeamsData.teams
                .Select(async x=>{
                    try
                    {
                        var data = await apiClient.get<TeamDetailsApiResponse>(string.Format("{0}/teams/{1}", baseUrl, x.id));
                        return data;
                    }
                    catch (System.Exception ex)
                    {
                        //It is because it is not possible to receive more than 10 success responses in 1 minute
                        return null;
                    }
                }).ToArray();

            Task.WaitAll(playersRequests);

            var playersData = playersRequests
                .Select(x=> x.Result)
                .Where(x=> x != null).ToList();

            return playersData;
        }
        public async Task ImportLeage(string leagueCode)
        {
            var leageAndTeamsData = await GetCompetition(leagueCode);
            var playersData =  getPlayers(leageAndTeamsData);

            var competition = mapper.Map<Competition>(leageAndTeamsData.competition);

            dbContext.Competitions.Add(competition);
            competition.Teams = new List<TeamCompetition>();

            var teamsExternalIds = leageAndTeamsData.teams.Select(x=>x.id).ToList();
            var existingTeams = dbContext.Teams.Where(x=> teamsExternalIds.Contains(x.ExternalId)).ToList();

            foreach (var apiTeam in leageAndTeamsData.teams)
            {
                var existingTeam = existingTeams.FirstOrDefault(x=> x.ExternalId == apiTeam.id);

                if(existingTeam!=null){
                    competition.Teams.Add(new TeamCompetition{
                        Team = existingTeam,
                        Competition = competition
                    });
                    continue;
                }

                var team = mapper.Map<Team>(apiTeam);
                competition.Teams.Add(new TeamCompetition{
                    Team = team,
                    Competition = competition
                });

                var teamPlayersData = playersData.FirstOrDefault(x=> x.id == apiTeam.id);

                if(teamPlayersData != null){

                    team.Players =  teamPlayersData.squad
                        .Select( p=>{
                            var player = mapper.Map<Player>(p);
                            return player;
                        }).ToList();
                }

                dbContext.Teams.Add(team);
            }

            dbContext.SaveChanges();
        }
    }
}

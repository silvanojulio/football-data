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
using Microsoft.EntityFrameworkCore;

namespace FootballDataManagers
{
    public class PlayersManager : IPlayersManager
    {
        private readonly FootballDataBaseContext dbContext;
        private readonly IMapper mapper;

        public PlayersManager(FootballDataBaseContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }
        public async Task<int> GetTotalPlayersByLeage(string leageCode)
        {
            var competition = dbContext.Competitions.FirstOrDefault(x => x.Code == leageCode);
            if (competition == null) throw new ItemNotFoundException(null);

            const string query = @"
                select p.* from [Players] p 
                inner join [Teams] t on p.TeamId = t.Id
                inner join [TeamCompetition] tc on tc.TeamId = t.id
                inner join [Competitions] c on tc.CompetitionId = c.id
                where c.Code = {0}
            ";

            return await dbContext.Players.FromSqlRaw(query, leageCode).CountAsync();
        }
    }
}

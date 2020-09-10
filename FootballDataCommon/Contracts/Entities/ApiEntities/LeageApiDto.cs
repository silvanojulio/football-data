using System;
using System.Collections.Generic;

namespace FootballDataCommon.Contracts.Entities.ApiEntities
{
    public class LeageTeamsApiResponse
    {
        public int count {get; set;}
        public LeageApiDto competition {get; set;}
        public ICollection<TeamApiDto> teams { get; set; }
    }

    public class TeamDetailsApiResponse:BaseEntityDto
    {
        public ICollection<PlayerApiDto> squad { get; set; }
    }

    public class PlayerApiDto:BaseEntityDto
    {
        public string position {get; set;}
        public string countryOfBirth {get; set;}
        public string nationality {get; set;}
        public DateTime dateOfBirth {get; set;}
    }
    public class LeageApiDto:BaseEntityDto
    {
        public string code {get; set;}
        public BaseEntityDto area {get; set;}
    }

    public class TeamApiDto:BaseEntityDto
    {
        public string shortName {get; set;}
        public string tla {get; set;}
        public string email {get; set;}
        public BaseEntityDto area {get; set;}
    }
}

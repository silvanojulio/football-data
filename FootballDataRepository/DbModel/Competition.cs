using System.Collections.Generic;

namespace FootballDataRepository.DbModel
{
    public class Competition
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}

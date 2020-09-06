using System.Collections.Generic;

namespace FootballDataRepository.DbModel
{
    public class Team
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Email { get; set; }
        public ICollection<Player> Teams { get; set; }
        public Competition Competition { get; set; }
    }
}

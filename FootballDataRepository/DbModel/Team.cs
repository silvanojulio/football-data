using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballDataRepository.DbModel
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Email { get; set; }
        public ICollection<Player> Players { get; set; }
        //public ICollection<PlayerTeam> Players { get; set; }
        public Competition Competition { get; set; }
    }
}

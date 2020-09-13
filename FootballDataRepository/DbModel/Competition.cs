using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballDataRepository.DbModel
{
    public class Competition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FootballDataRepository.DbModel
{
    public class TeamCompetition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id{get;  set;}
        public Competition Competition { get; set; }
        public Team Team { get; set; }
    }
}

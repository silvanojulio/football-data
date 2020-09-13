using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballDataRepository.DbModel
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public int ExternalId{get; set;}
        public string Code { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
        //public ICollection<PlayerTeam> Teams{get; set;}
        public Team Team{get; set;}
    }
}

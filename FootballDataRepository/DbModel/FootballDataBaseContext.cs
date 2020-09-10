using Microsoft.EntityFrameworkCore;

namespace FootballDataRepository.DbModel
{
    public class FootballDataBaseContext : DbContext
    {
        public FootballDataBaseContext(DbContextOptions<FootballDataBaseContext> options) : base(options)
        {
        }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
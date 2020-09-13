using Microsoft.EntityFrameworkCore;

namespace FootballDataRepository.DbModel
{
    public class FootballDataBaseContext : DbContext
    {
        public FootballDataBaseContext(DbContextOptions<FootballDataBaseContext> options) : base(options)
        {
        }

        // Code First workarround.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Server=localhost\\sqlexpress;Integrated Security=true;Initial Catalog=FootballDataDb");
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

    }
}
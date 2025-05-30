using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;
using Domain.Entities.RelationshipEntities;
using Domain.Entities.ResultEntities;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<FantasyTeam> FantasyTeams { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Constructor> Constructors { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<UserLeague> UserLeagues { get; set; }
        public DbSet<TeamDriver> TeamDrivers { get; set; }
        public DbSet<TeamConstructor> TeamConstructors { get; set; }
        public DbSet<SeasonDriver> SeasonDrivers { get; set; }
        public DbSet<SeasonConstructor> SeasonConstructors { get; set; }
        public DbSet<RaceResult> RaceResults { get; set; }
        public DbSet<ConstructorResult> ConstructorResults { get; set; }
        public DbSet<GameweekTeam> GameweekTeams { get; set; }
        public DbSet<GameweekDriver> GameweekDrivers { get; set; }
        public DbSet<GameweekConstructor> GameweekConstructors { get; set; }

        // Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FantasyTeamConfiguration());
            modelBuilder.ApplyConfiguration(new DriverConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructorConfiguration());
            modelBuilder.ApplyConfiguration(new SeasonConfiguration());
            modelBuilder.ApplyConfiguration(new RaceConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfiguration(new TransferConfiguration());
            modelBuilder.ApplyConfiguration(new UserLeagueConfiguration());
            modelBuilder.ApplyConfiguration(new TeamDriverConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConstructorConfiguration());
            modelBuilder.ApplyConfiguration(new SeasonDriverConfiguration());
            modelBuilder.ApplyConfiguration(new SeasonConstructorConfiguration());
            modelBuilder.ApplyConfiguration(new RaceResultConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructorResultConfiguration());
            modelBuilder.ApplyConfiguration(new GameweekTeamConfiguration());
            modelBuilder.ApplyConfiguration(new GameweekDriverConfiguration());
            modelBuilder.ApplyConfiguration(new GameweekConstructorConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;
using Domain.Entities.RelationshipEntities;

namespace Infrastructure.Database
{
    public static class DbInitializer
    {
        public static async Task Seed(AppDbContext context)
        {
            if (!context.Seasons.Any())
            {
                // 1. Skapa Season 2024
                var season2024 = new Season
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000020024"),
                    Year = 2024,
                    Name = "2024 Formula 1 World Championship",
                    StartDate = new DateTime(2024, 3, 2),
                    EndDate = new DateTime(2024, 12, 8),
                    IsActive = true,
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow
                };

                context.Seasons.Add(season2024);

                // 2. Lägg till constructors
                var constructors = new List<Constructor>
                    {
                        new Constructor { Id = Guid.NewGuid(), Name = "Red Bull", Nationality = "Austrian", CurrentPrice = 28.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Mercedes", Nationality = "German", CurrentPrice = 26.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Ferrari", Nationality = "Italian", CurrentPrice = 25.5m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "McLaren", Nationality = "British", CurrentPrice = 22.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Aston Martin", Nationality = "British", CurrentPrice = 20.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Alpine", Nationality = "French", CurrentPrice = 18.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Williams", Nationality = "British", CurrentPrice = 15.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "RB (Visa Cash App)", Nationality = "Italian", CurrentPrice = 14.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Sauber (Stake)", Nationality = "Swiss", CurrentPrice = 13.0m, IsActive = true },
                        new Constructor { Id = Guid.NewGuid(), Name = "Haas", Nationality = "American", CurrentPrice = 12.0m, IsActive = true },
                    };
                context.Constructors.AddRange(constructors);

                // 3. Lägg till drivers
                var drivers = new List<Driver>
                    {
                        new Driver { Id = Guid.NewGuid(), FirstName = "Max", LastName = "Verstappen", Code = "VER", Nationality = "Dutch", DriverNumber = 1, CurrentPrice = 30.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Sergio", LastName = "Perez", Code = "PER", Nationality = "Mexican", DriverNumber = 11, CurrentPrice = 18.5m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Lewis", LastName = "Hamilton", Code = "HAM", Nationality = "British", DriverNumber = 44, CurrentPrice = 19.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "George", LastName = "Russell", Code = "RUS", Nationality = "British", DriverNumber = 63, CurrentPrice = 17.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Charles", LastName = "Leclerc", Code = "LEC", Nationality = "Monégasque", DriverNumber = 16, CurrentPrice = 18.5m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Carlos", LastName = "Sainz", Code = "SAI", Nationality = "Spanish", DriverNumber = 55, CurrentPrice = 17.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Lando", LastName = "Norris", Code = "NOR", Nationality = "British", DriverNumber = 4, CurrentPrice = 17.5m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Oscar", LastName = "Piastri", Code = "PIA", Nationality = "Australian", DriverNumber = 81, CurrentPrice = 15.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Fernando", LastName = "Alonso", Code = "ALO", Nationality = "Spanish", DriverNumber = 14, CurrentPrice = 16.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Lance", LastName = "Stroll", Code = "STR", Nationality = "Canadian", DriverNumber = 18, CurrentPrice = 13.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Pierre", LastName = "Gasly", Code = "GAS", Nationality = "French", DriverNumber = 10, CurrentPrice = 12.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Esteban", LastName = "Ocon", Code = "OCO", Nationality = "French", DriverNumber = 31, CurrentPrice = 12.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Alexander", LastName = "Albon", Code = "ALB", Nationality = "Thai", DriverNumber = 23, CurrentPrice = 11.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Logan", LastName = "Sargeant", Code = "SAR", Nationality = "American", DriverNumber = 2, CurrentPrice = 10.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Yuki", LastName = "Tsunoda", Code = "TSU", Nationality = "Japanese", DriverNumber = 22, CurrentPrice = 11.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Daniel", LastName = "Ricciardo", Code = "RIC", Nationality = "Australian", DriverNumber = 3, CurrentPrice = 10.5m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Valtteri", LastName = "Bottas", Code = "BOT", Nationality = "Finnish", DriverNumber = 77, CurrentPrice = 10.5m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Zhou", LastName = "Guanyu", Code = "ZHO", Nationality = "Chinese", DriverNumber = 24, CurrentPrice = 10.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Kevin", LastName = "Magnussen", Code = "MAG", Nationality = "Danish", DriverNumber = 20, CurrentPrice = 10.0m, IsActive = true },
                        new Driver { Id = Guid.NewGuid(), FirstName = "Nico", LastName = "Hülkenberg", Code = "HUL", Nationality = "German", DriverNumber = 27, CurrentPrice = 10.0m, IsActive = true }
                    };

                context.Drivers.AddRange(drivers);

                // 4. Skapa SeasonDrivers / SeasonConstructors
                var seasonDrivers = drivers.Select(driver => new SeasonDriver
                {
                    Id = Guid.NewGuid(),
                    SeasonId = season2024.Id,
                    DriverId = driver.Id,
                    ConstructorId = null, // (eller koppla korrekt stall om du vill)
                    StartingPrice = driver.CurrentPrice,
                    CurrentPrice = driver.CurrentPrice,
                    TotalPoints = 0
                }).ToList();

                var seasonConstructors = constructors.Select(c => new SeasonConstructor
                {
                    Id = Guid.NewGuid(),
                    SeasonId = season2024.Id,
                    ConstructorId = c.Id,
                    StartingPrice = c.CurrentPrice,
                    CurrentPrice = c.CurrentPrice,
                    TotalPoints = 0
                }).ToList();

                context.SeasonDrivers.AddRange(seasonDrivers);
                context.SeasonConstructors.AddRange(seasonConstructors);

                await context.SaveChangesAsync();
            }
        }
    }
}

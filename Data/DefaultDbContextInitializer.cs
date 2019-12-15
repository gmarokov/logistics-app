using System.Collections.Generic;
using System.Threading.Tasks;
using app.web.Data.Contracts;
using app.web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace app.web.Data
{
    public class DefaultDbContextInitializer : IDefaultDbContextInitializer
    {
        private readonly DefaultDbContext _context;

        public DefaultDbContextInitializer(DefaultDbContext context)
        {
            _context = context;
        }

        public bool EnsureCreated() => _context.Database.EnsureCreated();

        public void Migrate() => _context.Database.Migrate();

        public async Task Seed()
        {
            await this.SeedPlaces();
            await this.SeedRoads();
        }

        private async Task SeedPlaces()
        {
            if (!await _context.Places.AnyAsync())
            {
                var placesList = new List<Place>()
                {
                    new Place() { Name = "Sofia" },
                    new Place() { Name = "Plovdiv" },
                    new Place() { Name = "StaraZagora" },
                    new Place() { Name = "Burgas" },
                    new Place() { Name = "Varna"},
                    new Place() { Name = "Ruse" },
                    new Place() { Name = "VelikoTurnovo" }
                };

                _context.Places.AddRange(placesList);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedRoads()
        {
            if (await _context.Places.AnyAsync())
                if (!await _context.Roads.AnyAsync())
                    await CreateRoads(await _context.Places.ToListAsync());
        }

        private async Task CreateRoads(List<Place> places)
        {
            if (places.Count > 1)
            {
                var road = new Road()
                {
                    Place1 = places[0],
                    Place2 = places[1]
                };
                _context.Roads.Add(road);
                await _context.SaveChangesAsync();
            }
        }
    }
}
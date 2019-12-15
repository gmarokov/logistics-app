using System;
using System.Linq;
using System.Threading.Tasks;
using app.web.Data;
using app.web.Infrastructure.Contracts;
using app.web.Models.Entities;
using app.web.Models.Structs;
using Microsoft.EntityFrameworkCore;

namespace app.web.Infrastructure.Services
{
    public class LogisticsCenterService : ILogisticsCenterService
    {
        public readonly DistanceCalculator _calculator;
        public readonly DefaultDbContext _context;

        public LogisticsCenterService(DefaultDbContext context)
        {
            _calculator = new DistanceCalculator();
            _context = context;
        }

        public async Task TryCreateLogisticsCenter()
        {
            var graph = InitGraph();

            var furthestPlace = _calculator.FindFurthest(graph);
            var closestToTheFurthest = _calculator.FindClosestByNode(graph, furthestPlace.Key);

            if (HasLogisticsCenterAt(closestToTheFurthest.Key))
                throw new ArgumentException("The matched logistics center already exists. ");

            var place = _context.Places.FirstOrDefault(x => x.Name == closestToTheFurthest.Key);
            _context.LogisticsCenters.Add(new LogisticsCenter()
            {
                Place = place
            });
            await _context.SaveChangesAsync();
        }

        private bool HasLogisticsCenterAt(string place) =>
            _context.LogisticsCenters.Include(x => x.Place).Any(x => x.Place.Name == place);

        private Graph InitGraph()
        {
            Graph graph = new Graph();

            _context.Places.ForEachAsync(x => graph.AddNode(x.Name));
            _context.Roads.ForEachAsync(x => graph.AddConnection(x.Place1.Name, x.Place2.Name, x.Distance, true));

            return graph;
        }
    }
}
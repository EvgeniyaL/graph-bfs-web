using GraphBfs.Dtos;
using GraphBfs.HandlersContracts;
using GraphBfs.RepositoriesContracts;
using BusinessLayer.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphBfs.Handlers
{
    public class LogisticCenterHandler : ILogisticCenterHandler
    {
        private readonly IPathRepository _pathRepository;
        private readonly ILogisticCenterRepository _logisticCenterRepository;

        public LogisticCenterHandler(IPathRepository pathRepository, 
                                     ILogisticCenterRepository logisticCenterRepository)
        {
            _pathRepository = pathRepository;
            _logisticCenterRepository = logisticCenterRepository;
        }

        public async Task<LogisticCenterDto> ProcessLogisticCenter()
        {
            var paths = await _pathRepository.GetPaths();
            var currentLogisticCenter = (await _logisticCenterRepository.GetLogisticCenters()).LastOrDefault();
            var sum = paths.Sum(x => x.GetHashCode());
            if (sum == currentLogisticCenter?.CheckSum || sum == 0)
            {
                return currentLogisticCenter;
            }

            var pathsCount = GetPathsCount(paths);
            var cityResult = GetCityLogisticCenter(pathsCount);
            
            var logisticCenter = new LogisticCenterDto { Name = cityResult.Name, CheckSum = sum };
            await _logisticCenterRepository.InsertLogisticCenter(logisticCenter);
            return logisticCenter;
        }

        private Dictionary<CityDto, int> GetPathsCount(IEnumerable<PathDto> paths)
        {
            var cities = new HashSet<CityDto>();
            foreach (var item in paths)
            {
                cities.Add(item.InitialCity);
                cities.Add(item.EndCity);
            }

            Graph<CityDto> graph = 
                new Graph<CityDto>(cities, paths.Select(x => new Tuple<CityDto, CityDto>(x.InitialCity, x.EndCity)));
            CheckForNotConnectedEdges(cities, graph);

            var pathsCount = new Dictionary<CityDto, int>();

            foreach (var startCity in cities)
            {
                int totalCount = 0;
                var shortestPath = ShortestPath(graph, startCity);

                foreach (var endCity in cities)
                {
                    totalCount += shortestPath(endCity).Count();
                }
                pathsCount[startCity] = totalCount;
            }

            return pathsCount;
        }

        private static void CheckForNotConnectedEdges(HashSet<CityDto> cities, Graph<CityDto> graph)
        {
            if (cities.Count > 2)
            {
                foreach (var item in graph.AdjacencyList)
                {
                    if (item.Value.Count == 1)
                    {
                        cities.Remove(item.Key);
                    }
                }
            }
        }

        private CityDto GetCityLogisticCenter(Dictionary<CityDto, int> pathsCount)
        {
            CityDto cityLogisticCenter = null;
            var minValue = int.MaxValue;
            foreach (var item in pathsCount)
            {
                if (minValue > item.Value)
                {
                    minValue = item.Value;
                    cityLogisticCenter = item.Key;
                }
            }

            return cityLogisticCenter;
        }

        public Func<T, IEnumerable<T>> ShortestPath<T>(Graph<T> graph, T start)
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, IEnumerable<T>> shortestPath = v =>
            {
                var path = new List<T> { };

                var current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }
    }
}


using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TransitApi.Data;
using TransitApi.Models;
using System.Collections.Generic;

namespace TransitApi
{
    public class DataImporter
    {
        public static async Task ImportAvtoData(TransitDbContext dbContext, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} does not exist.");
                return;
            }

            Console.WriteLine("Clearing old data...");
            dbContext.Schedules.RemoveRange(dbContext.Schedules);
            dbContext.RouteStops.RemoveRange(dbContext.RouteStops);
            dbContext.Routes.RemoveRange(dbContext.Routes);
            dbContext.Stops.RemoveRange(dbContext.Stops);
            await dbContext.SaveChangesAsync();
            Console.WriteLine("Old data cleared.");

            var json = await File.ReadAllTextAsync(filePath);
            var routesData = JsonSerializer.Deserialize<List<RouteData>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Console.WriteLine($"Found {routesData?.Count ?? 0} routes to import.");

            if (routesData == null) return;

            foreach (var data in routesData)
            {
                Console.WriteLine($"Importing Route {data.Number}: {data.Name} ({data.Day})");

                var route = new TransitRoute
                {
                    Name = $"{data.Name} (День: {data.Day})",
                    Number = data.Number,
                    Type = Enum.TryParse<TransportType>(data.Type, out var type) ? type : TransportType.Bus
                };

                dbContext.Routes.Add(route);
                await dbContext.SaveChangesAsync();

                if (data.Stops != null)
                {
                    foreach (var s in data.Stops)
                    {
                        var stop = await dbContext.Stops.FirstOrDefaultAsync(x => x.Name == s.Name);
                        if (stop == null)
                        {
                            stop = new Stop
                            {
                                Name = s.Name
                            };
                            dbContext.Stops.Add(stop);
                            await dbContext.SaveChangesAsync();
                        }

                        var routeStop = new RouteStop
                        {
                            RouteId = route.Id,
                            StopId = stop.Id,
                            Order = s.Order,
                            OffsetMinutes = s.OffsetMinutes
                        };
                        dbContext.RouteStops.Add(routeStop);
                    }
                }

                if (data.Schedules != null)
                {
                    foreach(var timeStr in data.Schedules)
                    {
                        if(TimeOnly.TryParse(timeStr, out var time))
                        {
                            dbContext.Schedules.Add(new Schedule
                            {
                                RouteId = route.Id,
                                DepartureTime = time
                            });
                        }
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine("Import complete.");
        }

        private class RouteData
        {
            public string Number { get; set; }
            public string Name { get; set; }
            public string Day { get; set; }
            public string Type { get; set; }
            public List<StopData> Stops { get; set; }
            public List<string> Schedules { get; set; }
        }

        private class StopData
        {
            public string Name { get; set; }
            public int OffsetMinutes { get; set; }
            public int Order { get; set; }
        }
    }
}

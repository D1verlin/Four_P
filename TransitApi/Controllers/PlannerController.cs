using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransitApi.Data;

namespace TransitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlannerController : ControllerBase
{
    private readonly TransitDbContext _db;
    public PlannerController(TransitDbContext db) => _db = db;

    
    
    [HttpGet]
    public async Task<IActionResult> Plan([FromQuery] int from, [FromQuery] int to)
    {
        var fromStop = await _db.Stops.FindAsync(from);
        var toStop = await _db.Stops.FindAsync(to);
        if (fromStop == null || toStop == null)
            return BadRequest("Укажите корректные ID остановок.");

        var now = TimeOnly.FromDateTime(DateTime.Now);

        
        var directRoutes = await _db.RouteStops
            .Where(rs => rs.StopId == from)
            .Join(_db.RouteStops.Where(rs => rs.StopId == to),
                  rs1 => rs1.RouteId,
                  rs2 => rs2.RouteId,
                  (rs1, rs2) => new { FromRS = rs1, ToRS = rs2, Route = rs1.Route })
            .Where(x => x.FromRS.Order < x.ToRS.Order)
            .ToListAsync();

        if (!directRoutes.Any())
        {
            return Ok(new
            {
                From = fromStop.Name,
                To = toStop.Name,
                Routes = Array.Empty<object>(),
                Message = "Прямых маршрутов не найдено. Попробуйте с пересадкой."
            });
        }

        var result = new List<object>();

        foreach (var dr in directRoutes)
        {
            var route = dr.Route;
            var travelMinutes = dr.ToRS.OffsetMinutes - dr.FromRS.OffsetMinutes;

            
            var departures = await _db.Schedules
                .Where(s => s.RouteId == route.Id)
                .Select(s => s.DepartureTime)
                .ToListAsync();

            var nextDeparture = departures
                .Select(dep => dep.AddMinutes(dr.FromRS.OffsetMinutes))
                .Where(arr => arr >= now)
                .OrderBy(arr => arr)
                .FirstOrDefault();

            
            var intermediateStops = await _db.RouteStops
                .Where(rs => rs.RouteId == route.Id
                          && rs.Order >= dr.FromRS.Order
                          && rs.Order <= dr.ToRS.Order)
                .Include(rs => rs.Stop)
                .OrderBy(rs => rs.Order)
                .ToListAsync();

            result.Add(new
            {
                RouteId = route.Id,
                RouteNumber = route.Number,
                RouteName = route.Name,
                Type = route.Type.ToString(),
                TravelMinutes = travelMinutes,
                NextDeparture = nextDeparture != default ? nextDeparture.ToString("HH:mm") : null,
                ArrivalTime = nextDeparture != default
                    ? nextDeparture.AddMinutes(travelMinutes).ToString("HH:mm")
                    : null,
                MinutesUntil = nextDeparture != default
                    ? (int)Math.Max(0, Math.Round((nextDeparture - now).TotalMinutes))
                    : (int?)null,
                Stops = intermediateStops.Select(rs => new
                {
                    rs.Stop.Name,
                    Time = nextDeparture != default
                        ? nextDeparture.AddMinutes(rs.OffsetMinutes - dr.FromRS.OffsetMinutes).ToString("HH:mm")
                        : null
                })
            });
        }

        return Ok(new
        {
            From = fromStop.Name,
            To = toStop.Name,
            Routes = result.OrderBy(r => ((dynamic)r).TravelMinutes)
        });
    }
}

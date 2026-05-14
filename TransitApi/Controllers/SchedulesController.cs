using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransitApi.Data;

namespace TransitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly TransitDbContext _db;
    public SchedulesController(TransitDbContext db) => _db = db;

    
    
    [HttpGet]
    public async Task<IActionResult> GetSchedule([FromQuery] int routeId, [FromQuery] string period = "all")
    {
        var route = await _db.Routes
            .Include(r => r.RouteStops)
                .ThenInclude(rs => rs.Stop)
            .FirstOrDefaultAsync(r => r.Id == routeId);

        if (route == null) return NotFound();

        var allDepartures = await _db.Schedules
            .Where(s => s.RouteId == routeId)
            .Select(s => s.DepartureTime)
            .OrderBy(t => t)
            .ToListAsync();

        var now = TimeOnly.FromDateTime(DateTime.Now);

        var filtered = period switch
        {
            "now" => allDepartures.Where(t => t >= now && t <= now.AddHours(2)).ToList(),
            "morning" => allDepartures.Where(t => t.Hour >= 6 && t.Hour < 12).ToList(),
            "afternoon" => allDepartures.Where(t => t.Hour >= 12 && t.Hour < 18).ToList(),
            "evening" => allDepartures.Where(t => t.Hour >= 18).ToList(),
            _ => allDepartures
        };

        var stops = route.RouteStops
            .OrderBy(rs => rs.Order)
            .Select(rs => new { rs.Stop.Name, rs.OffsetMinutes })
            .ToList();

        
        var departures = filtered.Take(50).Select(dep =>
        {
            var stopTimes = stops.Select(s => new
            {
                s.Name,
                Time = dep.AddMinutes(s.OffsetMinutes).ToString("HH:mm")
            }).ToList();

            return new
            {
                Departure = dep.ToString("HH:mm"),
                IsCurrent = dep >= now && dep <= now.AddMinutes(route.FrequencyMinutes),
                StopTimes = stopTimes
            };
        }).ToList();

        return Ok(new
        {
            Route = new
            {
                route.Id, route.Number, route.Name,
                Type = route.Type.ToString(),
                route.StartStop, route.EndStop, route.FrequencyMinutes
            },
            Stops = stops.Select(s => s.Name).ToList(),
            Departures = departures
        });
    }
}

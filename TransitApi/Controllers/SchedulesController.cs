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
        var maxOffset = route.RouteStops.Any() ? route.RouteStops.Max(rs => rs.OffsetMinutes) : 0;

        var filtered = period switch
        {
            "now" => allDepartures.Where(t => 
            {
                var end = t.AddMinutes(maxOffset);
                bool isActiveNow = end >= t ? (now >= t && now <= end) : (now >= t || now <= end);
                var endLimit = now.AddHours(2);
                bool isStartingSoon = endLimit >= now ? (t >= now && t <= endLimit) : (t >= now || t <= endLimit);
                return isActiveNow || isStartingSoon;
            }).ToList(),
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

            var end = dep.AddMinutes(maxOffset);
            bool isCurrent = end >= dep ? (now >= dep && now <= end) : (now >= dep || now <= end);

            return new
            {
                Departure = dep.ToString("HH:mm"),
                IsCurrent = isCurrent,
                StopTimes = stopTimes
            };
        }).ToList();

        return Ok(new
        {
            Route = new
            {
                route.Id, route.Number, route.Name,
                Type = route.Type.ToString()
            },
            ServerTime = now.ToString("HH:mm"),
            MaxOffset = maxOffset,
            Stops = stops.Select(s => s.Name).ToList(),
            Departures = departures
        });
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransitApi.Data;
using TransitApi.Models;

namespace TransitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StopsController : ControllerBase
{
    private readonly TransitDbContext _db;
    public StopsController(TransitDbContext db) => _db = db;

    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        var query = _db.Stops.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.Name.Contains(search) || s.Address.Contains(search));

        var stops = await query
            .Select(s => new { s.Id, s.Name, s.Address, s.Direction })
            .ToListAsync();
        return Ok(stops);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var stop = await _db.Stops.FindAsync(id);
        if (stop == null) return NotFound();
        return Ok(stop);
    }

    
    
    [HttpGet("{id}/arrivals")]
    public async Task<IActionResult> GetArrivals(int id, [FromQuery] TransportType? type)
    {
        var stop = await _db.Stops.FindAsync(id);
        if (stop == null) return NotFound();

        var now = TimeOnly.FromDateTime(DateTime.Now);

        
        var routeStopsOnStop = await _db.RouteStops
            .Where(rs => rs.StopId == id)
            .Include(rs => rs.Route)
            .ToListAsync();

        if (type.HasValue)
            routeStopsOnStop = routeStopsOnStop.Where(rs => rs.Route.Type == type.Value).ToList();

        var arrivals = new List<object>();

        foreach (var rs in routeStopsOnStop)
        {
            
            var schedules = await _db.Schedules
                .Where(s => s.RouteId == rs.RouteId)
                .Select(s => s.DepartureTime)
                .ToListAsync();

            
            var nextArrivals = schedules
                .Select(dep => dep.AddMinutes(rs.OffsetMinutes))
                .Where(arr => arr >= now)
                .OrderBy(arr => arr)
                .Take(2)
                .Select(arr => new
                {
                    RouteId = rs.RouteId,
                    RouteNumber = rs.Route.Number,
                    RouteName = rs.Route.Name,
                    Type = rs.Route.Type.ToString(),
                    ArrivalTime = arr.ToString("HH:mm"),
                    MinutesUntil = (int)Math.Round((arr - now).TotalMinutes)
                })
                .ToList();

            arrivals.AddRange(nextArrivals);
        }

        var result = arrivals
            .Cast<dynamic>()
            .OrderBy(a => (int)a.MinutesUntil)
            .Take(8)
            .ToList();

        return Ok(result);
    }
}

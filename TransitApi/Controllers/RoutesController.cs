using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransitApi.Data;
using TransitApi.Models;

namespace TransitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly TransitDbContext _db;
    public RoutesController(TransitDbContext db) => _db = db;

    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TransportType? type)
    {
        var query = _db.Routes.AsQueryable();
        if (type.HasValue)
            query = query.Where(r => r.Type == type.Value);

        var routes = await query
            .Where(r => r.IsActive)
            .Select(r => new
            {
                r.Id, r.Number, r.Name,
                Type = r.Type.ToString()
            })
            .ToListAsync();
        return Ok(routes);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var route = await _db.Routes
            .Include(r => r.RouteStops)
                .ThenInclude(rs => rs.Stop)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (route == null) return NotFound();

        return Ok(new
        {
            route.Id, route.Number, route.Name,
            Type = route.Type.ToString(),
            Stops = route.RouteStops
                .OrderBy(rs => rs.Order)
                .Select(rs => new
                {
                    rs.StopId, rs.Stop.Name,
                    rs.Order, rs.OffsetMinutes
                })
        });
    }

    
    [HttpGet("{id}/stops")]
    public async Task<IActionResult> GetStops(int id)
    {
        var routeStops = await _db.RouteStops
            .Where(rs => rs.RouteId == id)
            .Include(rs => rs.Stop)
            .OrderBy(rs => rs.Order)
            .Select(rs => new
            {
                rs.StopId, rs.Stop.Name,
                rs.Order, rs.OffsetMinutes
            })
            .ToListAsync();

        return Ok(routeStops);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TransitApi.Data;
using TransitApi.Models;

namespace TransitApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly TransitDbContext _context;

    public AdminController(TransitDbContext context)
    {
        _context = context;
    }

    
    [HttpGet("stops")]
    public async Task<IActionResult> GetStops() => Ok(await _context.Stops.ToListAsync());

    [HttpPost("stops")]
    public async Task<IActionResult> CreateStop(Stop stop)
    {
        _context.Stops.Add(stop);
        await _context.SaveChangesAsync();
        return Ok(stop);
    }

    [HttpPut("stops/{id}")]
    public async Task<IActionResult> UpdateStop(int id, Stop stop)
    {
        if (id != stop.Id) return BadRequest();
        _context.Entry(stop).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("stops/{id}")]
    public async Task<IActionResult> DeleteStop(int id)
    {
        var stop = await _context.Stops.FindAsync(id);
        if (stop == null) return NotFound();
        _context.Stops.Remove(stop);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    [HttpGet("routes")]
    public async Task<IActionResult> GetRoutes() => Ok(await _context.Routes.ToListAsync());

    [HttpPost("routes")]
    public async Task<IActionResult> CreateRoute(TransitRoute route)
    {
        _context.Routes.Add(route);
        await _context.SaveChangesAsync();
        return Ok(route);
    }

    [HttpPut("routes/{id}")]
    public async Task<IActionResult> UpdateRoute(int id, TransitRoute route)
    {
        if (id != route.Id) return BadRequest();
        _context.Entry(route).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("routes/{id}")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
        var route = await _context.Routes.FindAsync(id);
        if (route == null) return NotFound();
        _context.Routes.Remove(route);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    [HttpGet("schedules")]
    public async Task<IActionResult> GetSchedules() => Ok(await _context.Schedules.ToListAsync());

    [HttpPost("schedules")]
    public async Task<IActionResult> CreateSchedule(Schedule schedule)
    {
        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();
        return Ok(schedule);
    }

    [HttpDelete("schedules/{id}")]
    public async Task<IActionResult> DeleteSchedule(int id)
    {
        var schedule = await _context.Schedules.FindAsync(id);
        if (schedule == null) return NotFound();
        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    [HttpGet("notifications")]
    public async Task<IActionResult> GetNotifications() => Ok(await _context.Notifications.ToListAsync());

    [HttpPost("notifications")]
    public async Task<IActionResult> CreateNotification(Notification notification)
    {
        notification.CreatedAt = DateTime.UtcNow;
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return Ok(notification);
    }

    [HttpDelete("notifications/{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null) return NotFound();
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers() => Ok(await _context.Users.Select(u => new { u.Id, u.Username, u.Email, u.IsAdmin }).ToListAsync());

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("import-avto")]
    [AllowAnonymous]
    public async Task<IActionResult> ImportAvto()
    {
        try
        {
            await DataImporter.ImportAvtoData(_context, @"C:\Users\Lenovo\Desktop\Projects\FOUR_P\clean_db.json");
            return Ok(new { message = "Data imported successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message, details = ex.ToString() });
        }
    }
}

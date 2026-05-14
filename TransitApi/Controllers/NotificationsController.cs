using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransitApi.Data;
using TransitApi.Models;

namespace TransitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly TransitDbContext _db;
    public NotificationsController(TransitDbContext db) => _db = db;

    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] NotificationType? type)
    {
        var query = _db.Notifications.AsQueryable();
        if (type.HasValue)
            query = query.Where(n => n.Type == type.Value);

        var notifications = await query
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new
            {
                n.Id,
                Type = n.Type.ToString(),
                n.Title, n.Message, n.AffectedRoutes,
                n.CreatedAt,
                TimeAgo = GetTimeAgo(n.CreatedAt)
            })
            .ToListAsync();

        return Ok(notifications);
    }

    private static string GetTimeAgo(DateTime createdAt)
    {
        var diff = DateTime.UtcNow - createdAt;
        if (diff.TotalMinutes < 1) return "Только что";
        if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} мин назад";
        if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} ч назад";
        return $"{(int)diff.TotalDays} дн назад";
    }
}

namespace TransitApi.Models;

public enum NotificationType
{
    Disruption,
    Delay,
    Info,
    Maintenance
}

public class Notification
{
    public int Id { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    
    public string AffectedRoutes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TransitRoute> AffectedTransitRoutes { get; set; } = new List<TransitRoute>();
}

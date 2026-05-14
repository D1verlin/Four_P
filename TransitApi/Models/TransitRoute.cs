namespace TransitApi.Models;

public enum TransportType
{
    Bus,
    Trolleybus,
    Tram,
    Minibus
}

public class TransitRoute
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public TransportType Type { get; set; }
    public string StartStop { get; set; } = string.Empty;
    public string EndStop { get; set; } = string.Empty;
    public int FrequencyMinutes { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

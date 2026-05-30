using System.Text.Json.Serialization;

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
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    public ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    [JsonIgnore]
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

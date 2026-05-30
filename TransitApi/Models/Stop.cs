using System.Text.Json.Serialization;

namespace TransitApi.Models;

public class Stop
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
}

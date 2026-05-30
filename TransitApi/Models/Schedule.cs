using System.Text.Json.Serialization;

namespace TransitApi.Models;


public class Schedule
{
    public int Id { get; set; }
    public int RouteId { get; set; }

    
    public TimeOnly DepartureTime { get; set; }

    [JsonIgnore]
    public TransitRoute Route { get; set; } = null!;
}

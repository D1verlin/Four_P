using System.Text.Json.Serialization;

namespace TransitApi.Models;




public class RouteStop
{
    public int Id { get; set; }
    public int RouteId { get; set; }
    public int StopId { get; set; }
    public int Order { get; set; }

    
    public int OffsetMinutes { get; set; }

    [JsonIgnore]
    public TransitRoute Route { get; set; } = null!;
    [JsonIgnore]
    public Stop Stop { get; set; } = null!;
}

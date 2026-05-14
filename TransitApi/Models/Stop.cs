namespace TransitApi.Models;

public class Stop
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;

    public ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
}

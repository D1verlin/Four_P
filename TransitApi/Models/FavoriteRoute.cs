namespace TransitApi.Models;

public class FavoriteRoute
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }

    public int TransitRouteId { get; set; }
    public TransitRoute? TransitRoute { get; set; }
}

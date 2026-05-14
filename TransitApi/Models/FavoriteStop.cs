namespace TransitApi.Models;

public class FavoriteStop
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }

    public int StopId { get; set; }
    public Stop? Stop { get; set; }
}

using System.Collections.Generic;

namespace TransitApi.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;

    public ICollection<FavoriteStop> FavoriteStops { get; set; } = new List<FavoriteStop>();
    public ICollection<FavoriteRoute> FavoriteRoutes { get; set; } = new List<FavoriteRoute>();
    public ICollection<FavoritePlace> FavoritePlaces { get; set; } = new List<FavoritePlace>();
}

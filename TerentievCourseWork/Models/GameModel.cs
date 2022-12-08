using System;
using Newtonsoft.Json;

namespace TerentievCourseWork.Models;

public class GameModel : IEquatable<GameModel>
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = String.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = String.Empty;

    [JsonProperty("image_path")]
    public string ImagePath { get; set; } = String.Empty;

    [JsonProperty("price")]
    public decimal Price { get; set; } = 0;

    [JsonProperty("release_date")]
    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    [JsonProperty("developer")]
    public string Developer { get; set; } = String.Empty;

    [JsonProperty("publisher")]
    public string Publisher { get; set; } = String.Empty;

    [JsonProperty("rating")]
    public float Rating { get; set; } = 0;


    public bool Equals(GameModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Title == other.Title && Description == other.Description &&
               ImagePath == other.ImagePath && Price == other.Price && ReleaseDate.Equals(other.ReleaseDate) &&
               Developer == other.Developer && Publisher == other.Publisher && Rating.Equals(other.Rating);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GameModel)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(Title);
        hashCode.Add(Description);
        hashCode.Add(ImagePath);
        hashCode.Add(Price);
        hashCode.Add(ReleaseDate);
        hashCode.Add(Developer);
        hashCode.Add(Publisher);
        hashCode.Add(Rating);
        return hashCode.ToHashCode();
    }
}
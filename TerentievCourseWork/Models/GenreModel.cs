using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TerentievCourseWork.Models;

public class GenreModel : IEquatable<GenreModel>
{
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; } = String.Empty;
    [JsonProperty("description")]
    public string Description { get; set; } = String.Empty;
    [JsonProperty("games")]
    public List<GameModel> Games { get; set; } = new();

    public bool Equals(GenreModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name && Description == other.Description && Games.Equals(other.Games);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GenreModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, Games);
    }
}
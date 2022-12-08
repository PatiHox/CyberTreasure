using System;
using Newtonsoft.Json;

namespace TerentievCourseWork.Models;

public class GameModel
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
}
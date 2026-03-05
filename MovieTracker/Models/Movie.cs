using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Movie
{
    //fix nameing convention to match api
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string Release_Date { get; set; }

    [JsonPropertyName("genre_ids")]
    public List<int> Genre_Ids { get; set; }

    [JsonPropertyName("poster_path")]
    public string Poster_Path { get; set; }

    public bool Watched { get; set; } = false;
}
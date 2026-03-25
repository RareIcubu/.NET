using System.Text.Json.Serialization;

namespace Lab2_api;

public class Movie
{
    public int Id { get; set; } 

    [JsonPropertyName("Title")]
    public string title  { get; set; }
    
    [JsonPropertyName("Year")]
    public string year { get; set; } 
    
    [JsonPropertyName("Released")]
    public string release_date { get; set; }
    
    [JsonPropertyName("Plot")]
    public string plot  { get; set; }
    
    [JsonPropertyName("Poster")]
    public string poster  { get; set; }
    
    [JsonPropertyName("Genre")]
    public string genre  { get; set; }
    
    [JsonPropertyName("Ratings")]
    public List<Rating> ratings { get; set; }
    
    public override string ToString()
    {
        return $"Tytuł: {title}, Rok: {year}, Gatunek: {genre}\nFabuła: {plot}";
    }
}
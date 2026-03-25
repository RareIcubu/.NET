using System.Text.Json.Serialization;

namespace Lab2_api;

public class Rating
{
    public int Id { get; set; } // Klucz główny
    
    [JsonPropertyName("Source")]
    public string source { get; set; }
    
    [JsonPropertyName("Value")]
    public string value { get; set; }

    public int MovieId { get; set; }
    [JsonIgnore] 
    public Movie movie { get; set; } 
}

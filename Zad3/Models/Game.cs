namespace Zad3.Models;

using System.ComponentModel.DataAnnotations;

public class Game
{
    public int Id { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }
    
    public int TotalScore { get; set; } = 0;
    public int VoteCount { get; set; } = 0;

    public string? ImageUrl { get; set; }
}
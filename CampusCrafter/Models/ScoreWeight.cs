using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record ScoreWeight 
{
    [Key] public int Id { get; set; }
    public ProgressType ProgressType { get; set; }
    [Range(0, 100)] public decimal Weight { get; set; }
}
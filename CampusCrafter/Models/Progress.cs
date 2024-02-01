using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Progress(
    ProgressType Type,
    [Range(0,1000)] decimal Score
)
{
    [Key] public int Id { get; set; }
};
using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Progress(
    ProgressType Type,
    decimal Score
)
{
    [Key] public int Id { get; set; }
};
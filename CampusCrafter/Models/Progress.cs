using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Progress(
    [property: Key] int Id,
    ProgressType Type,
    decimal Score
);
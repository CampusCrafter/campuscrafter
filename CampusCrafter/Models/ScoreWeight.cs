using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record ScoreWeight(
    [property: Key] int Id,
    ProgressType ProgressType,
    [property: Range(0, 100)] decimal Weight
);
using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record ScholarlyAchievement(
    [property: Key] int Id,
    ScholarlyAchievementType Type,
    string Description,
    [Range(0, 1000)] decimal? Score
);
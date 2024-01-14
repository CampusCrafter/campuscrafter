using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record ScholarlyAchievement(
    [property: Key] int Id,
    ScholarlyAchievementType Type,
    string Description
);
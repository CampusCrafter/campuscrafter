using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public class ScholarlyAchievement
{
    [Key] public int Id { get; set; }
    public ScholarlyAchievementType Type { get; set; }
    
    [MaxLength(1024)]
    public string Description { get; set; }
    [Range(0, 10000)] public decimal? Score { get; set; }
}
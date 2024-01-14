using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Candidate
{
    [Key, MaxLength(64)] public required string UserId { get; set; }
    public virtual required List<Progress> Progresses { get; set; }
    public virtual required List<ScholarlyAchievement> ScholarlyAchievements { get; set; }
    
    public virtual required List<Major> CompletedMajors { get; set; }
}
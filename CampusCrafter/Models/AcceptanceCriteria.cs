using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record AcceptanceCriteria
{
    [Key] public int Id { get; set; }
    [Range(0, 10000)] public int MaxStudents { get; set; }
    public virtual required List<ScoreWeight> ScoreWeights { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CampusCrafter.Models;

public record AcceptanceCriteria
{
    [Key] public int Id { get; set; }
    [Range(0, 10000), Display(Name = "Max Student Count")] public int MaxStudents { get; set; }
    [ValidateNever] public virtual required List<ScoreWeight> ScoreWeights { get; set; }
}
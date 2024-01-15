using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CampusCrafter.Models;

public record Course(
    [property: Key] int Id    
)
{
    [MaxLength(64)] public required string Name { get; set; }
    
    [ForeignKey("Semester")] public int SemesterId { get; set; }
    [ValidateNever] public required Semester Semester { get; set; }
}
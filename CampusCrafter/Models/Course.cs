using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Course
{
    [Key] public int Id { get; set; }
    [MaxLength(64)] public required string Name { get; set; }
    
    public required Semester Semester { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCrafter.Models;

public record Specialization : Course
{
    public required Major Major { get; set; }
};
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCrafter.Models;

public record Specialization : Major
{
    [ForeignKey("Major")]
    public int MajorId { get; set; }
    
    public required Major Major { get; set; }
};
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCrafter.Models;

public record Major
{
    [Key] public int Id { get; set; }
    [MaxLength(64)] public required string Name { get; set; } 
    public DegreeType GraduationDegree { get; set; }
    
    public required Department Department { get; set; }
    
    public virtual required List<Major> AllowsStudying { get; set; }
    public virtual required List<Major> Prerequisites { get; set; }
    
    public virtual required List<Specialization> Specializations { get; set; }
    
    public virtual required List<Course> Courses { get; set; }
    
    [ForeignKey("StudyPlan")] public int StudyPlanId { get; set; }
    public required StudyPlan StudyPlan { get; set; }
    
    // TODO: How do we calculate the minimum score required for an application to be accepted?
}
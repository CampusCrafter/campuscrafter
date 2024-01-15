using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCrafter.Models;

public record Major
{
    [Key] public int Id { get; set; }
    [MaxLength(64)] public required string Name { get; set; } 
    [Display(Name = "Degree Received upon Graduation")] public DegreeType GraduationDegree { get; set; }
    
    [ForeignKey("Department"), Display(Name = "Department")] public int DepartmentId { get; set; }
    public required Department Department { get; set; }
    
    public virtual required List<Major> AllowsStudying { get; set; }
    public virtual required List<Major> Prerequisites { get; set; }
    
    public virtual List<Specialization> Specializations { get; set; }
    
    public virtual required List<Course> Courses { get; set; }
    
    public required List<StudyPlan> StudyPlans { get; set; }
    
    // TODO: How do we calculate the minimum score required for an application to be accepted?
}
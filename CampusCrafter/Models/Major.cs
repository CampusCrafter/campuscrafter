using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CampusCrafter.Models;

public record Major
{
    [Key] public int Id { get; set; }
    [MaxLength(64)] public required string Name { get; set; } 
    [Display(Name = "Degree Received upon Graduation")] public DegreeType GraduationDegree { get; set; }
    
    [Display(Name = "Major Type")] public MajorType MajorType { get; set; }
    
    [ForeignKey("ParentMajor")] public int? ParentMajorId { get; set; } = null!;
    [ValidateNever] public Major? ParentMajor { get; set; } = null!;
    
    [ForeignKey("Department"), Display(Name = "Department")] public int DepartmentId { get; set; }
    [ValidateNever] public required Department Department { get; set; }
    [ValidateNever] public virtual required List<Major> Prerequisites { get; set; }
    
    [ValidateNever] public virtual required List<Major> Specializations { get; set; }
    
    [ValidateNever] public virtual required List<Course> Courses { get; set; }
    
    [ValidateNever, Display(Name = "Study Plans")] public required List<StudyPlan> StudyPlans { get; set; }
    
    [Display(Name = "Minimum score last year")] public decimal? MinScore { get; set; }
}
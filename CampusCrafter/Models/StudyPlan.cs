using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record StudyPlan
{
    [Key] public int Id { get; set; }
    
    public StageOfStudy StageOfStudy { get; set; }
    public StudyType StudyType { get; set; }
    public EducationProfile EducationProfile { get; set; }
    public required string Language { get; set; }
    
    public required Major Major { get; set; }
    
    public required AcceptanceCriteria AcceptanceCriteria { get; set; }

    public int SemesterCount => Major.Courses.Select(course => course.Semester).Distinct().Count();
}
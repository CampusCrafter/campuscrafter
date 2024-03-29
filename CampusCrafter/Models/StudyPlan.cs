﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace CampusCrafter.Models;

public record StudyPlan
{
    [Key] public int Id { get; set; }
    [MaxLength(64)] public required string Name { get; set; }
    
    [Display(Name = "Stage of Study")] public StageOfStudy StageOfStudy { get; set; }
    [Display(Name = "Study Type")] public StudyType StudyType { get; set; }
    [Display(Name = "Education Profile")] public EducationProfile EducationProfile { get; set; }
    [Display(Name = "Language")] public required string Language { get; set; }

    [ValidateNever, JsonIgnore] public Major? Major { get; set; } = null;

    [ValidateNever] public AcceptanceCriteria AcceptanceCriteria { get; set; } = null!;

    // [NotMapped]
    // public int SemesterCount => Major?.Courses.Select(course => course.Semester).Distinct().Count() ?? 0;
}
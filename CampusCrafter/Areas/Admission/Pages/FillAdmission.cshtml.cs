using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.Admission.Pages;

[Authorize(Roles = "Candidate")]
public class FillAdmissionModel(ApplicationRepository repository) : PageModel
{
    [TempData] public int MajorDegree { get; set; }
    
    [TempData] public string SelectedMajors { get; set; } = default!;
    
    public List<Major> Majors { get; set; } = [];

    public List<List<ScoreWeight>> ScoreWeights { get; set; } = [];

    public Candidate Candidate { get; set; } = default!;

    public DateTime DateTime { get; set; } = DateTime.Today;

    private List<ProgressType> FirstDegreeNeededProgressTypes { get; } =
    [
        ProgressType.MaturaBasicMath,
        ProgressType.MaturaBasicPolish,
        ProgressType.MaturaBasicForeignLanguage
    ];
    
    public async Task<IActionResult> OnGetAsync()
    {
        var selectedMajors = GetSelectedMajors();
        
        Majors = await repository.MajorsWithIdsAsync(selectedMajors.Select(s => s[0]));

        ScoreWeights = await repository.ScoreWeightsFromStudyPlansIds(selectedMajors.Select(s => s[1]));
            
        TempData.Keep();
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(List<ProgressType> progressTypes,
        List<decimal> score,
        List<ScholarlyAchievementType> scholarlyAchievementTypes,
        List<string> descriptions)
    {
        await Console.Out.WriteLineAsync("!!!!!!!!!!");

        if (!ModelState.IsValid)
        {
            TempData.Keep();
            return BadRequest("there are errors in form");
        }

        if (score.Any(a => a < 0))
        {
            TempData.Keep(); 
            return BadRequest("asses positive values in progress section");
        }

        if (MajorDegree == 0)
        {
            foreach (var progressType in FirstDegreeNeededProgressTypes)
            {
                if (!progressTypes.Contains(progressType))
                {
                    TempData.Keep();
                    return BadRequest("you need to asses your score in "+progressType);
                }
            }
        }
        else if (!progressTypes.Contains(ProgressType.UniversityStage1))
        {
            TempData.Keep();
            return BadRequest("you need to asses your score from university stage 1");
        }

        if (progressTypes.Distinct().Count() != progressTypes.Count())
        {
            TempData.Keep();
            return BadRequest("yuo can't asses points to same progress twice");
        }
            
        Candidate = new Candidate
        {
            UserId = User.Identity?.Name!,
            Progresses = [],
            ScholarlyAchievements = [],
            CompletedMajors = []
        };
            
        foreach (var iTuple in progressTypes.Zip(score))
        {
            var progress = new Progress(Type: iTuple.First, Score: iTuple.Second);
            Candidate.Progresses.Add(progress);
        }
            
        foreach (var iTuple in scholarlyAchievementTypes.Zip(descriptions).Skip(1))
        {
            var scholarlyAchievement = new ScholarlyAchievement { Type = iTuple.First, Description = iTuple.Second, Score = null};
            Candidate.ScholarlyAchievements.Add(scholarlyAchievement);
        }
        
        repository.Add(Candidate);
            
        var builder = new CandidateApplicationBuilder
        {
            ApplicantId = Candidate.UserId,
            Date = DateTime.Today
        };

        foreach (var majorId in GetSelectedMajors()
                     .Select(i => i[0]))
        {
            builder.MajorId = majorId;
            repository.Add(builder.Build());
        }
        
        TempData.Keep();

        await repository.SaveChangesAsync();
        
        return RedirectToPage("./Index");
    }

    private List<List<int>> GetSelectedMajors()
    {
        return SelectedMajors
            .Split("I")
            .Where(a => a != "")
            .Select(s => s.Split("s")
                .Select(int.Parse)
                .ToList())
            .ToList();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;

namespace CampusCrafter.Areas.Admission.Pages
{
    public class FillApplicationModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public FillApplicationModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(List<int> selectedMajors, MajorType majorType)
        {
            SelectedMajors = selectedMajors;
            MajorType = majorType;
            return Page();
        }

        public IList<int> SelectedMajors { get; set; } = default!;
        
        public MajorType MajorType { get; set; }
        
        public Candidate Candidate { get; set; } = default!;
        
        public CandidateApplication CandidateApplication { get; set; }
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost(List<ProgressType> progressTypes, List<decimal> score, List<ScholarlyAchievementType> scholarlyAchievementTypes, List<string> descriptions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            foreach (var iTuple in progressTypes.Zip(score))
            {
                Progress progress = default!;
                progress = progress with { Type = iTuple.First, Score = iTuple.Second};
                
                Candidate.Progresses.Add(progress);
            }
            
            foreach (var iTuple in scholarlyAchievementTypes.Zip(descriptions))
            {
                ScholarlyAchievement scholarlyAchievement = default!;
                scholarlyAchievement = scholarlyAchievement with { Type = iTuple.First, Description = iTuple.Second};
                
                Candidate.ScholarlyAchievements.Add(scholarlyAchievement);
            }

            CandidateApplication.Applicant = Candidate;

            return RedirectToPage("./ConfirmAdmission",(CandidateApplication, SelectedMajors));
        }
    }
}

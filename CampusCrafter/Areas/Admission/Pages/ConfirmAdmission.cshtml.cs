using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Admission.Pages
{
    public class ConfirmApplicationModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public ConfirmApplicationModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProgressType> ProgressTypes;

        public List<decimal> Score;

        public List<ScholarlyAchievementType> ScholarlyAchievementTypes;

        public List<string> Descriptions;

        public List<Major> SelectedMajors = [];
        
        public CandidateApplication CandidateApplication { get; set; }
        
        public Candidate Candidate { get; set; } = default!;

        public DateTime DateTime { get; set; }
        
        public IActionResult OnGet(List<ProgressType> progressTypes, List<decimal> score, List<ScholarlyAchievementType> scholarlyAchievementTypes, List<string> descriptions, List<int> selectedMajors)
        {
            ProgressTypes = progressTypes;
            Score = score;
            ScholarlyAchievementTypes = scholarlyAchievementTypes;
            Descriptions = descriptions;
            foreach (var i in selectedMajors)
            {
                SelectedMajors.Add(_context.Majors.Find(i)!);
            }
            DateTime = DateTime.Today;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var iTuple in ProgressTypes.Zip(Score))
            {
                Progress progress = default!;
                progress = progress with { Type = iTuple.First, Score = iTuple.Second};
                
                Candidate.Progresses.Add(progress);
                _context.Progresses.Add(progress);
            }
            
            foreach (var iTuple in ScholarlyAchievementTypes.Zip(Descriptions))
            {
                ScholarlyAchievement scholarlyAchievement = default!;
                scholarlyAchievement = scholarlyAchievement with { Type = iTuple.First, Description = iTuple.Second};
                
                Candidate.ScholarlyAchievements.Add(scholarlyAchievement);
                _context.ScholarlyAchievements.Add(scholarlyAchievement);
            }

            CandidateApplication.Applicant = Candidate;
            
            _context.Candidates.Add(Candidate);
            
            foreach (var major in SelectedMajors)
            {
                _context.CandidateApplications.Add(CandidateApplication with {Major = major});
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

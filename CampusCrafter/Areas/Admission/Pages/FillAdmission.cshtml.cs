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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.Admission.Pages
{
    public class FillAdmissionModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public FillAdmissionModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var selectedMajors = SelectedMajors.Split("I").Where(a => a!="").Select(Int32.Parse);
            
            foreach (var i in selectedMajors)
            {
                var major = await _context.Majors
                    .Include(m => m.StudyPlans)
                    .ThenInclude(s => s.AcceptanceCriteria)
                    .ThenInclude(s => s.ScoreWeights)
                    .Include(m => m.Department)
                    .Include(m => m.ParentMajor)
                    .Include(m => m.Specializations)
                    .Include(m => m.Courses)
                    .Include(m => m.Prerequisites)
                    .FirstOrDefaultAsync(m => m.Id == i);
                if (major is null)
                    return NotFound();
                
                Majors.Add(major);
            }
            
            return Page();
        }
        
        [TempData]
        public string SelectedMajors { get; set; }
        
        [TempData]
        public MajorType MajorType { get; set; }

        public List<Major> Majors { get; set; } = [];
        
        public Candidate Candidate { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Today;
        
        public CandidateApplicationBuilder CandidateApplicationBuilder { get; set; }
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(List<ProgressType> progressTypes,
                                    List<decimal> score,
                                    List<ScholarlyAchievementType> scholarlyAchievementTypes,
                                    List<string> descriptions,
                                    string selectedMajors)
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
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
                var progress = new Progress(Id: 0, Type: iTuple.First, Score: iTuple.Second);
                Candidate.Progresses.Add(progress);
            }
            
            foreach (var iTuple in scholarlyAchievementTypes.Zip(descriptions))
            {
                var scholarlyAchievement = new ScholarlyAchievement(0, iTuple.First, iTuple.Second);
                Candidate.ScholarlyAchievements.Add(scholarlyAchievement);
            }

            _context.Candidates.Add(Candidate);

            
            CandidateApplicationBuilder = new CandidateApplicationBuilder
            {
                ApplicantId = Candidate.UserId,
                Date = DateTime.Today
            };

            foreach (var majorId in selectedMajors.Split("I").Where(a => a != "").Select(Int32.Parse))
            {
                CandidateApplicationBuilder.MajorId = majorId;
                _context.CandidateApplications.Add(CandidateApplicationBuilder.Build());
            }

            await _context.SaveChangesAsync();
            

            return RedirectToPage("./Index");
        }
    }
}

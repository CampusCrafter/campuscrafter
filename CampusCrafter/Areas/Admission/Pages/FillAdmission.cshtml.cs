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
using Newtonsoft.Json;

namespace CampusCrafter.Areas.Admission.Pages
{
    public class FillAdmissionModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public FillAdmissionModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        [TempData]
        public MajorType MajorType { get; set; }
        
        public Candidate Candidate { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Today;
        
        [TempData]
        public string JsonCandidateApplication { get; set; }
        
        public CandidateApplicationBuilder CandidateApplicationBuilder { get; set; }
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost(List<ProgressType> progressTypes, List<decimal> score, List<ScholarlyAchievementType> scholarlyAchievementTypes, List<string> descriptions)
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
                var scholarlyAchievement = new ScholarlyAchievement(0, iTuple.First, iTuple.Second, null);
                Candidate.ScholarlyAchievements.Add(scholarlyAchievement);
            }

            // TODO: Refactor this to a DTO or a few with foreign keys only
            // (so instances of linked entities do not serialize to JSON)
            CandidateApplicationBuilder = new CandidateApplicationBuilder
            {
                Applicant = Candidate
            };

            JsonCandidateApplication = JsonConvert.SerializeObject(CandidateApplicationBuilder);

            return RedirectToPage("./ConfirmAdmission");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Newtonsoft.Json;

namespace CampusCrafter.Areas.Admission.Pages
{
    public class ConfirmAdmissionModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public ConfirmAdmissionModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [TempData]
        public string SelectedMajors { get; set; }

        public List<Major> Majors { get; set; } = [];

        public DateTime DateTime { get; set; }
        
        [TempData]
        public string JsonCandidateApplication { get; set; }
        
        public List<CandidateApplication> CandidateApplications { get; set; } = [];
        
        public string JsonCandidateApplications { get; set; }
        
        public Candidate Candidate { get; set; }
        
        public string JsonCandidate { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            DateTime = DateTime.Today;
            var candidateApplicationBuilder = JsonConvert.DeserializeObject<CandidateApplicationBuilder>(JsonCandidateApplication)!;
            var selectedMajors = SelectedMajors.Split("I").Where(a => a!="").Select(Int32.Parse);
            
            Majors = [];
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
            
            Candidate = candidateApplicationBuilder.Applicant!;
            JsonCandidate = JsonConvert.SerializeObject(Candidate);
            
            foreach (var major in Majors)
            {
                candidateApplicationBuilder.Major = major;
                var application = candidateApplicationBuilder.Build();
                CandidateApplications.Add(application);
            }
            JsonCandidateApplications = JsonConvert.SerializeObject(CandidateApplications);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string candidate, string candidateApplications)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Candidate = JsonConvert.DeserializeObject<Candidate>(candidate)!;
            CandidateApplications = JsonConvert.DeserializeObject<List<CandidateApplication>>(candidateApplications)!;
            
            // TODO: Ensure there are no duplicate progresses (for example, two scores of the same type - take the newer and delete the older)
            foreach (var progress in Candidate.Progresses)
            {
                _context.Progresses.Add(progress);
            }
            
            
            foreach (var achievement in Candidate.ScholarlyAchievements)
            {
                _context.ScholarlyAchievements.Add(achievement);
            }

            if (await _context.Candidates.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == Candidate.UserId) is null)
                _context.Candidates.Add(Candidate);
            else
                _context.Candidates.Update(Candidate);


            var date = DateTime;
            
            
            foreach (var application in CandidateApplications)
            {
                application.Date = date;
                application.Major = null;
                application.ApplicantId = application.Applicant.UserId;
                application.Applicant = null;
                
                _context.CandidateApplications.Add(application);
            }
            
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}

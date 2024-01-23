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
    public class ConfirmApplicationModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public ConfirmApplicationModel(CampusCrafter.Data.ApplicationDbContext context)
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
        public IActionResult OnGet()
        {
            DateTime = DateTime.Today;
            var candidateApplicationBuilder = JsonConvert.DeserializeObject<CandidateApplicationBuilder>(JsonCandidateApplication)!;
            var selectedMajors = SelectedMajors.Split("I").Where(a => a!="").Select(Int32.Parse);
            foreach (var i in selectedMajors)
            {
                Majors.Add(_context.Majors.Find(i)!);
            }
            
            Candidate = candidateApplicationBuilder.Applicant!;
            JsonCandidate = JsonConvert.SerializeObject(Candidate);
            JsonCandidateApplications = JsonConvert.SerializeObject(CandidateApplications);
            
            foreach (var major in Majors)
            {
                candidateApplicationBuilder.Major = major;
                var application = candidateApplicationBuilder.Build();
                CandidateApplications.Add(application);
            }

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
            
            foreach (var progress in Candidate.Progresses)
            {
                _context.Progresses.Add(progress);
            }

            
            foreach (var achievement in Candidate.ScholarlyAchievements)
            {
                _context.ScholarlyAchievements.Add(achievement);
            }

            
            _context.Candidates.Add(Candidate);


            var date = DateTime;

            
            foreach (var application in CandidateApplications)
            {
                application.Date = date;
                _context.CandidateApplications.Add(application);
            }
            
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}

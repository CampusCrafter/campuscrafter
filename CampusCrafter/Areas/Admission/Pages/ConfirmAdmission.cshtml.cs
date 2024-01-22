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

        public List<Major> SelectedMajors = [];

        public DateTime DateTime { get; set; }
        
        public CandidateApplication CandidateApplication { get; set; }
        
        public IActionResult OnGet(CandidateApplication candidateApplication, List<int> selectedMajors)
        {
            CandidateApplication = candidateApplication;
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

            foreach (var progress in CandidateApplication.Applicant.Progresses)
            {
                _context.Progresses.Add(progress);
            }
            
            foreach (var achievement in CandidateApplication.Applicant.ScholarlyAchievements)
            {
                _context.ScholarlyAchievements.Add(achievement);
            }
            
            _context.Candidates.Add(CandidateApplication.Applicant);

            CandidateApplication.Date = DateTime;
            
            foreach (var major in SelectedMajors)
            {
                _context.CandidateApplications.Add(CandidateApplication with {Major = major});
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

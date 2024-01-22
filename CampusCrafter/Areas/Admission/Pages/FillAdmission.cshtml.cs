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
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost(List<ProgressType> progressTypes, List<decimal> score, List<ScholarlyAchievementType> scholarlyAchievementTypes, List<string> descriptions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./ConfirmAdmission",(progressTypes,score,scholarlyAchievementTypes,descriptions,SelectedMajors));
        }
    }
}

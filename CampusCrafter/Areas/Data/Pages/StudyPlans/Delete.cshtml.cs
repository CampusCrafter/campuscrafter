using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Data.Pages.StudyPlans
{
    public class DeleteModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public DeleteModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudyPlan StudyPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyplan = await _context.StudyPlans.FirstOrDefaultAsync(m => m.Id == id);

            if (studyplan == null)
            {
                return NotFound();
            }
            else
            {
                StudyPlan = studyplan;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyplan = await _context.StudyPlans.FindAsync(id);
            if (studyplan != null)
            {
                StudyPlan = studyplan;
                _context.StudyPlans.Remove(StudyPlan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
